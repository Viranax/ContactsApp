using System;
using System.Windows.Forms;
using ContactsApp;

namespace ContactsAppUI
{
    public partial class MainForm : Form
    {
       
        public MainForm()
        {
            InitializeComponent();
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            var birthDate = new DateTime(1999, 01, 02);

            PhoneNumber phoneNumber = new PhoneNumber
            {
                Number = 71234567890
            };

            Contact contact = new Contact
            {
                Surname = "Иванов",
                Name = "Иван",
                PhoneNumber = phoneNumber,
                BirthDate = birthDate,
                IdVk = "Ivan_Ivan",
                Email = "Ivan_Ivan@gmail.com"
            };

            Project project = new Project();

            project.Contacts.Add(contact);

            ProjectManager projectManager = new ProjectManager();

            projectManager.SaveToFile(project);

            project = projectManager.DeserializeProject();

            surnameTextBox.Text = project.Contacts[0].Surname;
            nameTextBox.Text = project.Contacts[0].Name;
            phoneTextBox.Text = project.Contacts[0].PhoneNumber.ToString();
            birthDateBox.Text = project.Contacts[0].BirthDate.ToString();
            idVkTextBox.Text = project.Contacts[0].IdVk;
            emailTextBox.Text = project.Contacts[0].Email;


        }

    }
}

