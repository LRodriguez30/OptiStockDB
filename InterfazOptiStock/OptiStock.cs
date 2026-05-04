using InterfazOptiStock.Models;
using InterfazOptiStock.Models.HistorialCajas;
using InterfazOptiStock.Models.Notificaciones;
using InterfazOptiStock.Schemas;
using InterfazOptiStock.Schemas.Dtos.Auditorias;
using InterfazOptiStock.Schemas.Dtos.HistorialCajas;
using InterfazOptiStock.Schemas.Dtos.HistorialCostos;
using InterfazOptiStock.Schemas.Dtos.HistorialMovimientos;
using InterfazOptiStock.Schemas.Dtos.HistorialPrecios;
using InterfazOptiStock.Schemas.Dtos.Notificaciones;
using InterfazOptiStock.Schemas.Dtos.Reportes;
using InterfazOptiStock.Services;
using System;
using System.ComponentModel;

namespace InterfazOptiStock
{
    public partial class OptiStock : Form
    {
        // Servicios para acceder a los datos
        private readonly AuditoriasService _auditoriasService;
        private readonly ReportesService _reportesService;
        private readonly NotificacionesService _notificacionesService;

        private readonly HistorialCajasService _historialCajasService;
        private readonly HistorialMovimientosService _historialMovimientosService;
        private readonly HistorialCostosService _historialCostosService;
        private readonly HistorialPreciosService _historialPreciosService;

        private object? _registroSeleccionado;
        private string _registroOriginal = string.Empty;
        private int _ultimoIndiceFila;
        private int _ultimoIndiceColumna;
        private readonly Dictionary<(int row, int col), object?> _valoresOriginales = new();
        private HistorialCajasCreateDto? _dtoCreado;


        private readonly Color _colorBotonActivo = SystemColors.Control;
        private readonly Color _colorBotonInactivo = Color.Gray;

        private void ConfigurarBoton(Button boton, bool habilitado)
        {
            boton.Enabled = habilitado;

            boton.UseVisualStyleBackColor = false;
            boton.FlatStyle = FlatStyle.Flat;

            if (boton.Tag == null)
                boton.Tag = boton.BackColor;

            var colorBase = (Color)boton.Tag;

            boton.BackColor = habilitado
                ? colorBase
                : Color.FromArgb(100, colorBase);

            boton.ForeColor = SystemColors.ControlText;
        }



        public OptiStock()
        {
            InitializeComponent();

            _auditoriasService = new AuditoriasService();
            _reportesService = new ReportesService();
            _notificacionesService = new NotificacionesService();

            _historialCajasService = new HistorialCajasService();
            _historialMovimientosService = new HistorialMovimientosService();
            _historialCostosService = new HistorialCostosService();
            _historialPreciosService = new HistorialPreciosService();

            ConfigurarBoton(btnConfirmar, false);
            ConfigurarBoton(btnActualizar, false);
            ConfigurarBoton(btnEliminar, false);

            dgvOptiStockAcciones.Visible = false;
            dgvOptiStockResumen.Visible = false;
            dgvOptiStockDetalle.Visible = false;
            dgvOptiStockPeriodo.Visible = false;
            dgvOptiStockDatosNuevos.Visible = false;
            dgvOptiStockDatosAnteriores.Visible = false;
        }

        private string _coleccionActual = string.Empty;

        private void MostrarGridsSecundarios()
        {
            dgvOptiStockAcciones.Visible = false;
            dgvOptiStockResumen.Visible = false;
            dgvOptiStockDetalle.Visible = false;
            dgvOptiStockPeriodo.Visible = false;
            dgvOptiStockDatosNuevos.Visible = false;
            dgvOptiStockDatosAnteriores.Visible = false;

            switch (_coleccionActual)
            {
                case "Notificaciones":
                    dgvOptiStockAcciones.Visible = true;
                    break;

                case "Reportes":
                    dgvOptiStockPeriodo.Visible = true;
                    dgvOptiStockResumen.Visible = true;
                    dgvOptiStockDetalle.Visible = true;
                    break;

                case "Auditorias":
                    dgvOptiStockDatosNuevos.Visible = true;
                    dgvOptiStockDatosAnteriores.Visible = true;
                    break;
            }
        }

