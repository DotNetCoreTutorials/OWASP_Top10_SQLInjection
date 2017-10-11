using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace OWASPTop10.SqlInjection.Web.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("nonsensitive")]
        public string GetNonSensitiveDataById()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetValue<string>("ConnectionString")))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM NonSensitiveDataTable WHERE Id = {Request.Query["id"]}", connection);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string returnString = string.Empty;
                        returnString += $"Name : {reader["Name"]}. ";
                        returnString += $"Description : {reader["Description"]}";
                        return returnString;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
        }

        [HttpGet]
        [Route("nonsensitiveroute/{id}")]
        public string GetNonSensitiveDataByIdWithRoute(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetValue<string>("ConnectionString")))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM NonSensitiveDataTable WHERE Id = {id}", connection);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string returnString = string.Empty;
                        returnString += $"Name : {reader["Name"]}. ";
                        returnString += $"Description : {reader["Description"]}";
                        return returnString;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
        }

        [HttpGet]
        [Route("nonsensitivewithparam")]
        public string GetNonSensitiveDataByNameWithParam()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetValue<string>("ConnectionString")))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM NonSensitiveDataTable WHERE Name = @name", connection);
                command.Parameters.AddWithValue("@name", Request.Query["name"].ToString());
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string returnString = string.Empty;
                        returnString += $"Name : {reader["Name"]}. ";
                        returnString += $"Description : {reader["Description"]}";
                        return returnString;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
        }

        [HttpGet]
        [Route("nonsensitivewithsp")]
        public string GetNonSensitiveDataByNameWithSP()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetValue<string>("ConnectionString")))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SP_GetNonSensitiveDataByName", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@name", Request.Query["name"].ToString());
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string returnString = string.Empty;
                        returnString += $"Name : {reader["Name"]}. ";
                        returnString += $"Description : {reader["Description"]}";
                        return returnString;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
        }
    }
}
