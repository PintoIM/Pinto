using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace PintoNS.General
{
    public class ContactsManager
    {
        private MainForm mainForm;
        private DataGridView dgvContacts;
        private DataTable dataTable;
        private List<Contact> contacts = new List<Contact>();

        public ContactsManager(MainForm mainForm) 
        {
            this.mainForm = mainForm;
            dgvContacts = mainForm.dgvContacts;
            dataTable = (DataTable) mainForm.dgvContacts.DataSource;
        }

        private DataRow GetContactListEntry(string name) 
        {
            if (name == null) return null;

            foreach (DataRow row in dataTable.Rows) 
                if (((string)row[1]) == name) 
                    return row;

            return null;
        }

        private void AddContactListEntry(Contact contact) 
        {
            if (GetContactListEntry(contact.Name) == null)
                dataTable.Rows.Add(User.StatusToBitmap(contact.Status), contact.Name, contact.MOTD);
        }

        private void RemoveContactListEntry(Contact contact)
        {
            DataRow row;
            if ((row = GetContactListEntry(contact.Name)) != null) 
                dataTable.Rows.Remove(row);
        }

        private void UpdateContactListEntry(Contact contact) 
        {
            DataRow row;
            if ((row = GetContactListEntry(contact.Name)) != null)
            {
                row[0] = User.StatusToBitmap(contact.Status);
                row[1] = contact.Name;
                row[2] = contact.MOTD;
            }
        }

        public string GetContactNameFromRow(int rowIndex) 
        {
            foreach (DataRow row in dataTable.Rows)
            {
                if (dataTable.Rows.IndexOf(row) == rowIndex)
                    return (string)row[1];
            }

            return null;
        }

        public Contact GetContact(string name) 
        {
            if (name == null) return null;

            foreach (Contact contact in contacts.ToArray()) 
            {
                if (contact.Name == name)
                    return contact;
            }

            return null;
        }

        public void AddContact(Contact contact)
        {
            if (GetContact(contact.Name) == null) 
            {
                AddContactListEntry(contact);
                contacts.Add(contact);
            }
        }

        public void RemoveContact(Contact contact)
        {
            if (GetContact(contact.Name) != null)
            {
                RemoveContactListEntry(contact);
                contacts.Remove(contact);
            }
        }

        public void UpdateContact(Contact contact) 
        {
            if (GetContact(contact.Name) != null)
            {
                UpdateContactListEntry(contact);
                contacts.Remove(GetContact(contact.Name));
                contacts.Add(contact);
            }
        }

        public Contact[] GetContacts() => contacts.ToArray();
    }
}