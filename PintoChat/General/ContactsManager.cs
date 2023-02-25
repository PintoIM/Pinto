using PintoNS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoNS.General
{
    public class ContactsManager
    {
        private MainForm mainForm;
        private DataGridView dgvContacts;
        private List<Contact> contacts = new List<Contact>();

        public ContactsManager(MainForm mainForm) 
        {
            this.mainForm = mainForm;
            dgvContacts = mainForm.dgvContacts;
        }

        private DataGridViewRow GetContactListEntry(int id) 
        {
            if (id == -1) return null;

            foreach (DataGridViewRow row in dgvContacts.Rows) 
            {
                if (((int)row.Cells[0].Value) == id) 
                    return row;
            }

            return null;
        }

        private void AddContactListEntry(Contact contact) 
        {
            if (GetContactListEntry(contact.ID) == null) 
            {
                dgvContacts.Rows.Add(contact.ID, User.StatusToBitmap(contact.Status), contact.Name);
            }
        }

        private void RemoveContactListEntry(Contact contact)
        {
            DataGridViewRow row;
            if ((row = GetContactListEntry(contact.ID)) != null) 
            {
                dgvContacts.Rows.Remove(row);
            }
        }

        private void UpdateContactListEntry(Contact contact) 
        {
            DataGridViewRow row;
            if ((row = GetContactListEntry(contact.ID)) != null)
            {
                row.Cells[0].Value = contact.ID;
                row.Cells[1].Value = User.StatusToBitmap(contact.Status);
                row.Cells[2].Value = contact.Name;
            }
        }

        public int GetContactIDFromRow(int rowIndex) 
        {
            foreach (DataGridViewRow row in dgvContacts.Rows)
            {
                if (row.Index == rowIndex)
                    return (int)row.Cells[0].Value;
            }

            return -1;
        }

        public Contact GetContact(int id) 
        {
            if (id == -1) return null;

            foreach (Contact contact in contacts.ToArray()) 
            {
                if (contact.ID == id)
                    return contact;
            }

            return null;
        }

        public void AddContact(Contact contact)
        {
            if (GetContact(contact.ID) == null) 
            {
                AddContactListEntry(contact);
                contacts.Add(contact);
            }
        }

        public void RemoveContact(Contact contact)
        {
            if (GetContact(contact.ID) != null)
            {
                RemoveContactListEntry(contact);
                contacts.Remove(contact);
            }
        }
    }
}
