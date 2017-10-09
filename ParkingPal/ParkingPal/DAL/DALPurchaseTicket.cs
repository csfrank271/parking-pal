using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParkingPal.Models;
using System.Data.SqlClient;
using System.Data;


namespace ParkingPal.DAL
{
    public class DALPurchaseTicket
    {
        public static List<Rate> GetRate(decimal timeDuration, int carparkLocation)
        {
            List<Rate> rates = null;
            try
            {
                using (SqlConnection sqlConn = DALCommon.NewConnection())
                {
                    SqlCommand sqlComm = new SqlCommand("dbo.sp_get_rate", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@time_duration", SqlDbType.Decimal).Value = timeDuration;
                    sqlComm.Parameters.Add("@parkinglot_id", SqlDbType.Int).Value = carparkLocation;

                    sqlConn.Open();
                    var test = sqlComm.ExecuteNonQuery();
                    SqlDataReader reader = sqlComm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        rates = new List<Rate>();
                        while (reader.Read())
                        {
                            Rate rate = new Rate(
                                (int)reader["RateId"],
                                (int)reader["ParkingLotId"],
                                (decimal)reader["TimeDuration"],
                                (decimal)reader["HalfHourlyRate"]
                                );
                            rates.Add(rate);
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return rates;
        }

        public static List<ParkingBay> GetCarparkTypes(List<ParkingLot> parkingLots)
        {
            IEnumerable<int> parkingLotIds;
            parkingLotIds = parkingLots.Select(parkingLot => parkingLot.ID);
            List<ParkingBay> carparkTypes = null;
            try
            {
                using (SqlConnection sqlConn = DALCommon.NewConnection())
                {
                    SqlCommand sqlComm = new SqlCommand("dbo.sp_get_related_carpark_types", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlConn.Open();
                    sqlComm.ExecuteNonQuery();
                    SqlDataReader reader = sqlComm.ExecuteReader();

                    if (reader.HasRows)
                    {
                        carparkTypes = new List<ParkingBay>();
                        while (reader.Read())
                        {
                            ParkingBay carparkType = new ParkingBay(
                                (int)reader["ParkingLotID"],
                                (int)reader["CarparkTypeID"],
                                (int)reader["NumberOfParks"],
                                reader["CarparkType"].ToString()
                                );
                            carparkTypes.Add(carparkType);
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return carparkTypes;
        }

        public static ParkingLot GetParkingLot(int parkingLotLocation)
        {
            List<ParkingLot> parkingLots = null;
            try
            {
                using (SqlConnection sqlConn = DALCommon.NewConnection())
                {
                    SqlCommand sqlComm = new SqlCommand("dbo.sp_get_parkinglot", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@parkinglot_id", SqlDbType.Int).Value = parkingLotLocation;

                    sqlConn.Open();
                    var test = sqlComm.ExecuteNonQuery();
                    SqlDataReader reader = sqlComm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        parkingLots = new List<ParkingLot>();
                        while (reader.Read())
                        {
                            ParkingLot parkingLot = new ParkingLot(
                               (int)reader["ID"],
                               (int)reader["ManagerID"],
                               reader["ApprovalStatus"].ToString(),
                               reader["ShortName"].ToString(),
                               reader["LocationAddress"].ToString(),
                               reader["Coordinates"].ToString(),
                               reader["OpenTime"].ToString(),
                               reader["CloseTime"].ToString(),
                               (int)reader["AdminID"],
                               null
                               );
                            parkingLots.Add(parkingLot);
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return parkingLots.First();
        }

        public static ParkingBay GetCarparkType(int carparkType, int parkingLot)
        {
            List<ParkingBay> parkingBays = null;
            try
            {
                using (SqlConnection sqlConn = DALCommon.NewConnection())
                {
                    SqlCommand sqlComm = new SqlCommand("dbo.sp_get_carpark_type", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@carpark_type_id", SqlDbType.Int).Value = carparkType;
                    sqlComm.Parameters.Add("@parking_lot_id", SqlDbType.Int).Value = parkingLot;

                    sqlConn.Open();
                    var test = sqlComm.ExecuteNonQuery();
                    SqlDataReader reader = sqlComm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        parkingBays = new List<ParkingBay>();
                        while (reader.Read())
                        {
                            ParkingBay parkingBay = new ParkingBay(
                                (int)reader["ParkingLotID"],
                                (int)reader["CarparkTypeID"],
                                (int)reader["NumberOfParks"],
                                reader["CarparkType"].ToString()
                                );
                            parkingBays.Add(parkingBay);
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return parkingBays.First();
        }

        public static List<ParkingLot> GetParkingLots()
        {
            List<ParkingLot> parkingLots = null;
            try
            {
                using (SqlConnection sqlConn = DALCommon.NewConnection())
                {
                    SqlCommand sqlComm = new SqlCommand("dbo.sp_get_all_parkingLot", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlConn.Open();
                    sqlComm.ExecuteNonQuery();
                    SqlDataReader reader = sqlComm.ExecuteReader();

                    if (reader.HasRows)
                    {
                        parkingLots = new List<ParkingLot>();
                        while (reader.Read())
                        {
                            ParkingLot parkingLot = new ParkingLot(
                                (int)reader["ID"],
                                (int)reader["ManagerID"],
                                reader["ApprovalStatus"].ToString(),
                                reader["ShortName"].ToString(),
                                reader["LocationAddress"].ToString(),
                                reader["Coordinates"].ToString(),
                                reader["OpenTime"].ToString(),
                                reader["CloseTime"].ToString(),
                                (int)reader["AdminID"],
                                null
                                );
                            parkingLots.Add(parkingLot);
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return parkingLots;
        }

        private static string split(IEnumerable<int> inString)
        {
            string returnString = "";
            foreach (int i in inString)
            {
                returnString += "" + i + ",";
            }

            return returnString.Substring(0, returnString.Length);
        }


        public static int AddTicket(Ticket ticket)
        {
            int newTicketID = -1;
            try
            {
                using (SqlConnection sqlConn = DALCommon.NewConnection())
                {
                    SqlCommand sqlComm = new SqlCommand("dbo.sp_insert_ticket", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@ParkingLotID", SqlDbType.Int).Value = ticket.ParkingLotObject.ID;
                    sqlComm.Parameters.Add("@CarparkTypeID", SqlDbType.Int).Value = ticket.ParkingBayObject.CarparkTypeID;
                    sqlComm.Parameters.Add("@Rego", SqlDbType.NVarChar).Value = ticket.Rego;
                    sqlComm.Parameters.Add("@StartDateTime", SqlDbType.DateTime2).Value = ticket.StartDateTime;
                    sqlComm.Parameters.Add("@EndDateTime", SqlDbType.DateTime2).Value = ticket.EndDateTime;
                    sqlComm.Parameters.Add("@Rate", SqlDbType.Decimal).Value = ticket.Rate;
                    sqlComm.Parameters["@new_ticket_id"].Direction = ParameterDirection.Output;

                    sqlConn.Open();
                    sqlComm.ExecuteNonQuery();

                    newTicketID = (int)sqlComm.Parameters["@new_ticket_id"].Value;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return newTicketID;
        }
    }
}