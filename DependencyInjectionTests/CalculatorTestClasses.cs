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

    public class SuperMath : IMath
    {
        public ICalculator Calcuator { get; set; }
        public ICalculator2 Calcuator2 { get; set; }

        public SuperMath(ICalculator calculator)
        {
            Calcuator = calculator;
        }

        public SuperMath(ICalculator calculator, ICalculator2 calculator2)
        {
            Calcuator = calculator;
            Calcuator2 = calculator2;
        }
    }

}
