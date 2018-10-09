using DependencyInjection;
using Xunit;

namespace DependencyInjectionTests
{
    public class ContainerTests
    {
        [Fact]
        public void RegisterTypeTest_Sucess()
        {
            var container = new Container();
            container.Register<ICalculator, Calculator>();
            var registeredCalculator = container.Registered<ICalculator>();
            Assert.Equal(typeof(ICalculator), registeredCalculator.RegisteredType);
        }

        [Fact]
        public void RegisterConcreteTypeTest_Sucess()
        {
            var container = new Container();
            container.Register<ICalculator, Calculator>();
            var registeredCalculator = container.Registered<ICalculator>();
            Assert.Equal(typeof(Calculator), registeredCalculator.ConcreteType);
        }

        [Fact]
        public void RegisterDefaultLifecycleTypeTest_Sucess()
        {
            var container = new Container();
            container.Register<ICalculator, Calculator>();
            var registeredCalculator = container.Registered<ICalculator>();
            Assert.Equal(LifecycleType.Transient, registeredCalculator.LifecycleType);
        }

        [Fact]
        public void RegisterLifecycleSignletonTest_Sucess()
        {
            var container = new Container();
            container.Register<ICalculator, Calculator>(LifecycleType.Singleton);
            var registeredCalculator = container.Registered<ICalculator>();
            Assert.Equal(LifecycleType.Singleton, registeredCalculator.LifecycleType);
        }
    }
}
