using System;
using System.Windows.Forms;

namespace CosmoKids
{
    public partial class Edit_Customer : Form
    {
        public Edit_Customer()
        {
            InitializeComponent();

            date_of_agreement = dateTimePicker1;
            client_surname = textBox1;
            client_name = textBox2;
            client_date_birth = dateTimePicker2;
            father_name = textBox3;
            father_address = textBox4;
            father_home_phone = maskedTextBox1;
            father_cell_phone = maskedTextBox2;
            father_email = textBox5;
            father_place_employment = textBox6;
            father_work_phone = maskedTextBox3;
            mother_name = textBox10;
            mother_address = textBox9;
            mother_home_phone = maskedTextBox6;
            mother_cell_phone = maskedTextBox5;
            mother_email = textBox8;
            mother_place_employment = textBox7;
            mother_work_phone = maskedTextBox4;
            child_doctor_name = textBox20;
            child_doctor_address = textBox19;
            child_doctor_phone = maskedTextBox12;
            child_allergies = textBox18;
            emergency_person_name1 = textBox17;
            emergency_person_phone1 = maskedTextBox11;
            emergency_person_relationship1 = textBox16;
            emergency_person_name2 = textBox12;
            emergency_person_phone2 = maskedTextBox7;
            emergency_person_relationship2 = textBox11;
            person_authorized_pickup1 = textBox13;
            person_authorized_pickup2 = textBox14;
            end_trial_period = dateTimePicker3;
            sum_payment = textBox21;
        }

