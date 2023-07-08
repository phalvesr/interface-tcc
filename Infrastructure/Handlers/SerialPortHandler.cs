using InterfaceAquisicaoDadosMotorDc.Core.Abstractions;
using InterfaceAquisicaoDadosMotorDc.Core.Abstractions.Providers;
using InterfaceAquisicaoDadosMotorDc.Core.Model;
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

        public void IniciarCapturaDadosSerial(SerialPortModel serialPortModel)
        {
            logger.LogInformation("Iniciando captura com configuracao {@ConfiguracaoCapturaPortaSerial}", new 
            {
                serialPortModel.BaudRate,
                serialPortModel.DataBits,
                serialPortModel.Paridade,
                serialPortModel.PortName
            });

            this.serialPort.BaudRate = int.Parse(serialPortModel.BaudRate);
            this.serialPort.DataBits = int.Parse(serialPortModel.DataBits);
            this.serialPort.Parity = Enum.Parse<Parity>(serialPortModel.Paridade);
            this.serialPort.PortName = serialPortModel.PortName;

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

        public void Dispose()
        {
            logger.LogInformation("Finalizando porta serial");

            this.serialPort.Close();
            this.serialPort.Dispose();
        }
    }
}
