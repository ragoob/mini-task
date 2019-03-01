using Core.Bus;
using Core.Notifications;
using Domain.Commands.Organizations;
using Domain.Events;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CommandHandlers
{
    public class OrganizationCommandHandler : CommandHandler,
           IRequestHandler<AddNewOrganizationCommand, bool>,
          IRequestHandler<UpdateOrganizationCommand, bool>,
          IRequestHandler<DeleteOrganizationCommand, bool>
    {
        private readonly IRepository<Organization> _organizationRepository;
        private readonly IMediatorHandler _bus;
        public OrganizationCommandHandler(IRepository<Organization> organizationRepository,
                                    IUnitOfWork unitOfWork,
                                    IMediatorHandler bus,
                                    INotificationHandler<DomainNotification> notification) : base(unitOfWork, bus, notification)
        {
            _organizationRepository = organizationRepository;
            _bus = bus;
        }

        public Task<bool> Handle(AddNewOrganizationCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var Organization = new Organization(request.Id, request.Name, request.EmployeesCount);
            _organizationRepository.Add(Organization);
            if (Commit())
            {
                _bus.RaiseEvent(new OrganizationAddedEvent(request.Id, request.Name, request.EmployeesCount));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var Organization = new Organization(request.Id, request.Name, request.EmployeesCount);
            _organizationRepository.Update(Organization);
            if (Commit())
            {
                _bus.RaiseEvent(new OrganizationUpdatedEvent(request.Id, request.Name, request.EmployeesCount));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(DeleteOrganizationCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            _organizationRepository.Remove(request.Id);
            if (Commit())
            {
                _bus.RaiseEvent(new OrganizationDeletedEvent(request.Id));
            }

            return Task.FromResult(true);
        }
    }
}
