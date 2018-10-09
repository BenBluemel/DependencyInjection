using DependencyInjection;
using Xunit;

namespace DependencyInjectionTests
{
    public class ContainerTests
    {
        [Fact]
        public void RegisterTypeTest_Success()
        {
            var container = new Container();

            container.Register<ICalculator, Calculator>();
            var registeredCalculator = container.Registered<ICalculator>();

            Assert.Equal(typeof(ICalculator), registeredCalculator.RegisteredType);
        }

        [Fact]
        public void RegisterConcreteTypeTest_Success()
        {
            var container = new Container();

            container.Register<ICalculator, Calculator>();
            var registeredCalculator = container.Registered<ICalculator>();

            Assert.Equal(typeof(Calculator), registeredCalculator.ConcreteType);
        }

        [Fact]
        public void RegisterDefaultLifecycleTypeTest_Success()
        {
            var container = new Container();

            container.Register<ICalculator, Calculator>();
            var registeredCalculator = container.Registered<ICalculator>();

            Assert.Equal(LifecycleType.Transient, registeredCalculator.LifecycleType);
        }

        [Fact]
        public void RegisterLifecycleSignletonTest_Success()
        {
            var container = new Container();
            container.Register<ICalculator, Calculator>(LifecycleType.Singleton);

            var registeredCalculator = container.Registered<ICalculator>();

            Assert.Equal(LifecycleType.Singleton, registeredCalculator.LifecycleType);
        }

        [Fact]
        public void Resolve_NoParameter_Success()
        {
            var container = new Container();
            container.Register<ICalculator, Calculator>();

            var registeredCalculator = container.Resolve<ICalculator>();

            Assert.Equal(typeof(Calculator), registeredCalculator.GetType());

        }

        [Fact]
        public void Resolve_NoParameter_DifferentObjects_Success()
        {
            var container = new Container();
            container.Register<ICalculator, Calculator>();

            var registeredCalculator = container.Resolve<ICalculator>();
            var registeredCalculator2 = container.Resolve<ICalculator>();

            Assert.NotEqual(registeredCalculator, registeredCalculator2);
        }

        [Fact]
        public void Resolve_MissingRegister_MissingRegisterException()
        {
            var container = new Container();

            Assert.Throws<MissingRegisterException>(() => container.Resolve<ICalculator2>());
        }

    }
}
