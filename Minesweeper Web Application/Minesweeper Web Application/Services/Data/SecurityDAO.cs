using Minesweeper_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Minesweeper_Web_Application.Services.Data
{
    public class SecurityDAO
    {
        string connectionStr = "Data Source=(localdb)\\MSSQLLocalDB;initial catalog=cst247_minesweeper ;Integrated Security=True;";
        public bool RegisterUser(UserModel user)
        {
            bool registered = false;
            string userGender;
            userGender = CheckUserGender(user);

            SqlConnection conn = new SqlConnection(connectionStr);
            string insertQuery = "INSERT INTO dbo.users (FIRSTNAME, LASTNAME, AGE, GENDER, STATE, EMAILADDRESS, USERNAME, PASSWORD)" +
            " VALUES (@FIRSTNAME, @LASTNAME, @AGE, @GENDER, @STATE, @EMAILADDRESS, @USERNAME, @PASSWORD)";
            SqlCommand command = new SqlCommand(insertQuery, conn);

            try
            {
                command.Parameters.AddWithValue("@FIRSTNAME", user.FirstName);
                command.Parameters.AddWithValue("@LASTNAME", user.LastName);
                command.Parameters.AddWithValue("@AGE", user.Age);
                command.Parameters.AddWithValue("@GENDER", userGender);
                command.Parameters.AddWithValue("@STATE", user.State);
                command.Parameters.AddWithValue("@EMAILADDRESS", user.EmailAddress);
                command.Parameters.AddWithValue("@USERNAME", user.UserName);
                command.Parameters.AddWithValue("@PASSWORD", user.Password);

                conn.Open();
                command.ExecuteNonQuery();
                registered = true;

                System.Diagnostics.Debug.WriteLine("Records inserted successfully!");
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine("Error generated. Details: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }

            return registered;
        }

        public bool FindByUser(UserLoginModel user)
        {
            bool found = false;
            string retrieveQuery = "SELECT * FROM dbo.Users WHERE USERNAME = @Username AND PASSWORD = @Password";

            SqlConnection conn = new SqlConnection(connectionStr);
            SqlCommand command = new SqlCommand(retrieveQuery, conn);

            try
            {
                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 25).Value = user.Username;
                command.Parameters.Add("@PASSWORD", System.Data.SqlDbType.VarChar, 25).Value = user.Password;
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    found = true;
                }
                reader.Close();
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine("Error generated. Details: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }

            return found;
        }

        private string CheckUserGender(UserModel user)
        {
            string genderStr;
            if (user.Gender == Gender.Male)
            {
                genderStr = "M";
            }
            else
            {
                genderStr = "F";
            }
            return genderStr;
        }
    }
}