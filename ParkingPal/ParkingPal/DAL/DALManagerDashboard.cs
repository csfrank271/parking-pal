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
                            object adminIDObj = reader["AdminID"];
                            if (adminIDObj != DBNull.Value)
                            {
                                adminID = (int)reader["AdminID"];
                            }
                            ParkingLot parkingLot = new ParkingLot
                            (
                                (int)reader["ParkingLotID"],
                                adminID,
                                managerID,
                                reader["ApprovalStatus"].ToString(),
                                reader["ShortName"].ToString(),
                                reader["LocationAddress"].ToString(),
                                reader["Coordinates"].ToString(),
                                (TimeSpan)reader["OpenTime"],
                                (TimeSpan)reader["CloseTime"],
                                null
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

        // Updates a ParkingLot:
        public static void UpdateParkingLot(int parkingLotID, string shortName, string address,
            string coordinates, TimeSpan openTime, TimeSpan closeTime)
        {
            try
            {
                using (SqlConnection sqlConn = DALCommon.NewConnection())
                {
                    // Set the SQL command and its parameters
                    SqlCommand sqlComm = new SqlCommand("dbo.sp_update_parking_lot", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@parking_lot_id", SqlDbType.Int).Value = parkingLotID;
                    sqlComm.Parameters.Add("@short_name", SqlDbType.VarChar).Value = shortName;
                    sqlComm.Parameters.Add("@location_address", SqlDbType.VarChar, 50).Value = address;
                    sqlComm.Parameters.Add("@coordinates", SqlDbType.VarChar, 50).Value = coordinates;
                    sqlComm.Parameters.Add("@open_time", SqlDbType.Time).Value = openTime;
                    sqlComm.Parameters.Add("@close_time", SqlDbType.Time).Value = closeTime;

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

        // Attempts to add a ParkingLot into the DB.
        public static ParkingLot AddParkingLot(int managerID, string name, string address,
            string coordinates, TimeSpan openTime, TimeSpan closeTime)
        {
            // Initialise parking lot:
            ParkingLot parkingLot = null;

            try
            {
                using (SqlConnection sqlConn = DALCommon.NewConnection())
                {
                    // Set the SQL command to create the Parking Lot:
                    SqlCommand sqlComm = new SqlCommand("dbo.sp_create_parking_lot", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@manager_id", SqlDbType.Int).Value = managerID;
                    sqlComm.Parameters.Add("@short_name", SqlDbType.VarChar).Value = name;
                    sqlComm.Parameters.Add("@location_address", SqlDbType.VarChar).Value = address;
                    sqlComm.Parameters.Add("@coordinates", SqlDbType.VarChar, 255).Value = coordinates;
                    sqlComm.Parameters.Add("@open_time", SqlDbType.Time).Value = openTime;
                    sqlComm.Parameters.Add("@close_time", SqlDbType.Time).Value = closeTime;
                    sqlComm.Parameters.Add("@new_parking_lot_id", SqlDbType.Int);
                    sqlComm.Parameters["@new_parking_lot_id"].Direction = ParameterDirection.Output;

                    // Open the SQL connection and run the command:
                    sqlConn.Open();
                    sqlComm.ExecuteNonQuery();
                    int newParkingLotID = (int)sqlComm.Parameters["@new_parking_lot_id"].Value;

                    // Create the new ParkingLot object for ASP manipulation:
                    parkingLot = new ParkingLot (newParkingLotID, null, managerID, "U", name, address,
                        coordinates, openTime, closeTime, null);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return parkingLot;
        }
    }
}