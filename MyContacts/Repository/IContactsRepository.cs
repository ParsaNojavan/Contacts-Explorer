using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MyContacts
{
    interface IContactsRepository
    {
        DataTable SelectAll();
        DataTable SelectRow(int ContactID);
        bool Insert(string Name, string LastName, string Mobile, string Email, string Address, int Age);
        bool Update(int ContactID, string Name, string LastName, string Mobile, string Email, string Address, int Age);
        bool Delete(int ContactID);
        DataTable Search(string Parameter);
    }
}
