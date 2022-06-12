using System;
using System.Collections.Generic;

namespace EventDrivenExercise.Data.Models.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string IdNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
