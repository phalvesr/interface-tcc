using System.IO.Ports;

namespace InterfaceAquisicaoDadosMotorDc.Core.Abstractions
{
    internal interface ISerialPortHandler
    {
        string[] ListarSerialPortsDisponiveis();
        void RegisterSerialDataReceivedEventHandler(SerialDataReceivedEventHandler handler);
        void IniciarCapturaDadosSerial();
    }
}
