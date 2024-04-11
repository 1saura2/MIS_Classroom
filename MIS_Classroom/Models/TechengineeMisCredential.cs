using System;
using System.Collections.Generic;

namespace MIS_Classroom.Models
{
    public partial class TechengineeMisCredential
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string? Password { get; set; }
        public int? TypeId { get; set; }

        public virtual TechengineeMisUserType? Type { get; set; }
    }
}
