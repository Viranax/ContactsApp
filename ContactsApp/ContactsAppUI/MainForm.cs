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

            ProjectManager.SaveToFile(_project, _filePath, _directoryPath);
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

