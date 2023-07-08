namespace InterfaceAquisicaoDadosMotorDc
{
    partial class FormPrincipal
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            this.button1 = new System.Windows.Forms.Button();
            this.Btn_Salvar_Captura = new System.Windows.Forms.Button();
            this.Cb_Lista_SerialPorts = new System.Windows.Forms.ComboBox();
            this.Lb_Serial_Port_Combo_Box = new System.Windows.Forms.Label();
            this.Timer_Atualizacao_Portas_Seriais = new System.Windows.Forms.Timer(this.components);
            this.Btn_Iniciar_Captura_Dados = new System.Windows.Forms.Button();
            this.Voltage_Chart = new ScottPlot.FormsPlot();
            this.Current_Chart = new ScottPlot.FormsPlot();
            this.TimerCemMilissegundos = new System.Windows.Forms.Timer(this.components);
            this.Btn_Exportar_Graficos = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Tbx_Baud_Rate = new System.Windows.Forms.TextBox();
            this.Tbx_Data_Bits = new System.Windows.Forms.TextBox();
            this.Lbl_Data_Bits = new System.Windows.Forms.Label();
            this.Lbl_Paridade = new System.Windows.Forms.Label();
            this.Cb_Paridade = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 362);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Btn_Salvar_Captura
            // 
            this.Btn_Salvar_Captura.Location = new System.Drawing.Point(12, 410);
            this.Btn_Salvar_Captura.Name = "Btn_Salvar_Captura";
            this.Btn_Salvar_Captura.Size = new System.Drawing.Size(125, 41);
            this.Btn_Salvar_Captura.TabIndex = 1;
            this.Btn_Salvar_Captura.Text = "Salvar captura";
            this.Btn_Salvar_Captura.UseVisualStyleBackColor = true;
            this.Btn_Salvar_Captura.Click += new System.EventHandler(this.Btn_Salvar_Captura_Click);
            // 
            // Cb_Lista_SerialPorts
            // 
            this.Cb_Lista_SerialPorts.FormattingEnabled = true;
            this.Cb_Lista_SerialPorts.Location = new System.Drawing.Point(12, 58);
            this.Cb_Lista_SerialPorts.Name = "Cb_Lista_SerialPorts";
            this.Cb_Lista_SerialPorts.Size = new System.Drawing.Size(121, 23);
            this.Cb_Lista_SerialPorts.TabIndex = 4;
            // 
            // Lb_Serial_Port_Combo_Box
            // 
            this.Lb_Serial_Port_Combo_Box.AutoSize = true;
            this.Lb_Serial_Port_Combo_Box.Location = new System.Drawing.Point(12, 40);
            this.Lb_Serial_Port_Combo_Box.Name = "Lb_Serial_Port_Combo_Box";
            this.Lb_Serial_Port_Combo_Box.Size = new System.Drawing.Size(60, 15);
            this.Lb_Serial_Port_Combo_Box.TabIndex = 5;
            this.Lb_Serial_Port_Combo_Box.Text = "Serial port";
            // 
            // Timer_Atualizacao_Portas_Seriais
            // 
            this.Timer_Atualizacao_Portas_Seriais.Interval = 2000;
            this.Timer_Atualizacao_Portas_Seriais.Tick += new System.EventHandler(this.Timer_Atualizacao_Portas_Seriais_Tick);
            // 
            // Btn_Iniciar_Captura_Dados
            // 
            this.Btn_Iniciar_Captura_Dados.Location = new System.Drawing.Point(12, 363);
            this.Btn_Iniciar_Captura_Dados.Name = "Btn_Iniciar_Captura_Dados";
            this.Btn_Iniciar_Captura_Dados.Size = new System.Drawing.Size(125, 41);
            this.Btn_Iniciar_Captura_Dados.TabIndex = 6;
            this.Btn_Iniciar_Captura_Dados.Text = "Iniciar captura";
            this.Btn_Iniciar_Captura_Dados.UseVisualStyleBackColor = true;
            this.Btn_Iniciar_Captura_Dados.Click += new System.EventHandler(this.Btn_Iniciar_Captura_Dados_Click);
            // 
            // Voltage_Chart
            // 
            this.Voltage_Chart.Location = new System.Drawing.Point(140, 58);
            this.Voltage_Chart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Voltage_Chart.Name = "Voltage_Chart";
            this.Voltage_Chart.Size = new System.Drawing.Size(631, 216);
            this.Voltage_Chart.TabIndex = 7;
            // 
            // Current_Chart
            // 
            this.Current_Chart.Location = new System.Drawing.Point(140, 307);
            this.Current_Chart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Current_Chart.Name = "Current_Chart";
            this.Current_Chart.Size = new System.Drawing.Size(631, 216);
            this.Current_Chart.TabIndex = 8;
            // 
            // TimerCemMilissegundos
            // 
            this.TimerCemMilissegundos.Enabled = true;
            this.TimerCemMilissegundos.Tick += new System.EventHandler(this.TimerCemMilissegundos_Tick);
            // 
            // Btn_Exportar_Graficos
            // 
            this.Btn_Exportar_Graficos.Location = new System.Drawing.Point(12, 457);
            this.Btn_Exportar_Graficos.Name = "Btn_Exportar_Graficos";
            this.Btn_Exportar_Graficos.Size = new System.Drawing.Size(125, 41);
            this.Btn_Exportar_Graficos.TabIndex = 9;
            this.Btn_Exportar_Graficos.Text = "Exportar graficos";
            this.Btn_Exportar_Graficos.UseVisualStyleBackColor = true;
            this.Btn_Exportar_Graficos.Click += new System.EventHandler(this.Btn_Exportar_Graficos_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "Baud Rate";
            // 
            // Tbx_Baud_Rate
            // 
            this.Tbx_Baud_Rate.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Tbx_Baud_Rate.Location = new System.Drawing.Point(12, 108);
            this.Tbx_Baud_Rate.MaxLength = 6;
            this.Tbx_Baud_Rate.Name = "Tbx_Baud_Rate";
            this.Tbx_Baud_Rate.Size = new System.Drawing.Size(121, 23);
            this.Tbx_Baud_Rate.TabIndex = 12;
            // 
            // Tbx_Data_Bits
            // 
            this.Tbx_Data_Bits.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Tbx_Data_Bits.Location = new System.Drawing.Point(12, 160);
            this.Tbx_Data_Bits.MaxLength = 2;
            this.Tbx_Data_Bits.Name = "Tbx_Data_Bits";
            this.Tbx_Data_Bits.Size = new System.Drawing.Size(121, 23);
            this.Tbx_Data_Bits.TabIndex = 14;
            // 
            // Lbl_Data_Bits
            // 
            this.Lbl_Data_Bits.AutoSize = true;
            this.Lbl_Data_Bits.Location = new System.Drawing.Point(12, 142);
            this.Lbl_Data_Bits.Name = "Lbl_Data_Bits";
            this.Lbl_Data_Bits.Size = new System.Drawing.Size(53, 15);
            this.Lbl_Data_Bits.TabIndex = 13;
            this.Lbl_Data_Bits.Text = "Data Bits";
            // 
            // Lbl_Paridade
            // 
            this.Lbl_Paridade.AutoSize = true;
            this.Lbl_Paridade.Location = new System.Drawing.Point(12, 195);
            this.Lbl_Paridade.Name = "Lbl_Paridade";
            this.Lbl_Paridade.Size = new System.Drawing.Size(53, 15);
            this.Lbl_Paridade.TabIndex = 15;
            this.Lbl_Paridade.Text = "Paridade";
            // 
            // Cb_Paridade
            // 
            this.Cb_Paridade.FormattingEnabled = true;
            this.Cb_Paridade.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even",
            "Mark",
            "Space"});
            this.Cb_Paridade.Location = new System.Drawing.Point(12, 213);
            this.Cb_Paridade.Name = "Cb_Paridade";
            this.Cb_Paridade.Size = new System.Drawing.Size(121, 23);
            this.Cb_Paridade.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 537);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 15);
            this.label2.TabIndex = 17;
            this.label2.Text = "IFSP-SPO";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(374, 537);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(398, 15);
            this.label3.TabIndex = 18;
            this.label3.Text = "Software Livre - Codigo Fonte disponivel em: https://github.com/phalvesr";
            // 
            // FormPrincipal
            // 
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Cb_Paridade);
            this.Controls.Add(this.Lbl_Paridade);
            this.Controls.Add(this.Tbx_Data_Bits);
            this.Controls.Add(this.Lbl_Data_Bits);
            this.Controls.Add(this.Tbx_Baud_Rate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Btn_Exportar_Graficos);
            this.Controls.Add(this.Current_Chart);
            this.Controls.Add(this.Voltage_Chart);
            this.Controls.Add(this.Btn_Iniciar_Captura_Dados);
            this.Controls.Add(this.Lb_Serial_Port_Combo_Box);
            this.Controls.Add(this.Cb_Lista_SerialPorts);
            this.Controls.Add(this.Btn_Salvar_Captura);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IFSP - Sistema de aquisição de dados motor DC";
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private Button Btn_Salvar_Captura;
        private ComboBox Cb_Lista_SerialPorts;
        private Label Lb_Serial_Port_Combo_Box;
        private System.Windows.Forms.Timer Timer_Atualizacao_Portas_Seriais;
        private Button Btn_Iniciar_Captura_Dados;
        private ScottPlot.FormsPlot Voltage_Chart;
        private ScottPlot.FormsPlot Current_Chart;
        private System.Windows.Forms.Timer TimerCemMilissegundos;
        private Button Btn_Exportar_Graficos;
        private Label label1;
        private TextBox Tbx_Baud_Rate;
        private TextBox Tbx_Data_Bits;
        private Label Lbl_Data_Bits;
        private Label Lbl_Paridade;
        private ComboBox Cb_Paridade;
        private Label label2;
        private Label label3;
    }
}