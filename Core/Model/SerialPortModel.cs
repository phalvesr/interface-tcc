using System.IO.Ports;

namespace InterfaceAquisicaoDadosMotorDc.Core.Model
{
    public sealed class SerialPortModel
    {
        public string PortName { get; set; } = default!;
        public string BaudRate { get; set; } = default!;
        public string DataBits { get; set; } = default!;
        public string Paridade { get; set; } = default!;
    }
}
