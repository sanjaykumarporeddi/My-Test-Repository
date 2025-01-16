using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UserProfileManagement.BLL.Services;
using UserProfileManagement.DAL.Models;

namespace UserProfileManagement
{
    public partial class UserProfileForm : Form
    {
        private readonly UserProfileService _userProfileService;

        public UserProfileForm(UserProfileService userProfileService)
        {
            InitializeComponent();
            _userProfileService = userProfileService;
            LoadData();
        }

        private void LoadData()
        {
            // Populate branches
            var branches = _userProfileService.GetBranches();
            cboSystemA.DataSource = branches.ToList();
            cboSystemA.DisplayMember = "BranchName";
            cboSystemA.ValueMember = "BranchCode";

            cboSystemB.DataSource = branches.ToList();
            cboSystemB.DisplayMember = "BranchName";
            cboSystemB.ValueMember = "BranchCode";

            cboSystemC.DataSource = branches.ToList();
            cboSystemC.DisplayMember = "BranchName";
            cboSystemC.ValueMember = "BranchCode";

            cboSystemD.DataSource = branches.ToList();
            cboSystemD.DisplayMember = "BranchName";
            cboSystemD.ValueMember = "BranchCode";

            // Permissions checkbox initialization
            chkLN.Checked = false;
            chkBR.Checked = false;
            chkDF.Checked = false;
            chkPR.Checked = false;

            // Fetch and display user profiles in a data grid
            RefreshUserProfiles();
        }

        private void RefreshUserProfiles()
        {
            var userProfiles = _userProfileService.GetUserProfiles();
            // Bind to a data grid if available (e.g., dgvUserProfiles.DataSource = userProfiles).
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var userProfile = new UserProfile
            {
                UserProfileAccount = txtUserId.Text,
                UserProfileName = txtUserName.Text,
                UserProfileMailAddress = txtEmail.Text,
                IsAdmin = chkIsAdmin.Checked,
                UserProfileOperatorId = 1, // Use a placeholder operator ID for now
                UserProfileTimeStamp = DateTime.Now
            };

            _userProfileService.AddUserProfile(userProfile);
            MessageBox.Show("User Profile Saved Successfully!");
            RefreshUserProfiles();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (long.TryParse(txtUserId.Text, out long userProfileId))
            {
                _userProfileService.DeleteUserProfile(userProfileId);
                MessageBox.Show("User Profile Deleted Successfully!");
                RefreshUserProfiles();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var userProfile = new UserProfile
            {
                UserProfileId = long.Parse(txtUserId.Text),
                UserProfileAccount = txtUserId.Text,
                UserProfileName = txtUserName.Text,
                UserProfileMailAddress = txtEmail.Text,
                IsAdmin = chkIsAdmin.Checked,
                UserProfileOperatorId = 1,
                UserProfileTimeStamp = DateTime.Now
            };

            _userProfileService.UpdateUserProfile(userProfile);
            MessageBox.Show("User Profile Updated Successfully!");
            RefreshUserProfiles();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ClearForm()
        {
            txtUserId.Clear();
            txtUserName.Clear();
            txtEmail.Clear();
            chkIsAdmin.Checked = false;
            chkLN.Checked = false;
            chkBR.Checked = false;
            chkDF.Checked = false;
            chkPR.Checked = false;
        }
    }
}
