namespace InterfaceAquisicaoDadosMotorDc.Helpers
{
    public class ConversorDiscretoFisico
    {
        public static double ConverterParaTensao(int valorDiscreto)
        {
            const int QUANTIDADE_VALORES_POSSIVEIS_10_BITS = 1023;
            const double TENSAO_ADC = 5.0;

            // Como utilizei um divisor de tensao para baixar a tensao lida no adc em 1/3 tenho que fazer a seguinte transformação
            const double FATOR_REBAIXADOR_TENSAO = 3.0;

            double tensaoLida = ((double)valorDiscreto / QUANTIDADE_VALORES_POSSIVEIS_10_BITS) * TENSAO_ADC;

            double tensaoMotor = tensaoLida * FATOR_REBAIXADOR_TENSAO;

            return tensaoMotor;
        }

        public static double ConverterParaCorrente(int valorDiscreto)
        {
            const double OFFSET_SENSOR_CORRENTE = 2.5;

            const int QUANTIDADE_VALORES_POSSIVEIS_10_BITS = 1023;
            const double TENSAO_ADC = 5.0;

            const double VOLTS_POR_AMPERE = 185e-3;

            double tensaoLida = ((double)valorDiscreto / QUANTIDADE_VALORES_POSSIVEIS_10_BITS) * TENSAO_ADC;

            double corrente = (OFFSET_SENSOR_CORRENTE - tensaoLida) / VOLTS_POR_AMPERE;

            return Math.Abs(corrente);
        }
    }
}
