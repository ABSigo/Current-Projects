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
    ///  Created 2016/11/25 
    /// 
    /// Interaction logic for frmAddEditMember.xaml
    /// </summary>
    public partial class frmAddEditMember : Window
    {
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/19
        /// </summary>
        public List<Member> listOMembers { get; set; }
        private Member _member = null;
        private ClassManager _classManager = new ClassManager();
        private UserManager _userManager = new UserManager();
        private MemberManager _memberManager = new MemberManager();
        private TrainerManager _trainerManager = new TrainerManager();
        private List<Class> classList = new List<Class>();
        private List<Trainer> trainerList = new List<Trainer>();
        private List<Member> memberList = new List<Member>();

        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/19
        /// 
        /// This is add mode for member
        /// </summary>
        /// <param name="memberManager"></param>
        public frmAddEditMember(MemberManager memberManager)
        {
            InitializeComponent();
            _memberManager = memberManager;
        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/19
        /// 
        /// This is edit mode for Member
        /// 
        /// </summary>
        /// <param name="memberManager"></param>
        /// <param name="member"></param>
        public frmAddEditMember(MemberManager memberManager, Member member)
        
        {
            InitializeComponent();
            _memberManager = memberManager;
            _member = member;

        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/19
        /// 
        /// Loads Window of Add Edit Member form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_member == null) // add mode
            {
                this.Title = "Add a New Member!";


                cboPoleLevel.Items.Add("Level 1");
                cboPoleLevel.Items.Add("Level 2");
                cboPoleLevel.Items.Add("Level 3");
                cboPoleLevel.Items.Add("Level 4");

                cboAcroLevel.Items.Add("Level 1");
                cboAcroLevel.Items.Add("Level 2");
                cboAcroLevel.Items.Add("Level 3");
                cboAcroLevel.Items.Add("Level 4");

                cboSilksLevel.Items.Add("Level 1");
                cboSilksLevel.Items.Add("Level 2");
                cboSilksLevel.Items.Add("Level 3");
                cboSilksLevel.Items.Add("Level 4");

                cboLyraLevel.Items.Add("Level 1");
                cboLyraLevel.Items.Add("Level 2");
                cboLyraLevel.Items.Add("Level 3");
                cboLyraLevel.Items.Add("Level 4");

                cboMembership.Items.Add("First");
                cboMembership.Items.Add("Daily");
                cboMembership.Items.Add("Monthly");
                cboMembership.Items.Add("Groupon");
                cboMembership.Items.Add("Private");



            }
            else // edit mode
            {
                this.Title = "Editing the record for " + _member.FirstName;

                txtMemberID.Text = _member.MemberID;
                txtFirstName.Text = _member.FirstName;
                txtLastName.Text = _member.LastName;
                txtBirthday.Text = _member.Birthday.ToShortDateString();
                txtPhoneNumber.Text = _member.PhoneNumber;
                cboMembership.Text = _member.MembershipTypeID;
                cboPoleLevel.Text = _member.MemberPoleLevel;
                cboAcroLevel.Text = _member.MemberAcroLevel;
                cboSilksLevel.Text = _member.MemberSilksLevel;
                cboLyraLevel.Text = _member.MemberLyraLevel;

                cboPoleLevel.Items.Add("Level 1");
                cboPoleLevel.Items.Add("Level 2");
                cboPoleLevel.Items.Add("Level 3");
                cboPoleLevel.Items.Add("Level 4");

                cboAcroLevel.Items.Add("Level 1");
                cboAcroLevel.Items.Add("Level 2");
                cboAcroLevel.Items.Add("Level 3");
                cboAcroLevel.Items.Add("Level 4");

                cboSilksLevel.Items.Add("Level 1");
                cboSilksLevel.Items.Add("Level 2");
                cboSilksLevel.Items.Add("Level 3");
                cboSilksLevel.Items.Add("Level 4");

                cboLyraLevel.Items.Add("Level 1");
                cboLyraLevel.Items.Add("Level 2");
                cboLyraLevel.Items.Add("Level 3");
                cboLyraLevel.Items.Add("Level 4");


                cboAcroLevel.SelectedItem = _member.MemberAcroLevel;
                cboPoleLevel.SelectedItem = _member.MemberPoleLevel;
                cboSilksLevel.SelectedItem = _member.MemberSilksLevel;
                cboLyraLevel.SelectedItem = _member.MemberLyraLevel;

                chkStatus.IsChecked = _member.Status;
                datStartDate.SelectedDate = _member.StartDate;

                cboMembership.Items.Add("First");
                cboMembership.Items.Add("Daily");
                cboMembership.Items.Add("Monthly");
                cboMembership.Items.Add("Groupon");
                cboMembership.Items.Add("Private");

            }
            if (!cboMembership.Items.Contains(_member.MembershipTypeID))
            {
                cboMembership.Items.Add(_member.MembershipTypeID);
            }

        }

       /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/19
        /// 
        /// Cancels Edit Member without saving changes
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to exit?", "Exit Edit Member", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.Yes)
	{
		 this.Close();
	}
          
        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/25
        /// 
        /// Confirms add and edit of members
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
           
            if (_member != null) // edit mode
            {
                try
                {
                    Member meember = _userManager.makeMember(txtMemberID.Text, txtFirstName.Text, txtLastName.Text, txtPhoneNumber.Text, (bool)chkStatus.IsChecked, DateTime.Parse(txtBirthday.Text).Date, DateTime.Parse(datStartDate.Text), cboMembership.Text, cboPoleLevel.Text, cboAcroLevel.Text, cboSilksLevel.Text, cboLyraLevel.Text);
                    _userManager.UpdateMember(_member, meember);

                    listOMembers = _memberManager.GetFullMemberList();
                    MessageBox.Show("Update Completed!");
                    this.DialogResult = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Update Not Complete.");
                }
            }
            else // add mode
            {
                try
                {

                    Member neember = _userManager.newMember(txtMemberID.Text, txtFirstName.Text, txtLastName.Text, txtPhoneNumber.Text, (bool)chkStatus.IsChecked, DateTime.Parse(txtBirthday.Text).Date, DateTime.Parse(datStartDate.Text), cboMembership.Text, cboPoleLevel.Text, cboAcroLevel.Text, cboSilksLevel.Text, cboLyraLevel.Text);
                    _userManager.createNewMember(txtMemberID.Text, txtFirstName.Text, txtLastName.Text, txtPhoneNumber.Text, (bool)chkStatus.IsChecked, DateTime.Parse(txtBirthday.Text).Date, DateTime.Parse(datStartDate.Text), cboMembership.Text, cboPoleLevel.Text, cboAcroLevel.Text, cboSilksLevel.Text, cboLyraLevel.Text);


                    listOMembers = _memberManager.GetFullMemberList();
                    MessageBox.Show("Member Added!");
                    _memberManager.GetFullMemberList();

                    this.DialogResult = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Add Not Complete.");
                }
                
                
            }
            
        }
       
    }
}
