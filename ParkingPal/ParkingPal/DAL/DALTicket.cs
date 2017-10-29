using ParkingPal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ParkingPal.DAL
{
    public class DALTicket
    {
        public static int GetUsedParkingLots(int parkingLotId)
        {
            int used = 0;
            try
            {
                using (SqlConnection sqlConn = DALCommon.NewConnection())
                {
                    SqlCommand sqlComm = new SqlCommand("dbo.sp_current_carpark_status", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@CarparkId", SqlDbType.Int).Value = parkingLotId;

                    sqlConn.Open();
                    sqlComm.ExecuteNonQuery();
                    SqlDataReader reader = sqlComm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            used = (int)reader["Amount"];
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return used;
        }

        public static int GetTotalParkingLots(int parkingLotId)
        {
            int used = 0;
            try
            {
                using (SqlConnection sqlConn = DALCommon.NewConnection())
                {
                    SqlCommand sqlComm = new SqlCommand("dbo.sp_total_carparks", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@CarparkId", SqlDbType.Int).Value = parkingLotId;

                    sqlConn.Open();
                    sqlComm.ExecuteNonQuery();
                    SqlDataReader reader = sqlComm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            used = (int)reader["Amount"];
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return used;
        }

        public static Ticket UpdateTicket (Ticket ticket)
        {
            Ticket returnTicket = ticket;
            try
            {
                using (SqlConnection sqlConn = DALCommon.NewConnection())
                {
                    SqlCommand sqlComm = new SqlCommand("dbo.sp_update_ticket", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@TicketId", SqlDbType.Int).Value = ticket.TicketID;
                    sqlComm.Parameters.Add("@EndDateTime", SqlDbType.DateTime2).Value = ticket.EndDateTime;

                    sqlConn.Open();
                    sqlComm.ExecuteNonQuery();
                }
            }
            catch(Exception exception)
            {
                throw exception;
            }
            return returnTicket;
        }
        public static string GetType(int typeId, int parkingLotId)
        {
            string type = "";
            try
            {
                using (SqlConnection sqlConn = DALCommon.NewConnection())
                {
                    SqlCommand sqlComm = new SqlCommand("dbo.sp_get_carpark_type", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@carpark_type_id", SqlDbType.Int).Value = typeId;
                    sqlComm.Parameters.Add("parking_lot_id", SqlDbType.Int).Value = parkingLotId;
                    sqlConn.Open();
                    sqlComm.ExecuteNonQuery();
                    SqlDataReader reader = sqlComm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return reader["CarparkType"].ToString();
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return type;
        }

        public static TimeSpan GetParkingLotEndTime(int locationId)
        {
            TimeSpan endTime = new TimeSpan(23, 59, 59);
            try
            {
                using (SqlConnection sqlConn = DALCommon.NewConnection())
                {
                    SqlCommand sqlComm = new SqlCommand("dbo.sp_get_parkinglot", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@parkinglot_id", SqlDbType.Int).Value = locationId;
                    sqlConn.Open();
                    sqlComm.ExecuteNonQuery();
                    SqlDataReader reader = sqlComm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return (TimeSpan)reader["CloseTime"];
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return endTime;

        }

        public static string GetLocation(int locationId)
        {
            string location = "";
            try
            {
                using (SqlConnection sqlConn = DALCommon.NewConnection())
                {
                    SqlCommand sqlComm = new SqlCommand("dbo.sp_get_parkinglot", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@parkinglot_id", SqlDbType.Int).Value = locationId;

                    sqlConn.Open();
                    sqlComm.ExecuteNonQuery();
                    SqlDataReader reader = sqlComm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return reader["ShortName"].ToString();
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return location;
        }
        public static List<Ticket> GetTicket(int ticketId)
        {
            List<Ticket> tickets = null;
            try
            {
                using (SqlConnection sqlConn = DALCommon.NewConnection())
                {
                    SqlCommand sqlComm = new SqlCommand("dbo.sp_get_ticket", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@ticketId", SqlDbType.Int).Value = ticketId;

                    sqlConn.Open();
                    sqlComm.ExecuteNonQuery();
                    SqlDataReader reader = sqlComm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        tickets = new List<Ticket>();
                        while (reader.Read())
                        {
                            Ticket y = new Ticket
                           (
                                (int)reader["ID"],
                                reader["Rego"].ToString(),
                                (DateTime)reader["StartDateTime"],
                                (DateTime)reader["EndDateTime"],
                                (decimal)reader["Rate"],
                                reader["ParkingLotID"].ToString(),
                                reader["CarparkTypeID"].ToString(),
                                null,
                                null,
                                null,
                                null,
                                reader["Email"].ToString()
                           );
                            var test = y;
                            tickets.Add(y);
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return tickets;
        }
    }
}
