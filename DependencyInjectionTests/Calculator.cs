namespace DependencyInjectionTests
{
    public interface ICalculator
    {
    }

    public interface ICalculator2
    {
    }

    public interface IMath
    {
    }

    public class Calculator : ICalculator
    {
    }

    public class Math : IMath
    {
        public Math(ICalculator calculator)
        {
        }
    }
}
