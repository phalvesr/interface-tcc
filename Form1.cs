using FluentValidation;
using InterfaceAquisicaoDadosMotorDc.Core.Abstractions.Providers;
using InterfaceAquisicaoDadosMotorDc.Core.Model;
using InterfaceAquisicaoDadosMotorDc.Core.UseCases.Interfaces;
using InterfaceAquisicaoDadosMotorDc.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace InterfaceAquisicaoDadosMotorDc
{
    public partial class FormPrincipal : Form
    {
        const int TOTAL_AMOSTRAS = 1_000;

        private bool isCapturandoDados = false;

        private readonly IListSerialPortsUseCase listaSerialPortsUseCase;
        private readonly ISerialDataReceivedHandler serialDataReceivedHandler;
        private readonly IStartSerialDataCaptureUseCase startSerialDataCaptureUseCase;
        private readonly IStopSerialDataCapture stopSerialDataCaptureUseCase;
        private readonly ISaveCsvFileUseCase saveCsvFileUseCase;
        private readonly ISendAlertNotification sendAlertNotification;
        private readonly TopicOptions topicOptions;
        private readonly ILogProvider logger;
        private readonly IValidator<SerialPortModel> serialPortModelValidator;
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
            topicOptions = serviceProvider.GetRequiredService<TopicOptions>();
            sendAlertNotification = serviceProvider.GetRequiredService<ISendAlertNotification>();
            logger = serviceProvider.GetRequiredService<ILogProvider>();
            serialPortModelValidator = serviceProvider.GetRequiredService<IValidator<SerialPortModel>>();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            logger.LogDebug("Iniciando configuracao do timer");
            Timer_Atualizacao_Portas_Seriais.Start();

            logger.LogDebug("Configurando graficos da tela");
            SetChartsLabels();
            DisableChartGrid();

            var voltageSignal = Voltage_Chart.Plot.AddSignal(voltagesToSave, label: "Tensao (V)");
            Voltage_Chart.Plot.SetAxisLimits(0, TOTAL_AMOSTRAS, 0, 15);

            voltageSignal.LineColor = Color.DarkGreen;


            var currentSignal = Current_Chart.Plot.AddSignal(currentsToSave, label: "Corrente (A)");
            Current_Chart.Plot.SetAxisLimits(0, TOTAL_AMOSTRAS, -1.5, 1.5);

            currentSignal.LineColor = Color.DarkGreen;

            logger.LogDebug("Atrelando evento de recepcao de dados na serial");
            this.serialDataReceivedHandler.SerialDataParsed += SerialDataReceivedHandler_SerialDataParsed;
        }

        private void SerialDataReceivedHandler_SerialDataParsed(object? sender, EventArgs e)
        {
            logger.LogDebug("Dado recebido na serial. Iniciando processamento");

            var serialDataReceived = (ISerialDataReceivedHandler)sender!;

            var voltage = serialDataReceived!.Voltage;
            var current = serialDataReceived!.Current;

            UpdateVoltagePlot(ConversorDiscretoFisico.ConverterParaTensao(voltage));
            AtualizarLeituraCorrente(ConversorDiscretoFisico.ConverterParaCorrente(current));
            indexAmostras++;
        }

        private void DisableChartGrid()
        {
            Voltage_Chart.Plot.Grid(false);
            Current_Chart.Plot.Grid(false);
        }

        private void SetChartsLabels()
        {
            Voltage_Chart.Plot.XLabel("Tempo (s)");
            Voltage_Chart.Plot.YLabel("Voltage (V)");
            Voltage_Chart.Plot.Title("Voltage", true, Color.Black);
            Voltage_Chart.Configuration.DoubleClickBenchmark = false;

            Current_Chart.Plot.XLabel("Tempo (s)");
            Current_Chart.Plot.YLabel("Current (A)");
            Current_Chart.Plot.Title("Current", true, Color.Black);
            Current_Chart.Configuration.DoubleClickBenchmark = false;
        }

        private void Btn_Salvar_Captura_Click(object sender, EventArgs e)
        {
            logger.LogDebug("Iniciando processo de salvamento de captura");

            if (!isCapturandoDados)
            {
                logger.LogWarning("Processo de salvamento de captura requisitado com captura de dados em status de nao capturando. Retornando do procedimento.");

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
                logger.LogInformation("Usuario recusou salvar dados. Retornando imediatamente de {NomeMetodo}", nameof(Btn_Salvar_Captura_Click));

                return;
            }

            this.saveCsvFileUseCase.GerarCsv(fileDialog.FileName, this.voltagesToSave, this.currentsToSave, indexAmostras)
            .match(_ =>
            {
                logger.LogInformation("Arquivo {NomeArquivo} salvo com sucesso!");

                MessageBox.Show("Arquivo salvo com sucesso!", "Arquivo salvo!", MessageBoxButtons.OK);
            }, erro =>
            {
                logger.LogWarning("Erro ao salvar arquivo {NomeArquivo}. Mensagem de erro: {MensagemErro}", erro.Message);

                MessageBox.Show(erro.Message, "Erro ao salvar arquivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            });

        }

        private void Timer_Atualizacao_Portas_Seriais_Tick(object sender, EventArgs e)
        {
            logger.LogDebug("Rotina do timer de atualizacao de portas seriais sendo executada");

            Timer_Atualizacao_Portas_Seriais.Stop();

            var serialPorts = listaSerialPortsUseCase.Execute();

            Cb_Lista_SerialPorts.Items.Clear();
            Cb_Lista_SerialPorts.Items.AddRange(serialPorts);

            Timer_Atualizacao_Portas_Seriais.Start();

            logger.LogDebug("Fim da execucao da rotina do timer de atualizacao de portas seriais");
        }

        private void AtualizarLeituraCorrente(double readedCurrent)
        {
            if (indexAmostras >= TOTAL_AMOSTRAS)
            {
                indexAmostras = 0;
            }

            currentsToSave[indexAmostras] = readedCurrent;

            if (readedCurrent * 1000 > topicOptions.CurrentThresholdInMilliampere)
            {
                sendAlertNotification.SendNotification(NotificationType.CurrentThesholdReached, DateTimeOffset.UtcNow);
            }
        }

        private void UpdateVoltagePlot(double readedVoltage)
        {
            
            if (indexAmostras >= TOTAL_AMOSTRAS)
            {
                indexAmostras = 0;
            }

            voltagesToSave[indexAmostras] = readedVoltage;
            
            if (readedVoltage * 1000 > topicOptions.VoltageThresholdInMillivolts)
            {
                sendAlertNotification.SendNotification(NotificationType.VoltageThesholdReached, DateTimeOffset.UtcNow);
            }
        }

        private void Btn_Iniciar_Captura_Dados_Click(object sender, EventArgs e)
        {
            var portName = Cb_Lista_SerialPorts.Text;
            var baudRate = Tbx_Baud_Rate.Text;
            var dataBits = Tbx_Data_Bits.Text;
            var parity = Cb_Paridade.Text;

            var serialPortModel = new SerialPortModel
            {
                PortName = portName,
                BaudRate = baudRate,
                DataBits = dataBits,
                Paridade = parity
            };

            var validationResult = serialPortModelValidator.Validate(serialPortModel);

            if (!validationResult.IsValid)
            {
                logger.LogWarning("Tentativa de inicio de captura de dados sem configuracao de porta correta");

                MessageBox.Show(string.Join($",{Environment.NewLine}{Environment.NewLine}", 
                    validationResult.Errors.Select(e => e.ErrorMessage).ToArray()), 
                    "Dados porta serial invalidos",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            logger.LogInformation("Tentando iniciar captura de dados na porta {NomePorta}", portName);

            this.isCapturandoDados = !this.isCapturandoDados;

            if (this.isCapturandoDados)
            {
                logger.LogInformation("Iniciando captura de dados");

                IniciarCapturaDados(serialPortModel);
                return;
            }

            logger.LogInformation("Parando captura de dados");
            PararCapturaDados();
        }

        private void IniciarCapturaDados(SerialPortModel serialPortModel)
        {
            this.serialDataReceivedHandler.Execute();
            this.startSerialDataCaptureUseCase.Execute(serialPortModel);

            isCapturandoDados = true;
            Btn_Iniciar_Captura_Dados.Text = "Parar captura";

            DesabilitarControladoresPortaSerial();
        }

        private void DesabilitarControladoresPortaSerial()
        {
            Cb_Lista_SerialPorts.Enabled = false;
            Tbx_Baud_Rate.Enabled = false;
            Tbx_Data_Bits.Enabled = false;
            Cb_Paridade.Enabled = false;
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

            HabilitarControladoresPortaSerial();
        }

        private void HabilitarControladoresPortaSerial()
        {
            Cb_Lista_SerialPorts.Enabled = true;
            Tbx_Baud_Rate.Enabled = true;
            Tbx_Data_Bits.Enabled = true;
            Cb_Paridade.Enabled = true;
        }

        private void TimerCemMilissegundos_Tick(object sender, EventArgs e)
        {
            Current_Chart.Render();
            Voltage_Chart.Render();
        }

        private void Btn_Exportar_Graficos_Click(object sender, EventArgs e)
        {
            using var fileDialog = new FolderBrowserDialog();

            fileDialog.InitialDirectory = "";

            var dialogResult = fileDialog.ShowDialog(this);

            if (dialogResult != DialogResult.OK) 
            {
                return;
            }

            using var bitmapPlotTensao = new Bitmap(800, 600);
            using var bitmapPlotCorrente = new Bitmap(800, 600);

            Voltage_Chart.Plot.Render(bitmapPlotTensao, true);
            Current_Chart.Plot.Render(bitmapPlotCorrente, true);

            var epochOffsetSalvamento = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

            var diretorioPlot = Path.Combine(fileDialog.SelectedPath, epochOffsetSalvamento);

            CreateDirectoryAtPathIfDoesNotExists(diretorioPlot);

            var nomeArquivoCorrente = $"{epochOffsetSalvamento}-plot-corrente.png";
            var nomeArquivoTensao = $"{epochOffsetSalvamento}-plot-tensao.png";

            bitmapPlotCorrente.Save(Path.Combine(diretorioPlot, nomeArquivoCorrente), ImageFormat.Png);
            bitmapPlotTensao.Save(Path.Combine(diretorioPlot, nomeArquivoTensao), ImageFormat.Png);
        }

        private void CreateDirectoryAtPathIfDoesNotExists(string path)
        {
            if (Directory.Exists(path))
            {
                return;
            }

            Directory.CreateDirectory(path);
        }
    }
}