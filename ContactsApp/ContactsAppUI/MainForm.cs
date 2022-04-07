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

        /// <summary>
        /// Добавление контакта.
        /// </summary>
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

        /// <summary>
        /// Вывод окна о программе.
        /// </summary>
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

        /// <summary>
        /// Редактирование контакта.
        /// </summary>
        private void EditButton_Click(object sender, EventArgs e)
        {
            if (ContactsListBox.SelectedIndex == -1)
            {
                MessageBox.Show(@"Select the contact.", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var sortContacts = new Project();
                var selectedContact = _viewedContacts[ContactsListBox.SelectedIndex];
                var contactForm = new ContactForm { Contact = selectedContact };
                var dialogResult = contactForm.ShowDialog();
                if (dialogResult != DialogResult.OK)
                {
                    return;
                }
                var index = _project.Contacts.FindIndex(x => Equals(x, contactForm.Contact));
                _project.Contacts.RemoveAt(index);
                _project.Contacts.Insert(index, contactForm.Contact);
                _project.Contacts = sortContacts.SortContacts(_project.Contacts);
                UpdateContactsList(contactForm.Contact);
                ProjectManager.SaveToFile(_project, _filePath, _directoryPath);
            }
        }

        /// <summary>
        /// Сохранение при выходе из программы.
        /// </summary>
        private void MainForm_Closed(object sender, FormClosedEventArgs e)
        {
            ProjectManager.SaveToFile(_project, _filePath, _directoryPath);
        }

        /// <summary>
        /// Выход из программы через меню.
        /// </summary>
        private void exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Удаление контакта.
        /// </summary>
        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (ContactsListBox.SelectedIndex == -1)
            {
                MessageBox.Show(@"Select the contact.", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var selectedIndex = ContactsListBox.SelectedIndex;
                var result = MessageBox.Show($@"Do you really want to delete this contact: 
                    {_project.Contacts[selectedIndex].Surname}?", @"Confirmation",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (result != DialogResult.OK)
                {
                    return;
                }
                _project.Contacts.RemoveAt(selectedIndex);
                ContactsListBox.Items.RemoveAt(selectedIndex);
                ProjectManager.SaveToFile(_project, _filePath, _directoryPath);
                if (ContactsListBox.Items.Count > 0)
                {
                    ContactsListBox.SelectedIndex = 0;
                }
            }
        }
    }
}

