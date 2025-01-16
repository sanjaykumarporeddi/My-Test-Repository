using System;
using System.Collections.Generic;
using System.Data.OleDb;
using UserProfileManagement.DAL.Models;

namespace UserProfileManagement.DAL.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly string _connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=your_database_path.mdb";

        public void Add(UserProfile userProfile)
        {
            using (OleDbConnection con = new OleDbConnection(_connectionString))
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("INSERT INTO UserProfile (UserProfileAccount, UserProfileDomainName, UserProfileName, UserProfileMailAddress, UserProfileUserLevelToUserAdmin, UserProfileStatus, UserProfileOperatorId, UserProfileTimeStamp) VALUES (@Account, @DomainName, @Name, @MailAddress, @IsAdmin, 0, @OperatorId, @TimeStamp)", con);
                cmd.Parameters.AddWithValue("@Account", userProfile.UserProfileAccount);
                cmd.Parameters.AddWithValue("@DomainName", userProfile.UserProfileDomainName);
                cmd.Parameters.AddWithValue("@Name", userProfile.UserProfileName);
                cmd.Parameters.AddWithValue("@MailAddress", userProfile.UserProfileMailAddress);
                cmd.Parameters.AddWithValue("@IsAdmin", userProfile.IsAdmin ? "Y" : "N");
                cmd.Parameters.AddWithValue("@OperatorId", userProfile.UserProfileOperatorId); // This should be set by the application logic
                cmd.Parameters.AddWithValue("@TimeStamp", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(UserProfile userProfile)
        {
            using (OleDbConnection con = new OleDbConnection(_connectionString))
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("UPDATE UserProfile SET UserProfileAccount = @Account, UserProfileDomainName = @DomainName, UserProfileName = @Name, UserProfileMailAddress = @MailAddress, UserProfileUserLevelToUserAdmin = @IsAdmin WHERE UserProfileId = @Id", con);
                cmd.Parameters.AddWithValue("@Account", userProfile.UserProfileAccount);
                cmd.Parameters.AddWithValue("@DomainName", userProfile.UserProfileDomainName);
                cmd.Parameters.AddWithValue("@Name", userProfile.UserProfileName);
                cmd.Parameters.AddWithValue("@MailAddress", userProfile.UserProfileMailAddress);
                cmd.Parameters.AddWithValue("@IsAdmin", userProfile.IsAdmin ? "Y" : "N");
                cmd.Parameters.AddWithValue("@Id", userProfile.UserProfileId);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(long userProfileId)
        {
            using (OleDbConnection con = new OleDbConnection(_connectionString))
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("UPDATE UserProfile SET UserProfileStatus = -1 WHERE UserProfileId = @Id", con);
                cmd.Parameters.AddWithValue("@Id", userProfileId);
                cmd.ExecuteNonQuery();
            }
        }

        public List<UserProfile> GetAll()
        {
            List<UserProfile> userProfiles = new List<UserProfile>();

            using (OleDbConnection con = new OleDbConnection(_connectionString))
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM UserProfile WHERE UserProfileStatus = 0", con);
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    UserProfile userProfile = new UserProfile
                    {
                        UserProfileId = (long)reader["UserProfileId"],
                        UserProfileAccount = reader["UserProfileAccount"].ToString(),
                        UserProfileDomainName = reader["UserProfileDomainName"].ToString(),
                        UserProfileName = reader["UserProfileName"].ToString(),
                        UserProfileMailAddress = reader["UserProfileMailAddress"].ToString(),
                        IsAdmin = reader["UserProfileUserLevelToUserAdmin"].ToString() == "Y",
                        UserProfileOperatorId = (long)reader["UserProfileOperatorId"],
                        UserProfileTimeStamp = (DateTime)reader["UserProfileTimeStamp"]
                    };
                    userProfiles.Add(userProfile);
                }
            }

            return userProfiles;
        }

        public List<Branch> GetBranches()
        {
            List<Branch> branches = new List<Branch>();

            using (OleDbConnection con = new OleDbConnection(_connectionString))
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT BranchCode, BranchName FROM Branch", con);
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Branch branch = new Branch
                    {
                        BranchCode = reader["BranchCode"].ToString(),
                        BranchName = reader["BranchName"].ToString()
                    };
                    branches.Add(branch);
                }
            }

            return branches;
        }

        public List<LocalSystem> GetSystems()
        {
            List<LocalSystem> systems = new List<LocalSystem>();

            using (OleDbConnection con = new OleDbConnection(_connectionString))
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT LocalSystemId, LocalSystemName FROM LocalSystem", con);
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    LocalSystem system = new LocalSystem
                    {
                        LocalSystemId = (long)reader["LocalSystemId"],
                        LocalSystemName = reader["LocalSystemName"].ToString()
                    };
                    systems.Add(system);
                }
            }

            return systems;
        }
    }
}