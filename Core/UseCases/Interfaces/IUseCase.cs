namespace InterfaceAquisicaoDadosMotorDc.Core.UseCases.Interfaces
{
    internal interface IUseCase <T>
    {
        T Execute();
    }

    internal interface IVoidUseCase<TParam>
    {
        void Execute(TParam param);
    }

    internal interface IUseCase
    {
        void Execute();
    }

}
