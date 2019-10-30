using Autofac;
using NServiceBus;

namespace Leviathan.Sync
{
    public class NsbModule : Module
    {
        private readonly string _endpointName;

        public NsbModule(string endpointName)
        {
            _endpointName = endpointName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EndpointConfiguration>().WithParameter(new TypedParameter(typeof(string), _endpointName)).OnActivating(e =>
            {
                e.Instance.UseSerialization<XmlSerializer>();

                e.Instance.UseContainer<AutofacBuilder>(c => c.ExistingLifetimeScope(e.Context.Resolve<ILifetimeScope>()));

                e.Instance.UseTransport<LearningTransport>();

                e.Instance.UsePersistence<InMemoryPersistence>();
            });
        }

    }
}
