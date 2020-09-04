using Minesweeper_Web_Application.Models;
using Minesweeper_Web_Application.Services.Business;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Minesweeper_Web_Application.Services.Data
{
    

    public class SecurityDAO
    {
        string connectionStr = "Data Source=(localdb)\\MSSQLLocalDB;initial catalog=cst247_minesweeper ;Integrated Security=True;";
        //const string key = "asdf78954wer7q5re72er54wr5654dsf";

        public bool CheckUsername(UserModel user)
        {
            string testing = "SELECT 1 FROM dbo.Users WHERE USERNAME = @Username";
            SqlConnection conn = new SqlConnection(connectionStr);
            SqlCommand command = new SqlCommand(testing, conn);

            try
            {
                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 25).Value = user.UserName;
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    return true;
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine("Error generated. Details: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }

            return false;
        }

        public bool RegisterUser(UserModel user)
        {
            bool registered = false;

            string userGender;
            userGender = CheckUserGender(user);

            user.Password = Encrypt(System.Web.Configuration.WebConfigurationManager.AppSettings["Key"], user.Password);

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

                Debug.WriteLine("Records inserted successfully!");
            }
            catch (SqlException e)
            {
                Debug.WriteLine("Error generated. Details: " + e.ToString());
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
            string retrieveQuery = "SELECT * FROM dbo.Users WHERE USERNAME = @Username";

            SqlConnection conn = new SqlConnection(connectionStr);
            SqlCommand command = new SqlCommand(retrieveQuery, conn);

            try
            {
                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 25).Value = user.Username;
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    found = true;
                    UserModel currUser = new UserModel();
                    while(reader.Read())
                    {
                        if (Decrypt(System.Web.Configuration.WebConfigurationManager.AppSettings["Key"], reader.GetString(8)) != user.Password)
                        {
                            return false;
                        }
                        currUser.FirstName = reader.GetString(1);
                        currUser.LastName = reader.GetString(2);
                        currUser.Age = reader.GetInt32(3);
                        if (reader.GetString(4) == "M")
                        {
                            currUser.Gender = Gender.Male;
                        }
                        else
                        {
                            currUser.Gender = Gender.Female;
                        }
                        currUser.State = reader.GetString(5);
                        currUser.EmailAddress = reader.GetString(6);
                        currUser.UserName = reader.GetString(7);
                        UserManagement.Instance._loggedUser = currUser;
                    }
                }
                reader.Close();
            }
            catch(SqlException e)
            {
                Debug.WriteLine("Error generated in retrieval. Details: " + e.ToString());
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


        public static string Encrypt(string key, string incStr)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using(Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using(MemoryStream ms = new MemoryStream())
                {
                    using(CryptoStream cs = new CryptoStream((Stream)ms, encryptor, CryptoStreamMode.Write))
                    {
                        using(StreamWriter streamWriter = new StreamWriter((Stream)cs))
                        {
                            streamWriter.Write(incStr);
                        }
                        array = ms.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string Decrypt(string key, string incStr)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(incStr);

            using(Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(buffer))
                {
                    using(CryptoStream cs = new CryptoStream((Stream)ms, decryptor, CryptoStreamMode.Read))
                    {
                        using(StreamReader sr = new StreamReader((Stream)cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}