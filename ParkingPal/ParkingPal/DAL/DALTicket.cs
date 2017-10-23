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
        public static List<Ticket> GetTicket(int ticketId) {
            List<Ticket> tickets = null;
            try
            {
                using(SqlConnection sqlConn = DALCommon.NewConnection())
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
                           // Ticket ticket = new Ticket(
                            //    (int)reader["TicketId"],
                             //   reader["rego"]
                             //   );
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