        private void dgvOptiStock_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvOptiStock.IsCurrentCellDirty)
                dgvOptiStock.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvOptiStock_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            ConfigurarBoton(btnConfirmar, true);
        }

        private void dgvOptiStock_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row = dgvOptiStock.Rows[e.RowIndex];

            if (row.IsNewRow || row.DataBoundItem == null)
                return;

            var item = row.DataBoundItem;

            bool esNuevo = string.IsNullOrWhiteSpace(((dynamic)item)._id);

            if (esNuevo)
            {
                ConfigurarBoton(btnConfirmar, true);
                return;
            }

            if (!ReferenceEquals(item, _registroSeleccionado))
                return;

            string actual = SerializarRegistro(item);
            bool modificado = actual != _registroOriginal;

            ConfigurarBoton(btnActualizar, modificado);
        }


        private string SerializarRegistro(object registro)
        {
            return System.Text.Json.JsonSerializer.Serialize(registro);
        }

        private void RevertirCeldaActual()
        {
            var key = (_ultimoIndiceFila, _ultimoIndiceColumna);

            if (_valoresOriginales.TryGetValue(key, out var valor))
            {
                dgvOptiStock.Rows[_ultimoIndiceFila]
                    .Cells[_ultimoIndiceColumna]
                    .Value = valor;
            }
        }


        private void dgvOptiStock_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var cell = dgvOptiStock.Rows[e.RowIndex].Cells[e.ColumnIndex];

            _valoresOriginales[(e.RowIndex, e.ColumnIndex)] = cell.Value;

            _ultimoIndiceFila = e.RowIndex;
            _ultimoIndiceColumna = e.ColumnIndex;
        }

        private void dgvOptiStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvOptiStock.IsCurrentCellInEditMode)
            {
                dgvOptiStock.EndEdit();
                e.Handled = true;
            }
        }




        private bool TieneCambiosPendientes()
        {
            if (_registroSeleccionado == null || string.IsNullOrWhiteSpace(_registroOriginal))
                return false;

            var actual = SerializarRegistro(_registroSeleccionado);
            return actual != _registroOriginal;
        }


        private void RevertirFila(int rowIndex)
        {
            if (string.IsNullOrWhiteSpace(_registroOriginal))
                return;

            var tipo = dgvOptiStock.Rows[rowIndex].DataBoundItem.GetType();
            var original = System.Text.Json.JsonSerializer.Deserialize(_registroOriginal, tipo);

            if (original == null)
                return;

            var props = tipo.GetProperties();

            foreach (var prop in props)
            {
                var colName = prop.Name;

                if (!dgvOptiStock.Columns.Contains(colName))
                    continue;

                var value = prop.GetValue(original);

                dgvOptiStock.Rows[rowIndex].Cells[colName].Value = value;
            }
        }


        private bool _bloqueandoCambioFila = false;

        private void dgvOptiStock_SelectionChanged(object sender, EventArgs e)
        {
            if (_bloqueandoCambioFila)
                return;

            if (dgvOptiStock.CurrentRow == null)
            {
                ConfigurarBoton(btnEliminar, false);
                ConfigurarBoton(btnActualizar, false);
                return;
            }

            int filaActual = dgvOptiStock.CurrentRow.Index;
            int colActual = dgvOptiStock.CurrentCell?.ColumnIndex ?? 0;

            bool cambioDeFila = filaActual != _ultimoIndiceFila;

            if (_registroSeleccionado != null && TieneCambiosPendientes() && cambioDeFila)
            {
                var result = MessageBox.Show(
                    "Hay cambios sin guardar. ¿Deseas descartarlos?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    _bloqueandoCambioFila = true;

                    dgvOptiStock.CurrentCell =
                        dgvOptiStock.Rows[_ultimoIndiceFila]
                            .Cells[_ultimoIndiceColumna];

                    _bloqueandoCambioFila = false;
                    return;
                }

                RevertirFila(_ultimoIndiceFila);
            }

            var nuevo = dgvOptiStock.CurrentRow.DataBoundItem;

            if (ReferenceEquals(_registroSeleccionado, nuevo))
                return;

            _registroSeleccionado = nuevo;

            var copia = ClonarObjeto(_registroSeleccionado);
            _registroOriginal = SerializarRegistro(copia);

            _ultimoIndiceFila = filaActual;
            _ultimoIndiceColumna = colActual;

            if (_coleccionActual == "Notificaciones" &&
                _registroSeleccionado is Notificaciones notificacion)
            {
                CargarNotificacionDetalle(notificacion);
                MostrarGridsSecundarios();
            }

            ConfigurarBoton(btnEliminar, true);
        }






        private T ClonarObjeto<T>(T obj)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(obj);
            return System.Text.Json.JsonSerializer.Deserialize<T>(json)!;
        }



        private void CargarAuditoriaDetalle(Auditorias auditoria)
        {
            dgvOptiStockDatosNuevos.DataSource = ConvertirObjetoLista(auditoria.DatosNuevos);

            dgvOptiStockDatosAnteriores.DataSource = ConvertirObjetoLista(auditoria.DatosAnteriores);
        }

        private void CargarReporteDetalle(Reportes reporte)
        {
            dgvOptiStockPeriodo.DataSource = new[]
            {
                reporte.Periodo
            };

            dgvOptiStockResumen.DataSource = new[]
            {
                reporte.Resumen
            };

            dgvOptiStockDetalle.DataSource = reporte.Detalle;
        }

        private void CargarNotificacionDetalle(Notificaciones notificacion)
        {
            dgvOptiStockAcciones.DataSource = null;

            var datos = ConvertirObjetoLista(notificacion.Acciones);

            dgvOptiStockAcciones.DataSource = datos;

            dgvOptiStockAcciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvOptiStockAcciones.Columns.Count > 0)
            {
                dgvOptiStockAcciones.Columns[0].HeaderText = "Acción";
                dgvOptiStockAcciones.Columns[1].HeaderText = "Valor";
            }
        }



        private List<PropiedadDinamica> ConvertirObjetoLista(object? objeto)
        {
            if (objeto == null)
                return new();

            return objeto.GetType()
                         .GetProperties()
                         .Select(p => new PropiedadDinamica
                         {
                             Propiedad = p.Name,
                             Valor = p.GetValue(objeto)?.ToString() ?? string.Empty
                         })
                         .ToList();
        }



        private async void btnConfirmar_Click(object sender, EventArgs e)
        {
            switch (_coleccionActual)
            {
                case "Auditorias":
                    await GuardarNuevosAsync<Auditorias, AuditoriasCreateDto>(
                        (IEnumerable<Auditorias>)dgvOptiStock.DataSource!,
                        auditoria => string.IsNullOrWhiteSpace(auditoria._id),
                        auditoria => new AuditoriasCreateDto
                        {
                            IdEmpresa = auditoria.IdEmpresa,
                            IdUsuario = auditoria.IdUsuario,
                            NombreUsuario = auditoria.NombreUsuario,
                            Modulo = auditoria.Modulo,
                            Accion = auditoria.Accion,
                            Descripcion = auditoria.Descripcion,
                            Entidad = auditoria.Entidad,
                            IdEntidad = auditoria.IdEntidad,
                            DatosAnteriores = auditoria.DatosAnteriores != null ? auditoria.DatosAnteriores : new object(),
                            DatosNuevos = auditoria.DatosNuevos != null ? auditoria.DatosNuevos : new object(),
                            FechaEvento = auditoria.FechaEvento,
                            NombreGrupo = auditoria.NombreGrupo,
                            IdAutorizadoPor = auditoria.IdAutorizadoPor
                        },
                        _auditoriasService.CreateAuditoriaAsync
                    );
                    break;

                case "Reportes":
                    await GuardarNuevosAsync<Reportes, ReportesCreateDto>(
                        (IEnumerable<Reportes>)dgvOptiStock.DataSource!,
                        reporte => string.IsNullOrWhiteSpace(reporte._id),
                        reporte => new ReportesCreateDto
                        {
                            IdEmpresa = reporte.IdEmpresa,
                            TipoReporte = reporte.TipoReporte,
                            FechaGeneracion = reporte.FechaGeneracion,
                            Periodo = reporte.Periodo,
                            Resumen = reporte.Resumen,
                            Detalle = reporte.Detalle,
                            IdGeneradoPor = reporte.IdGeneradoPor
                        },
                        _reportesService.CreateReporteAsync
                    );
                    break;

                case "Notificaciones":
                    await GuardarNuevosAsync<Notificaciones, NotificacionesCreateDto>(
                        (IEnumerable<Notificaciones>)dgvOptiStock.DataSource!,
                        notificacion => string.IsNullOrWhiteSpace(notificacion._id),
                        notificacion => new NotificacionesCreateDto
                        {
                            IdEmpresa = notificacion.IdEmpresa,
                            Tipo = notificacion.Tipo,
                            Titulo = notificacion.Titulo,
                            Mensaje = notificacion.Mensaje,
                            Prioridad = notificacion.Prioridad,
                            Modulo = notificacion.Modulo,
                            IdReferencia = notificacion.IdReferencia,
                            Leida = notificacion.Leida,
                            FechaCreacion = notificacion.FechaCreacion,
                            FechaLectura = notificacion.FechaLectura,
                            UsuarioDestino = notificacion.UsuarioDestino,
                            Acciones = notificacion.Acciones
                        },
                        _notificacionesService.CreateNotificacionAsync
                    );
                    break;

                case "HistorialCajas":
                    try
                    {
                        if (!ValidarYCargarDto() || _dtoCreado == null)
                        {
                            MessageBox.Show("Datos inválidos o incompletos.");
                            return;
                        }

                        await _historialCajasService.CreateHistorialCajaAsync(_dtoCreado);

                        _dtoCreado = null;

                        MessageBox.Show("Registros creados correctamente.");
                        btnConfirmar.Enabled = false;
                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    break;

                case "HistorialMovimientos":
                    await GuardarNuevosAsync<HistorialMovimientos, HistorialMovimientosCreateDto>(
                        (IEnumerable<HistorialMovimientos>)dgvOptiStock.DataSource!,
                        historialMovimiento => string.IsNullOrWhiteSpace(historialMovimiento._id),
                        historialMovimiento => new HistorialMovimientosCreateDto
                        {
                            IdEmpresa = historialMovimiento.IdEmpresa,
                            IdProducto = historialMovimiento.IdProducto,
                            CodigoProducto = historialMovimiento.CodigoProducto,
                            NombreProducto = historialMovimiento.NombreProducto,
                            TipoMovimiento = historialMovimiento.TipoMovimiento,
                            Cantidad = historialMovimiento.Cantidad,
                            StockAnterior = historialMovimiento.StockAnterior,
                            StockNuevo = historialMovimiento.StockNuevo,
                            Concepto = historialMovimiento.Concepto,
                            IdDocumento = historialMovimiento.IdDocumento,
                            NumeroDocumento = historialMovimiento.NumeroDocumento,
                            IdUsuario = historialMovimiento.IdUsuario,
                            FechaMovimiento = historialMovimiento.FechaMovimiento,
                            Observaciones = historialMovimiento.Observaciones
                        },
                        _historialMovimientosService.CreateHistorialMovimientoAsync
                    );
                    break;

                case "HistorialCostos":
                    await GuardarNuevosAsync<HistorialCostos, HistorialCostosCreateDto>(
                        (IEnumerable<HistorialCostos>)dgvOptiStock.DataSource!,
                        historialCosto => string.IsNullOrWhiteSpace(historialCosto._id),
                        historialCosto => new HistorialCostosCreateDto
                        {
                            IdEmpresa = historialCosto.IdEmpresa,
                            IdProducto = historialCosto.IdProducto,
                            CodigoProducto = historialCosto.CodigoProducto,
                            NombreProducto = historialCosto.NombreProducto,
                            CostoAnterior = historialCosto.CostoAnterior,
                            CostoNuevo = historialCosto.CostoNuevo,
                            IdProveedor = historialCosto.IdProveedor,
                            NombreProveedor = historialCosto.NombreProveedor,
                            Motivo = historialCosto.Motivo,
                            IdUsuario = historialCosto.IdUsuario,
                            NombreUsuario = historialCosto.NombreUsuario,
                            FechaCambio = historialCosto.FechaCambio
                        },
                        _historialCostosService.CreateHistorialCostoAsync
                    );
                    break;

                case "HistorialPrecios":
                    await GuardarNuevosAsync<HistorialPrecios, HistorialPreciosCreateDto>(
                        (IEnumerable<HistorialPrecios>)dgvOptiStock.DataSource!,
                        historialPrecio => string.IsNullOrWhiteSpace(historialPrecio._id),
                        historialPrecio => new HistorialPreciosCreateDto
                        {
                            IdEmpresa = historialPrecio.IdEmpresa,
                            IdProducto = historialPrecio.IdProducto,
                            CodigoProducto = historialPrecio.CodigoProducto,
                            NombreProducto = historialPrecio.NombreProducto,
                            PrecioAnterior = historialPrecio.PrecioAnterior,
                            PrecioNuevo = historialPrecio.PrecioNuevo,
                            Motivo = historialPrecio.Motivo,
                            IdUsuario = historialPrecio.IdUsuario,
                            NombreUsuario = historialPrecio.NombreUsuario,
                            FechaCambio = historialPrecio.FechaCambio
                        },
                        _historialPreciosService.CreateHistorialPrecioAsync
                    );
                    break;

                default:
                    MessageBox.Show("Seleccione una colección.");
                    return;
            }

            MessageBox.Show("Registros creados correctamente.");
            btnConfirmar.Enabled = false;
        }

        private async Task GuardarNuevosAsync<TEntidad, TCreateDto>(
        IEnumerable<TEntidad> datos,
        Func<TEntidad, bool> esNuevo,
        Func<TEntidad, TCreateDto> mapearDto,
        Func<TCreateDto, Task> crearAsync)
        where TEntidad : class
        {
            var nuevos = datos
                .Where(x => x != null && esNuevo(x))
                .ToList();

            if (!nuevos.Any())
                return;

            foreach (var item in nuevos)
            {
                var dto = mapearDto(item);
                await crearAsync(dto);
            }
        }


        private void dgvOptiStockCrear_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvOptiStockCrear.IsCurrentCellDirty)
                dgvOptiStockCrear.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvOptiStockCrear_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            this.BeginInvoke(new Action(() =>
            {
                ConfigurarBoton(btnConfirmar, ValidarYCargarDto());
            }));
        }


        private bool ValidarYCargarDto()
        {
            _dtoCreado = null;

            var row = dgvOptiStockCrear.Rows
                .Cast<DataGridViewRow>()
                .FirstOrDefault(r => !r.IsNewRow);

            if (row == null)
                return false;

            try
            {
                _dtoCreado = new HistorialCajasCreateDto
                {
                    IdEmpresa = ParseGuid(row.Cells["IdEmpresa"].Value),
                    IdCaja = ParseGuid(row.Cells["IdCaja"].Value),
                    NombreCaja = ParseString(row.Cells["NombreCaja"].Value),
                    IdTurno = ParseString(row.Cells["IdTurno"].Value),
                    IdUsuario = ParseGuid(row.Cells["IdUsuario"].Value),
                    TipoMovimiento = ParseString(row.Cells["TipoMovimiento"].Value),
                    Concepto = ParseString(row.Cells["Concepto"].Value),
                    Monto = ParseDecimal(row.Cells["Monto"].Value),
                    SaldoAnterior = ParseDecimal(row.Cells["SaldoAnterior"].Value),
                    SaldoNuevo = ParseDecimal(row.Cells["SaldoNuevo"].Value),
                    IdOperacion = ParseGuid(row.Cells["IdOperacion"].Value),
                    FechaCambio = DateTime.UtcNow,
                    Observaciones = row.Cells["Observaciones"].Value?.ToString()
                };

                return true;
            }
            catch
            {
                _dtoCreado = null;
                return false;
            }
        }






        private Guid ParseGuid(object value)
        {
            if (value is Guid g)
                return g;

            if (value == null || value == DBNull.Value)
                throw new Exception("GUID vacío");

            var text = value.ToString()?.Trim();

            if (string.IsNullOrWhiteSpace(text))
                throw new Exception("GUID vacío");

            if (Guid.TryParse(text, out var result))
                return result;

            throw new Exception($"GUID inválido: {text}");
        }




        private decimal ParseDecimal(object value)
        {
            if (value == null || !decimal.TryParse(value.ToString(), out var result))
                throw new Exception("Decimal inválido");

            return result;
        }

        private string ParseString(object value)
        {
            var text = value?.ToString();

            if (string.IsNullOrWhiteSpace(text))
                throw new Exception("Campo vacío");

            return text;
        }







        private async void btnAuditorias_Click(object sender, EventArgs e)
        {
            _coleccionActual = "Auditorias";
            dgvOptiStock.DataSource = await _auditoriasService.GetAuditoriasAsync();
            MostrarGridsSecundarios();
        }

        private async void btnReportes_Click(object sender, EventArgs e)
        {
            _coleccionActual = "Reportes";
            dgvOptiStock.DataSource = await _reportesService.GetReportesAsync();
            MostrarGridsSecundarios();
        }

        private async void btnCajas_Click(object sender, EventArgs e)
        {
            _coleccionActual = "HistorialCajas";
            dgvOptiStock.DataSource = await _historialCajasService.GetHistorialCajasAsync();

            dgvOptiStock.Columns["_id"].ReadOnly = true;
            dgvOptiStock.Columns["IdEmpresa"].ReadOnly = true;
            dgvOptiStock.Columns["IdCaja"].ReadOnly = true;
            dgvOptiStock.Columns["NombreCaja"].ReadOnly = true;
            dgvOptiStock.Columns["IdTurno"].ReadOnly = true;
            dgvOptiStock.Columns["IdUsuario"].ReadOnly = true;
            dgvOptiStock.Columns["TipoMovimiento"].ReadOnly = true;
            dgvOptiStock.Columns["Concepto"].ReadOnly = true;
            dgvOptiStock.Columns["Monto"].ReadOnly = true;
            dgvOptiStock.Columns["SaldoAnterior"].ReadOnly = true;
            dgvOptiStock.Columns["SaldoNuevo"].ReadOnly = true;
            dgvOptiStock.Columns["IdOperacion"].ReadOnly = true;
            dgvOptiStock.Columns["FechaCambio"].ReadOnly = true;

            dgvOptiStockCrear.Columns.Clear();
            dgvOptiStockCrear.Rows.Clear();

            foreach (var prop in typeof(PropiedadesHistorialCajas).GetProperties())
            {
                if (prop.Name == "_id" || prop.Name == "FechaCambio") continue;

                dgvOptiStockCrear.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = prop.Name,
                    HeaderText = prop.Name,
                    DataPropertyName = null
                });
            }

            dgvOptiStockCrear.Rows.Add();
        }

        private async void btnPrecios_Click(object sender, EventArgs e)
        {
            _coleccionActual = "HistorialPrecios";
            dgvOptiStock.DataSource = await _historialPreciosService.GetHistorialPreciosAsync();
            MostrarGridsSecundarios();
        }

        private async void btnCostos_Click(object sender, EventArgs e)
        {
            _coleccionActual = "HistorialCostos";
            dgvOptiStock.DataSource = await _historialCostosService.GetHistorialCostosAsync();
            MostrarGridsSecundarios();
        }

        private async void btnMovimientos_Click(object sender, EventArgs e)
        {
            _coleccionActual = "HistorialMovimientos";
            dgvOptiStock.DataSource = await _historialMovimientosService.GetHistorialMovimientosAsync();
            MostrarGridsSecundarios();
        }

        private async void btnNotificaciones_Click(object sender, EventArgs e)
        {
            var data = await _notificacionesService.GetNotificacionesAsync();
            dgvOptiStock.DataSource = data;

            dgvOptiStock.Columns["Acciones"].Visible = false;
            dgvOptiStockCrear.DataSource = new BindingList<PropiedadesNotificaciones>
            {
                new PropiedadesNotificaciones()
            };

        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_registroSeleccionado == null)
            {
                MessageBox.Show("Seleccione un registro.");
                return;
            }

            var confirmacion = MessageBox.Show(
                "¿Eliminar este registro?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes)
                return;

            try
            {
                switch (_coleccionActual)
                {
                    case "Auditorias":
                        await _auditoriasService.DeleteAuditoriaAsync(
                            ((Auditorias)_registroSeleccionado)._id!);
                        break;

                    case "Reportes":
                        await _reportesService.DeleteReporteAsync(
                            ((Reportes)_registroSeleccionado)._id!);
                        break;

                    case "Notificaciones":
                        await _notificacionesService.DeleteNotificacionAsync(
                            ((Notificaciones)_registroSeleccionado)._id!);
                        break;

                    case "HistorialCajas":
                        await _historialCajasService.DeleteHistorialCajaAsync(
                            ((HistorialCajas)_registroSeleccionado)._id!);

                        if (dgvOptiStock.CurrentRow?.DataBoundItem is HistorialCajas item)
                        {
                            var list = (IList<HistorialCajas>)dgvOptiStock.DataSource!;
                            list.Remove(item);
                        }

                        break;

                    case "HistorialMovimientos":
                        await _historialMovimientosService.DeleteHistorialMovimientoAsync(
                            ((HistorialMovimientos)_registroSeleccionado)._id!);
                        break;

                    case "HistorialCostos":
                        await _historialCostosService.DeleteHistorialCostoAsync(
                            ((HistorialCostos)_registroSeleccionado)._id!);
                        break;

                    case "HistorialPrecios":
                        await _historialPreciosService.DeleteHistorialPrecioAsync(
                            ((HistorialPrecios)_registroSeleccionado)._id!);
                        break;
                }

                _registroSeleccionado = null;

                MessageBox.Show("Registro eliminado.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvOptiStock.CurrentRow?.DataBoundItem is null)
            {
                MessageBox.Show("Seleccione un registro.");
                return;
            }

            try
            {
                switch (_coleccionActual)
                {
                    case "Auditorias":
                        {
                            if (dgvOptiStock.CurrentRow.DataBoundItem is not Auditorias auditoria ||
                                string.IsNullOrWhiteSpace(auditoria._id))
                                return;

                            var dto = new AuditoriasUpdateDto
                            {
                                DatosAnteriores = auditoria.DatosAnteriores ?? new object(),
                                DatosNuevos = auditoria.DatosNuevos ?? new object()
                            };

                            await _auditoriasService.UpdateAuditoriaAsync(auditoria._id, dto);
                            break;
                        }

                    case "Notificaciones":
                        {
                            if (dgvOptiStock.CurrentRow.DataBoundItem is not Notificaciones notificacion ||
                                string.IsNullOrWhiteSpace(notificacion._id))
                                return;

                            var dto = new NotificacionesUpdateDto
                            {
                                Leida = notificacion.Leida,
                                Acciones = notificacion.Acciones
                            };

                            await _notificacionesService.UpdateNotificacionAsync(notificacion._id, dto);
                            break;
                        }

                    case "HistorialCajas":
                        {
                            if (dgvOptiStock.CurrentRow.DataBoundItem is not HistorialCajas caja ||
                                string.IsNullOrWhiteSpace(caja._id))
                                return;

                            var dto = new HistorialCajasUpdateDto
                            {
                                Observaciones = caja.Observaciones
                            };

                            await _historialCajasService.UpdateHistorialCajaAsync(caja._id, dto);
                            break;
                        }

                    case "HistorialMovimientos":
                        {
                            if (dgvOptiStock.CurrentRow.DataBoundItem is not HistorialMovimientos movimiento ||
                                string.IsNullOrWhiteSpace(movimiento._id))
                                return;

                            var dto = new HistorialMovimientosUpdateDto
                            {
                                Observaciones = movimiento.Observaciones
                            };

                            await _historialMovimientosService.UpdateHistorialMovimientoAsync(movimiento._id, dto);
                            break;
                        }

                    case "HistorialCostos":
                        {
                            if (dgvOptiStock.CurrentRow.DataBoundItem is not HistorialCostos costo ||
                                string.IsNullOrWhiteSpace(costo._id))
                                return;

                            var dto = new HistorialCostosUpdateDto
                            {
                                NombreProveedor = costo.NombreProveedor,
                                Motivo = costo.Motivo,
                                NombreUsuario = costo.NombreUsuario
                            };

                            await _historialCostosService.UpdateHistorialCostoAsync(costo._id, dto);
                            break;
                        }

                    case "HistorialPrecios":
                        {
                            if (dgvOptiStock.CurrentRow.DataBoundItem is not HistorialPrecios precio ||
                                string.IsNullOrWhiteSpace(precio._id))
                                return;

                            var dto = new HistorialPreciosUpdateDto
                            {
                                Motivo = precio.Motivo,
                                NombreUsuario = precio.NombreUsuario
                            };

                            await _historialPreciosService.UpdateHistorialPrecioAsync(precio._id, dto);
                            break;
                        }

                    default:
                        MessageBox.Show("Colección no soportada.");
                        return;
                }

                _registroSeleccionado = dgvOptiStock.CurrentRow.DataBoundItem;
                _registroOriginal = SerializarRegistro(_registroSeleccionado);

                ConfigurarBoton(btnActualizar, false);

                MessageBox.Show("Registro actualizado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private async void btnRefrescar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_coleccionActual))
            {
                MessageBox.Show(
                    "Primero debe cargar una colección utilizando los botones disponibles.",
                    "Información",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                switch (_coleccionActual)
                {
                    case "Auditorias":
                        await _auditoriasService.GetAuditoriasAsync();
                        break;

                    case "Reportes":
                        await _reportesService.GetReportesAsync();
                        break;

                    case "Notificaciones":
                        await _notificacionesService.GetNotificacionesAsync();
                        break;

                    case "HistorialCajas":
                        await _historialCajasService.GetHistorialCajasAsync();
                        break;

                    case "HistorialMovimientos":
                        await _historialMovimientosService.GetHistorialMovimientosAsync();
                        break;

                    case "HistorialCostos":
                        await _historialCostosService.GetHistorialCostosAsync();
                        break;

                    case "HistorialPrecios":
                        await _historialPreciosService.GetHistorialPreciosAsync();
                        break;

                    default:
                        MessageBox.Show(
                            "La colección seleccionada no es válida.",
                            "Advertencia",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error al refrescar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}
