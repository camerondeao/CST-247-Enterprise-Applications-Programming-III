using Minesweeper_Web_Application.Models;
using Minesweeper_Web_Application.Services.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Minesweeper_Web_Application.Services.Data
{
    public class GameDAO
    {
        readonly string connectionStr = "Data Source=(localdb)\\MSSQLLocalDB;initial catalog=cst247_minesweeper ;Integrated Security=True;";
        SqlConnection conn = null;

        public bool SaveGameBombs(GameBoardModel board, UserModel user, int totalClicks)
        {
            bool saved = false;
            EstablishConnection();

            if(CheckUser(user))
            {
                if(!CheckIfScoreExists(user))
                {
                    NewUserScore(board, user, totalClicks);
                }
                else
                {
                    UpdateUserScore(board, user, totalClicks);
                }
            }
            else
            {
                InsertUserScore(board, user, totalClicks);
            }

            return saved;
        }

        public void UpdateUserScore(GameBoardModel board, UserModel user, int totalClicks)
        {
            string query = "UPDATE dbo.gamestate SET SAVEBOMB1 = @BOMB1, SAVEBOMB2 = @BOMB2, SAVEBOMB3 = @BOMB3, SAVEBOMB4 = @BOMB4, " +
                "SAVEVISITED1 = @SAVEVISITED1, SAVEVISITED2 = @SAVEVISITED2, SAVEVISITED3 = @SAVEVISITED3, SAVEVISITED4 = @SAVEVISITED4, " +
                "CURRENTCLICKS = @TOTALCLICKS WHERE USERNAME = @USERNAME";
            SqlCommand command = new SqlCommand(query, conn);

            try
            {
                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 25).Value = user.UserName;
                command.Parameters.Add("@BOMB1", System.Data.SqlDbType.BigInt).Value = board.SaveBomb1;
                command.Parameters.Add("@BOMB2", System.Data.SqlDbType.BigInt).Value = board.SaveBomb2;
                command.Parameters.Add("@BOMB3", System.Data.SqlDbType.BigInt).Value = board.SaveBomb3;
                command.Parameters.Add("@BOMB4", System.Data.SqlDbType.BigInt).Value = board.SaveBomb4;
                command.Parameters.Add("@SAVEVISITED1", System.Data.SqlDbType.BigInt).Value = board.SaveVisited1;
                command.Parameters.Add("@SAVEVISITED2", System.Data.SqlDbType.BigInt).Value = board.SaveVisited2;
                command.Parameters.Add("@SAVEVISITED3", System.Data.SqlDbType.BigInt).Value = board.SaveVisited3;
                command.Parameters.Add("@SAVEVISITED4", System.Data.SqlDbType.BigInt).Value = board.SaveVisited4;
                command.Parameters.Add("@TOTALCLICKS", System.Data.SqlDbType.Int).Value = totalClicks;

                Debug.WriteLine("UPDATING Data going into DB: Bomb1:" + board.SaveBomb1 + " Bomb2:" + board.SaveBomb2 + " Bomb3:" + board.SaveBomb3 + " Bomb4:" + board.SaveBomb4 +
                    " Visited1:" + board.SaveVisited1 + " Visited2:" + board.SaveVisited2 + " Visited3:" + board.SaveVisited3 + " Visted4:" + board.SaveVisited4);

                MineSweeperLogger.GetInstance().Info(String.Format("Updated board state for {0}.", user.UserName));

                conn.Open();
                command.ExecuteNonQuery();
                Debug.WriteLine("Data is updated");
            }
            catch (SqlException e)
            {
                Debug.WriteLine("Error generated. Details: " + e);
                MineSweeperLogger.GetInstance().Error(e, String.Format("Error occurred updating {0}'s board state.", user.UserName));
            }
            finally
            {
                conn.Close();
            }
        }

        public void NewUserScore(GameBoardModel board, UserModel user, int totalClicks)
        {
            string query = "UPDATE dbo.gamestate SET SAVEBOMB1 = @BOMB1, SAVEBOMB2 = @BOMB2, SAVEBOMB3 = @BOMB3, SAVEBOMB4 = @BOMB4, " +
                "SAVEVISITED1 = @SAVEVISITED1, SAVEVISITED2 = @SAVEVISITED2, SAVEVISITED3 = @SAVEVISITED3, SAVEVISITED4 = @SAVEVISITED4, " +
                "CURRENTCLICKS = @TOTALCLICKS WHERE USERNAME = @USERNAME";
            SqlCommand command = new SqlCommand(query, conn);

            try
            {
                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 25).Value = user.UserName;
                command.Parameters.Add("@BOMB1", System.Data.SqlDbType.BigInt).Value = board.SaveBomb1;
                command.Parameters.Add("@BOMB2", System.Data.SqlDbType.BigInt).Value = board.SaveBomb2;
                command.Parameters.Add("@BOMB3", System.Data.SqlDbType.BigInt).Value = board.SaveBomb3;
                command.Parameters.Add("@BOMB4", System.Data.SqlDbType.BigInt).Value = board.SaveBomb4;
                command.Parameters.Add("@SAVEVISITED1", System.Data.SqlDbType.BigInt).Value = board.SaveVisited1;
                command.Parameters.Add("@SAVEVISITED2", System.Data.SqlDbType.BigInt).Value = board.SaveVisited2;
                command.Parameters.Add("@SAVEVISITED3", System.Data.SqlDbType.BigInt).Value = board.SaveVisited3;
                command.Parameters.Add("@SAVEVISITED4", System.Data.SqlDbType.BigInt).Value = board.SaveVisited4;
                command.Parameters.Add("@TOTALCLICKS", System.Data.SqlDbType.Int).Value = totalClicks;

                Debug.WriteLine("Data going into DB: Bomb1:" + board.SaveBomb1 + " Bomb2:" + board.SaveBomb2 + " Bomb3:" + board.SaveBomb3 + " Bomb4:" + board.SaveBomb4+
                    " Visited1:"+board.SaveVisited1 + " Visited2:" + board.SaveVisited2 + " Visited3:" + board.SaveVisited3 + " Visted4:" + board.SaveVisited4);
                MineSweeperLogger.GetInstance().Info(String.Format("Inserted new board state for {0}.", user.UserName));
                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Debug.WriteLine("Error generated. Details: " + e);
                MineSweeperLogger.GetInstance().Error(e, String.Format("Error occurred when inserting board state for {0}.", user.UserName));
            }
            finally
            {
                conn.Close();
            }
        }

        public void DeleteScore(UserModel user)
        {
            EstablishConnection();
            string query = "UPDATE dbo.gamestate SET SAVEBOMB1 = @BOMB1, SAVEBOMB2 = @BOMB2, SAVEBOMB3 = @BOMB3, SAVEBOMB4 = @BOMB4, " +
                "SAVEVISITED1 = @SAVEVISITED1, SAVEVISITED2 = @SAVEVISITED2, SAVEVISITED3 = @SAVEVISITED3, SAVEVISITED4 = @SAVEVISITED4, " +
                "CURRENTCLICKS = @TOTALCLICKS WHERE USERNAME = @USERNAME";
            SqlCommand command = new SqlCommand(query, conn);

            try
            {
                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 25).Value = user.UserName;
                command.Parameters.Add("@BOMB1", System.Data.SqlDbType.BigInt).Value = -1;
                command.Parameters.Add("@BOMB2", System.Data.SqlDbType.BigInt).Value = 0;
                command.Parameters.Add("@BOMB3", System.Data.SqlDbType.BigInt).Value = 0;
                command.Parameters.Add("@BOMB4", System.Data.SqlDbType.BigInt).Value = 0;
                command.Parameters.Add("@SAVEVISITED1", System.Data.SqlDbType.BigInt).Value = 0;
                command.Parameters.Add("@SAVEVISITED2", System.Data.SqlDbType.BigInt).Value = 0;
                command.Parameters.Add("@SAVEVISITED3", System.Data.SqlDbType.BigInt).Value = 0;
                command.Parameters.Add("@SAVEVISITED4", System.Data.SqlDbType.BigInt).Value = 0;
                command.Parameters.Add("@TOTALCLICKS", System.Data.SqlDbType.Int).Value = 0;
                conn.Open();
                command.ExecuteNonQuery();
                MineSweeperLogger.GetInstance().Info(String.Format("Deleted board state for {0}.", user.UserName));
            }
            catch(SqlException e)
            {
                Debug.WriteLine("Error generated. Details: " + e);
                MineSweeperLogger.GetInstance().Error(e, String.Format("Error occurred when deleting {0}'s board state.", user.UserName));
            }
            finally
            {
                conn.Close();
            }
        }

        public void InsertUserScore(GameBoardModel board, UserModel user, int totalClicks)
        {
            string query = "INSERT INTO dbo.gamestate (USERNAME, SAVEBOMB1, SAVEBOMB2, SAVEBOMB3, SAVEBOMB4, SAVEVISITED1, SAVEVISITED2, SAVEVISITED3, SAVEVISITED4, CURRENTCLICKS)"
                + " VALUES (@USERNAME, @SAVEBOMB1, @SAVEBOMB2, @SAVEBOMB3, @SAVEBOMB4, @SAVEVISITED1, @SAVEVISITED2, @SAVEVISITED3, @SAVEVISITED4, @TOTALCLICKS)";
            SqlCommand command = new SqlCommand(query, conn);

            try
            {
                command.Parameters.AddWithValue("@USERNAME", user.UserName);
                command.Parameters.AddWithValue("@SAVEBOMB1", board.SaveBomb1);
                command.Parameters.AddWithValue("@SAVEBOMB2", board.SaveBomb2);
                command.Parameters.AddWithValue("@SAVEBOMB3", board.SaveBomb3);
                command.Parameters.AddWithValue("@SAVEBOMB4", board.SaveBomb4);
                command.Parameters.AddWithValue("@SAVEVISITED1", board.SaveVisited1);
                command.Parameters.AddWithValue("@SAVEVISITED2", board.SaveVisited2);
                command.Parameters.AddWithValue("@SAVEVISITED3", board.SaveVisited3);
                command.Parameters.AddWithValue("@SAVEVISITED4", board.SaveVisited4);
                command.Parameters.AddWithValue("@TOTALCLICKS", totalClicks);

                conn.Open();
                command.ExecuteNonQuery();
                MineSweeperLogger.GetInstance().Info(String.Format("Inserted new board state for {0}.", user.UserName));
            }
            catch(SqlException e)
            {
                Debug.WriteLine("Error generated. Details: " + e);
                MineSweeperLogger.GetInstance().Error(e, String.Format("Error occurred when inserting board state for {0}.", user.UserName));
            }
            finally
            {
                conn.Close();
            }
        }

        public bool CheckUser(UserModel user)
        {
            string query = "SELECT 1 FROM dbo.gamestate WHERE USERNAME = @Username";
            SqlCommand command = new SqlCommand(query, conn);

            try
            {
                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 25).Value = user.UserName;
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    conn.Close();
                    return true;
                }
            }
            catch(SqlException e)
            {
                Debug.WriteLine("Error generated. Details: " + e);
                MineSweeperLogger.GetInstance().Error(e, String.Format("Error occurred when check board state for {0}.", user.UserName));
            }
            finally
            {
                conn.Close();
            }

            return false;
        }

        public bool CheckIfScoreExists(UserModel user)
        {
            if(conn == null)
            {
                EstablishConnection();
            }
            string query = "SELECT 1 FROM dbo.gamestate WHERE USERNAME = @Username AND SAVEBOMB1 != -1";
            SqlCommand command = new SqlCommand(query, conn);
            try
            {
                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 25).Value = user.UserName;
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    conn.Close();
                    return true;
                }
            }
            catch(SqlException e)
            {
                Debug.WriteLine("Error generated. Details: " + e);
                MineSweeperLogger.GetInstance().Error(e, String.Format("An error occurred when checking board state for {0}.", user.UserName));
            }
            finally
            {
                conn.Close();
            }
            return false;
        }

        public List<long> RetrieveUserScore(List<long> inc, UserModel user)
        {
            EstablishConnection();
            string query = "SELECT * FROM dbo.gamestate WHERE USERNAME = @Username";
            SqlCommand command = new SqlCommand(query, conn);
            
            try
            {
                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 25).Value = user.UserName;
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        inc.Add(reader.GetInt64(2));
                        inc.Add(reader.GetInt64(3));
                        inc.Add(reader.GetInt64(4));
                        inc.Add(reader.GetInt64(5));
                        inc.Add(reader.GetInt64(6));
                        inc.Add(reader.GetInt64(7));
                        inc.Add(reader.GetInt64(8));
                        inc.Add(reader.GetInt64(9));
                        inc.Add(reader.GetInt32(10));
                    }
                }
            }
            catch(SqlException e)
            {
                Debug.WriteLine("Error generated. Details: " + e);
                MineSweeperLogger.GetInstance().Error(e, String.Format("An error occurred during retrieval of board state for {0}.", user.UserName));
            }
            finally
            {
                conn.Close();
            }
            return inc;
        }

        public void EstablishConnection()
        {
            conn = new SqlConnection(connectionStr);
        }
    }
}