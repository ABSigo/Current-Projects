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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WPFPresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Member _member = null;
        private Trainer _trainer = null;
        private ClassManager _classManager = new ClassManager();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            hideAllTabs();
            txtEmail.Focus();
        }

        private void hideAllTabs()
        {
            // hide the tabs
            tabSchedule.Visibility = Visibility.Hidden;
            tabTrainers.Visibility = Visibility.Hidden;
            tabMembers.Visibility = Visibility.Hidden;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var email = txtEmail.Text;
            var password = txtPassword.Password;
            var usrMgr = new UserManager();

            if (_member == null && _trainer == null)
            {
                try
                {
                    //UserManager.AuthenticateUser(email, password);

                    if (MemberManager.VerifyMember(email, password))
                    {

                        _member = MemberManager.getMember(email);
                    }
                    else if (TrainerManager.VerifyTrainer(email, password))
                    {

                        _trainer = TrainerManager.getTrainer(email);
                    }


                    MessageBox.Show("Welcome Back!");

                    txtEmail.Clear();
                    txtPassword.Clear();
                    txtPassword.IsEnabled = false;
                    txtEmail.IsEnabled = false;
                    btnLogin.Content = "Log Out";
                    showTabs();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Incorrect Login. Please Try Again!");
                }

            }
            else
            {
                _trainer = null;
                _member = null;
                txtEmail.IsEnabled = true;
                txtPassword.IsEnabled = true;
                btnLogin.Content = "Login";
                txtEmail.Focus();

                hideAllTabs();
            }
        }

        private void showTabs()
        {
            if (_member != null)
            {
                tabSchedule.Visibility = Visibility.Visible;
                tabsetMain.Visibility = Visibility.Visible;
            }
            else if (_trainer != null)
            {
                tabSchedule.Visibility = Visibility.Visible;
                tabTrainers.Visibility = Visibility.Visible;
                tabMembers.Visibility = Visibility.Visible;
                tabsetMain.Visibility = Visibility.Visible;
            }

        }
    }





}



