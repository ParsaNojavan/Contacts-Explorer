using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyContacts
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (Contacts_DBEntities data = new Contacts_DBEntities())
            {
                dgContacts.AutoGenerateColumns = false;
                dgContacts.DataSource = data.MyContacts.ToList();
            }
        }

        private void Refresh()
        {
            using (Contacts_DBEntities data = new Contacts_DBEntities())
            {
                dgContacts.DataSource = data.MyContacts.ToList();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddOrEdit changeForm = new AddOrEdit();
            changeForm.ShowDialog();
            if(changeForm.DialogResult == DialogResult.OK)
            {
                Refresh();
            }
        }

        private void btnDelete_ButtonClick(object sender, EventArgs e)
        {
            if (dgContacts.CurrentRow != null)
            {
                string name = dgContacts.CurrentRow.Cells[1].Value.ToString();
                string lastname = dgContacts.CurrentRow.Cells[2].Value.ToString();
                string fullname = name + " " + lastname;
                if (MessageBox.Show($"Are you Sure you want to remove {fullname}?","Warning",MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int ContactID = int.Parse(dgContacts.CurrentRow.Cells[0].Value.ToString());
                   using(Contacts_DBEntities data = new Contacts_DBEntities())
                    {
                        MyContact contact = data.MyContacts.Single(c=>c.ContactID==ContactID);
                        data.MyContacts.Remove(contact);
                        data.SaveChanges();
                    }
                    Refresh();
                }
            }
            else
            {
                MessageBox.Show("Please choose a person");
            }
        }

        private void btnEdit_ButtonClick(object sender, EventArgs e)
        {
            AddOrEdit changeForm = new AddOrEdit();
            changeForm.TypeID = int.Parse(dgContacts.CurrentRow.Cells[0].Value.ToString());
            changeForm.ShowDialog();
            if (changeForm.DialogResult == DialogResult.OK)
            {
                Refresh();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           using(Contacts_DBEntities data = new Contacts_DBEntities())
            {
                dgContacts.DataSource = data.MyContacts.Where(c=>c.Name.Contains(textBox1.Text)||c.LastName.Contains(textBox1.Text)).ToList();
            }
        }
    }
}
