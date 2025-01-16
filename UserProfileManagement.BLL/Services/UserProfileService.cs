using System.Collections.Generic;
using UserProfileManagement.DAL.Models;
using UserProfileManagement.DAL.Repositories;

namespace UserProfileManagement.BLL.Services
{
    public class UserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileService(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        public void AddUserProfile(UserProfile userProfile)
        {
            _userProfileRepository.Add(userProfile);
        }

        public void UpdateUserProfile(UserProfile userProfile)
        {
            _userProfileRepository.Update(userProfile);
        }

        public void DeleteUserProfile(long userProfileId)
        {
            _userProfileRepository.Delete(userProfileId);
        }

        public List<UserProfile> GetUserProfiles()
        {
            return _userProfileRepository.GetAll();
        }

        public List<Branch> GetBranches()
        {
            return _userProfileRepository.GetBranches();
        }

        public List<LocalSystem> GetSystems()
        {
            return _userProfileRepository.GetSystems();
        }

        public List<string> GetPermissions()
        {
            return new List<string> { "Read", "Write", "Execute" };
        }
    }
}