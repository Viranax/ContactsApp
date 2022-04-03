using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactsApp
{
    /// <summary>
    /// Класс содержит список всех контактов.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Коллекция контактов.
        /// </summary>
        public List<Contact> Contacts = new List<Contact>();

        /// <summary>
        /// Сортировка листа.
        /// </summary>
        /// <param name="contacts"> Лист для сортировки.</param>
        /// <returns></returns>
        public List<Contact> SortContacts(List<Contact> contacts)
        {
            var sortedContacts = from u in contacts orderby u.Surname select u;
            return sortedContacts.ToList();
        }
    }
}