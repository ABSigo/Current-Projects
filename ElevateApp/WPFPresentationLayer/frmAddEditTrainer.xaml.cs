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
    /// Interaction logic for frmAddEditTrainer.xaml
    /// </summary>
    public partial class frmAddEditTrainer : Window
    {
        public List<Trainer> listOTrainers { get; set; }
        private Trainer _trainer = null;
        private ClassManager _classManager = new ClassManager();
        private UserManager _userManager = new UserManager();
        private MemberManager _memberManager = new MemberManager();
        private TrainerManager _trainerManager = new TrainerManager();
        private List<Class> classList = new List<Class>();
        private List<Trainer> trainerList = new List<Trainer>();
        private List<Member> memberList = new List<Member>();

        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/25
        /// 
        /// This is add mode for trainer
        /// </summary>
        /// <param name="trainer"></param>
        public frmAddEditTrainer(Trainer trainer)
        {// add mode
            InitializeComponent();
            _trainer = trainer;
        }

        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/25
        /// 
        /// This is edit mode for trainer
        /// </summary>
        /// <param name="trainerManager"></param>
        /// <param name="trainer"></param>
        public frmAddEditTrainer(TrainerManager trainerManager, Trainer trainer)
        {// edit mode 

            InitializeComponent();
            _trainerManager = trainerManager;
            _trainer = trainer;
        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/25
        /// 
        /// Cancels Add edit trainer without saving changes
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
        /// Confirms update of Trainer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            Trainer treener = _userManager.createTrainer(txtTrainerID.Text, txtFirstName.Text, txtLastName.Text, txtPhoneNumber.Text, (bool)chkStatus.IsChecked, cboPoleLevel.Text, cboAcroLevel.Text, cboSilksLevel.Text, cboLyraLevel.Text);
            _userManager.UpdateTrainer(_trainer, treener);

            listOTrainers = _trainerManager.GetFullTrainerList();
            MessageBox.Show("Update Completed!");
            this.DialogResult = true;
        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2016/11/25
        /// 
        /// Edits and Adds trainers to trainer list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_trainer == null) // add mode
            {
                this.Title = "Add a New Trainer";

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


            }
            else // edit mode
            {
                this.Title = "Editing the record for " + _trainer.TrainerFirstName;

                txtTrainerID.Text = _trainer.TrainerID;
                txtFirstName.Text = _trainer.TrainerFirstName;
                txtLastName.Text = _trainer.TrainerLastName;
                txtPhoneNumber.Text = _trainer.TrainerPhoneNumber;
                cboPoleLevel.Text = _trainer.TrainerPoleLevel;
                cboAcroLevel.Text = _trainer.TrainerAcroLevel;
                cboSilksLevel.Text = _trainer.TrainerSilksLevel;
                cboLyraLevel.Text = _trainer.TrainerLyraLevel;

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

                cboPoleLevel.SelectedItem = _trainer.TrainerPoleLevel;
                cboAcroLevel.SelectedItem = _trainer.TrainerAcroLevel;
                cboSilksLevel.SelectedItem = _trainer.TrainerSilksLevel;
                cboLyraLevel.SelectedItem = _trainer.TrainerLyraLevel;

            }

            {

            }
        }
    }
}
