using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ParkingPal.DAL
{
    public class DALLogin
    {
        public static char GetAppUserType(string userName,
            string password)
        {
            SqlConnection sqlConn = DALCommon.NewConnection();
            SqlCommand sqlComm = new SqlCommand("dbo.sp_get_user_type", sqlConn);

            char userType = '?';

            try
            {
                // Open the SQL connection. Set the command type to 'stored procedure':
                sqlConn.Open();
                sqlComm.CommandType = CommandType.StoredProcedure;
                // Set the input parameter(s):
                sqlComm.Parameters.Add("@user_name", SqlDbType.VarChar, 50).Value = userName;
                sqlComm.Parameters.Add("@user_password", SqlDbType.VarChar, 50).Value = password;
                sqlComm.Parameters.Add("@user_type", SqlDbType.Char, 1);
                sqlComm.Parameters["@user_type"].Direction = ParameterDirection.Output;

                // Run the command and retrieve output:
                sqlComm.ExecuteNonQuery();
                string test = (string)sqlComm.Parameters["@user_type"].Value;
                userType = test.ToCharArray()[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConn.Close();
            }

            return userType;
        }
    }
}