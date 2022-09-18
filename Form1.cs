using InterfaceAquisicaoDadosMotorDc.Core.UseCases.Interfaces;
using InterfaceAquisicaoDadosMotorDc.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace InterfaceAquisicaoDadosMotorDc
{
    public partial class FormPrincipal : Form
    {
        // tempo total de captura: (TEMPO_AMOSTRAS / 0.050)
        const int TOTAL_AMOSTRAS = 10_000;

        private bool isCapturandoDados = false;

        private readonly IListSerialPortsUseCase listaSerialPortsUseCase;
        private readonly ISerialDataReceivedHandler serialDataReceivedHandler;
        private readonly IStartSerialDataCaptureUseCase startSerialDataCaptureUseCase;
        private readonly IStopSerialDataCapture stopSerialDataCaptureUseCase;
        private readonly ISaveCsvFileUseCase saveCsvFileUseCase;

        private int indexAmostras = 0;

        private readonly double[] voltagesToSave = new double[TOTAL_AMOSTRAS];
        private readonly double[] currentsToSave = new double[TOTAL_AMOSTRAS];

        public FormPrincipal(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            listaSerialPortsUseCase = serviceProvider.GetRequiredService<IListSerialPortsUseCase>();
            serialDataReceivedHandler = serviceProvider.GetRequiredService<ISerialDataReceivedHandler>();
            startSerialDataCaptureUseCase = serviceProvider.GetRequiredService<IStartSerialDataCaptureUseCase>();
            stopSerialDataCaptureUseCase = serviceProvider.GetRequiredService<IStopSerialDataCapture>();
            saveCsvFileUseCase = serviceProvider.GetRequiredService<ISaveCsvFileUseCase>();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            Timer_Atualizacao_Portas_Seriais.Start();

            SetChartsLabels();
            DisableChartGrid();

            var voltageSignal = Voltage_Chart.Plot.AddSignal(voltagesToSave, 0.5, label: "Voltage (V)");
            Voltage_Chart.Plot.SetAxisLimits(0, TOTAL_AMOSTRAS, 0, 15);

            voltageSignal.LineColor = Color.DarkGreen;


            var currentSignal = Current_Chart.Plot.AddSignal(currentsToSave, 0.5, label: "Current (A)");
            Current_Chart.Plot.SetAxisLimits(0, TOTAL_AMOSTRAS, -2, 2);

            currentSignal.LineColor = Color.DarkGreen;


            this.serialDataReceivedHandler.SerialDataParsed += SerialDataReceivedHandler_SerialDataParsed;
        }

        private void SerialDataReceivedHandler_SerialDataParsed(object? sender, EventArgs e)
        {
            
            var serialDataReceived = (ISerialDataReceivedHandler)sender!;

            var voltage = serialDataReceived!.Voltage;
            var current = serialDataReceived!.Current;

            UpdateVoltagePlot(ConversorDiscretoFisico.ConverterParaTensao(voltage));
            UpdateCurrentPlot(ConversorDiscretoFisico.ConverterParaCorrente(current));
            indexAmostras++;
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
            Voltage_Chart.Plot.Title("Voltage", true, Color.Black);

            Current_Chart.Plot.XLabel("Samples");
            Current_Chart.Plot.YLabel("Current (A)");
            Current_Chart.Plot.Title("Current", true, Color.Black);
        }

        private void Btn_Salvar_Captura_Click(object sender, EventArgs e)
        {
            if (!isCapturandoDados)
            {
                MessageBox.Show("Captura de dados não foi iniciada", "Captura não acionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            var resultParada = MessageBox.Show("Deseja parar a captura dos dados?", "Parando captura", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (resultParada != DialogResult.Yes)
            {
                return;
            }

            PararCapturaDados();

            using var fileDialog = new SaveFileDialog();
            fileDialog.InitialDirectory = "";
            fileDialog.RestoreDirectory = true;


            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            this.saveCsvFileUseCase.GerarCsv(fileDialog.FileName, this.voltagesToSave, this.currentsToSave, indexAmostras)
            .match(_ =>
            {
                MessageBox.Show("Arquivo salvo com sucesso!", "Arquivo salvo!", MessageBoxButtons.OK);
            }, erro =>
            {
                MessageBox.Show(erro.Message, "Erro ao salvar arquivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            });

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
            if (indexAmostras >= TOTAL_AMOSTRAS)
            {
                indexAmostras = 0;
            }

            currentsToSave[indexAmostras] = readedCurrent;
        }

        private void UpdateVoltagePlot(double readedVoltage)
        {
            
            if (indexAmostras >= TOTAL_AMOSTRAS)
            {
                indexAmostras = 0;
            }

            voltagesToSave[indexAmostras] = readedVoltage;
        }

        private void Btn_Iniciar_Captura_Dados_Click(object sender, EventArgs e)
        {
            var selectedPortName = Cb_Lista_SerialPorts.Text;

            if (string.IsNullOrEmpty(selectedPortName) || string.IsNullOrEmpty(selectedPortName))
            {
                MessageBox.Show("Por favor, selecione uma porta serial", "Porta serial inválida",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.isCapturandoDados = !this.isCapturandoDados;

            if (this.isCapturandoDados)
            {
                IniciarCapturaDados(selectedPortName);
                return;
            }

            PararCapturaDados();
        }

        private void IniciarCapturaDados(string selectedPortName)
        {
            this.serialDataReceivedHandler.Execute();
            this.startSerialDataCaptureUseCase.Execute(selectedPortName);

            isCapturandoDados = true;
            Btn_Iniciar_Captura_Dados.Text = "Parar captura";
        }

        private void PararCapturaDados()
        {
            stopSerialDataCaptureUseCase.Execute();

            this.isCapturandoDados = false;
            Btn_Iniciar_Captura_Dados.Text = "Iniciar captura";

            var limparDados = MessageBox.Show("Deseja limpar dados?", "Limpar dados", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (limparDados == DialogResult.Yes)
            {
                for (var i = 0; i < indexAmostras; i++)
                {
                    this.voltagesToSave[i] = 0;
                    this.currentsToSave[i] = 0;
                }
                indexAmostras = 0;
            }

        }

        private void TimerCemMilissegundos_Tick(object sender, EventArgs e)
        {
            Current_Chart.Render();
            Voltage_Chart.Render();
        }
    }
}