using ElevateAppDataObjects;
using ElevateAppLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFPresentationLayer
{
    /// <summary>
    /// Ariel Sigo
    /// Created 2016/11/25
    /// 
    /// 
    /// Interaction logic for frmChangePassword.xaml
    /// </summary>
    public partial class frmChangePassword : Window
    {
        Member _member;
        Trainer _trainer;
        UserManager _userManager;

        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/25
        /// 
        /// Allows user to change password
        /// </summary>
        /// <param name="member"></param>
        /// <param name="trainer"></param>
        /// <param name="userManager"></param>
        public frmChangePassword(Member member, Trainer trainer, UserManager userManager)
        
        {
            InitializeComponent();
            _member = member;
            _trainer = trainer;
            _userManager = userManager;
        }
        /// <summary>
        /// Ariel Sigo  
        /// Created 2016/11/25
        /// 
        /// Loads window with change password form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        { // need to make sure you are logged in.
            if (_member == null && _trainer == null)
            {
                MessageBox.Show("Please log in first!");
                this.Close();
            }
        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/25
        /// 
        /// Submits and validates input for changing user's password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var oldPassword = txtOldPassword.Password;
            var newPassword = txtNewPassword.Password;
            var confirmPassword = txtConfirmPassword.Password;

            if (this.txtNewPassword.ToString() == "")

            {
                MessageBox.Show("You need to enter a New Password!"); // make sure new password isn't blank
                txtNewPassword.Focus();
                return;
            }
            if (this.txtOldPassword.ToString() == "")
            {
                MessageBox.Show("You need to enter your Old Password!"); // make sure old password isn't blank
                txtOldPassword.Focus();
                return;
            }

            if (newPassword == oldPassword) // make user sure is choosing a new password
            {
                MessageBox.Show("You can not use the same password!");
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                txtNewPassword.Focus();
                return;
            }
            if (!(newPassword.Equals(confirmPassword))) // make sure user knows what password was chosen
            {
                MessageBox.Show("New Passwords must match!");
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                txtNewPassword.Focus();
                return;
               

            }
            string userType;
            string userID;
            if (_member != null)
	{
		 userType = "member";
         userID = _member.MemberID;

	}
            else
	{
		 userType = "trainer";
         userID = _trainer.TrainerID;
	}
            try
            {
                if (_userManager.UpdatePassword(userID, oldPassword, newPassword, userType))
                {
                    MessageBox.Show("Password has been changed!");
                }
                else
                {
                    MessageBox.Show("Password Change NOT successful");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("There was an error trying to access the data. Contact Administrator \n" + ex.Message);
            }

        }

        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/25
        /// 
        /// Cancels password change without saving changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
             var result = MessageBox.Show("Are you sure you want to quit?", "Abandon Password Change", MessageBoxButton.YesNo, MessageBoxImage.Stop);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
            
        }
    }

}
