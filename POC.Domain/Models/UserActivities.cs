using System.ComponentModel.DataAnnotations;

namespace POCNT.Domain.Models
{
    public class UserActivities
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ActivityType { get; set; }

        [StringLength(50)]
        public string SessionId { get; set; }

        [StringLength(100)]
        public string? OtherNotes { get; set; } = string.Empty;

        public DateTime ActivityTimeStamp { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; }
    }
}
