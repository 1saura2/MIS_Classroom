using System;
using System.Collections.Generic;

namespace MIS_Classroom.Models
{
    public partial class TechengineeMisUserType
    {
        public TechengineeMisUserType()
        {
            TechengineeMisCredentials = new HashSet<TechengineeMisCredential>();
        }

        public int TypeId { get; set; }
        public string? UserType { get; set; }

        public virtual ICollection<TechengineeMisCredential> TechengineeMisCredentials { get; set; }
    }
}
