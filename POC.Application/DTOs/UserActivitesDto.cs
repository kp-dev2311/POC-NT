using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCNT.Application.DTOs
{
    public class UserActivitesDto
    {
        public int Id { get; set; }
        public string? ActivityType { get; set; }
        public string? OtherNotes { get; set; }
        public string? SessionId { get; set; }
        public DateTime ActivityTimeStamp { get; set; }
        public int UserId { get; set; }
    }

    public class ActivityReqDto
    {
        public int NoOfActivity { get; set; }
        public int intervelOfActivity { get; set; }
        public int UserId { get; set; }
    }

    public class ActivityReqFilterDto
    {
        public int NoOfActivity { get; set; }
        public int intervelOfActivity { get; set; }
    }


    public class UserActivitesResponseDto
    {
        public int Id { get; set; }
        public string? ActivityType { get; set; }
        public string? UserName { get; set; }
        public DateTime ActivityTimeStamp { get; set; }
    }

}
