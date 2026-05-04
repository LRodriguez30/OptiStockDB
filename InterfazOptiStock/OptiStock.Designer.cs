namespace InterfazOptiStock
{
    partial class OptiStock
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            btnNotificaciones = new FontAwesome.Sharp.IconButton();
            btnCajas = new FontAwesome.Sharp.IconButton();
            btnPrecios = new FontAwesome.Sharp.IconButton();
            btnCostos = new FontAwesome.Sharp.IconButton();
            btnReportes = new FontAwesome.Sharp.IconButton();
            btnAuditorias = new FontAwesome.Sharp.IconButton();
            dgvOptiStock = new DataGridView();
            btnConfirmar = new FontAwesome.Sharp.IconButton();
            btnEliminar = new FontAwesome.Sharp.IconButton();
            btnActualizar = new FontAwesome.Sharp.IconButton();
            btnRefrescar = new FontAwesome.Sharp.IconButton();
            btnMovimientos = new FontAwesome.Sharp.IconButton();
            dgvOptiStockAcciones = new DataGridView();
            dgvOptiStockResumen = new DataGridView();
            dgvOptiStockDetalle = new DataGridView();
            dgvOptiStockPeriodo = new DataGridView();
            dgvOptiStockDatosNuevos = new DataGridView();
            dgvOptiStockDatosAnteriores = new DataGridView();
            dgvOptiStockCrear = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvOptiStock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvOptiStockAcciones).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvOptiStockResumen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvOptiStockDetalle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvOptiStockPeriodo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvOptiStockDatosNuevos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvOptiStockDatosAnteriores).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvOptiStockCrear).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.AliceBlue;
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1596, 62);
            panel1.TabIndex = 0;
            // 
            // btnNotificaciones
            // 
            btnNotificaciones.BackColor = Color.SteelBlue;
            btnNotificaciones.Cursor = Cursors.Hand;
            btnNotificaciones.FlatAppearance.BorderColor = Color.LightSkyBlue;
            btnNotificaciones.FlatAppearance.BorderSize = 5;
            btnNotificaciones.Font = new Font("Lucida Bright", 7F, FontStyle.Bold);
            btnNotificaciones.ForeColor = Color.White;
            btnNotificaciones.IconChar = FontAwesome.Sharp.IconChar.Bell;
            btnNotificaciones.IconColor = Color.White;
            btnNotificaciones.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnNotificaciones.IconSize = 80;
            btnNotificaciones.ImageAlign = ContentAlignment.TopCenter;
            btnNotificaciones.Location = new Point(746, 68);
            btnNotificaciones.Name = "btnNotificaciones";
            btnNotificaciones.Padding = new Padding(10);
            btnNotificaciones.Size = new Size(153, 122);
            btnNotificaciones.TabIndex = 6;
            btnNotificaciones.Text = "Notificaciones";
            btnNotificaciones.TextAlign = ContentAlignment.BottomCenter;
            btnNotificaciones.UseVisualStyleBackColor = false;
            btnNotificaciones.Click += btnNotificaciones_Click;
            // 
            // btnCajas
            // 
            btnCajas.BackColor = Color.DarkSlateBlue;
            btnCajas.Cursor = Cursors.Hand;
            btnCajas.FlatAppearance.BorderColor = Color.SlateBlue;
            btnCajas.FlatAppearance.BorderSize = 5;
            btnCajas.Font = new Font("Lucida Bright", 8F, FontStyle.Bold);
            btnCajas.ForeColor = Color.White;
            btnCajas.IconChar = FontAwesome.Sharp.IconChar.CashRegister;
            btnCajas.IconColor = Color.White;
            btnCajas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCajas.IconSize = 80;
            btnCajas.ImageAlign = ContentAlignment.TopCenter;
            btnCajas.Location = new Point(32, 68);
            btnCajas.Name = "btnCajas";
            btnCajas.Padding = new Padding(10);
            btnCajas.Size = new Size(153, 122);
            btnCajas.TabIndex = 5;
            btnCajas.Text = "Cajas";
            btnCajas.TextAlign = ContentAlignment.BottomCenter;
            btnCajas.UseVisualStyleBackColor = false;
            btnCajas.Click += btnCajas_Click;
            // 
            // btnPrecios
            // 
            btnPrecios.BackColor = Color.Green;
            btnPrecios.Cursor = Cursors.Hand;
            btnPrecios.FlatAppearance.BorderColor = Color.MediumSeaGreen;
            btnPrecios.FlatAppearance.BorderSize = 5;
            btnPrecios.Font = new Font("Lucida Bright", 8F, FontStyle.Bold);
            btnPrecios.ForeColor = Color.White;
            btnPrecios.IconChar = FontAwesome.Sharp.IconChar.Tags;
            btnPrecios.IconColor = Color.White;
            btnPrecios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnPrecios.IconSize = 80;
            btnPrecios.ImageAlign = ContentAlignment.TopCenter;
            btnPrecios.Location = new Point(191, 68);
            btnPrecios.Name = "btnPrecios";
            btnPrecios.Padding = new Padding(10);
            btnPrecios.Size = new Size(153, 122);
            btnPrecios.TabIndex = 3;
            btnPrecios.Text = "Precios";
            btnPrecios.TextAlign = ContentAlignment.BottomCenter;
            btnPrecios.UseVisualStyleBackColor = false;
            btnPrecios.Click += btnPrecios_Click;
            // 
            // btnCostos
            // 
            btnCostos.BackColor = Color.DarkGoldenrod;
            btnCostos.Cursor = Cursors.Hand;
            btnCostos.FlatAppearance.BorderColor = Color.Goldenrod;
            btnCostos.FlatAppearance.BorderSize = 5;
            btnCostos.Font = new Font("Lucida Bright", 8F, FontStyle.Bold);
            btnCostos.ForeColor = Color.White;
            btnCostos.IconChar = FontAwesome.Sharp.IconChar.MoneyBillTrendUp;
            btnCostos.IconColor = Color.White;
            btnCostos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCostos.IconSize = 80;
            btnCostos.ImageAlign = ContentAlignment.TopCenter;
            btnCostos.Location = new Point(350, 68);
            btnCostos.Name = "btnCostos";
            btnCostos.Padding = new Padding(10);
            btnCostos.Size = new Size(153, 122);
            btnCostos.TabIndex = 4;
            btnCostos.Text = "Costos";
            btnCostos.TextAlign = ContentAlignment.BottomCenter;
            btnCostos.UseVisualStyleBackColor = false;
            btnCostos.Click += btnCostos_Click;
            // 
            // btnReportes
            // 
            btnReportes.BackColor = Color.Brown;
            btnReportes.Cursor = Cursors.Hand;
            btnReportes.FlatAppearance.BorderColor = Color.LightCoral;
            btnReportes.FlatAppearance.BorderSize = 5;
            btnReportes.Font = new Font("Lucida Bright", 8F, FontStyle.Bold);
            btnReportes.ForeColor = Color.White;
            btnReportes.IconChar = FontAwesome.Sharp.IconChar.FilePdf;
            btnReportes.IconColor = Color.White;
            btnReportes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnReportes.IconSize = 80;
            btnReportes.Location = new Point(1047, 68);
            btnReportes.Name = "btnReportes";
            btnReportes.Padding = new Padding(10);
            btnReportes.Size = new Size(153, 122);
            btnReportes.TabIndex = 1;
            btnReportes.Text = "Reportes";
            btnReportes.TextAlign = ContentAlignment.BottomCenter;
            btnReportes.UseVisualStyleBackColor = false;
            btnReportes.Click += btnReportes_Click;
            // 
            // btnAuditorias
            // 
            btnAuditorias.BackColor = Color.RoyalBlue;
            btnAuditorias.Cursor = Cursors.Hand;
            btnAuditorias.FlatAppearance.BorderColor = Color.LightSkyBlue;
            btnAuditorias.FlatAppearance.BorderSize = 5;
            btnAuditorias.Font = new Font("Lucida Bright", 8F, FontStyle.Bold);
            btnAuditorias.ForeColor = Color.White;
            btnAuditorias.IconChar = FontAwesome.Sharp.IconChar.ClipboardCheck;
            btnAuditorias.IconColor = Color.White;
            btnAuditorias.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAuditorias.IconSize = 80;
            btnAuditorias.ImageAlign = ContentAlignment.TopCenter;
            btnAuditorias.Location = new Point(1342, 68);
            btnAuditorias.Name = "btnAuditorias";
            btnAuditorias.Padding = new Padding(10);
            btnAuditorias.Size = new Size(153, 122);
            btnAuditorias.TabIndex = 0;
            btnAuditorias.Text = "Auditorías";
            btnAuditorias.TextAlign = ContentAlignment.BottomCenter;
            btnAuditorias.UseVisualStyleBackColor = false;
            btnAuditorias.Click += btnAuditorias_Click;
            // 
            // dgvOptiStock
            // 
            dgvOptiStock.BackgroundColor = SystemColors.ControlLight;
            dgvOptiStock.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOptiStock.Location = new Point(37, 208);
            dgvOptiStock.Name = "dgvOptiStock";
            dgvOptiStock.RowHeadersWidth = 62;
            dgvOptiStock.Size = new Size(625, 424);
            dgvOptiStock.TabIndex = 1;
            dgvOptiStock.CellBeginEdit += dgvOptiStock_CellBeginEdit;
            dgvOptiStock.CellValueChanged += dgvOptiStock_CellValueChanged;
            dgvOptiStock.CurrentCellDirtyStateChanged += dgvOptiStock_CurrentCellDirtyStateChanged;
            dgvOptiStock.SelectionChanged += dgvOptiStock_SelectionChanged;
            dgvOptiStock.UserAddedRow += dgvOptiStock_UserAddedRow;
            dgvOptiStock.KeyDown += dgvOptiStock_KeyDown;
            // 
            // btnConfirmar
            // 
            btnConfirmar.BackColor = Color.ForestGreen;
            btnConfirmar.IconChar = FontAwesome.Sharp.IconChar.Check;
            btnConfirmar.IconColor = Color.White;
            btnConfirmar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnConfirmar.Location = new Point(37, 780);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(83, 48);
            btnConfirmar.TabIndex = 2;
            btnConfirmar.UseVisualStyleBackColor = false;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.OrangeRed;
            btnEliminar.Enabled = false;
            btnEliminar.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
            btnEliminar.IconColor = Color.White;
            btnEliminar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnEliminar.IconSize = 40;
            btnEliminar.Location = new Point(126, 780);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(83, 48);
            btnEliminar.TabIndex = 3;
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = Color.RoyalBlue;
            btnActualizar.IconChar = FontAwesome.Sharp.IconChar.PenClip;
            btnActualizar.IconColor = Color.White;
            btnActualizar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnActualizar.IconSize = 40;
            btnActualizar.Location = new Point(215, 780);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(83, 48);
            btnActualizar.TabIndex = 4;
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnRefrescar
            // 
            btnRefrescar.BackColor = Color.SlateGray;
            btnRefrescar.IconChar = FontAwesome.Sharp.IconChar.ArrowRotateLeft;
            btnRefrescar.IconColor = Color.White;
            btnRefrescar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnRefrescar.IconSize = 40;
            btnRefrescar.Location = new Point(579, 780);
            btnRefrescar.Name = "btnRefrescar";
            btnRefrescar.Size = new Size(83, 48);
            btnRefrescar.TabIndex = 5;
            btnRefrescar.UseVisualStyleBackColor = false;
            btnRefrescar.Click += btnRefrescar_Click;
            // 
            // btnMovimientos
            // 
            btnMovimientos.BackColor = Color.SaddleBrown;
            btnMovimientos.Cursor = Cursors.Hand;
            btnMovimientos.FlatAppearance.BorderColor = Color.Goldenrod;
            btnMovimientos.FlatAppearance.BorderSize = 5;
            btnMovimientos.Font = new Font("Lucida Bright", 8F, FontStyle.Bold);
            btnMovimientos.ForeColor = Color.White;
            btnMovimientos.IconChar = FontAwesome.Sharp.IconChar.ArrowRightArrowLeft;
            btnMovimientos.IconColor = Color.White;
            btnMovimientos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnMovimientos.IconSize = 80;
            btnMovimientos.ImageAlign = ContentAlignment.TopCenter;
            btnMovimientos.Location = new Point(509, 68);
            btnMovimientos.Name = "btnMovimientos";
            btnMovimientos.Padding = new Padding(10);
            btnMovimientos.Size = new Size(153, 122);
            btnMovimientos.TabIndex = 7;
            btnMovimientos.Text = "Movimientos";
            btnMovimientos.TextAlign = ContentAlignment.BottomCenter;
            btnMovimientos.UseVisualStyleBackColor = false;
            btnMovimientos.Click += btnMovimientos_Click;
            // 
            // dgvOptiStockAcciones
            // 
            dgvOptiStockAcciones.BackgroundColor = SystemColors.ControlLight;
            dgvOptiStockAcciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOptiStockAcciones.Location = new Point(687, 208);
            dgvOptiStockAcciones.Name = "dgvOptiStockAcciones";
            dgvOptiStockAcciones.RowHeadersWidth = 62;
            dgvOptiStockAcciones.Size = new Size(275, 566);
            dgvOptiStockAcciones.TabIndex = 8;
            // 
            // dgvOptiStockResumen
            // 
            dgvOptiStockResumen.BackgroundColor = SystemColors.ControlLight;
            dgvOptiStockResumen.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOptiStockResumen.Location = new Point(988, 405);
            dgvOptiStockResumen.Name = "dgvOptiStockResumen";
            dgvOptiStockResumen.RowHeadersWidth = 62;
            dgvOptiStockResumen.Size = new Size(275, 174);
            dgvOptiStockResumen.TabIndex = 9;
            // 
            // dgvOptiStockDetalle
            // 
            dgvOptiStockDetalle.BackgroundColor = SystemColors.ControlLight;
            dgvOptiStockDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOptiStockDetalle.Location = new Point(988, 601);
            dgvOptiStockDetalle.Name = "dgvOptiStockDetalle";
            dgvOptiStockDetalle.RowHeadersWidth = 62;
            dgvOptiStockDetalle.Size = new Size(275, 173);
            dgvOptiStockDetalle.TabIndex = 11;
            // 
            // dgvOptiStockPeriodo
            // 
            dgvOptiStockPeriodo.BackgroundColor = SystemColors.ControlLight;
            dgvOptiStockPeriodo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOptiStockPeriodo.Location = new Point(988, 208);
            dgvOptiStockPeriodo.Name = "dgvOptiStockPeriodo";
            dgvOptiStockPeriodo.RowHeadersWidth = 62;
            dgvOptiStockPeriodo.Size = new Size(275, 171);
            dgvOptiStockPeriodo.TabIndex = 10;
            // 
            // dgvOptiStockDatosNuevos
            // 
            dgvOptiStockDatosNuevos.BackgroundColor = SystemColors.ControlLight;
            dgvOptiStockDatosNuevos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOptiStockDatosNuevos.Location = new Point(1289, 208);
            dgvOptiStockDatosNuevos.Name = "dgvOptiStockDatosNuevos";
            dgvOptiStockDatosNuevos.RowHeadersWidth = 62;
            dgvOptiStockDatosNuevos.Size = new Size(275, 263);
            dgvOptiStockDatosNuevos.TabIndex = 12;
            // 
            // dgvOptiStockDatosAnteriores
            // 
            dgvOptiStockDatosAnteriores.BackgroundColor = SystemColors.ControlLight;
            dgvOptiStockDatosAnteriores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOptiStockDatosAnteriores.Location = new Point(1289, 497);
            dgvOptiStockDatosAnteriores.Name = "dgvOptiStockDatosAnteriores";
            dgvOptiStockDatosAnteriores.RowHeadersWidth = 62;
            dgvOptiStockDatosAnteriores.Size = new Size(275, 277);
            dgvOptiStockDatosAnteriores.TabIndex = 13;
            // 
            // dgvOptiStockCrear
            // 
            dgvOptiStockCrear.BackgroundColor = SystemColors.ControlLight;
            dgvOptiStockCrear.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOptiStockCrear.Location = new Point(37, 660);
            dgvOptiStockCrear.Name = "dgvOptiStockCrear";
            dgvOptiStockCrear.RowHeadersWidth = 62;
            dgvOptiStockCrear.Size = new Size(625, 114);
            dgvOptiStockCrear.TabIndex = 14;
            dgvOptiStockCrear.AllowUserToAddRows = false;
            dgvOptiStockCrear.CurrentCellDirtyStateChanged += dgvOptiStockCrear_CurrentCellDirtyStateChanged;
            dgvOptiStockCrear.CellValueChanged += dgvOptiStockCrear_CellValueChanged;
            // 
            // OptiStock
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1596, 852);
            Controls.Add(dgvOptiStockCrear);
            Controls.Add(dgvOptiStockDatosAnteriores);
            Controls.Add(dgvOptiStockDatosNuevos);
            Controls.Add(dgvOptiStockDetalle);
            Controls.Add(dgvOptiStockPeriodo);
            Controls.Add(dgvOptiStockResumen);
            Controls.Add(dgvOptiStockAcciones);
            Controls.Add(btnMovimientos);
            Controls.Add(btnNotificaciones);
            Controls.Add(btnRefrescar);
            Controls.Add(btnCajas);
            Controls.Add(btnActualizar);
            Controls.Add(btnPrecios);
            Controls.Add(btnEliminar);
            Controls.Add(btnCostos);
            Controls.Add(btnConfirmar);
            Controls.Add(btnReportes);
            Controls.Add(dgvOptiStock);
            Controls.Add(btnAuditorias);
            Controls.Add(panel1);
            Name = "OptiStock";
            Text = "OptiStock";
            ((System.ComponentModel.ISupportInitialize)dgvOptiStock).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvOptiStockAcciones).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvOptiStockResumen).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvOptiStockDetalle).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvOptiStockPeriodo).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvOptiStockDatosNuevos).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvOptiStockDatosAnteriores).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvOptiStockCrear).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private FontAwesome.Sharp.IconButton btnAuditorias;
        private FontAwesome.Sharp.IconButton btnReportes;
        private FontAwesome.Sharp.IconButton btnCostos;
        private FontAwesome.Sharp.IconButton btnPrecios;
        private FontAwesome.Sharp.IconButton btnCajas;
        private FontAwesome.Sharp.IconButton btnNotificaciones;
        private DataGridView dgvOptiStock;
        private FontAwesome.Sharp.IconButton btnConfirmar;
        private FontAwesome.Sharp.IconButton btnEliminar;
        private FontAwesome.Sharp.IconButton btnActualizar;
        private FontAwesome.Sharp.IconButton btnRefrescar;
        private FontAwesome.Sharp.IconButton btnMovimientos;
        private DataGridView dgvOptiStockAcciones;
        private DataGridView dgvOptiStockResumen;
        private DataGridView dgvOptiStockDetalle;
        private DataGridView dgvOptiStockPeriodo;
        private DataGridView dgvOptiStockDatosNuevos;
        private DataGridView dgvOptiStockDatosAnteriores;
        private DataGridView dgvOptiStockCrear;
    }
}
