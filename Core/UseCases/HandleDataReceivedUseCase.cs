using InterfaceAquisicaoDadosMotorDc.Core.Abstractions;
using InterfaceAquisicaoDadosMotorDc.Core.UseCases.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Ports;

namespace InterfaceAquisicaoDadosMotorDc.Core.UseCases
{
    internal class HandleDataReceivedUseCase : ISerialDataReceivedHandler
    {

        public int Voltage { get; private set; }
        public int Current { get; private set; }

        private readonly ISerialPortHandler serialPortHandler;
        public event EventHandler SerialDataParsed;

        public HandleDataReceivedUseCase(IServiceProvider serviceProvider)
        {
            serialPortHandler = serviceProvider.GetRequiredService<ISerialPortHandler>();
        }

        public void Execute()
        {
            this.serialPortHandler.RegisterSerialDataReceivedEventHandler(this.handler);
        }

        private void handler(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                var serialPort = sender as SerialPort;
                var linha = serialPort!.ReadLine();

                var (tensaoDiscretizada, correnteDiscretizada) = ParseLinhaRecebida(linha.Trim());

                this.Voltage = tensaoDiscretizada;
                this.Current = correnteDiscretizada;

                SerialDataParsed.Invoke(this, null!);
            }
            // Deveria ocorrer somente quando a porta for fechada e o buffer serial retornar um valor que,
            // ao ser parseado gera exception
            catch (SystemException) {  }
        }

        private (int tensaoDiscretizada, int correnteDiscretizada) ParseLinhaRecebida(string linha)
        {
            try
            {
                var dados = linha.Split(';');

                var tensao = int.Parse(dados.ElementAt(0).Split(':').ElementAt(1));
                var corrente = int.Parse(dados.ElementAt(1).Split(':').ElementAt(1));

                return (tensao, corrente);
            }
            catch (Exception)
            {
                return (0, 0);
            }
        }
    }
}
