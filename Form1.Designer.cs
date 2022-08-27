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
            this.button1 = new System.Windows.Forms.Button();
            this.Btn_Salvar_Captura = new System.Windows.Forms.Button();
            this.Cb_Lista_SerialPorts = new System.Windows.Forms.ComboBox();
            this.Lb_Serial_Port_Combo_Box = new System.Windows.Forms.Label();
            this.Timer_Atualizacao_Portas_Seriais = new System.Windows.Forms.Timer(this.components);
            this.Btn_Iniciar_Captura_Dados = new System.Windows.Forms.Button();
            this.Voltage_Chart = new ScottPlot.FormsPlot();
            this.Current_Chart = new ScottPlot.FormsPlot();
            this.Lbl_Teste = new System.Windows.Forms.Label();
            this.Lbl_Teste_2 = new System.Windows.Forms.Label();
            this.TimerCemMilissegundos = new System.Windows.Forms.Timer(this.components);
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
            this.Btn_Salvar_Captura.Location = new System.Drawing.Point(12, 482);
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
            this.Timer_Atualizacao_Portas_Seriais.Interval = 500;
            this.Timer_Atualizacao_Portas_Seriais.Tick += new System.EventHandler(this.Timer_Atualizacao_Portas_Seriais_Tick);
            // 
            // Btn_Iniciar_Captura_Dados
            // 
            this.Btn_Iniciar_Captura_Dados.Location = new System.Drawing.Point(12, 435);
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
            // Lbl_Teste
            // 
            this.Lbl_Teste.AutoSize = true;
            this.Lbl_Teste.Location = new System.Drawing.Point(16, 280);
            this.Lbl_Teste.Name = "Lbl_Teste";
            this.Lbl_Teste.Size = new System.Drawing.Size(38, 15);
            this.Lbl_Teste.TabIndex = 9;
            this.Lbl_Teste.Text = "label1";
            // 
            // Lbl_Teste_2
            // 
            this.Lbl_Teste_2.AutoSize = true;
            this.Lbl_Teste_2.Location = new System.Drawing.Point(16, 307);
            this.Lbl_Teste_2.Name = "Lbl_Teste_2";
            this.Lbl_Teste_2.Size = new System.Drawing.Size(38, 15);
            this.Lbl_Teste_2.TabIndex = 10;
            this.Lbl_Teste_2.Text = "label1";
            // 
            // TimerCemMilissegundos
            // 
            this.TimerCemMilissegundos.Enabled = true;
            this.TimerCemMilissegundos.Tick += new System.EventHandler(this.TimerCemMilissegundos_Tick);
            // 
            // FormPrincipal
            // 
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.Lbl_Teste_2);
            this.Controls.Add(this.Lbl_Teste);
            this.Controls.Add(this.Current_Chart);
            this.Controls.Add(this.Voltage_Chart);
            this.Controls.Add(this.Btn_Iniciar_Captura_Dados);
            this.Controls.Add(this.Lb_Serial_Port_Combo_Box);
            this.Controls.Add(this.Cb_Lista_SerialPorts);
            this.Controls.Add(this.Btn_Salvar_Captura);
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
        private Label Lbl_Teste;
        private Label Lbl_Teste_2;
        private System.Windows.Forms.Timer TimerCemMilissegundos;
    }
}