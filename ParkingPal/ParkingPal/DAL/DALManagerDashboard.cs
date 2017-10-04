using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParkingPal.Models;
using System.Data.SqlClient;
using System.Data;

namespace ParkingPal.DAL
{
    public class DALManagerDashboard
    {
        // Retrieves an appUser by searching for one with a matching userName and userPassword.
        public static List<InspectorUser> GetManagerInspectors(int managerID)
        {
            // Initialise inspectorUsers:
            List<InspectorUser> inspectorUsers = null;

            try
            {
                using (SqlConnection sqlConn = DALCommon.NewConnection())
                {
                    // Set the SQL command and its parameters
                    SqlCommand sqlComm = new SqlCommand("dbo.sp_get_inspectors_for_manager", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@manager_id", SqlDbType.Int).Value = managerID;

                    // Open the SQL connection and run the command:
                    sqlConn.Open();
                    sqlComm.ExecuteNonQuery();
                    SqlDataReader reader = sqlComm.ExecuteReader();

                    // Retrieve the record if it exists:
                    if(reader.HasRows)
                    {
                        inspectorUsers = new List<InspectorUser>();
                        while (reader.Read())
                        {
                            AppUser appUser = new AppUser
                            (
                                (int)reader["AppUserID"],
                                reader["UserName"].ToString(),
                                reader["UserPassword"].ToString(),
                                reader["FirstName"].ToString(),
                                reader["LastName"].ToString(),
                                reader["AppUserType"].ToString().ToCharArray()[0]
                            );
                            Inspector inspector = new Inspector
                            (
                                (int)reader["InspectorID"],
                                (int)reader["AppUserID"],
                                (int)reader["ManagerID"]
                            );
                            inspectorUsers.Add(new InspectorUser(appUser, inspector));
                        }
                    }
                    
                    // Close the reader:
                    reader.Close();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return inspectorUsers;
        }

        // Deletes an Inspector and its associated AppUser entry:
        public static void DeleteInspector(int inspectorID)
        {
            try
            {
                using (SqlConnection sqlConn = DALCommon.NewConnection())
                {
                    // Set the SQL command and its parameters
                    SqlCommand sqlComm = new SqlCommand("dbo.sp_delete_inspector", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@inspector_id", SqlDbType.Int).Value = inspectorID;

                    // Open the SQL connection and run the command:
                    sqlConn.Open();
                    sqlComm.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        // Updates an Inspector and its associated AppUser entry:
        public static void UpdateInspector(int inspectorID, string password, string firstName, string lastName)
        {
            try
            {
                using (SqlConnection sqlConn = DALCommon.NewConnection())
                {
                    // Set the SQL command and its parameters
                    SqlCommand sqlComm = new SqlCommand("dbo.sp_update_inspector", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@inspector_id", SqlDbType.Int).Value = inspectorID;
                    sqlComm.Parameters.Add("@password", SqlDbType.VarChar, 50).Value = password;
                    sqlComm.Parameters.Add("@first_name", SqlDbType.VarChar, 50).Value = firstName;
                    sqlComm.Parameters.Add("@last_name", SqlDbType.VarChar, 50).Value = lastName;

                    // Open the SQL connection and run the command:
                    sqlConn.Open();
                    sqlComm.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        // Attempts to add an Inspector into the DB. Returns null if Inspector already exists.
        public static InspectorUser AddInspector(int managerID, string userName, string password,
            string firstName, string lastName)
        {
            // Initialise appUser:
            InspectorUser inspectorUser = null;

            try
            {
                using (SqlConnection sqlConn = DALCommon.NewConnection())
                {
                    // Set the SQL command and its parameters
                    SqlCommand sqlComm = new SqlCommand("dbo.sp_create_inspector", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@manager_id", SqlDbType.Int).Value = managerID;
                    sqlComm.Parameters.Add("@user_name", SqlDbType.VarChar, 50).Value = userName;
                    sqlComm.Parameters.Add("@password", SqlDbType.VarChar, 50).Value = password;
                    sqlComm.Parameters.Add("@first_name", SqlDbType.VarChar, 50).Value = firstName;
                    sqlComm.Parameters.Add("@last_name", SqlDbType.VarChar, 50).Value = lastName;
                    sqlComm.Parameters.Add("@new_inspector_id", SqlDbType.Int);
                    sqlComm.Parameters["@new_inspector_id"].Direction = ParameterDirection.Output;

                    // Open the SQL connection and run the command:
                    sqlConn.Open();
                    sqlComm.ExecuteNonQuery();

                    int newInspectorID = (int)sqlComm.Parameters["@new_inspector_id"].Value;
                    if (newInspectorID != -1)
                    {
                        sqlComm = new SqlCommand("dbo.sp_get_inspector_user", sqlConn);
                        sqlComm.CommandType = CommandType.StoredProcedure;
                        sqlComm.Parameters.Add("@inspector_id", SqlDbType.Int).Value = newInspectorID;
                        sqlComm.ExecuteNonQuery();
                        SqlDataReader reader = sqlComm.ExecuteReader();
                        // Retrieve the record if it exists:
                        while (reader.Read())
                        {
                            AppUser appUser = new AppUser
                            (
                                (int)reader["AppUserID"],
                                reader["UserName"].ToString(),
                                reader["UserPassword"].ToString(),
                                reader["FirstName"].ToString(),
                                reader["LastName"].ToString(),
                                'I'
                            );
                            Inspector inspector = new Inspector
                            (
                                (int)reader["InspectorID"],
                                (int)reader["AppUserID"],
                                (int)reader["ManagerID"]
                            );
                            inspectorUser = new InspectorUser(appUser, inspector);
                        }
                        // Close the reader:
                        reader.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return inspectorUser;
        }

        // Retrieves all the ParkingLots associated with a Manager.
        public static List<ParkingLot> GetManagerParkingLots(int managerID)
        {
            // Initialise ParkingLots:
            List<ParkingLot> parkingLots = null;

            try
            {
                using (SqlConnection sqlConn = DALCommon.NewConnection())
                {
                    // Set the SQL command and its parameters
                    SqlCommand sqlComm = new SqlCommand("dbo.sp_get_parking_lots_for_manager", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@manager_id", SqlDbType.Int).Value = managerID;

                    // Open the SQL connection and run the command:
                    sqlConn.Open();
                    sqlComm.ExecuteNonQuery();
                    SqlDataReader reader = sqlComm.ExecuteReader();

                    // Retrieve the record if it exists:
                    if (reader.HasRows)
                    {
                        parkingLots = new List<ParkingLot>();
                        while (reader.Read())
                        {
                            int? adminID = null;
                            if (reader["AdminID"] != null) adminID = (int)reader["AdminID"];

                            ParkingLot parkingLot = new ParkingLot
                            (
                                (int)reader["ParkingLotID"],
                                (int)reader["ManagerID"],
                                adminID,
                                reader["ApprovalStatus"].ToString().ToCharArray()[0],
                                reader["ShortName"].ToString(),
                                reader["LocationAddress"].ToString(),
                                reader["Coordinates"].ToString(),
                                (DateTime)reader["OpenTime"],
                                (DateTime)reader["CloseTime"]
                            );
                            parkingLots.Add(parkingLot);
                        }
                    }

                    // Close the reader:
                    reader.Close();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return parkingLots;
        }
    }
}