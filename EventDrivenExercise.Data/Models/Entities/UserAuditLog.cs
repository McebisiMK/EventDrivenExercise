using System;
using System.Collections.Generic;

namespace EventDrivenExercise.Data.Models.Entities
{
    public partial class UserAuditLog
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string Event { get; set; } = null!;
    }
}
