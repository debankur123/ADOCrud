using ADOCrud.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ADOCrud.Service
{
    public class UserDAL
    {
        private static string connectionString = "Data Source=KIB-012173-LT\\SQLSERVER2019;Initial Catalog=MvcCrudAdo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        SqlConnection con = new SqlConnection(connectionString);

        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter sda;
        DataTable dt;

        public List<UserModel> Getusers()
        {
            cmd = new SqlCommand("sp_select",con);
            cmd.CommandType = CommandType.StoredProcedure; // This defines what type of command we are passing
            sda = new SqlDataAdapter(cmd);// This helps to store data in dataTable at time of data retrival
            dt = new DataTable(); 
            sda.Fill(dt); // Stores data in data table's object


            List<UserModel> list = new List<UserModel>();
            foreach (DataRow dr in dt.Rows) // To insert rows in List in datatable where the filels are in user table
            {
                list.Add(new UserModel
                {
                    Id = Convert.ToInt32(dr["Id"]), // inserting data into data table
                    Name = dr["Name"].ToString(),
                    Email = dr["Email"].ToString(),
                    Age = Convert.ToInt32(dr["Age"])
                });
            }
            return list;

        }

        public bool InsertUser(UserModel user)
        {
            try
            {
                cmd = new SqlCommand("sp_insert", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Here we are addding/passing values to database
                cmd.Parameters.AddWithValue("@name", user.Name);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@age", user.Age);

                con.Open(); // To open the sql server bridge
                int r = cmd.ExecuteNonQuery(); // returns the no of rows affected as it is a sp
                if (r > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateUser(UserModel user)
        {
            try
            {
                cmd = new SqlCommand("sp_update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", user.Name);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@age", user.Age);
                cmd.Parameters.AddWithValue("@id", user.Id);

                con.Open();
                int r = cmd.ExecuteNonQuery();
                if (r > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int DeleteUser(int id)
        {
            try
            {
                cmd = new SqlCommand("sp_delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id",id);
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
