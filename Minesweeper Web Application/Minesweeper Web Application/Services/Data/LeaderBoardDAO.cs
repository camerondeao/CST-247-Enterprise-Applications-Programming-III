using Minesweeper_Web_Application.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Diagnostics;
using Minesweeper_Web_Application.Services.Utility;
using System.Web.Script.Serialization;
using System;
using System.Reflection;

namespace Minesweeper_Web_Application.Services.Data
{
    public class LeaderBoardDAO
    {
        readonly string connectionStr = "Data Source=(localdb)\\MSSQLLocalDB;initial catalog=cst247_minesweeper ;Integrated Security=True;";
        public void InsertHighScore(UserModel user, decimal time, int totalClicks)
        {
            //If this returns true we'll break out of the method.
            //Only returns true if a user exists within the method.
            if(CheckUserScore(user, time, totalClicks))
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
                MineSweeperLogger.GetInstance().Info("Inserted high score for user: " + new JavaScriptSerializer().Serialize(user.UserName));
            }
            catch(SqlException e)
            {
                Debug.WriteLine("An error has occurred. Details: " + e.ToString());
                MineSweeperLogger.GetInstance().Error(e, "An error occurred.");
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
                        user.TotalClicks = reader.GetInt32(3);
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
                MineSweeperLogger.GetInstance().Error(e, "An error occurred.");
            }
            finally
            {
                conn.Close();
            }
            
            return inc;
        }

        public bool CheckUserScore(UserModel user, decimal time, int totalClicks)
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
                            UpdateUserScore(user.UserName, time, totalClicks);
                        }
                    }
                    newTimeCheck = true;
                }
            }
            catch(SqlException e)
            {
                Debug.WriteLine("Error generated. Details: " + e.ToString());
                MineSweeperLogger.GetInstance().Error(e, "An error occurred.");
            }
            finally
            {
                conn.Close();
            }

            return newTimeCheck;
        }

        private void UpdateUserScore(string username, decimal time, int totalClicks)
        {
            string updateQuery = "UPDATE dbo.leaderboard SET TIME = @Time, TOTALCLICKS = @TotalClicks WHERE USERNAME = @Username";
            SqlConnection conn = new SqlConnection(connectionStr);
            SqlCommand command = new SqlCommand(updateQuery, conn);

            try
            {
                command.Parameters.Add("@Username", System.Data.SqlDbType.VarChar, 25).Value = username;
                command.Parameters.Add("@Time", System.Data.SqlDbType.Decimal).Value = time;
                command.Parameters.Add("@TotalClicks", System.Data.SqlDbType.Int).Value = totalClicks;
                conn.Open();
                command.ExecuteNonQuery();

                MineSweeperLogger.GetInstance().Info("Updated high score for user: " + username + " " + time.ToString());
            }
            catch(SqlException e)
            {
                Debug.WriteLine("Error generated. Details: " + e.ToString());
                MineSweeperLogger.GetInstance().Error(e, "An error occurred.");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}