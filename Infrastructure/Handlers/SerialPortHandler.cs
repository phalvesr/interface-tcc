using InterfaceAquisicaoDadosMotorDc.Core.Abstractions;
using InterfaceAquisicaoDadosMotorDc.Core.Abstractions.Providers;
using System.IO.Ports;

namespace InterfaceAquisicaoDadosMotorDc.Infrastructure.Handlers
{
    internal class SerialPortHandler : ISerialPortHandler
    {
        private readonly SerialPort serialPort = new SerialPort();
        private readonly ILogProvider logger;

        private SerialDataReceivedEventHandler? serialDataReceivedEventHandler;

        public SerialPortHandler(ILogProvider logger)
        {
            this.logger = logger;
        }

        public void IniciarCapturaDadosSerial(string portName)
        {
            const int baudRate = 9600;
            const int dataBits = 8;
            const Parity parity = Parity.None;

            logger.LogInformation("Iniciando captura com configuracao {@ConfiguracaoCapturaPortaSerial}", new 
            { 
                BaudRate = baudRate,
                DataBits = dataBits,
                Parity = parity,
                PortName = portName
            });

            this.serialPort.BaudRate = baudRate;
            this.serialPort.DataBits = dataBits;
            this.serialPort.Parity = parity;
            this.serialPort.PortName = portName;

            this.serialPort.Open();
        }

        public void PararCapturaDadosSerial()
        {
            if (!this.serialPort.IsOpen)
            {
                logger.LogWarning("Tentativa de fechar porta serial que nao esta em status de aberta. Retornando sem fazer nada");

                return;
            }

            logger.LogInformation("Fechando porta serial");
            serialPort.Close();
        }

        public string[] ListarSerialPortsDisponiveis()
        {
            return SerialPort.GetPortNames();
        }

        public void RegisterSerialDataReceivedEventHandler(SerialDataReceivedEventHandler handler)
        {
            logger.LogDebug("Registrando handler para dado recebido na porta serial");
            this.serialPort.DataReceived += handler;
        }

        ~SerialPortHandler()
        {
            logger.LogInformation("Finalizando porta serial");

            this.serialPort.Close();
            this.serialPort.Dispose();
        }
    }
}
