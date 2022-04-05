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

        /// <summary>
        /// Загрузка главной формы.
        /// </summary>
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

        /// <summary>
        /// Сортировка и вывод контактов.
        /// </summary>
        private void UpdateContactsList(Contact contact)
        {
            var sortedContacts = new Project();
            _viewedContacts = sortedContacts.SortContacts(FindTextBox.Text, _project.Contacts);
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
        /// Вывод данных контакта на главную форму.
        /// </summary>
        private void ViewContacts(IReadOnlyList<Contact> contacts)
        {
            var index = ContactsListBox.SelectedIndex;
            if (index == -1)
            {
                ClearContactsView();
                return;
            }
            surnameTextBox.Text = contacts[index].Surname;
            nameTextBox.Text = contacts[index].Name;
            phoneTextBox.Text = $@"+{contacts[index].PhoneNumber.Number}";
            emailTextBox.Text = contacts[index].Email;
            idVkTextBox.Text = contacts[index].IdVk;
            birthDateBox.Text = contacts[index].BirthDate.ToString("dd.MM.yyyy");
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
            var newContact = new Contact { PhoneNumber = new PhoneNumber() };
            var contactForm = new ContactForm { Contact = newContact };
            var dialogResult = contactForm.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                return;
            }
            _project.Contacts.Add(contactForm.Contact);
            _project.Contacts = _project.SortContacts(_project.Contacts);
            UpdateContactsList(contactForm.Contact);
            ProjectManager.SaveToFile(_project, _filePath, _directoryPath);
        }

        private void About_Click(object sender, EventArgs e)
        {
            var about = new AboutForm();
            about.ShowDialog();
        }


        /// <summary>
        /// Поиск контакта с помощью поля поиска.
        /// </summary>
        private void FindTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ContactsListBox.SelectedIndex >= 0)
            {
                var selectedContact = _viewedContacts[ContactsListBox.SelectedIndex];
                UpdateContactsList(selectedContact);
            }
            else
            {
                UpdateContactsList(null);
            }
        }

        /// <summary>
        /// Вывод выбранного контакта.
        /// </summary>
        private void ContactsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sortContacts = new Project();
            ViewContacts(sortContacts.SortContacts(FindTextBox.Text, _project.Contacts));
        }
    }
}

