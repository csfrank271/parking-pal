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
    }
}