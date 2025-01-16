using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using UserProfileManagement.DAL.Models;

namespace UserProfileManagement.DAL.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile userProfile);
        void Update(UserProfile userProfile);
        void Delete(long userProfileId);
        List<UserProfile> GetAll();
        List<Branch> GetBranches();
        List<LocalSystem> GetSystems();
    }
}
