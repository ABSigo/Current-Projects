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
    /// Ariel Sigo
    /// Created 2016/11/25
    /// 
    /// 
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Member _member = null;
        private Trainer _trainer = null;
        private ClassManager _classManager = new ClassManager();
        private UserManager _userManager = new UserManager();
        private MemberManager _memberManager = new MemberManager();
        private TrainerManager _trainerManager = new TrainerManager();
        private List<Class> classList = new List<Class>();
        private List<Trainer> trainerList = new List<Trainer>();
        private List<Member> _memberList;
        /// <summary>
        /// /// Ariel Sigo
        /// Created 2016/11/25
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/25
        /// 
        /// Loads main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            hideAllTabs();
            txtEmail.Focus();
            RefreshMemberList();
            classList = _classManager.GetFullClassList();

            
        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/25
        /// 
        /// Hides all tabs before user successfully log ins
        /// 
        /// </summary>
        private void hideAllTabs()
        {
            // hide the tabs
            tabSchedule.Visibility = Visibility.Hidden;
            tabTrainers.Visibility = Visibility.Hidden;
            tabMembers.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/25
        /// 
        /// Refreshes memberlist after successful trainer log in
        /// </summary>
        private void RefreshMemberList()
        {
            _memberList = _memberManager.GetFullMemberList();

           
        }

        
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/25
        /// 
        /// Validates user input and creates a successful login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var email = txtEmail.Text;
            var password = txtPassword.Password;
            var usrMgr = new UserManager();
            

            if (_member == null && _trainer == null)
            {
                try
                {
                    

                    if (MemberManager.VerifyMember(email, password))
                    {

                        _member = MemberManager.getMember(email);
                        trainerList = _trainerManager.GetFullTrainerList();
                    }
                    else if (TrainerManager.VerifyTrainer(email, password))
                    {

                        _trainer = TrainerManager.getTrainer(email);

                        try
                        {
                            _memberList = _memberManager.GetFullMemberList();
                            trainerList = _trainerManager.GetFullTrainerList();
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("There was a problem retireiving your Member and Trainer Lists \n " + ex.Message);
                        }
                        trainerInterface();
                        memberInterface();
                    }

                    if (_member != null) // login for member
                    {
                        MessageBox.Show("Welcome Back " + _member.FirstName + " " + _member.LastName + "! \n");
                        dgSchedule.ItemsSource = classList;
                        dgTrainers.ItemsSource = trainerList;

                        txtEmail.Clear();
                        txtPassword.Clear();
                        txtPassword.IsEnabled = false;
                        txtEmail.IsEnabled = false;
                        btnLogin.Content = "Log Out";
                        populateTables();
                        showTabs();
                    }
                    else if (_trainer != null) // login for trainer
                    {
                        MessageBox.Show("Welcome Back " + _trainer.TrainerFirstName + " " + _trainer.TrainerLastName + "! \n");
                        dgSchedule.ItemsSource = classList;


                        txtEmail.Clear();
                        txtPassword.Clear();
                        txtPassword.IsEnabled = false;
                        txtEmail.IsEnabled = false;
                        btnLogin.Content = "Log Out";
                        populateTables();
                        showTabs();
                    }
                 
                    else
                    {
                        MessageBox.Show("Incorrect Login. Please Try Again!");
                        txtPassword.Clear();
                    }
                    
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
                dgSchedule.Visibility = Visibility.Hidden;
                dgMembers.Visibility = Visibility.Hidden;
                dgTrainers.Visibility = Visibility.Hidden;
                hideAllTabs();

            }
            
        }
         /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/25
         /// Populates Tables based on successful log in
         /// 
         /// </summary>
        private void populateTables()
        {
            dgSchedule.Visibility = Visibility.Visible;
            dgTrainers.Visibility = Visibility.Visible;
            dgMembers.Visibility = Visibility.Visible;
            dgSchedule.ItemsSource = classList;
            dgSchedule.Items.Refresh();
        }
        

        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/25
        /// 
        /// Shows different tabs depending on which user logs in 
        /// </summary>
        private void showTabs()
        {
            if (_member != null)
            {
                tabTrainers.Visibility = Visibility.Visible;
                tabMembers.Visibility = Visibility.Hidden;
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
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/25
        /// 
        /// What a member sees when they log in successfully
        /// </summary>
        private void memberInterface()
        {
            if (_member != null)
            {
                dgTrainers.ItemsSource = trainerList;
                dgSchedule.ItemsSource = classList;
            }
        }
        /// <summary>
        ///  Ariel Sigo 
        ///  Created 2016/11/25
        ///  
        /// What a trainer sees when they log in successfully
        /// </summary>
        private void trainerInterface()
        {
            if (_trainer != null)
            {
                dgTrainers.ItemsSource = trainerList;
                dgMembers.ItemsSource = _memberList;
            }
        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/25
        /// 
        /// Launchces change password after successful log in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (_userManager == null)
            {
                MessageBox.Show("You must be logged in to change your password.");
            }
            else
            {
                var passwordWindow = new frmChangePassword(_member, _trainer, _userManager);
                passwordWindow.ShowDialog();
            }

            // code to launch a password change window
        }


        
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/25
        /// 
        /// Adds the added member to list of members
        /// </summary>
   
        private void btnAddMember_Click_1(object sender, RoutedEventArgs e)
        {
            var addEditMemberWindow = new frmAddEditMember(_memberManager);
            var result = addEditMemberWindow.ShowDialog();
            if (result == true)
            {
                RefreshMemberList();
                dgMembers.ItemsSource = _memberList;
                dgMembers.Items.Refresh();
            }

            
        }
       /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/25
        /// 
        /// Edits a current members information 
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void btnEditMember_Click(object sender, RoutedEventArgs e)
        {
            if (dgMembers.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("You haven't selected anything!");
                return;
            }
            else
            {
                var member = (Member)dgMembers.SelectedItem;
                var addEditMemberForm = new frmAddEditMember(_memberManager, member);
                var result = addEditMemberForm.ShowDialog();
                if (result == true)
                {
                    dgMembers.ItemsSource = addEditMemberForm.listOMembers;
                }
            }
        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/25
        /// 
        /// Quits program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


       

       
    }





}



