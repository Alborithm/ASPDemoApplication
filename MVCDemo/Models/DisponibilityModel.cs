using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySQLConnection.DataConnection;

namespace MVCDemo.Models
{
    public class DisponibilityModel
    {
        public string OEEDisponibilityID { get; set; }
        public string MachineID { get; set; }
        public string OEEDisponibilityStatus { get; set; }
        public string FailDictionaryCode { get; set; }
        public string OEEDisponibilityTimeStamp { get; set; }

        public static List<DisponibilityModel> GetDisponibilityModels()
        {
            List<DisponibilityModel> disponibilityModels = new List<DisponibilityModel>();
            // Creates instance of database connection
            DBConnect disponibilityDB = new DBConnect();

            // Select everything from database
            List<string>[] list = disponibilityDB.Select();

            for (int i = 0; i < list[0].Count; i++)
            {
                disponibilityModels.Add(new DisponibilityModel
                { OEEDisponibilityID = list[0][i], MachineID = list[1][i], OEEDisponibilityStatus = list[2][i], FailDictionaryCode= list[3][i], OEEDisponibilityTimeStamp = list[4][i] });
            }

            return disponibilityModels;
        }

        public static string[] GetBarChartFailData()
        {
            List<string>[] barChartData = new List<string>[2];

            List<DisponibilityModel> disponibilityModels = GetDisponibilityModels();

            barChartData[0] = (from temp in disponibilityModels
                               select temp.FailDictionaryCode).ToList();

            // Fail Names
            barChartData[0] = new List<string>();
            barChartData[1] = new List<string>();


            // Scans the collection
            foreach (var item in disponibilityModels)
            {
                // If disponibility status is false means the machine is on failure and has fail data
                if (item.OEEDisponibilityStatus == "False")
                {
                    // Adds fail name if not already added
                    if (!barChartData[0].Contains(item.FailDictionaryCode))
                    {
                        barChartData[0].Add(item.FailDictionaryCode);
                    }
                }
            }
            for (int i = 0; i < barChartData[0].Count; i++)
            {
                int failCount = disponibilityModels.FindAll(element => element.FailDictionaryCode.Equals(barChartData[0][i])).Count;
                barChartData[1].Add(failCount.ToString());

                // Add quotes to the names
                barChartData[0][i] = "\'" + barChartData[0][i] + "\'";
            }

            string[] result = new string[2];
            result[0] = string.Join(",", barChartData[0]);
            result[1] = string.Join(",", barChartData[1]);

            return result;
        }


    }
}