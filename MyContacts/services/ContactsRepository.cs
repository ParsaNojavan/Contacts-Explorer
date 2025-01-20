using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContacts
{
    internal class ContactsRepository : IContactsRepository
    {
        private string _connection = "Data Source =ASUS\\PARSA;Initial Catalog = Contacts_DB ;Integrated Security = true";
        public static string SearchItem; 
        public bool Delete(int ContactID)
        {
            SqlConnection connection = new SqlConnection(_connection);
            try
            {
                string query = $"DELETE FROM MyContacts WHERE ContactID={ContactID}";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool Insert(string Name, string LastName, string Mobile, string Email, string Address, int Age)
        {
            SqlConnection connection = new SqlConnection(_connection);
            try
            {
                
                string query = "Insert Into MyContacts (Name,LastName,Mobile,Email,Address,Age) Values (@Name,@LastName,@Mobile,@Email,@Address,@Age)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@LastName", LastName);
                command.Parameters.AddWithValue("@Mobile", Mobile);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@Address", Address);
                command.Parameters.AddWithValue("@Age", Age);
                connection.Open();
                command.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public DataTable SelectAll()
        {
            string query = "Select * From MyContacts";
            SqlConnection connection = new SqlConnection(_connection);
            SqlDataAdapter adapter = new SqlDataAdapter(query,connection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }

        public DataTable SelectRow(int ContactID)
        {
            string query = $"Select * From MyContacts Where ContactID={ContactID}";
            SqlConnection connection = new SqlConnection(_connection);
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }

        public bool Update(int ContactID, string Name, string LastName, string Mobile, string Email, string Address, int Age)
        {
            SqlConnection connection = new SqlConnection(_connection);
            try
            {
                string query = $"update MyContacts set Name='{Name}',LastName='{LastName}'," +
               $"Mobile='{Mobile}',Address='{Address}',Age='{Age}' where ContactID={ContactID}";  
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public DataTable Search(string Parameter)
        {
            string query = "select * from MyContacts where Name Like @Search";
            SqlConnection connection = new SqlConnection(_connection);
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.SelectCommand.Parameters.AddWithValue("@Search", "%" + Parameter + "%");
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }
    }
}
