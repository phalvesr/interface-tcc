namespace InterfaceAquisicaoDadosMotorDc.Helpers
{
    internal class ConversorDiscretoFisico
    {
        internal static double ConverterParaTensao(int valorDiscreto)
        {
            const int QUANTIDADE_VALORES_POSSIVEIS_10_BITS = 1023;
            const double TENSAO_ADC = 5.0;

            // Como utilizei um divisor de tensao para baixar a tensao lida no adc em 1/3 tenho que fazer a seguinte transformação
            const double FATOR_REBAIXADOR_TENSAO = 3.0;

            double tensaoLida = ((double)valorDiscreto / QUANTIDADE_VALORES_POSSIVEIS_10_BITS) * TENSAO_ADC;

            double tensaoMotor = tensaoLida * FATOR_REBAIXADOR_TENSAO;

            return tensaoMotor;
        }

        internal static double ConverterParaCorrente(int valorDiscreto)
        {
            const int QUANTIDADE_VALORES_POSSIVEIS_10_BITS = 1023;
            const double TENSAO_ADC = 5.0;
            const double VOLTS_POR_AMPERE = 66e-3;

            double tensaoLida = ((double)valorDiscreto / QUANTIDADE_VALORES_POSSIVEIS_10_BITS) * TENSAO_ADC;

            double corrente = (tensaoLida - 2.5) / VOLTS_POR_AMPERE;

            return corrente;
        }
    }
}
