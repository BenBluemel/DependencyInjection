using DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace DependencyInjectionTests
{
    public class ContainerTests
    {
        [Fact]
        public void RegisterTypeTest_Success()
        {
            IContainer container = new Container();

            container.Register<ICalculator, Calculator>();
            var registeredCalculator = container.Registered<ICalculator>();

            Assert.Equal(typeof(ICalculator), registeredCalculator.RegisteredType);
        }

        [Fact]
        public void RegisterConcreteTypeTest_Success()
        {
            IContainer container = new Container();

            container.Register<ICalculator, Calculator>();
            var registeredCalculator = container.Registered<ICalculator>();

            Assert.Equal(typeof(Calculator), registeredCalculator.ConcreteType);
        }

        [Fact]
        public void RegisterDefaultLifecycleTypeTest_Success()
        {
            IContainer container = new Container();

            container.Register<ICalculator, Calculator>();
            var registeredCalculator = container.Registered<ICalculator>();

            Assert.Equal(LifecycleType.Transient, registeredCalculator.LifecycleType);
        }

        [Fact]
        public void RegisterLifecycleSingletonTest_Success()
        {
            IContainer container = new Container();
            container.Register<ICalculator, Calculator>(LifecycleType.Singleton);

            var registeredCalculator = container.Registered<ICalculator>();

            Assert.Equal(LifecycleType.Singleton, registeredCalculator.LifecycleType);
        }

        [Fact]
        public void ResolveNoParameter_Success()
        {
            IContainer container = new Container();
            container.Register<ICalculator, Calculator>();

            var registeredCalculator = container.Resolve<ICalculator>();

            Assert.Equal(typeof(Calculator), registeredCalculator.GetType());

        }

        [Fact]
        public void ResolveGenericNoParameter_Success()
        {
            IContainer container = new Container();
            container.Register<IList<string>, List<string>>();

            var registeredCalculator = container.Resolve<IList<string>>();

            Assert.Equal(typeof(List<string>), registeredCalculator.GetType());

        }

        [Fact]
        public void ResolveNoParameterTransientObjects_Success()
        {
            IContainer container = new Container();
            container.Register<ICalculator, Calculator>();

            var registeredCalculator = container.Resolve<ICalculator>();
            var registeredCalculator2 = container.Resolve<ICalculator>();

            Assert.NotEqual(registeredCalculator, registeredCalculator2);
        }

        [Fact]
        public void ResolveNoParameterSingletonObjects_Success()
        {
            IContainer container = new Container();
            container.Register<ICalculator, Calculator>(LifecycleType.Singleton);

            var registeredCalculator = container.Resolve<ICalculator>();
            var registeredCalculator2 = container.Resolve<ICalculator>();

            Assert.Equal(registeredCalculator, registeredCalculator2);
        }

        [Fact]
        public void ResolveOneParameterTransient_Success()
        {
            IContainer container = new Container();
            container.Register<ICalculator, Calculator>();
            container.Register<IMath, Math>();

            var registeredMath = container.Resolve<IMath>();

            Assert.Equal(typeof(Math), registeredMath.GetType());
        }

        [Fact]
        public void ResolveOneParameterSingleton_Success()
        {
            IContainer container = new Container();
            container.Register<ICalculator, Calculator>();
            container.Register<IMath, Math>(LifecycleType.Singleton);

            var registeredMath = container.Resolve<IMath>();
            var registeredMath2 = container.Resolve<IMath>();

            Assert.Equal(registeredMath2, registeredMath);
        }

        [Fact]
        public void ResolveMultipleContructorsAllParametersRegistered_Success()
        {
            IContainer container = new Container();
            container.Register<ICalculator, Calculator>();
            container.Register<ICalculator2, Calculator2>();
            container.Register<ISuperMath, SuperMath>();

            var registeredMath = container.Resolve<ISuperMath>();

            Assert.NotNull(registeredMath.Calcuator2);
        }

        [Fact]
        public void RegisterBaseClass_Success()
        {
            IContainer container = new Container();
            container.Register<Calculator, SuperCalculator>();

            Calculator calculator = container.Resolve<Calculator>();

            Assert.Equal(typeof(SuperCalculator), calculator.GetType());
        }

        [Fact]
        public void RegisterUnassignableTypes_InvalidCastException()
        {
            IContainer container = new Container();
            Assert.Throws<InvalidCastException>(() => container.Register<ICalculator2, Calculator>());
        }


        [Fact]
        public void ResolveMissingRegister_MissingRegisterException()
        {
            var container = new Container();

            Assert.Throws<MissingRegistryException>(() => container.Resolve<ICalculator2>());
        }
    }
}
