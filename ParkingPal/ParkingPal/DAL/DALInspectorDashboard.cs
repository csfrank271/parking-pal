using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParkingPal.Models;
using System.Data.SqlClient;
using System.Data;

namespace ParkingPal.DAL
{
    public class DALInspectorDashboard
    {
        // Retrieves a list of non-expired Tickets associated with a registration number.
        public static List<Ticket> GetCurrentTicketsForRego(string rego)
        {
            // Initialise tickets:
            List<Ticket> tickets = null;

            try
            {
                using (SqlConnection sqlConn = DALCommon.NewConnection())
                {
                    // Set the SQL command and its parameters
                    SqlCommand sqlComm = new SqlCommand("dbo.sp_get_current_tickets_for_registration", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@registration_number", SqlDbType.VarChar, 7).Value = rego;

                    // Open the SQL connection and run the command:
                    sqlConn.Open();
                    sqlComm.ExecuteNonQuery();
                    SqlDataReader reader = sqlComm.ExecuteReader();

                    // Retrieve the record if it exists:
                    if (reader.HasRows)
                    {
                        tickets = new List<Ticket>();
                        while (reader.Read())
                        {
                            Ticket ticket = new Ticket
                            (
                                (int)reader["TicketID"],
                                reader["Rego"].ToString(),
                                (DateTime)reader["StartDateTime"],
                                (DateTime)reader["EndDateTime"],
                                (float)reader["Rate"],
                                reader["ShortName"].ToString()
                            );
                            tickets.Add(ticket);
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

            return tickets;
        }
    }
}