namespace TP5Sim
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.btnSimular = new System.Windows.Forms.Button();
            this.txtDesde = new System.Windows.Forms.TextBox();
            this.txtHasta = new System.Windows.Forms.TextBox();
            this.txtCantidadSim = new System.Windows.Forms.TextBox();
            this.labelDesde = new System.Windows.Forms.Label();
            this.labelHasta = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Fila = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Evento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reloj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TiempoLlegadaA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProximaLlegadaA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstadoA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RND_LM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TiempoLlegadaM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProximaLlegadaM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstadoM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TiempoEnsamblajeE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProximoEnsamblajeE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstadoE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RND_1R = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RND_2R = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TiempoLlegadaR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProximaLlegadaR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstadoR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TiempoEnsamblajeAR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProximoTricicloAR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstadoAR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Triciclo1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Triciclo2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Triciclo3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Triciclo4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Triciclo5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIAreaEnsamblaje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIAreaRuedas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TITotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CantidadTriciclos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGrid
            // 
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Fila,
            this.Evento,
            this.Reloj,
            this.TiempoLlegadaA,
            this.ProximaLlegadaA,
            this.StockA,
            this.EstadoA,
            this.RND_LM,
            this.TiempoLlegadaM,
            this.ProximaLlegadaM,
            this.StockM,
            this.EstadoM,
            this.TiempoEnsamblajeE,
            this.ProximoEnsamblajeE,
            this.EstadoE,
            this.RND_1R,
            this.RND_2R,
            this.TiempoLlegadaR,
            this.ProximaLlegadaR,
            this.StockR,
            this.EstadoR,
            this.TiempoEnsamblajeAR,
            this.ProximoTricicloAR,
            this.EstadoAR,
            this.Triciclo1,
            this.Triciclo2,
            this.Triciclo3,
            this.Triciclo4,
            this.Triciclo5,
            this.TIAreaEnsamblaje,
            this.TIAreaRuedas,
            this.TITotal,
            this.CantidadTriciclos});
            this.dataGrid.Location = new System.Drawing.Point(8, 12);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.Size = new System.Drawing.Size(1423, 300);
            this.dataGrid.TabIndex = 0;
            this.dataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGrid_CellContentClick);
            // 
            // btnSimular
            // 
            this.btnSimular.Location = new System.Drawing.Point(630, 548);
            this.btnSimular.Name = "btnSimular";
            this.btnSimular.Size = new System.Drawing.Size(138, 36);
            this.btnSimular.TabIndex = 1;
            this.btnSimular.Text = "Simular";
            this.btnSimular.UseVisualStyleBackColor = true;
            // 
            // txtDesde
            // 
            this.txtDesde.Location = new System.Drawing.Point(556, 377);
            this.txtDesde.Name = "txtDesde";
            this.txtDesde.Size = new System.Drawing.Size(118, 20);
            this.txtDesde.TabIndex = 2;
            // 
            // txtHasta
            // 
            this.txtHasta.Location = new System.Drawing.Point(721, 377);
            this.txtHasta.Name = "txtHasta";
            this.txtHasta.Size = new System.Drawing.Size(118, 20);
            this.txtHasta.TabIndex = 3;
            this.txtHasta.TextChanged += new System.EventHandler(this.TxtHasta_TextChanged);
            // 
            // txtCantidadSim
            // 
            this.txtCantidadSim.Location = new System.Drawing.Point(556, 427);
            this.txtCantidadSim.Name = "txtCantidadSim";
            this.txtCantidadSim.Size = new System.Drawing.Size(118, 20);
            this.txtCantidadSim.TabIndex = 4;
            // 
            // labelDesde
            // 
            this.labelDesde.AutoSize = true;
            this.labelDesde.Location = new System.Drawing.Point(560, 362);
            this.labelDesde.Name = "labelDesde";
            this.labelDesde.Size = new System.Drawing.Size(38, 13);
            this.labelDesde.TabIndex = 5;
            this.labelDesde.Text = "Desde";
            // 
            // labelHasta
            // 
            this.labelHasta.AutoSize = true;
            this.labelHasta.Location = new System.Drawing.Point(725, 363);
            this.labelHasta.Name = "labelHasta";
            this.labelHasta.Size = new System.Drawing.Size(35, 13);
            this.labelHasta.TabIndex = 6;
            this.labelHasta.Text = "Hasta";
            this.labelHasta.Click += new System.EventHandler(this.LabelHasta_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(553, 411);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Cantidad de Simulaciones";
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // Fila
            // 
            this.Fila.HeaderText = "Fila";
            this.Fila.Name = "Fila";
            this.Fila.ReadOnly = true;
            // 
            // Evento
            // 
            this.Evento.HeaderText = "Evento";
            this.Evento.Name = "Evento";
            this.Evento.ReadOnly = true;
            // 
            // Reloj
            // 
            this.Reloj.HeaderText = "Reloj";
            this.Reloj.Name = "Reloj";
            this.Reloj.ReadOnly = true;
            // 
            // TiempoLlegadaA
            // 
            this.TiempoLlegadaA.HeaderText = "TiempoLlegadaA";
            this.TiempoLlegadaA.Name = "TiempoLlegadaA";
            this.TiempoLlegadaA.ReadOnly = true;
            // 
            // ProximaLlegadaA
            // 
            this.ProximaLlegadaA.HeaderText = "ProximaLlegadaA";
            this.ProximaLlegadaA.Name = "ProximaLlegadaA";
            this.ProximaLlegadaA.ReadOnly = true;
            // 
            // StockA
            // 
            this.StockA.HeaderText = "StockA";
            this.StockA.Name = "StockA";
            this.StockA.ReadOnly = true;
            // 
            // EstadoA
            // 
            this.EstadoA.HeaderText = "EstadoA";
            this.EstadoA.Name = "EstadoA";
            this.EstadoA.ReadOnly = true;
            // 
            // RND_LM
            // 
            this.RND_LM.HeaderText = "RND_LM";
            this.RND_LM.Name = "RND_LM";
            this.RND_LM.ReadOnly = true;
            // 
            // TiempoLlegadaM
            // 
            this.TiempoLlegadaM.HeaderText = "TiempoLlegadaM";
            this.TiempoLlegadaM.Name = "TiempoLlegadaM";
            this.TiempoLlegadaM.ReadOnly = true;
            // 
            // ProximaLlegadaM
            // 
            this.ProximaLlegadaM.HeaderText = "ProximaLlegadaM";
            this.ProximaLlegadaM.Name = "ProximaLlegadaM";
            this.ProximaLlegadaM.ReadOnly = true;
            // 
            // StockM
            // 
            this.StockM.HeaderText = "StockM";
            this.StockM.Name = "StockM";
            this.StockM.ReadOnly = true;
            // 
            // EstadoM
            // 
            this.EstadoM.HeaderText = "EstadoM";
            this.EstadoM.Name = "EstadoM";
            this.EstadoM.ReadOnly = true;
            // 
            // TiempoEnsamblajeE
            // 
            this.TiempoEnsamblajeE.HeaderText = "TiempoEnsamblajeE";
            this.TiempoEnsamblajeE.Name = "TiempoEnsamblajeE";
            this.TiempoEnsamblajeE.ReadOnly = true;
            // 
            // ProximoEnsamblajeE
            // 
            this.ProximoEnsamblajeE.HeaderText = "ProximoEnsamblajeE";
            this.ProximoEnsamblajeE.Name = "ProximoEnsamblajeE";
            this.ProximoEnsamblajeE.ReadOnly = true;
            // 
            // EstadoE
            // 
            this.EstadoE.HeaderText = "EstadoE";
            this.EstadoE.Name = "EstadoE";
            this.EstadoE.ReadOnly = true;
            // 
            // RND_1R
            // 
            this.RND_1R.HeaderText = "RND_1R";
            this.RND_1R.Name = "RND_1R";
            this.RND_1R.ReadOnly = true;
            // 
            // RND_2R
            // 
            this.RND_2R.HeaderText = "RND_2R";
            this.RND_2R.Name = "RND_2R";
            this.RND_2R.ReadOnly = true;
            // 
            // TiempoLlegadaR
            // 
            this.TiempoLlegadaR.HeaderText = "TiempoLlegadaR";
            this.TiempoLlegadaR.Name = "TiempoLlegadaR";
            this.TiempoLlegadaR.ReadOnly = true;
            // 
            // ProximaLlegadaR
            // 
            this.ProximaLlegadaR.HeaderText = "ProximaLlegadaR";
            this.ProximaLlegadaR.Name = "ProximaLlegadaR";
            this.ProximaLlegadaR.ReadOnly = true;
            // 
            // StockR
            // 
            this.StockR.HeaderText = "StockR";
            this.StockR.Name = "StockR";
            this.StockR.ReadOnly = true;
            // 
            // EstadoR
            // 
            this.EstadoR.HeaderText = "EstadoR";
            this.EstadoR.Name = "EstadoR";
            this.EstadoR.ReadOnly = true;
            // 
            // TiempoEnsamblajeAR
            // 
            this.TiempoEnsamblajeAR.HeaderText = "TiempoEnsamblajeAR";
            this.TiempoEnsamblajeAR.Name = "TiempoEnsamblajeAR";
            this.TiempoEnsamblajeAR.ReadOnly = true;
            // 
            // ProximoTricicloAR
            // 
            this.ProximoTricicloAR.HeaderText = "ProximoTricicloAR";
            this.ProximoTricicloAR.Name = "ProximoTricicloAR";
            this.ProximoTricicloAR.ReadOnly = true;
            // 
            // EstadoAR
            // 
            this.EstadoAR.HeaderText = "EstadoAR";
            this.EstadoAR.Name = "EstadoAR";
            this.EstadoAR.ReadOnly = true;
            // 
            // Triciclo1
            // 
            this.Triciclo1.HeaderText = "Triciclo1";
            this.Triciclo1.Name = "Triciclo1";
            this.Triciclo1.ReadOnly = true;
            // 
            // Triciclo2
            // 
            this.Triciclo2.HeaderText = "Triciclo2";
            this.Triciclo2.Name = "Triciclo2";
            this.Triciclo2.ReadOnly = true;
            // 
            // Triciclo3
            // 
            this.Triciclo3.HeaderText = "Triciclo3";
            this.Triciclo3.Name = "Triciclo3";
            this.Triciclo3.ReadOnly = true;
            // 
            // Triciclo4
            // 
            this.Triciclo4.HeaderText = "Triciclo4";
            this.Triciclo4.Name = "Triciclo4";
            this.Triciclo4.ReadOnly = true;
            // 
            // Triciclo5
            // 
            this.Triciclo5.HeaderText = "Triciclo5";
            this.Triciclo5.Name = "Triciclo5";
            this.Triciclo5.ReadOnly = true;
            // 
            // TIAreaEnsamblaje
            // 
            this.TIAreaEnsamblaje.HeaderText = "TIAreaEnsamblaje";
            this.TIAreaEnsamblaje.Name = "TIAreaEnsamblaje";
            this.TIAreaEnsamblaje.ReadOnly = true;
            // 
            // TIAreaRuedas
            // 
            this.TIAreaRuedas.HeaderText = "TIAreaRuedas";
            this.TIAreaRuedas.Name = "TIAreaRuedas";
            this.TIAreaRuedas.ReadOnly = true;
            // 
            // TITotal
            // 
            this.TITotal.HeaderText = "TITotal";
            this.TITotal.Name = "TITotal";
            this.TITotal.ReadOnly = true;
            // 
            // CantidadTriciclos
            // 
            this.CantidadTriciclos.HeaderText = "CantidadTriciclos";
            this.CantidadTriciclos.Name = "CantidadTriciclos";
            this.CantidadTriciclos.ReadOnly = true;
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1443, 596);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelHasta);
            this.Controls.Add(this.labelDesde);
            this.Controls.Add(this.txtCantidadSim);
            this.Controls.Add(this.txtHasta);
            this.Controls.Add(this.txtDesde);
            this.Controls.Add(this.btnSimular);
            this.Controls.Add(this.dataGrid);
            this.Name = "FormPrincipal";
            this.ShowIcon = false;
            this.Text = "Simulador de Produccion de Triciclos";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Button btnSimular;
        private System.Windows.Forms.TextBox txtDesde;
        private System.Windows.Forms.TextBox txtHasta;
        private System.Windows.Forms.TextBox txtCantidadSim;
        private System.Windows.Forms.Label labelDesde;
        private System.Windows.Forms.Label labelHasta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fila;
        private System.Windows.Forms.DataGridViewTextBoxColumn Evento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reloj;
        private System.Windows.Forms.DataGridViewTextBoxColumn TiempoLlegadaA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProximaLlegadaA;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockA;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstadoA;
        private System.Windows.Forms.DataGridViewTextBoxColumn RND_LM;
        private System.Windows.Forms.DataGridViewTextBoxColumn TiempoLlegadaM;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProximaLlegadaM;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockM;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstadoM;
        private System.Windows.Forms.DataGridViewTextBoxColumn TiempoEnsamblajeE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProximoEnsamblajeE;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstadoE;
        private System.Windows.Forms.DataGridViewTextBoxColumn RND_1R;
        private System.Windows.Forms.DataGridViewTextBoxColumn RND_2R;
        private System.Windows.Forms.DataGridViewTextBoxColumn TiempoLlegadaR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProximaLlegadaR;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockR;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstadoR;
        private System.Windows.Forms.DataGridViewTextBoxColumn TiempoEnsamblajeAR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProximoTricicloAR;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstadoAR;
        private System.Windows.Forms.DataGridViewTextBoxColumn Triciclo1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Triciclo2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Triciclo3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Triciclo4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Triciclo5;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIAreaEnsamblaje;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIAreaRuedas;
        private System.Windows.Forms.DataGridViewTextBoxColumn TITotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn CantidadTriciclos;
    }
}

