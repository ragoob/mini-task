using ApplicationServices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationServices.Intefaces
{
    public interface IOrganizationService
    {
        void Create(OrganizationModel organization);

        void Update(OrganizationModel organization);

        void Delete(int id);

        OrganizationModel GetById(int id);


        IEnumerable<OrganizationModel> GetOrganizations();
    }
}
