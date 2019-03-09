using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationServices.Models
{
   public class ProjectModel
    {
        public int Id { get; set; }

        /// <summary>
        /// getter or setter project Name
        /// </summary>
        public string Name { get;  set; }

        /// <summary>
        /// getter or setter Project Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// getter or setter Project Pricvicy 
        /// </summary>
        public bool IsPrivate { get;  set; }

        public int OrganizationId { get; set; }

    }
}
