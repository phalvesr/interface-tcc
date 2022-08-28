using InterfaceAquisicaoDadosMotorDc.Core.Model.Response;
using InterfaceAquisicaoDadosMotorDc.Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAquisicaoDadosMotorDc.Core.UseCases.Interfaces
{
    internal interface ISaveCsvFileUseCase
    {
        Either<ArquivoNaoSalvo, ArquivoSalvoComSucesso> GerarCsv(
            string path,
            double[] amostrasTensao,
            double[] amostrasCorrente,
            int quantidadeAmostras);
    }
}
