using DependencyInjection;

namespace WebApplication.IoC
{
    public sealed class DependencyInjector
    {
        public static DependencyInjector Instance { get; } = new DependencyInjector();

        public IContainer Container { get; } = new Container();

        static DependencyInjector()
        {
        }

        private DependencyInjector()
        {
        }
    }
}