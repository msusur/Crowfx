namespace Crow.Library.InjectionContainer
{
    internal class DIContainer
    {
        private static readonly IInjectionContainer _injector = new PoorCrowsDependencyContainer();

        public static IInjectionContainer DefaultContainer
        {
            get { return _injector; }
        }
    }
}