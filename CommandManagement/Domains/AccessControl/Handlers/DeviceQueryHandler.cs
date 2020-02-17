using Core.Queries;
using CommandManagement.Domains.Devices.Queries;
using Marten;
using System;
using System.Threading;
using System.Threading.Tasks;
using CommandManagement.Domains.AccessControl.View;

namespace CommandManagement.Domains.AccessControl.Handlers
{
    public class DeviceQueryHandler: IQueryHandler<GetDevice, DeviceView>
    {
        private readonly IDocumentSession session;

        public DeviceQueryHandler(
            IDocumentSession session
        )
        {
            this.session = session ?? throw new ArgumentNullException(nameof(session));
        }

        public Task<DeviceView> Handle(GetDevice request, CancellationToken cancellationToken)
        {
            return session.LoadAsync<DeviceView>(request.Id, cancellationToken);
        }
    }
}
