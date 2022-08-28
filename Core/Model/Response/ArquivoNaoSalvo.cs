namespace InterfaceAquisicaoDadosMotorDc.Core.Model.Response
{
    internal class ArquivoNaoSalvo
    {
        public string Message { get; }

        public ArquivoNaoSalvo(string message)
        {
            this.Message = message;
        }
    }
}
