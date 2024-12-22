using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class UserMessage
    {
        public int MessageId { get; set; }
        public int? UserId { get; set; }
        public DateTime? MessageDate { get; set; }
        public string? MessageText { get; set; }

        public virtual User? User { get; set; }
    }
}
