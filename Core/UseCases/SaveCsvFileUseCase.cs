using InterfaceAquisicaoDadosMotorDc.Core.Model;
using InterfaceAquisicaoDadosMotorDc.Core.Model.Response;
using InterfaceAquisicaoDadosMotorDc.Core.Types;
using InterfaceAquisicaoDadosMotorDc.Core.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAquisicaoDadosMotorDc.Core.UseCases
{
    internal class SaveCsvFileUseCase : ISaveCsvFileUseCase
    {
        public Either<ArquivoNaoSalvo, ArquivoSalvoComSucesso> GerarCsv(
            string path,
            double[] amostrasTensao, 
            double[] amostrasCorrente, 
            int quantidadeAmostras)
        {
            try
            {
                if (quantidadeAmostras == 0)
                {
                    return Either<ArquivoNaoSalvo, ArquivoSalvoComSucesso>.left(new ArquivoNaoSalvo("Quantidade de amostras zerada!"));
                }

                var filePath = path.EndsWith(".csv") ? path : path + ".csv";

                using var fileStream = new FileStream(filePath, FileMode.CreateNew);
                using var streamWritter = new StreamWriter(fileStream);

                streamWritter.WriteLine("amostra,valor tensao (V),valor corrente (A)");

                var amostrasTensaoCapturada = amostrasTensao.Take(quantidadeAmostras);
                var amostrasCorrenteCapturada = amostrasCorrente.Take(quantidadeAmostras);

                var linhaAmostraHolder = new LinhaAmostraCsv();
                for (var i = 0; i < amostrasTensaoCapturada.Count(); i++)
                {
                    linhaAmostraHolder.AmostraTensao = amostrasTensaoCapturada.ElementAt(i);
                    linhaAmostraHolder.AmostraCorrente = amostrasCorrenteCapturada.ElementAt(i);
                    linhaAmostraHolder.NumeroAmostra = i;

                    streamWritter.WriteLine(linhaAmostraHolder);
                }

                return Either<ArquivoNaoSalvo, ArquivoSalvoComSucesso>.right(new ArquivoSalvoComSucesso());

            }
            catch (Exception e)
            {
                return Either<ArquivoNaoSalvo, ArquivoSalvoComSucesso>.left(new ArquivoNaoSalvo(e.Message));
            }
        }
    }
}
