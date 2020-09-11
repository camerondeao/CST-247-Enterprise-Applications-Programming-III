using Minesweeper_Web_Application.Models;
using Minesweeper_Web_Application.Services.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;

namespace Minesweeper_Web_Application.Services.Data
{
    public class LeaderBoardDAO
    {
        string connectionStr = "Data Source=(localdb)\\MSSQLLocalDB;initial catalog=cst247_minesweeper ;Integrated Security=True;";
        public void InsertHighScore(UserModel user, decimal time)
        {
            //If this returns true we'll break out of the method.
            //Only returns true if a user exists within the method.
            if(CheckUserScore(user, time))
            {
                return;
            }

            string insert = "INSERT INTO dbo.leaderboard (USERNAME, TIME) VALUES (@Username, @Time)";

            SqlConnection conn = new SqlConnection(connectionStr);
            SqlCommand command = new SqlCommand(insert, conn);

            try
            {
                command.Parameters.AddWithValue("@Username", user.UserName);
                command.Parameters.AddWithValue("@Time", time);

                conn.Open();
                command.ExecuteNonQuery();

                Debug.WriteLine("Inserted successfully!");
            }
            catch(SqlException e)
            {
                Debug.WriteLine("An error has occurred. Details: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public List<LeaderBoard> GetScores(List<LeaderBoard> inc)
        {
            string retrieval = "SELECT * FROM dbo.leaderboard";

            SqlConnection conn = new SqlConnection(connectionStr);
            SqlCommand command = new SqlCommand(retrieval, conn);

            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        LeaderBoard user = new LeaderBoard();
                        user.Username = reader.GetString(1);
                        user.Time = reader.GetDecimal(2);

                        Debug.WriteLine(reader.GetString(1));
                        Debug.WriteLine(reader.GetDecimal(2));

                        inc.Add(user);
                    }
                }

                inc = inc.OrderBy(t => t.Time).ToList();

                for(int i = 0; i < inc.Count; i++)
                {
                    inc[i].Rank = i + 1;
                }
            }
            catch(SqlException e)
            {
                Debug.WriteLine("An error has occurred. Details: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }
            
            return inc;
        }

        public bool CheckUserScore(UserModel user, decimal time)
        {
            bool newTimeCheck = false;
            string query = "SELECT * FROM dbo.leaderboard WHERE USERNAME = @Username";

            SqlConnection conn = new SqlConnection(connectionStr);
            SqlCommand command = new SqlCommand(query, conn);

            try
            {
                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 25).Value = user.UserName;
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        //We'll only update the user score if it's lower
                        //than the stored score for the user.
                        if(reader.GetDecimal(2) >= time)
                        {
                            UpdateUserScore(user.UserName, time);
                        }
                    }
                    newTimeCheck = true;
                }
            }
            catch(SqlException e)
            {
                Debug.WriteLine("Error generated. Details: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }

            return newTimeCheck;
        }

        private void UpdateUserScore(string username, decimal time)
        {
            string updateQuery = "UPDATE dbo.leaderboard SET TIME = @Time WHERE USERNAME = @Username";
            SqlConnection conn = new SqlConnection(connectionStr);
            SqlCommand command = new SqlCommand(updateQuery, conn);

            try
            {
                command.Parameters.Add("@Username", System.Data.SqlDbType.VarChar, 25).Value = username;
                command.Parameters.Add("@Time", System.Data.SqlDbType.Decimal).Value = time;

                conn.Open();
                command.ExecuteNonQuery();

                Debug.WriteLine("UPDATED DATA");
            }
            catch(SqlException e)
            {
                Debug.WriteLine("Error generated. Details: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
    }
}