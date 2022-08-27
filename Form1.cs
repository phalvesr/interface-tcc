using InterfaceAquisicaoDadosMotorDc.Core.UseCases.Interfaces;
using InterfaceAquisicaoDadosMotorDc.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace InterfaceAquisicaoDadosMotorDc
{
    public partial class FormPrincipal : Form
    {
        const int TOTAL_AMOSTRAS = 10_000;
        private bool isCapturandoDados = false;
        
        //private readonly IServiceProvider serviceProvider;
        private readonly IListSerialPortsUseCase listaSerialPortsUseCase;
        private readonly ISerialDataReceivedHandler serialDataReceivedHandler;

        private int voltagesIndex = 0;
        private int currentsIndex = 0;

        private readonly double[] voltagesToSave = new double[TOTAL_AMOSTRAS];
        private readonly double[] currentsToSave = new double[TOTAL_AMOSTRAS];

        public FormPrincipal(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            //this.serviceProvider = serviceProvider;
            listaSerialPortsUseCase = serviceProvider.GetRequiredService<IListSerialPortsUseCase>();
            serialDataReceivedHandler = serviceProvider.GetRequiredService<ISerialDataReceivedHandler>();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            Timer_Atualizacao_Portas_Seriais.Start();

            SetChartsLabels();
            DisableChartGrid();

            Voltage_Chart.Plot.AddSignal(voltagesToSave, 0.5, label: "Voltage (V)");
            Voltage_Chart.Plot.SetAxisLimits(0, TOTAL_AMOSTRAS, 0, 5);

            Current_Chart.Plot.AddSignal(currentsToSave, 0.5, label: "Current (A)");
            Current_Chart.Plot.SetAxisLimits(0, TOTAL_AMOSTRAS, 0, 5);

            this.serialDataReceivedHandler.SerialDataParsed += SerialDataReceivedHandler_SerialDataParsed;
        }

        private void SerialDataReceivedHandler_SerialDataParsed(object? sender, EventArgs e)
        {
            
            var serialDataReceived = (ISerialDataReceivedHandler)sender!;

            var v = serialDataReceived!.Voltage;
            var c = serialDataReceived!.Current;

            UpdateVoltagePlot(ConversorDiscretoFisico.ConverterParaTensao(v));
            UpdateCurrentPlot(ConversorDiscretoFisico.ConverterParaCorrente(c));
        }

        private void DisableChartGrid()
        {
            Voltage_Chart.Plot.Grid(false);
            Current_Chart.Plot.Grid(false);
        }

        private void SetChartsLabels()
        {
            Voltage_Chart.Plot.XLabel("Samples");
            Voltage_Chart.Plot.YLabel("Voltage (V)");
            Voltage_Chart.Plot.Title("Voltage", true, Color.DarkOrange);

            Current_Chart.Plot.XLabel("Samples");
            Current_Chart.Plot.YLabel("Current (A)");
            Current_Chart.Plot.Title("Current", true, Color.DarkOrange);
        }

        private void Btn_Salvar_Captura_Click(object sender, EventArgs e)
        {
            if (!isCapturandoDados)
            {
                MessageBox.Show("Captura de dados não foi iniciada", "Captura não acionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
        }

        private void Timer_Atualizacao_Portas_Seriais_Tick(object sender, EventArgs e)
        {
            Timer_Atualizacao_Portas_Seriais.Stop();

            var serialPorts = listaSerialPortsUseCase.Execute();

            Cb_Lista_SerialPorts.Items.Clear();
            Cb_Lista_SerialPorts.Items.AddRange(serialPorts);

            Timer_Atualizacao_Portas_Seriais.Start();
        }

        private void UpdateCurrentPlot(double readedCurrent)
        {
            if (currentsIndex >= TOTAL_AMOSTRAS)
            {
                currentsIndex = 0;
            }

            currentsToSave[currentsIndex] = readedCurrent;

            currentsIndex++;
        }

        private void UpdateVoltagePlot(double readedVoltage)
        {
            
            if (voltagesIndex >= TOTAL_AMOSTRAS)
            {
                voltagesIndex = 0;
            }

            voltagesToSave[voltagesIndex] = readedVoltage;

            voltagesIndex++;
        }

        private void Btn_Iniciar_Captura_Dados_Click(object sender, EventArgs e)
        {
            this.isCapturandoDados = !this.isCapturandoDados;

            if (this.isCapturandoDados)
            {
                Btn_Iniciar_Captura_Dados.Text = "Iniciar captura";

                IniciarCapturaDados();
                return;
            }

            Btn_Iniciar_Captura_Dados.Text = "Parar captura";
            PararCapturaDados();
        }

        private void IniciarCapturaDados()
        {
            // Iniciar a porta serial selecionada pelo usuario
            // Executar a leitura dos dados
            // Adicionar os dados lidos ao array de dados
            // Imprimir no grafico
            this.serialDataReceivedHandler.Execute();
        }

        private void PararCapturaDados()
        {

        }

        private void TimerCemMilissegundos_Tick(object sender, EventArgs e)
        {
            Current_Chart.Render();
            Voltage_Chart.Render();
        }
    }
}