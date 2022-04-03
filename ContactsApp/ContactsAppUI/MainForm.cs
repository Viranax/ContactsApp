using ContactsApp;
using System;
using System.Windows.Forms;

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
            PhoneNumber phoneNumber = new PhoneNumber
            {
                Number = 71234567890
            };
            Contact contact = new Contact
            {
                Surname = "Иванов",
                Name = "Иван",
                PhoneNumber = phoneNumber
            };
            var birthDate = new DateTime(1999, 01, 01);
            contact.BirthDate = birthDate;
            contact.IdVk = "Ivanov_Ivan";
            contact.Email = "Ivanov@gmail.com";
            Project project = new Project();
            project.Contacts.Add(contact);
            project.Contacts.Add(contact);
            label1.Text = contact.Name;
            label2.Text = contact.Surname;
            label3.Text = phoneNumber.Number.ToString();
            label4.Text = project.Contacts.ToString();
        }
    }
}
