using System.IO.Ports;

namespace InterfaceAquisicaoDadosMotorDc.Core.Abstractions
{
    public interface ISerialPortHandler
    {
        string[] ListarSerialPortsDisponiveis();
        void RegisterSerialDataReceivedEventHandler(SerialDataReceivedEventHandler handler);
        void IniciarCapturaDadosSerial(string portName);
        void PararCapturaDadosSerial();
    }
}
