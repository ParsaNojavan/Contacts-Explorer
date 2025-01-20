using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyContacts
{
    public partial class AddOrEdit : Form
    {
        IContactsRepository repos;
        public int TypeID = 0;
        public AddOrEdit()
        {
            InitializeComponent();
            repos = new ContactsRepository();
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
                DataTable dt = repos.SelectRow(TypeID);
                txtName.Text= dt.Rows[0][1].ToString();
                txtLastName.Text = dt.Rows[0][2].ToString();
                txtMobile.Text = dt.Rows[0][3].ToString();
                txtEmail.Text = dt.Rows[0][4].ToString();
                txtAddress.Text = dt.Rows[0][5].ToString();
                nmrAge.Value = (int)dt.Rows[0][6];
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
            if (TypeID == 0)
            {
                if (ValidateInputs())
                {
                    bool isSuccess = repos.Insert(txtName.Text, txtLastName.Text, txtMobile.Text, txtEmail.Text, txtAddress.Text, (int)nmrAge.Value);
                    if (isSuccess == true)
                    {
                        MessageBox.Show("Contact Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Contact Coudnt Add", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                if (ValidateInputs())
                {
                    bool isSuccess = repos.Update(TypeID,txtName.Text, txtLastName.Text, txtMobile.Text, txtEmail.Text, txtAddress.Text, (int)nmrAge.Value);
                    if (isSuccess == true)
                    {
                        MessageBox.Show("Contact Edited Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Contact Coudnt Add", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }
    }
}
