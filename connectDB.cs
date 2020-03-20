using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace CosmoKids
{
    class ConnectDB : IDisposable
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


        /// <summary>
        /// Return max ID of specified table
        /// </summary>
        /// <param name="tbl">It's a name of table which you want to get max ID</param>
        /// <returns>Max(ID)</returns>
        public int GetMaxTableID(string tbl)
        {
            string query = string.Format("select max(ID) from {0}", tbl);
            MySqlCommand command = new MySqlCommand(query, connection);
            //command.Parameters.AddWithValue("table", table);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds, "customers");
            return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
        }

        ///////////////////////////////////////////////////////////////////    


        ////////////////////Open connection to database
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
        //////////////////////////////////////////////////

        
        ///////////////////////////Close connection
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
        /////////////////////////////////////////////


        /////////////////////////////Method for loading customers from database
        public void LoadCustomers(DataGridView dg)
        {
            if (this.OpenConnection() == true)
            {
                MySqlCommand command = new MySqlCommand("SELECT ID, concat(CLIENT_NAME, ' ', CLIENT_SURNAME) AS FIO, FATHER_CELL_PHONE, MOTHER_CELL_PHONE, DATE_OF_AGREEMENT  FROM customers", connection);
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                DataSet ds = new DataSet();
                //Заполнение DataGridView наименованиями полей
                da.Fill(ds, "customers");

                dg.Invoke(new MethodInvoker(delegate ()
                {
                    dg.DataSource = ds.Tables[0];
                }));

                this.CloseConnection();                
            }
        }
        //////////////////////////////////////////////////////////////////////////


        /////////////////////Method for load additional information of customer
        public void LoadAdditionalInfo(int idd, DataGridView dg)
        {
            if (this.OpenConnection() == true)
            {
                //MySqlCommand command = new MySqlCommand("select CLIENT_DATE_BIRTCH, FATHER_NAME, FATHER_ADDRESS, FATHER_HOME_PHONE, FATHER_EMAIL, FATHER_PLACE_OF_EMPLOYMENT, FATHER_WORK_PHONE, MOTHER_NAME, MOTHER_ADDRESS, MOTHER_HOME_PHONE, MOTHER_EMAIL, MOTHER_PLACE_OF_EMPLOYMENT, MOTHER_WORK_PHONE, CHILD_NAME_DOCTOR, CHILD_ADDRESS_DOCTOR, CHILD_PHONE_DOCTOR, CHILD_ALLERGIES, EMERGENCY_PERSON1_NAME, EMERGENCY_PERSON1_PHONE, EMERGENCY_PERSON1_RELATIONSHIP_TO_CHILD, EMERGENCY_PERSON2_NAME, EMERGENCY_PERSON2_PHONE, EMERGENCY_PERSON2_RELATIONSHIP_TO_CHILD, PERSON_AUTHORIZED_PICKUP1, PERSON_AUTHORIZED_PICKUP2, DURATION_OF_TRIAL_PERIOD, END_OF_TRIAL_PERIOD from customers where ID=@id", connection);
                MySqlCommand command = new MySqlCommand("SELECT 'CLIENT\\'S BIRTHDAY', CLIENT_DATE_BIRTCH FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'FATHER\\'S NAME', FATHER_NAME FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'FATHER\\'S ADDRESS', FATHER_ADDRESS FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'FATHER\\'S HOME PHONE', FATHER_HOME_PHONE FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'FATHER\\'S EMAIL', FATHER_EMAIL FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'FATHER\\'S PLACE OF EMPLOYMENT', FATHER_PLACE_OF_EMPLOYMENT FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'FATHER\\'S WORK PHONE', FATHER_WORK_PHONE FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'MOTHER\\'S NAME', MOTHER_NAME FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'MOTHER\\'S ADDRESS', MOTHER_ADDRESS FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'MOTHER\\'S HOME PHONE', MOTHER_HOME_PHONE FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'MOTHER\\'S EMAIL', MOTHER_EMAIL FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'MOTHER\\'S PLACE OF EMPLOYMENT', MOTHER_PLACE_OF_EMPLOYMENT FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'MOTHER\\'S WORK PHONE', MOTHER_WORK_PHONE FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'CHILD\\'S DOCTOR NAME', CHILD_NAME_DOCTOR FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'CHILD\\'S DOCTOR ADDRESS', CHILD_ADDRESS_DOCTOR FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'CHILD\\'S DOCTOR PHONE', CHILD_PHONE_DOCTOR FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'CHILD\\'S ALLERGIES', CHILD_ALLERGIES FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'EMERGENCY PERSON NAME 1', EMERGENCY_PERSON1_NAME FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'EMERGENCY PERSON PHONE 1', EMERGENCY_PERSON1_PHONE FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'EMERGENCY PERSON RELATIONSHIP TO CHILD 1', EMERGENCY_PERSON1_RELATIONSHIP_TO_CHILD FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'EMERGENCY PERSON NAME 2', EMERGENCY_PERSON1_NAME FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'EMERGENCY PERSON PHONE 2', EMERGENCY_PERSON1_PHONE FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'EMERGENCY PERSON RELATIONSHIP TO CHILD 2', EMERGENCY_PERSON1_RELATIONSHIP_TO_CHILD FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'PERSON AUTHORIZED PICKUP 1', PERSON_AUTHORIZED_PICKUP1 FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'PERSON AUTHORIZED PICKUP 2', PERSON_AUTHORIZED_PICKUP2 FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'DURATION OF TRIAL PERIOD', DURATION_OF_TRIAL_PERIOD FROM customers WHERE ID = @id " +
                                                        "UNION " +
                                                        "SELECT 'END OF TRIAL PERIOD', END_OF_TRIAL_PERIOD FROM customers WHERE ID = @id "
                                                        , connection);
                command.Parameters.AddWithValue("id", idd);
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                DataSet ds = new DataSet();
                //Заполнение DataGridView наименованиями полей
                da.Fill(ds, "customers");

                dg.Invoke(new MethodInvoker(delegate ()
                {
                    dg.DataSource = ds.Tables[0];
                }));

                this.CloseConnection();
            }
        }
        ///////////////////////////////////////////////////////////////////////////
        

        //////////////////////////////Method for loading payments
        public void LoadPayments(int idd, DataGridView dg)
        {
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand("select ID, CUSTOMERS_ID, PERIOD, SUMM_4_OPL, OPL_SUMM, DATE_OF_PAY, STATUS from pays where CUSTOMERS_ID=@id", connection);
                cmd.Parameters.AddWithValue("id", idd);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "pays");

                dg.Invoke(new MethodInvoker(delegate ()
                {
                    dg.DataSource = ds.Tables[0];
                }));

                this.CloseConnection();
            }

            
        }
        /////////////////////////////////////////////////////////////////////////////////


        ////////////////////////////Method for loading notes
        public void LoadNotes(int idd, TextBox tb)
        {
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand("select NOTES from pays where ID=@id", connection);
                cmd.Parameters.AddWithValue("@id", idd);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "pays");

                tb.Invoke(new MethodInvoker(delegate ()
                {
                    tb.Text = ds.Tables[0].Rows[0][0].ToString();
                }));

                this.CloseConnection();            
            }
        }
        //////////////////////////////////////////////////////////////////////////////
        

        //////////////////////////Method for getting kind of payment
        public void GetKindPay(ComboBox cb)
        {
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand("select * from kind_pay", connection);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string res = dr.GetString(1);
                    cb.Items.Add(res);
                }

                this.CloseConnection();
            }        
                       
        }
        //////////////////////////////////////////////////////////////


        //Method for adding customer
        public void Add_customer(string date_of_agreement, string client_surname, string client_name, string client_date_birthday, string father_name, string father_address, string father_home_phone, string father_cell_phone, string father_email, string father_place_of_employment, string father_work_phone, string mother_name, string mother_address, string mother_home_phone, string mother_cell_phone, string mother_email, string mother_place_of_employment, string mother_work_phone, string child_name_doctor, string child_address_doctor, string child_phone_doctor, string child_allergies, string emergency_person1_name, string emergency_person1_phone, string emergency_person1_relationship_to_child, string emergency_person2_name, string emergency_person2_phone, string emergency_person2_relationship_to_child, string person_authorized_pickup1, string person_authorized_pickup2, string duration_of_trial_period, string end_of_trial_period, double sum_of_payment)
        {           
            string query = "insert into customers (DATE_OF_AGREEMENT, CLIENT_SURNAME, CLIENT_NAME, CLIENT_DATE_BIRTCH, FATHER_NAME, FATHER_ADDRESS, FATHER_HOME_PHONE, FATHER_CELL_PHONE, FATHER_EMAIL, FATHER_PLACE_OF_EMPLOYMENT, FATHER_WORK_PHONE, MOTHER_NAME, MOTHER_ADDRESS, MOTHER_HOME_PHONE, MOTHER_CELL_PHONE, MOTHER_EMAIL, MOTHER_PLACE_OF_EMPLOYMENT, MOTHER_WORK_PHONE, CHILD_NAME_DOCTOR, CHILD_ADDRESS_DOCTOR, CHILD_PHONE_DOCTOR, CHILD_ALLERGIES, EMERGENCY_PERSON1_NAME, EMERGENCY_PERSON1_PHONE, EMERGENCY_PERSON1_RELATIONSHIP_TO_CHILD, EMERGENCY_PERSON2_NAME, EMERGENCY_PERSON2_PHONE, EMERGENCY_PERSON2_RELATIONSHIP_TO_CHILD, PERSON_AUTHORIZED_PICKUP1, PERSON_AUTHORIZED_PICKUP2, DURATION_OF_TRIAL_PERIOD, END_OF_TRIAL_PERIOD, SUM_OF_PAYMENT, DATETIME_CREATE) values (STR_TO_DATE(@date_of_agreement, '%m/%d/%Y'), @client_surname, @client_name, STR_TO_DATE(@client_date_birthday, '%m/%d/%Y'), @father_name, @father_address, @father_home_phone, @father_cell_phone, @father_email, @father_place_of_employment, @father_work_phone, @mother_name, @mother_address, @mother_home_phone, @mother_cell_phone, @mother_email, @mother_place_of_employment, @mother_work_phone, @child_name_doctor, @child_address_doctor, @child_phone_doctor, @child_allergies, @emergency_person1_name, @emergency_person1_phone, @emergency_person1_relationship_to_child, @emergency_person2_name, @emergency_person2_phone, @emergency_person2_relationship_to_child, @person_authorized_pickup1, @person_authorized_pickup2, @duration_of_trial_period, STR_TO_DATE(@end_of_trial_period, '%m/%d/%Y'), @sum_of_payment, NOW())";
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

            int cust_id = this.GetMaxTableID("customers"); //Get ID of inserted customer
            
            int mn = DateTime.Now.Month;

            if (this.OpenConnection() == true)
            {
                for (int i = mn; i <= 12; i++)
                {
                    string per = String.Concat(i, ".", DateTime.Now.Year);
                    string query1 = "insert into pays (CUSTOMERS_ID, PERIOD, SUMM_4_OPL, DATETIME_CREATE) values (@customer_id, @period, @summ_4_opl, NOW())";
                    MySqlCommand cmd1 = new MySqlCommand(query1, connection);
                    cmd1.Parameters.AddWithValue("@customer_id", cust_id);
                    cmd1.Parameters.AddWithValue("@period", per);
                    cmd1.Parameters.AddWithValue("@summ_4_opl", sum_of_payment);
                    cmd1.ExecuteNonQuery();
                }

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
            dt1.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["DATE_OF_AGREEMENT"]);
            tb1.Text = ds.Tables[0].Rows[0]["CLIENT_SURNAME"].ToString();
            tb2.Text = ds.Tables[0].Rows[0]["CLIENT_NAME"].ToString();
            dt2.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["CLIENT_DATE_BIRTCH"]);
            tb3.Text = ds.Tables[0].Rows[0]["FATHER_NAME"].ToString();
            tb4.Text = ds.Tables[0].Rows[0]["FATHER_ADDRESS"].ToString();
            mb1.Text = ds.Tables[0].Rows[0]["FATHER_HOME_PHONE"].ToString();
            mb2.Text = ds.Tables[0].Rows[0]["FATHER_CELL_PHONE"].ToString();
            tb5.Text = ds.Tables[0].Rows[0]["FATHER_EMAIL"].ToString();
            tb6.Text = ds.Tables[0].Rows[0]["FATHER_PLACE_OF_EMPLOYMENT"].ToString();
            mb3.Text = ds.Tables[0].Rows[0]["FATHER_WORK_PHONE"].ToString();
            tb10.Text = ds.Tables[0].Rows[0]["MOTHER_NAME"].ToString();
            tb9.Text = ds.Tables[0].Rows[0]["MOTHER_ADDRESS"].ToString();
            mb6.Text = ds.Tables[0].Rows[0]["MOTHER_HOME_PHONE"].ToString();
            mb5.Text = ds.Tables[0].Rows[0]["MOTHER_CELL_PHONE"].ToString();
            tb8.Text = ds.Tables[0].Rows[0]["MOTHER_EMAIL"].ToString();
            tb7.Text = ds.Tables[0].Rows[0]["MOTHER_PLACE_OF_EMPLOYMENT"].ToString();
            mb4.Text = ds.Tables[0].Rows[0]["MOTHER_WORK_PHONE"].ToString();
            tb20.Text = ds.Tables[0].Rows[0]["CHILD_NAME_DOCTOR"].ToString();
            tb19.Text = ds.Tables[0].Rows[0]["CHILD_ADDRESS_DOCTOR"].ToString();
            mb12.Text = ds.Tables[0].Rows[0]["CHILD_PHONE_DOCTOR"].ToString();
            tb18.Text = ds.Tables[0].Rows[0]["CHILD_ALLERGIES"].ToString();
            tb17.Text = ds.Tables[0].Rows[0]["EMERGENCY_PERSON1_NAME"].ToString();
            mb11.Text = ds.Tables[0].Rows[0]["EMERGENCY_PERSON1_PHONE"].ToString();
            tb16.Text = ds.Tables[0].Rows[0]["EMERGENCY_PERSON1_RELATIONSHIP_TO_CHILD"].ToString();
            tb12.Text = ds.Tables[0].Rows[0]["EMERGENCY_PERSON2_NAME"].ToString();
            mb7.Text = ds.Tables[0].Rows[0]["EMERGENCY_PERSON2_PHONE"].ToString();
            tb11.Text = ds.Tables[0].Rows[0]["EMERGENCY_PERSON2_RELATIONSHIP_TO_CHILD"].ToString();
            tb13.Text = ds.Tables[0].Rows[0]["PERSON_AUTHORIZED_PICKUP1"].ToString();
            tb14.Text = ds.Tables[0].Rows[0]["PERSON_AUTHORIZED_PICKUP2"].ToString();
            dt3.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["END_OF_TRIAL_PERIOD"]);
            tb21.Text = ds.Tables[0].Rows[0]["SUM_OF_PAYMENT"].ToString();

            this.CloseConnection();
        }

        //Method for delete customer
        public void Del_customer(int id)
        {

        }

        //Method for edit customer
        public void Edit_customer(int id, string date_of_agreement, string client_surname, string client_name, string client_date_birthday, string father_name, string father_address, string father_home_phone, string father_cell_phone, string father_email, string father_place_of_employment, string father_work_phone, string mother_name, string mother_address, string mother_home_phone, string mother_cell_phone, string mother_email, string mother_place_of_employment, string mother_work_phone, string child_name_doctor, string child_address_doctor, string child_phone_doctor, string child_allergies, string emergency_person1_name, string emergency_person1_phone, string emergency_person1_relationship_to_child, string emergency_person2_name, string emergency_person2_phone, string emergency_person2_relationship_to_child, string person_authorized_pickup1, string person_authorized_pickup2, string duration_of_trial_period, string end_of_trial_period, double sum_of_payment)
        {
            string query = "update customers set DATE_OF_AGREEMENT = STR_TO_DATE(@date_of_agreement, '%m/%d/%Y'), CLIENT_SURNAME = @client_surname, CLIENT_NAME = @client_name, CLIENT_DATE_BIRTCH = STR_TO_DATE(@client_date_birthday, '%m/%d/%Y'), FATHER_NAME = @father_name, FATHER_ADDRESS = @father_address, FATHER_HOME_PHONE = @father_home_phone, FATHER_CELL_PHONE = @father_cell_phone, FATHER_EMAIL = @father_email, FATHER_PLACE_OF_EMPLOYMENT = @father_place_of_employment, FATHER_WORK_PHONE = @father_work_phone, MOTHER_NAME = @mother_name, MOTHER_ADDRESS = @mother_address, MOTHER_HOME_PHONE = @mother_home_phone, MOTHER_CELL_PHONE = @mother_cell_phone, MOTHER_EMAIL = @mother_email, MOTHER_PLACE_OF_EMPLOYMENT = @mother_place_of_employment, MOTHER_WORK_PHONE = @mother_work_phone, CHILD_NAME_DOCTOR = @child_name_doctor, CHILD_ADDRESS_DOCTOR = @child_address_doctor, CHILD_PHONE_DOCTOR = @child_phone_doctor, CHILD_ALLERGIES = @child_allergies, EMERGENCY_PERSON1_NAME = @emergency_person1_name, EMERGENCY_PERSON1_PHONE = @emergency_person1_phone, EMERGENCY_PERSON1_RELATIONSHIP_TO_CHILD = @emergency_person1_relationship_to_child, EMERGENCY_PERSON2_NAME = @emergency_person2_name, EMERGENCY_PERSON2_PHONE = @emergency_person2_phone, EMERGENCY_PERSON2_RELATIONSHIP_TO_CHILD = @emergency_person2_relationship_to_child, PERSON_AUTHORIZED_PICKUP1 = @person_authorized_pickup1, PERSON_AUTHORIZED_PICKUP2 = @person_authorized_pickup2, DURATION_OF_TRIAL_PERIOD = @duration_of_trial_period, END_OF_TRIAL_PERIOD = STR_TO_DATE(@end_of_trial_period, '%m/%d/%Y'), SUM_OF_PAYMENT = @sum_of_payment, DATETIME_UPDATE = NOW() where id = @id";
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
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }
        /////////////////////////////////////////////////////



        ///////////////////////////////////Method for adding payment     
        public void AddPayment(int idd, string tb1, string dt1, string cb1, string tb2)
        {
            if (this.OpenConnection() == true)
            {
                //Select all data/periods in student which are not paid or paid partly
                string query = "select CUSTOMERS_ID, PERIOD, SUMM_4_OPL, OPL_SUMM from pays where CUSTOMERS_ID=@id and (OPL_SUMM = 0 or SUMM_4_OPL > OPL_SUMM) order by (SUBSTRING_INDEX(PERIOD, '.', -1) + 0), (SUBSTRING_INDEX(PERIOD, '.', 1) + 0)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", idd);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "pays");

                double sum_pay = Convert.ToDouble(tb1);
                double input_summ = sum_pay;
                double common_sum = 0;
                
                for (int p = 0; p <= ds.Tables[0].Rows.Count - 1; p++)
                {
                    common_sum += (Convert.ToDouble(ds.Tables[0].Rows[p]["SUMM_4_OPL"]) - Convert.ToDouble(ds.Tables[0].Rows[p]["OPL_SUMM"]));
                }
            
                if (sum_pay > common_sum)
                {
                    MessageBox.Show("Amount which you've entered more than you should pay in this year.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.CloseConnection();
                    return;
                }

                
                //Processing array and fill unpaid and underpaid periods
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    double diff_sum = Convert.ToDouble(ds.Tables[0].Rows[i]["SUMM_4_OPL"]) - Convert.ToDouble(ds.Tables[0].Rows[i]["OPL_SUMM"]);

                    if (input_summ < diff_sum)
                    {
                        string query1 = "update pays set OPL_SUMM = OPL_SUMM + @input_summ, DATE_OF_PAY = STR_TO_DATE(@dt1, '%m/%d/%Y'), STATUS = @cb1, NOTES = @tb2 where CUSTOMERS_ID = @idd and PERIOD = @period";
                        MySqlCommand cmd1 = new MySqlCommand(query1, connection);
                        cmd1.Parameters.AddWithValue("@input_summ", input_summ);
                        cmd1.Parameters.AddWithValue("@dt1", dt1);
                        cmd1.Parameters.AddWithValue("@cb1", cb1);
                        cmd1.Parameters.AddWithValue("@tb2", tb2);
                        cmd1.Parameters.AddWithValue("@idd", ds.Tables[0].Rows[i]["CUSTOMERS_ID"]);
                        cmd1.Parameters.AddWithValue("@period", ds.Tables[0].Rows[i]["PERIOD"]);
                        cmd1.ExecuteNonQuery();
                        break;
                    }

                    input_summ -= diff_sum;

                    string query2 = "update pays set OPL_SUMM = @summ_4_opl, DATE_OF_PAY = STR_TO_DATE(@dt1, '%m/%d/%Y'), STATUS = @cb1, NOTES = @tb2 where CUSTOMERS_ID = @idd and PERIOD = @period";
                    MySqlCommand cmd2 = new MySqlCommand(query2, connection);
                    cmd2.Parameters.AddWithValue("@summ_4_opl", Convert.ToDouble(ds.Tables[0].Rows[i]["SUMM_4_OPL"]));
                    cmd2.Parameters.AddWithValue("@dt1", dt1);
                    cmd2.Parameters.AddWithValue("@cb1", cb1);
                    cmd2.Parameters.AddWithValue("@tb2", tb2);
                    cmd2.Parameters.AddWithValue("@idd", ds.Tables[0].Rows[i]["CUSTOMERS_ID"]);
                    cmd2.Parameters.AddWithValue("@period", ds.Tables[0].Rows[i]["PERIOD"]);
                    cmd2.ExecuteNonQuery();

                    if (input_summ < Convert.ToDouble(ds.Tables[0].Rows[i]["SUMM_4_OPL"]))
                    {
                        string query3 = "update pays set OPL_SUMM = @input_summ, DATE_OF_PAY = STR_TO_DATE(@dt1, '%m/%d/%Y'), STATUS = @cb1, NOTES = @tb2 where CUSTOMERS_ID = @idd and PERIOD = @period";
                        MySqlCommand cmd3 = new MySqlCommand(query3, connection);
                        cmd3.Parameters.AddWithValue("@input_summ", input_summ);
                        cmd3.Parameters.AddWithValue("@dt1", dt1);
                        cmd3.Parameters.AddWithValue("@cb1", cb1);
                        cmd3.Parameters.AddWithValue("@tb2", tb2);
                        cmd3.Parameters.AddWithValue("@idd", ds.Tables[0].Rows[i]["CUSTOMERS_ID"]);
                        cmd3.Parameters.AddWithValue("@period", ds.Tables[0].Rows[i + 1]["PERIOD"]);
                        cmd3.ExecuteNonQuery();
                        break;
                    }

                }

                this.CloseConnection();

                //Add order
                this.AddOrder(idd, "Payment order", dt1, "Visiting daycare", sum_pay, cb1);

                MessageBox.Show("Payment accepted succesfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);                              
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        


        ////////////////////////////////Method for Adding order
        public void AddOrder(int customer_id, string type_doc, string date_pay, string osnovan, double summ4opl, string status)
        {
            if (this.OpenConnection() == true)
            {
                string query = "insert into orders (CUSTOMERS_ID, TYPE_DOC, DATE_PAY, OSNOVANIE, SUMM_OPL, STATUS, DATETIME_CREATE) values (@customer_id, @type_doc, STR_TO_DATE(@date_pay, '%m/%d/%Y'), @osnovan, @summ4opl, @status, NOW())";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@customer_id", customer_id);
                cmd.Parameters.AddWithValue("@type_doc", type_doc);
                cmd.Parameters.AddWithValue("@date_pay", date_pay);
                cmd.Parameters.AddWithValue("@osnovan", osnovan);
                cmd.Parameters.AddWithValue("@summ4opl", summ4opl);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }
        /////////////////////////////////////////////////////////
        


        ///////////////////Method for getting debt of current period
        public double GetDebtCurrPeriod(int idd)
        {
            if (this.OpenConnection() == true)
            {
                string query = "select SUMM_4_OPL - OPL_SUMM from pays where PERIOD = CONCAT(month(now()), '.', year(now())) and CUSTOMERS_ID = @idd";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@idd", idd);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "pays");
                //if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    double res = Convert.ToDouble(ds.Tables[0].Rows[0][0]);
                    this.CloseConnection();
                    return res;
                }
                else
                {
                    this.CloseConnection();
                    return 0;
                }
            }
            else return 0;
        }
        ///////////////////////////////////////////////////////
        


        //////////////////Method for getting debts for previous months
        public double GetDebtsPrevMonths(int idd)
        {
            if (this.OpenConnection() == true)
            {
                string query = "select SUM(SUMM_4_OPL- OPL_SUMM) from pays where SUBSTRING_INDEX(PERIOD, '.', 1) < MONTH(NOW()) and SUBSTRING_INDEX(PERIOD, '.', -1) <= YEAR(NOW()) and CUSTOMERS_ID = @idd and (SUMM_4_OPL > OPL_SUMM or OPL_SUMM < 1)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@idd", idd);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "pays");
                if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                {
                    double res = Convert.ToDouble(ds.Tables[0].Rows[0][0]);
                    this.CloseConnection();
                    return res;
                }
                else
                {
                    this.CloseConnection();
                    return 0;
                }
            }
            else return 0;
        }
        /////////////////////////////////////////////////////////////
        


        //////////////////Method for getting pays for previous and current periods
        public double GetPrevCurrPayments(int idd)
        {
            if (this.OpenConnection() == true)
            {
                string query = "select SUM(OPL_SUMM) from pays where CUSTOMERS_ID = @idd and (OPL_SUMM is not NULL and OPL_SUMM > 0)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@idd", idd);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "pays");
                if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                {
                    double res = Convert.ToDouble(ds.Tables[0].Rows[0][0]);
                    this.CloseConnection();
                    return res;
                }
                else
                {
                    this.CloseConnection();
                    return 0;
                }
            }
            else return 0;
        }
        /////////////////////////////////////////////////////////////////
        


        ///////////////////Method for getting whole sum of payment for year
        public double GetWholeSumofPayment(int idd)
        {
            if (this.OpenConnection() == true)
            {
                string query = "select SUM(SUMM_4_OPL) from pays where CUSTOMERS_ID = @idd";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@idd", idd);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "pays");
                if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                {
                    double res = Convert.ToDouble(ds.Tables[0].Rows[0][0]);
                    this.CloseConnection();
                    return res;
                }
                else
                {
                    this.CloseConnection();
                    return 0;
                }
            }
            else return 0;
        }
        ///////////////////////////////////////////////////////////



        ////////////////Method for tie scan checks to customer
        /// <summary>
        /// Method for binding documents to customer
        /// </summary>
        /// <param name="idd">ID of customer.</param>
        /// <param name="file">File for binding.</param>
        /// <param name="filename">Name of binding file.</param>
        public void TieDocumentToCustomer(int idd, byte[] file, string filename)
        {
            if (this.OpenConnection() == true)
            {
                string query = "insert into documents (CUSTOMERS_ID, IMG_DOC, FILE_NAME, DATETIME_CREATE) values (@customer_id, @file, @filename, NOW())";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@customer_id", idd);
                cmd.Parameters.AddWithValue("@filename", filename);
                cmd.Parameters.Add("@file", MySqlDbType.VarBinary).Value = file;
                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }
        ////////////////////////////////////////////////////////
        


        /////////////Method for getting count of rows in customer's documents
        public int GetCountRows(int idd)
        {
            if (this.OpenConnection() == true)
            {
                string query = "select count(*) from documents where CUSTOMERS_ID = @customer_id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@customer_id", idd);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "documents");
                this.CloseConnection();
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
            else
            {
                return 0;
            }
        }
        ////////////////////////////////////////////////////////////////////
        


        /////////////Method for loading documents of customer
        public void LoadCustomerDocuments(int idd, DataGridView dg)
        {
            if (this.OpenConnection() == true)
            {
                string query = "select ID, CUSTOMERS_ID, IMG_DOC, FILE_NAME from documents where CUSTOMERS_ID = @customer_id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@customer_id", idd);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "documents");
                dg.DataSource = ds.Tables[0];

                this.CloseConnection();
            }
        }
        ////////////////////////////////////////////////////////////
        


        ////////////Method for showing customer's document
        public void ShowCustomerDocument(int idd)
        {
            if (this.OpenConnection() == true)
            {
                string query = "select IMG_DOC, FILE_NAME from documents where ID = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", idd);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "documents");

                string filename = ds.Tables[0].Rows[0]["FILE_NAME"].ToString();
                byte[] file = (byte[])ds.Tables[0].Rows[0]["IMG_DOC"];

                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\customer_doc\"))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\customer_doc\");
                }
                
                FileStream fileStream = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\customer_doc\" + filename, FileMode.Create, FileAccess.ReadWrite);
                BinaryWriter binaryWriter = new BinaryWriter(fileStream);
                binaryWriter.Write(file);
                binaryWriter.Close();
                
                Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\customer_doc\" + filename);

                this.CloseConnection();
            }
        }
        //////////////////////////////////////////////////////////
        


        //////////////Method for saving customer's document
        public void SaveCustomerDocument(int idd, string path)
        {
            if (this.OpenConnection() == true)
            {
                string query = "select IMG_DOC, FILE_NAME from documents where ID = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", idd);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "documents");

                string filename = ds.Tables[0].Rows[0]["FILE_NAME"].ToString();
                byte[] file = (byte[])ds.Tables[0].Rows[0]["IMG_DOC"];
                
                FileStream fileStream = new FileStream(path + @"\" + filename, FileMode.Create, FileAccess.ReadWrite);
                BinaryWriter binaryWriter = new BinaryWriter(fileStream);
                binaryWriter.Write(file);
                binaryWriter.Close();

                MessageBox.Show("File has been saved succesfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.CloseConnection();
            }
        }
        ////////////////////////////////////////////////////////////
        


        ////////////////Method for deleting customer's document
        public void DeleteCustomerDocument(int idd)
        {
            if (this.OpenConnection() == true)
            {
                string query = "delete from documents where ID = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", idd);
                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }


        //Implementation of interface of IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose managed resources
                connection.Close();
            }
            // free native resources
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



    }
}
