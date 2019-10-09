using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace EnterDataToDataBase
{
    public static class GetHttpRequest
    {
        [FunctionName("GetHttpRequest")]
        public static async Task<string> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var names = new List<string>();

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var str = Environment.GetEnvironmentVariable("sqldb_connection");
            using (SqlConnection conn = new SqlConnection(str))
            {
            conn.Open();
                var text = "Select name from Name;";

                using (SqlCommand cmd = new SqlCommand(text, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var name = reader["Name"].ToString();
                            names.Add(name);
                        }
                    }
                }

            }


            var returnType = JsonConvert.SerializeObject(names);
            //var returnType = "{"+"\"names\""+":"+ JsonConvert.SerializeObject(names) + "}";
            return returnType;
        }
    }
}
