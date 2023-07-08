using FluentValidation;
using InterfaceAquisicaoDadosMotorDc.Core.Model;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAquisicaoDadosMotorDc.Helpers
{
    internal class SerialPortModelValidator : AbstractValidator<SerialPortModel>
    {
        public SerialPortModelValidator()
        {
            RuleFor(x => x.Paridade).IsEnumName(typeof(Parity))
                .WithMessage(x => 
                    string.Format("Nome '{0}' nao e um valor valido para paridade. Valores validos: {1}", 
                        x.Paridade, GetFormatedEnumValues<Parity>()
                    )
                );

            RuleFor(x => x.BaudRate).Must(BeConvertableToInt)
                .WithMessage(x => string.Format("Baud rate '{0}' nao pode ser convertido em inteiro", 
                    x.BaudRate)
                );

            RuleFor(x => x.DataBits).Must(BeConvertableToInt)
                .WithMessage(x => string.Format("Data Bits '{0}' nao pode ser convertido em inteiro", 
                    x.DataBits)
            );

            RuleFor(x => x.PortName)
                .NotNull().WithMessage("Nome da porta serial nao pode ser null")
                .NotEmpty().WithMessage("Nome da porta serial nao pode ser vazio");
        }

        private string GetFormatedEnumValues<T>() where T : struct, Enum
        {
            return string.Join('|', Enum.GetValues<T>());
        }

        private bool BeConvertableToInt(string intMaybe)
        {
            return int.TryParse(intMaybe, out var _);
        }
    }
}
