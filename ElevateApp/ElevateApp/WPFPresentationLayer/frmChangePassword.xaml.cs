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
    /// Interaction logic for frmChangePassword.xaml
    /// </summary>
    public partial class frmChangePassword : Window
    {
        public frmChangePassword()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtOldPassword.Focus();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var oldPassword = txtOldPassword.Password;
            var newPassword = txtNewPassword.Password;
            var confirmPassword = txtConfirmPassword;

            if (newPassword == oldPassword) // make sure user is choosing a new password
            {
                MessageBox.Show("You need to choose a new password.");
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                txtNewPassword.Focus();
                return;
            }
            if (newPassword != confirmPassword) // make sure user knows what password was chosen
	{
		 MessageBox.Show("New Password and Confirm must match.");
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                txtNewPassword.Focus();
                return;
	}
            try
            {
                if (_member == null && _trainer == null)
            {
                try
                {
                    //UserManager.AuthenticateUser(email, password);

                    if (UserAcessor.(email, password))
                    {

                        _member = MemberManager.getMember(email);
                    }
                    else if (TrainerManager.VerifyTrainer(email, password))
                    {

                        _trainer = TrainerManager.getTrainer(email);
                    }
               
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
    