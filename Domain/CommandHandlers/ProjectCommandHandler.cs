using Core.Bus;
using Core.Notifications;
using Domain.Commands.Project;
using Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Domain.Models;
using Domain.Events;
namespace Domain.CommandHandlers
{
    public class ProjectCommandHandler : CommandHandler,
          IRequestHandler<AddNewProjectCommand, bool>,
         IRequestHandler<UpdateProjectCommand, bool>,
         IRequestHandler<DeleteProjectCommand, bool>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMediatorHandler _bus;

        public ProjectCommandHandler(IProjectRepository projectRepository,
                                      IUnitOfWork unitOfWork,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notification) : base(unitOfWork, bus, notification)
        {
            _projectRepository = projectRepository;
            _bus = bus;
        }
       
        public Task<bool> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var project = new Project(request.Id, request.Name, request.Description, request.IsPrivate);
            
            _projectRepository.Update(project);
            if (Commit())
            {
                _bus.RaiseEvent(new UpdateProjectEvent(request.Id, request.Name, request.Description, request.IsPrivate));
                
            }

           
            return Task.FromResult(true);
        }

        public Task<bool> Handle(AddNewProjectCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var project = new Project(default(int), request.Name, request.Description, request.IsPrivate);
           
            _projectRepository.Add(project);
            if (Commit())
            {
                _bus.RaiseEvent(new AddNewProjectEvent(request.AggregateId, request.Name, request.Description, request.IsPrivate));

            }
           

            return Task.FromResult(true);
        }

        public Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }           
            _projectRepository.Remove(request.Id);
            if (Commit())
            {
                _bus.RaiseEvent(new DeleteProjectEvent(request.Id, request.Name, request.Description, request.IsPrivate));

            }

            
            return Task.FromResult(true);
        }
    }
}
