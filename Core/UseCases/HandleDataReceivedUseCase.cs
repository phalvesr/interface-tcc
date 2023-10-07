using InterfaceAquisicaoDadosMotorDc.Core.Abstractions;
using InterfaceAquisicaoDadosMotorDc.Core.Abstractions.Providers;
using InterfaceAquisicaoDadosMotorDc.Core.UseCases.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Ports;

namespace InterfaceAquisicaoDadosMotorDc.Core.UseCases
{
    public class HandleDataReceivedUseCase : ISerialDataReceivedHandler
    {

        public int Voltage { get; private set; }
        public int Current { get; private set; }

        private readonly ISerialPortHandler serialPortHandler;
        private readonly ILogProvider logger;

        public event EventHandler SerialDataParsed;

        public HandleDataReceivedUseCase(IServiceProvider serviceProvider)
        {
            serialPortHandler = serviceProvider.GetRequiredService<ISerialPortHandler>();
            logger = serviceProvider.GetRequiredService<ILogProvider>();
        }

        public void Execute()
        {
            this.serialPortHandler.RegisterSerialDataReceivedEventHandler(this.Handler);
        }

        private void Handler(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                var serialPort = sender as SerialPort;
                var linha = serialPort!.ReadLine();
                
                var linhaLimpa = linha.Trim();

                logger.LogDebug("Tentando processar linha recebida com conteudo {ConteudoLinha}", linhaLimpa);

                var (tensaoDiscretizada, correnteDiscretizada) = ParseLinhaRecebida(linhaLimpa);

                this.Voltage = tensaoDiscretizada;
                this.Current = correnteDiscretizada;

                SerialDataParsed.Invoke(this, null!);
            }
            // Deveria ocorrer somente quando a porta for fechada e o buffer serial retornar um valor que,
            // ao ser parseado gera exception
            catch (Exception ex) 
            {
                logger.LogWarning("[{ExceptionType}] Exception lancada ao tentar parsear informacao recebida", ex.GetType());
                logger.LogError(ex, "[{ExceptionType}] Dados da exception", ex.GetType());
            }
        }

        internal (int tensaoDiscretizada, int correnteDiscretizada) ParseLinhaRecebida(string linha)
        {
            try
            {
                var dados = linha.Split(';');

                var tensao = int.Parse(dados.ElementAt(0).Split(':').ElementAt(1));
                var corrente = int.Parse(dados.ElementAt(1).Split(':').ElementAt(1));

                return (tensao, corrente);
            }
            catch (Exception ex)
            {
                logger.LogWarning("[{ExceptionType}] Exception lancada ao tentar parsear informacao recebida", ex.GetType());
                logger.LogError(ex, "[{ExceptionType}] Dados da exception. Retornando valor default de (0, 0)", ex.GetType());
                return (0, 0);
            }
        }
    }
}
