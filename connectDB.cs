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
                DataSet ds = new DataSet();
                connection.Close();
                //Заполнение DataGridView наименованиями полей 
                da.Fill(ds, "daycare");            
                d1.DataSource = ds.Tables[0];
                
                this.CloseConnection();
            }
        }*/

        //Method for loading customers from database
        public void LoadCustomers(DataGridView dg)
        {
            if (this.OpenConnection() == true)
            {
                MySqlCommand command = new MySqlCommand("SELECT ID, concat(CLIENT_NAME, ' ', CLIENT_SURNAME) AS FIO, FATHER_CELL_PHONE, MOTHER_CELL_PHONE, DATE_OF_AGREEMENT  FROM customers", connection);
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                DataSet ds = new DataSet();
                //Заполнение DataGridView наименованиями полей
                da.Fill(ds, "customers");
                dg.DataSource = ds.Tables[0];

                this.CloseConnection();
            }
        }

        //Method for adding customer
        public void Add_customer(string date_of_agreement, string client_surname, string client_name, string client_date_birthday, string father_name, string father_address, string father_home_phone, string father_cell_phone, string father_email, string father_place_of_employment, string father_work_phone, string mother_name, string mother_address, string mother_home_phone, string mother_cell_phone, string mother_email, string mother_place_of_employment, string mother_work_phone, string child_name_doctor, string child_address_doctor, string child_phone_doctor, string child_allergies, string emergency_person1_name, string emergency_person1_phone, string emergency_person1_relationship_to_child, string emergency_person2_name, string emergency_person2_phone, string emergency_person2_relationship_to_child, string person_authorized_pickup1, string person_authorized_pickup2, string duration_of_trial_period, string end_of_trial_period, double sum_of_payment)
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

        //Method of loading customer for editing him
        public void Load_customers_4_editing(int idd, DateTimePicker dt1, TextBox tb1, TextBox tb2, DateTimePicker dt2, TextBox tb3, TextBox tb4, MaskedTextBox mb1, MaskedTextBox mb2, TextBox tb5, TextBox tb6, MaskedTextBox mb3, TextBox tb10, TextBox tb9, MaskedTextBox mb6, MaskedTextBox mb5, TextBox tb8, TextBox tb7, MaskedTextBox mb4, TextBox tb20, TextBox tb19, MaskedTextBox mb12, TextBox tb18, TextBox tb17, MaskedTextBox mb11, TextBox tb16, TextBox tb12, MaskedTextBox mb7, TextBox tb11, TextBox tb13, TextBox tb14, DateTimePicker dt3, TextBox tb21)
        {
            string query = "select * from customers where ID=@id";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            cmd.Parameters.AddWithValue("id", idd);
            da.Fill(ds, "customers");

            //Assign result of query to elements on form
            dt1.Value = Convert.ToDateTime(ds.Tables[0].Rows[0][1]);
            tb1.Text = ds.Tables[0].Rows[0][2].ToString();
            tb2.Text = ds.Tables[0].Rows[0][3].ToString();
            dt2.Value = Convert.ToDateTime(ds.Tables[0].Rows[0][4]);
            tb3.Text = ds.Tables[0].Rows[0][5].ToString();
            tb4.Text = ds.Tables[0].Rows[0][6].ToString();
            mb1.Text = ds.Tables[0].Rows[0][7].ToString();
            mb2.Text = ds.Tables[0].Rows[0][8].ToString();
            tb5.Text = ds.Tables[0].Rows[0][9].ToString();
            tb6.Text = ds.Tables[0].Rows[0][10].ToString();
            mb3.Text = ds.Tables[0].Rows[0][11].ToString();
            tb10.Text = ds.Tables[0].Rows[0][12].ToString();
            tb9.Text = ds.Tables[0].Rows[0][13].ToString();
            mb6.Text = ds.Tables[0].Rows[0][14].ToString();
            mb5.Text = ds.Tables[0].Rows[0][15].ToString();
            tb8.Text = ds.Tables[0].Rows[0][16].ToString();
            tb7.Text = ds.Tables[0].Rows[0][17].ToString();
            mb4.Text = ds.Tables[0].Rows[0][18].ToString();
            tb20.Text = ds.Tables[0].Rows[0][19].ToString();
            tb19.Text = ds.Tables[0].Rows[0][20].ToString();
            mb12.Text = ds.Tables[0].Rows[0][21].ToString();
            tb18.Text = ds.Tables[0].Rows[0][22].ToString();
            tb17.Text = ds.Tables[0].Rows[0][23].ToString();
            mb11.Text = ds.Tables[0].Rows[0][24].ToString();
            tb16.Text = ds.Tables[0].Rows[0][25].ToString();
            tb12.Text = ds.Tables[0].Rows[0][26].ToString();
            mb7.Text = ds.Tables[0].Rows[0][27].ToString();
            tb11.Text = ds.Tables[0].Rows[0][28].ToString();
            tb13.Text = ds.Tables[0].Rows[0][29].ToString();
            tb14.Text = ds.Tables[0].Rows[0][30].ToString();
            dt3.Value = Convert.ToDateTime(ds.Tables[0].Rows[0][32]);
            tb21.Text = ds.Tables[0].Rows[0][33].ToString();


            this.CloseConnection();
        }

        //Method for delete customer
        public void Del_customer(int id)
        {

        }



    }
}
