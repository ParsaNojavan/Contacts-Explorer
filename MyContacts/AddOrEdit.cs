using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MyContacts
{
    public partial class AddOrEdit : Form
    {
        Contacts_DBEntities data = new Contacts_DBEntities();
        public int TypeID = 0;
        public AddOrEdit()
        {
            InitializeComponent();
        }

        private void AddOrEdit_Load(object sender, EventArgs e)
        {
            if (TypeID == 0)
            {
                this.Text = "Add";
            }
            else
            {
                this.Text = "Edit";
                MyContact contact = data.MyContacts.Find(TypeID);
                txtName.Text = contact.Name.ToString();
                txtLastName.Text = contact.LastName.ToString();
                txtMobile.Text = contact.Mobile.ToString();
                txtEmail.Text = contact.Email.ToString();
                txtAddress.Text = contact.Address.ToString();
                nmrAge.Value = (int)contact.Age;
            }
        }

        bool ValidateInputs()
        {
            if (txtName.Text == "" || txtLastName.Text == "" || txtMobile.Text == "" || txtEmail.Text == "" || nmrAge.Value == 0)
            {
                MessageBox.Show("Please fill all the fields");
                return false;
            }
            return true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            if (ValidateInputs())
            {

                if (TypeID == 0)
                {
                    MyContact contact = new MyContact()
                    {
                        Name = txtName.Text,
                        LastName = txtLastName.Text,
                        Mobile = txtMobile.Text,
                        Email = txtEmail.Text,
                        Address = txtAddress.Text,
                        Age = int.Parse(nmrAge.Value.ToString())
                    };
                    data.MyContacts.Add(contact);
                }
                else
                {
                    var contact = data.MyContacts.Find(TypeID);
                    contact.Name = txtName.Text;
                    contact.LastName = txtLastName.Text;
                    contact.Mobile = txtMobile.Text;
                    contact.Email = txtEmail.Text;
                    contact.Address = txtAddress.Text;
                    contact.Age = int.Parse(nmrAge.Value.ToString());
                }
                data.SaveChanges();
                MessageBox.Show("Contact Added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
        }
    }
}

