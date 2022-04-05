using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ContactsApp;

namespace ContactsAppUI
{
    public partial class MainForm : Form
    {
        private Project _project = new Project();

        private readonly string _filePath = ProjectManager.FilePath();

        private readonly string _directoryPath = ProjectManager.DirectoryPath();

        private List<Contact> _viewedContacts = new List<Contact>();

        public MainForm()
        {
            InitializeComponent();
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            _project = ProjectManager.LoadFromFile(_filePath);

            if (_project.Contacts.Count == 0)
            {
                return;
            }
            _viewedContacts = _project.Contacts;

            UpdateContactsList(null);

            ProjectManager.SaveToFile(_project, _filePath, _directoryPath);
        }

        private void UpdateContactsList(Contact contact)
        {
            var sortedContacts = new Project();
            _viewedContacts = sortedContacts.SortContacts(findTextBox.Text, _project.Contacts);
            var index = _viewedContacts.FindIndex(x => x == contact);
            ContactsListBox.Items.Clear();
            foreach (var t in _viewedContacts)
            {
                ContactsListBox.Items.Add(t.Surname);
            }
            if (index == -1 && ContactsListBox.Items.Count != 0)
            {
                index = 0;
            }
            ContactsListBox.SelectedIndex = index;
            if (index == -1)
            {
                ClearContactsView();
            }
        }

        /// <summary>
        /// Очищения полей для вывода контакта.
        /// </summary>
        private void ClearContactsView()
        {
            surnameTextBox.Text = "";
            nameTextBox.Text = "";
            phoneTextBox.Text = "";
            emailTextBox.Text = "";
            idVkTextBox.Text = "";
            birthDateBox.Text = "";
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var addcontact = new ContactForm(); 
            addcontact.ShowDialog(); 
        }

        private void about_Click(object sender, EventArgs e)
        {
            var about = new AboutForm();
            about.ShowDialog();
        }
    }
}

