using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserProfileManagement.DAL.Models
{
    public class UserProfile
    {
        public long UserProfileId { get; set; }
        public string UserProfileAccount { get; set; }
        public string UserProfileDomainName { get; set; }
        public string UserProfileName { get; set; }
        public string UserProfileMailAddress { get; set; }
        public bool IsAdmin { get; set; }
        public long UserProfileOperatorId { get; set; }
        public DateTime UserProfileTimeStamp { get; set; }
    }
}
