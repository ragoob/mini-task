using ApplicationServices.Intefaces;
using ApplicationServices.Models;
using Core.Bus;
using Domain.Commands.Organizations;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationServices.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IRepository<Organization> _organizationRepository;
        private readonly IMediatorHandler _bus;

        public OrganizationService(IRepository<Organization> organizationrepository, IMediatorHandler bus)
        {
            _organizationRepository = organizationrepository;
            _bus = bus;
        }
        public void Create(OrganizationModel organization)
        {
            var addNewOrganizationCommand = new AddNewOrganizationCommand(organization.Name, organization.EmployeesCount);
            _bus.SendCommand(addNewOrganizationCommand);
        }

        public void Delete(int id)
        {
            var deleteOrganizationCommand = new DeleteOrganizationCommand(id);
            _bus.SendCommand(deleteOrganizationCommand);
        }

        public OrganizationModel GetById(int id)
        {
            var entity = _organizationRepository.GetById(id);
           return  entity != null ?    new OrganizationModel()
            {
               Id = id,
               EmployeesCount = entity.EmployeesCount,
               Name = entity.Name
            } : null;
        }

        public IEnumerable<OrganizationModel> GetOrganizations()
        {
            return _organizationRepository.GetAll()
                .Select(c => new OrganizationModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    EmployeesCount = c.EmployeesCount

                });
        }

        public void Update(OrganizationModel organization)
        {
            var updateOrganizationCommand = new UpdateOrganizationCommand(organization.Id, organization.Name, organization.EmployeesCount);
            _bus.SendCommand(updateOrganizationCommand);
        }
    }
}
