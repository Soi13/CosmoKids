using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace CosmoKids
{
    class ConnectDB
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public ConnectDB()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "127.0.0.1";
            database = "cosmokids_db";
            uid = "root";
            password = "89302810";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //Open connection to database
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
                    case 0:
                        MessageBox.Show("Cannot connect to server.");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
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
                MessageBox.Show(ex.Message);
                return false;
            }
        }

      
        /*
        public void LoadData(DataGridView d1)
        {            
            if (this.OpenConnection() == true)
            {                
                MySqlCommand command = new MySqlCommand("SELECT * FROM daycare", connection);
                MySqlDataAdapter da = new MySqlDataAdapter(command);//Переменная объявлена как глобальная
                MySqlCommandBuilder cb = new MySqlCommandBuilder(da);
                DataSet ds = new DataSet();
                connection.Close();
                //Заполнение DataGridView наименованиями полей 
                da.Fill(ds, "daycare");            
                d1.DataSource = ds.Tables[0];
                
                this.CloseConnection();
            }
        }*/

        //Method for loading customers from database
        public void LoadCustomers(DataGridView d2)
        {
            if (this.OpenConnection() == true)
            {
                MySqlCommand command = new MySqlCommand("SELECT ID, concat(CLIENT_NAME, ' ', CLIENT_SURNAME) AS FIO, FATHER_CELL_PHONE, MOTHER_CELL_PHONE, DATE_OF_AGREEMENT  FROM customers", connection);
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                MySqlCommandBuilder cb = new MySqlCommandBuilder(da);
                DataSet ds = new DataSet();
                //Заполнение DataGridView наименованиями полей
                da.Fill(ds, "customers");
                d2.DataSource = ds.Tables[0];

                this.CloseConnection();
            }
        }

        //Method for adding customer
        public void add_customer(string date_of_agreement, string client_surname, string client_name, string client_date_birthday, string father_name, string father_address, string father_home_phone, string father_cell_phone, string father_email, string father_place_of_employment, string father_work_phone, string mother_name, string mother_address, string mother_home_phone, string mother_cell_phone, string mother_email, string mother_place_of_employment, string mother_work_phone, string child_name_doctor, string child_address_doctor, string child_phone_doctor, string child_allergies, string emergency_person1_name, string emergency_person1_phone, string emergency_person1_relationship_to_child, string emergency_person2_name, string emergency_person2_phone, string emergency_person2_relationship_to_child, string person_authorized_pickup1, string person_authorized_pickup2, string duration_of_trial_period, string end_of_trial_period, double sum_of_payment)
        {
            string query = "insert into customers (DATE_OF_AGREEMENT, CLIENT_SURNAME, CLIENT_NAME, CLIENT_DATE_BIRTCH, FATHER_NAME, FATHER_ADDRESS, FATHER_HOME_PHONE, FATHER_CELL_PHONE, FATHER_EMAIL, FATHER_PLACE_OF_EMPLOYMENT, FATHER_WORK_PHONE, MOTHER_NAME, MOTHER_ADDRESS, MOTHER_HOME_PHONE, MOTHER_CELL_PHONE, MOTHER_EMAIL, MOTHER_PLACE_OF_EMPLOYMENT, MOTHER_WORK_PHONE, CHILD_NAME_DOCTOR, CHILD_ADDRESS_DOCTOR, CHILD_PHONE_DOCTOR, CHILD_ALLERGIES, EMERGENCY_PERSON1_NAME, EMERGENCY_PERSON1_PHONE, EMERGENCY_PERSON1_RELATIONSHIP_TO_CHILD, EMERGENCY_PERSON2_NAME, EMERGENCY_PERSON2_PHONE, EMERGENCY_PERSON2_RELATIONSHIP_TO_CHILD, PERSON_AUTHORIZED_PICKUP1, PERSON_AUTHORIZED_PICKUP2, DURATION_OF_TRIAL_PERIOD, END_OF_TRIAL_PERIOD, SUM_OF_PAYMENT, DATETIME) values (STR_TO_DATE(@date_of_agreement, '%m/%d/%Y'), @client_surname, @client_name, STR_TO_DATE(@client_date_birthday, '%m/%d/%Y'), @father_name, @father_address, @father_home_phone, @father_cell_phone, @father_email, @father_place_of_employment, @father_work_phone, @mother_name, @mother_address, @mother_home_phone, @mother_cell_phone, @mother_email, @mother_place_of_employment, @mother_work_phone, @child_name_doctor, @child_address_doctor, @child_phone_doctor, @child_allergies, @emergency_person1_name, @emergency_person1_phone, @emergency_person1_relationship_to_child, @emergency_person2_name, @emergency_person2_phone, @emergency_person2_relationship_to_child, @person_authorized_pickup1, @person_authorized_pickup2, @duration_of_trial_period, STR_TO_DATE(@end_of_trial_period, '%m/%d/%Y'), @sum_of_payment, NOW())";
            if (this.OpenConnection() == true)
            {                           
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@date_of_agreement", date_of_agreement);
                cmd.Parameters.AddWithValue("@client_surname", client_surname);
                cmd.Parameters.AddWithValue("@client_name", client_name);
                cmd.Parameters.AddWithValue("@client_date_birthday", client_date_birthday);
                cmd.Parameters.AddWithValue("@father_name", father_name);
                cmd.Parameters.AddWithValue("@father_address", father_address);
                cmd.Parameters.AddWithValue("@father_home_phone", father_home_phone);
                cmd.Parameters.AddWithValue("@father_cell_phone", father_cell_phone);
                cmd.Parameters.AddWithValue("@father_email", father_email);
                cmd.Parameters.AddWithValue("@father_place_of_employment", father_place_of_employment);
                cmd.Parameters.AddWithValue("@father_work_phone", father_work_phone);
                cmd.Parameters.AddWithValue("@mother_name", mother_name);
                cmd.Parameters.AddWithValue("@mother_address", mother_address);
                cmd.Parameters.AddWithValue("@mother_home_phone", mother_home_phone);
                cmd.Parameters.AddWithValue("@mother_cell_phone", mother_cell_phone);
                cmd.Parameters.AddWithValue("@mother_email", mother_email);
                cmd.Parameters.AddWithValue("@mother_place_of_employment", mother_place_of_employment);
                cmd.Parameters.AddWithValue("@mother_work_phone", mother_work_phone);
                cmd.Parameters.AddWithValue("@child_name_doctor", child_name_doctor);
                cmd.Parameters.AddWithValue("@child_address_doctor", child_address_doctor);
                cmd.Parameters.AddWithValue("@child_phone_doctor", child_phone_doctor);
                cmd.Parameters.AddWithValue("@child_allergies", child_allergies);
                cmd.Parameters.AddWithValue("@emergency_person1_name", emergency_person1_name);
                cmd.Parameters.AddWithValue("@emergency_person1_phone", emergency_person1_phone);
                cmd.Parameters.AddWithValue("@emergency_person1_relationship_to_child", emergency_person1_relationship_to_child);
                cmd.Parameters.AddWithValue("@emergency_person2_name", emergency_person2_name);
                cmd.Parameters.AddWithValue("@emergency_person2_phone", emergency_person2_phone);
                cmd.Parameters.AddWithValue("@emergency_person2_relationship_to_child", emergency_person2_relationship_to_child);
                cmd.Parameters.AddWithValue("@person_authorized_pickup1", person_authorized_pickup1);
                cmd.Parameters.AddWithValue("@person_authorized_pickup2", person_authorized_pickup2);
                cmd.Parameters.AddWithValue("@duration_of_trial_period", duration_of_trial_period);
                cmd.Parameters.AddWithValue("@end_of_trial_period", end_of_trial_period);
                cmd.Parameters.AddWithValue("@sum_of_payment", sum_of_payment);
                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }

            //MessageBox.Show("Customer added successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
