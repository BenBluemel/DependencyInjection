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

    public class Calculator2 : ICalculator2
    {
    }

    public class SuperCalculator : Calculator
    {
    }

    public class Math : IMath
    {
        public Math(ICalculator calculator)
        {
        }
    }

    public interface ISuperMath
    {
        ICalculator Calcuator { get; set; }
        ICalculator2 Calcuator2 { get; set; }

    }

    public class SuperMath : ISuperMath
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
