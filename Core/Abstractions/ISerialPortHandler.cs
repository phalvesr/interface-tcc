using InterfaceAquisicaoDadosMotorDc.Core.Model;
using System.IO.Ports;

namespace InterfaceAquisicaoDadosMotorDc.Core.Abstractions
{
    public interface ISerialPortHandler : IDisposable
    {
        string[] ListarSerialPortsDisponiveis();
        void RegisterSerialDataReceivedEventHandler(SerialDataReceivedEventHandler handler);
        void IniciarCapturaDadosSerial(SerialPortModel serialPortModel);
        void PararCapturaDadosSerial();
    }
}
