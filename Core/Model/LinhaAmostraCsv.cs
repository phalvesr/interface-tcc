namespace InterfaceAquisicaoDadosMotorDc.Core.Model
{
    internal class LinhaAmostraCsv
    {
        public double AmostraTensao { get; set; }
        public double AmostraCorrente { get; set; }
        public int NumeroAmostra { get; set; }


        override public string ToString()
        {
            return string.Format("{0},{1},{2}", NumeroAmostra, AmostraTensao, AmostraCorrente);
        }
    }
}
