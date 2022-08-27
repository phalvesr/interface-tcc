using InterfaceAquisicaoDadosMotorDc.Core.Abstractions;
using System.IO.Ports;

namespace InterfaceAquisicaoDadosMotorDc.Infrastructure.Handlers
{
    internal class SerialPortHandler : ISerialPortHandler
    {
        private readonly SerialPort serialPort = new SerialPort();
        private SerialDataReceivedEventHandler? serialDataReceivedEventHandler;

        public void IniciarCapturaDadosSerial(string portName)
        {
            // TODO: Passar o nome da porta como argumento
            this.serialPort.BaudRate = 9600;
            this.serialPort.DataBits = 8;
            this.serialPort.Parity = Parity.None;
            this.serialPort.PortName = portName;

            this.serialPort.Open();
        }

        public void PararCapturaDadosSerial()
        {
            if (!this.serialPort.IsOpen)
            {
                return;
            }

            serialPort.Close();
        }

        public string[] ListarSerialPortsDisponiveis()
        {
            return SerialPort.GetPortNames();
        }

        public void RegisterSerialDataReceivedEventHandler(SerialDataReceivedEventHandler handler)
        {
            this.serialPort.DataReceived += handler;
        }

        ~SerialPortHandler()
        {
            this.serialPort.Dispose();
        }
    }
}
