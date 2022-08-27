namespace InterfaceAquisicaoDadosMotorDc.Helpers
{
    internal class ConversorDiscretoFisico
    {
        internal static double ConverterParaTensao(int valorDiscreto)
        {
            const int QUANTIDADE_VALORES_POSSIVEIS_10_BITS = 1023;
            const double TENSAO_ADC = 5.0;

            // TODO: apagar:
            var a = ((double)valorDiscreto / QUANTIDADE_VALORES_POSSIVEIS_10_BITS) * TENSAO_ADC;

            return ((double)valorDiscreto / QUANTIDADE_VALORES_POSSIVEIS_10_BITS) * TENSAO_ADC;
        }

        internal static double ConverterParaCorrente(int valorDiscreto)
        {
            const int QUANTIDADE_VALORES_POSSIVEIS_10_BITS = 1023;
            const double TENSAO_ADC = 5.0;

            var b = ((double)valorDiscreto / QUANTIDADE_VALORES_POSSIVEIS_10_BITS) * TENSAO_ADC;

            return ((double)valorDiscreto / QUANTIDADE_VALORES_POSSIVEIS_10_BITS) * TENSAO_ADC;
        }
    }
}
