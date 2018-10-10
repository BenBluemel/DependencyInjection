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
        public void RegisterLifecycleSingletonTest_Success()
        {
            var container = new Container();
            container.Register<ICalculator, Calculator>(LifecycleType.Singleton);

            var registeredCalculator = container.Registered<ICalculator>();

            Assert.Equal(LifecycleType.Singleton, registeredCalculator.LifecycleType);
        }

        [Fact]
        public void ResolveNoParameter_Success()
        {
            var container = new Container();
            container.Register<ICalculator, Calculator>();

            var registeredCalculator = container.Resolve<ICalculator>();

            Assert.Equal(typeof(Calculator), registeredCalculator.GetType());

        }

        [Fact]
        public void ResolveNoParameterTransientObjects_Success()
        {
            var container = new Container();
            container.Register<ICalculator, Calculator>();

            var registeredCalculator = container.Resolve<ICalculator>();
            var registeredCalculator2 = container.Resolve<ICalculator>();

            Assert.NotEqual(registeredCalculator, registeredCalculator2);
        }

        [Fact]
        public void ResolveNoParameterSingletonObjects_Success()
        {
            var container = new Container();
            container.Register<ICalculator, Calculator>(LifecycleType.Singleton);

            var registeredCalculator = container.Resolve<ICalculator>();
            var registeredCalculator2 = container.Resolve<ICalculator>();

            Assert.Equal(registeredCalculator, registeredCalculator2);
        }

        [Fact]
        public void ResolveOneParameterTransient_Success()
        {
            var container = new Container();
            container.Register<ICalculator, Calculator>();
            container.Register<IMath, Math>();

            var registeredMath = container.Resolve<IMath>();

            Assert.Equal(typeof(Math), registeredMath.GetType());
        }

        [Fact]
        public void ResolveOneParameterSingleton_Success()
        {
            var container = new Container();
            container.Register<ICalculator, Calculator>();
            container.Register<IMath, Math>(LifecycleType.Singleton);

            var registeredMath = container.Resolve<IMath>();
            var registeredMath2 = container.Resolve<IMath>();

            Assert.Equal(registeredMath2, registeredMath);
        }

        [Fact]
        public void ResolveMissingRegister_MissingRegisterException()
        {
            var container = new Container();

            Assert.Throws<MissingRegistryException>(() => container.Resolve<ICalculator2>());
        }

    }
}
