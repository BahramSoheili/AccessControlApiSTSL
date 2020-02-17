using Core.Commands;
using Core.Storage;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CommandManagement.Domains.RBACs.Commands;
using System.Collections.Generic;

namespace CommandManagement.Domains.RBACs.Handlers
{
    class RoleCommandHandler:
        ICommandHandler<CreateRole>,
        ICommandHandler<UpdateRole>
    {
        private readonly IRepository<Role> roleRepository;
        private readonly IRepository<Operation> operationRepository;

        public RoleCommandHandler(
            IRepository<Role> _roleRepository,
            IRepository<Operation> _operationRepository
        )
        {
            this.roleRepository = _roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            this.operationRepository = _operationRepository ?? throw new ArgumentNullException(nameof(operationRepository));

        }
        public async Task<Unit> Handle(CreateRole request, CancellationToken cancellationToken)
        {
           var role = Role.Create(request.Id, request.Data, request.Operations);
            await roleRepository.Add(role, cancellationToken);
            return Unit.Value;
        }
        public async Task<Unit> Handle(UpdateRole request, CancellationToken cancellationToken)
        {
            var role = await roleRepository.Find(request.Id, cancellationToken);
            role.Update(request.Id, request.Data,  request.Operations);
            await roleRepository.Update(role, cancellationToken);
            return Unit.Value;
        }
    }
}
