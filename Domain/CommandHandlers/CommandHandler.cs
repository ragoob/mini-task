using Core.Bus;
using Core.Commands;
using Core.Notifications;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.CommandHandlers
{
   public class CommandHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;
        public CommandHandler(IUnitOfWork unitOfWork, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications)
        {
            _unitOfWork = unitOfWork;
            _bus = bus;
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected void NotifyValidationErrors(Command message)
        {
            foreach (var error in message.ValidationResult.Errors)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, error.ErrorMessage));
            }
        }

        public bool Commit()
        {
            if (_notifications.HasNotifications()) return false;
            if (_unitOfWork.Commit()) return true;

            _bus.RaiseEvent(new DomainNotification("Save changes", "error in during save changes"));
            return false;
        }
    }
}
