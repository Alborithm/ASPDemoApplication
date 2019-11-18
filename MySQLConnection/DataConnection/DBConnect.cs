using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MySQLConnection.DataConnection
{
    public class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public string ErrorMessage { get; set; }

        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "localhost";
            database = "itm_oee_v0.1";
            uid = "root";
            password = "ThemuffinMan531";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    // Error Message to be implemented
                    case 0:
                        ErrorMessage = "Cannot connect to server.  Contact administrator";
                        break;

                    case 1045:
                        ErrorMessage = "Invalid username/password, please try again";
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        //Insert statement
        public void Insert()
        {
            string query = "INSERT INTO oee_disponibility " +
                "(`OEE_DISPONIBILITY_ID`, `MACHINE_ID`, `OEE_DISPONIBILITY_STATUS`, `FAIL_DICTIONARY_CODE`, `OEE_DISPONIBILITY_TIMESTAMP`) " +
                "VALUES" +
                "(1000, 1, 0, 'fpre0001', STR_TO_DATE(\"2017-08-14 10:40:10\", \"%Y-%m-%d %H:%i:%s\")) ";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Update statement
        public void Update()
        {
            string query = "UPDATE `itm_oee_v0.1`.`oee_disponibility` SET" +
                "`OEE_DISPONIBILITY_ID` = 1000," +
                "`MACHINE_ID` = 1," +
                "`OEE_DISPONIBILITY_STATUS` = 1," +
                "`FAIL_DICTIONARY_CODE` = null," +
                "`OEE_DISPONIBILITY_TIMESTAMP` = STR_TO_DATE(\"2017-08-14 10:40:10\", \"%Y-%m-%d %H:%i:%s\")" +
                "WHERE `OEE_DISPONIBILITY_ID` = 1000; ";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Delete statement
        public void Delete()
        {
            string query = "DELETE FROM `oee_disponibility` WHERE `OEE_DISPONIBILITY_ID` = 1000 ";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Select statement
        public List<string>[] Select()
        {
            string query = "SELECT * FROM `oee_disponibility`";

            //Create a list to store the result
            List<string>[] list = new List<string>[5];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
            list[4] = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["OEE_DISPONIBILITY_ID"] + "");
                    list[1].Add(dataReader["MACHINE_ID"] + "");
                    list[2].Add(dataReader["OEE_DISPONIBILITY_STATUS"] + "");
                    list[3].Add(dataReader["FAIL_DICTIONARY_CODE"] + "");
                    list[4].Add(dataReader["OEE_DISPONIBILITY_TIMESTAMP"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        //Count statement
        public int Count()
        {
            string query = "SELECT Count(*) FROM oee.disponibility";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        //Backup
        public void Backup()
        {
            // To be implented
        }

        //Restore
        public void Restore()
        {
            // To be implemented
        }
    }
}