        public DateTimePicker date_of_agreement { get; }
        public TextBox client_surname { get; }
        public TextBox client_name { get; }
        public DateTimePicker client_date_birth { get; }
        public TextBox father_name { get; }
        public TextBox father_address { get; }
        public MaskedTextBox father_home_phone { get; }
        public MaskedTextBox father_cell_phone { get; }
        public TextBox father_email { get; }
        public TextBox father_place_employment { get; }
        public MaskedTextBox father_work_phone { get; }
        public TextBox mother_name { get; }
        public TextBox mother_address { get; }
        public MaskedTextBox mother_home_phone { get; }
        public MaskedTextBox mother_cell_phone { get; }
        public TextBox mother_email { get; }
        public TextBox mother_place_employment { get; }
        public MaskedTextBox mother_work_phone { get; }
        public TextBox child_doctor_name { get; }
        public TextBox child_doctor_address { get; }
        public MaskedTextBox child_doctor_phone { get; }
        public TextBox child_allergies { get; }
        public TextBox emergency_person_name1 { get; }
        public MaskedTextBox emergency_person_phone1 { get; }
        public TextBox emergency_person_relationship1 { get; }
        public TextBox emergency_person_name2 { get; }
        public MaskedTextBox emergency_person_phone2 { get; }
        public TextBox emergency_person_relationship2 { get; }
        public TextBox person_authorized_pickup1 { get; }
        public TextBox person_authorized_pickup2 { get; }
        public DateTimePicker end_trial_period { get; }
        public TextBox sum_payment { get; }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("Surname of client can't be empty!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textBox2.Text.Length == 0)
            {
                MessageBox.Show("Name of client can't be empty!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ((textBox3.Text.Length == 0) && (textBox10.Text.Length == 0))
            {
                MessageBox.Show("You have to fill father's or mother's name!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if ((textBox4.Text.Length == 0) && (textBox9.Text.Length == 0))
            {
                MessageBox.Show("You have to fill father's or mother's address!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ((maskedTextBox2.Text.Length == 0) && (maskedTextBox5.Text.Length == 0))
            {
                MessageBox.Show("You have to fill father's or mother's cell phone!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ((textBox5.Text.Length == 0) && (textBox8.Text.Length == 0))
            {
                MessageBox.Show("You have to fill father's or mother's Email!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ((textBox13.Text.Length == 0) && (textBox14.Text.Length == 0))
            {
                MessageBox.Show("You have to fill at least one person who authorized to pickup child!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textBox21.Text.Length == 0)
            {
                MessageBox.Show("Sum of payment can't be empty!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Checking for correction of email
            CheckEmail ch_father = new CheckEmail(textBox5.Text);
            if (!ch_father.CheckEM())
            {
                MessageBox.Show("Incorrect format of father's Email", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CheckEmail ch_mother = new CheckEmail(textBox8.Text);
            if (!ch_mother.CheckEM())
            {
                MessageBox.Show("Incorrect format of mother's Email", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CosmoKids customer = new CosmoKids();
            customer.Date_of_agreement = dateTimePicker1.Value.ToShortDateString();
            customer.Client_surname = textBox1.Text;
            customer.Client_name = textBox2.Text;
            customer.Client_date_birth = dateTimePicker2.Value.Date.ToShortDateString();
            customer.Father_name = textBox3.Text;
            customer.Father_address = textBox4.Text;
            customer.Father_home_phone = maskedTextBox1.Text;
            customer.Father_cell_phone = maskedTextBox2.Text;
            customer.Father_email = textBox5.Text;
            customer.Father_place_of_employment = textBox6.Text;
            customer.Father_work_phone = maskedTextBox3.Text;
            customer.Mother_name = textBox10.Text;
            customer.Mother_address = textBox9.Text;
            customer.Mother_home_phone = maskedTextBox6.Text;
            customer.Mother_cell_phone = maskedTextBox5.Text;
            customer.Mother_email = textBox8.Text;
            customer.Mother_place_of_employment = textBox7.Text;
            customer.Mother_work_phone = maskedTextBox4.Text;
            customer.Child_name_doctor = textBox20.Text;
            customer.Child_address_doctor = textBox19.Text;
            customer.Child_phone_doctor = maskedTextBox12.Text;
            customer.Child_allergies = textBox18.Text;
            customer.Emergency_person1_name = textBox17.Text;
            customer.Emergency_person1_phone = maskedTextBox11.Text;
            customer.Emergency_person1_relationchip_to_child = textBox16.Text;
            customer.Emergency_person2_name = textBox12.Text;
            customer.Emergency_person2_phone = maskedTextBox7.Text;
            customer.Emergency_person2_relationchip_to_child = textBox11.Text;
            customer.Person_authorized_pickup1 = textBox13.Text;
            customer.Person_authorized_pickup2 = textBox14.Text;
            customer.End_of_trial_period = dateTimePicker3.Value.Date.ToShortDateString();
            customer.Sum_of_payment = Convert.ToInt32(textBox21.Text);

            Customer_catalog customer_catalog = (Customer_catalog)this.Owner;
            int val = Convert.ToInt32(customer_catalog.dg2.CurrentRow.Cells[0].Value);

            ConnectDB edit_customer = new ConnectDB();
            edit_customer.Edit_customer(val, customer.Date_of_agreement, customer.Client_surname, customer.Client_name, customer.Client_date_birth, customer.Father_name, customer.Father_address, customer.Father_home_phone, customer.Father_cell_phone, customer.Father_email, customer.Father_place_of_employment, customer.Father_work_phone, customer.Mother_name, customer.Mother_address, customer.Mother_home_phone, customer.Mother_cell_phone, customer.Mother_email, customer.Mother_place_of_employment, customer.Mother_work_phone, customer.Child_name_doctor, customer.Child_address_doctor, customer.Child_phone_doctor, customer.Child_allergies, customer.Emergency_person1_name, customer.Emergency_person1_phone, customer.Emergency_person1_relationchip_to_child, customer.Emergency_person2_name, customer.Emergency_person2_phone, customer.Emergency_person2_relationchip_to_child, customer.Person_authorized_pickup1, customer.Person_authorized_pickup2, CosmoKids.Duration_of_trial_period, customer.End_of_trial_period, customer.Sum_of_payment);

            //Update of DataGridView after inserting data
            Customer_catalog cc = (Customer_catalog)this.Owner;
            edit_customer.LoadCustomers(cc.dg2);

            //Display count of records
            cc.st1.Items[0].Text = "Total records: " + Convert.ToString(cc.dg2.Rows.Count);

            this.Close();
        }

        private void Edit_Customer_Shown(object sender, EventArgs e)
        {
            Customer_catalog customer_catalog = (Customer_catalog)this.Owner;
            int val = Convert.ToInt32(customer_catalog.dg2.CurrentRow.Cells[0].Value);

            ConnectDB conn = new ConnectDB();
            conn.Load_customers_4_editing(val, dateTimePicker1, textBox1, textBox2, dateTimePicker2, textBox3, textBox4, maskedTextBox1, maskedTextBox2, textBox5, textBox6, maskedTextBox3, textBox10, textBox9, maskedTextBox6, maskedTextBox5, textBox8, textBox7, maskedTextBox4, textBox20, textBox19, maskedTextBox12, textBox18, textBox17, maskedTextBox11, textBox16, textBox12, maskedTextBox7, textBox11, textBox13, textBox14, dateTimePicker3, textBox21);


        }
    }
}
