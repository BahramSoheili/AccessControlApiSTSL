using Core.Queries;
using Marten;
using System;
using System.Threading;
using System.Threading.Tasks;
using CommandManagement.Domains.AccessControl.Views;
using CommandManagement.Domains.AccessControl.Queries;

namespace CommandManagement.Domains.AccessControl.Handlers
{
    public class DeviceTypeQueryHandler: IQueryHandler<GetDeviceType, DeviceTypeView>
    {
        private readonly IDocumentSession session;
        public DeviceTypeQueryHandler(
            IDocumentSession session
        )
        {
            this.session = session ?? throw new ArgumentNullException(nameof(session));
        }

        public Task<DeviceTypeView> Handle(GetDeviceType request, CancellationToken cancellationToken)
        {
            return session.LoadAsync<DeviceTypeView>(request.Id, cancellationToken);
        }
    }
}

