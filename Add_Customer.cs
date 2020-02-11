using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CosmoKids
{
    public partial class Add_Customer : Form
    {
        public Add_Customer()
        {
            InitializeComponent();            
        }

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

            ConnectDB new_customer = new ConnectDB();
            new_customer.Add_customer(customer.Date_of_agreement, customer.Client_surname, customer.Client_name, customer.Client_date_birth, customer.Father_name, customer.Father_address, customer.Father_home_phone, customer.Father_cell_phone, customer.Father_email, customer.Father_place_of_employment, customer.Father_work_phone, customer.Mother_name, customer.Mother_address, customer.Mother_home_phone, customer.Mother_cell_phone, customer.Mother_email, customer.Mother_place_of_employment, customer.Mother_work_phone, customer.Child_name_doctor, customer.Child_address_doctor, customer.Child_phone_doctor, customer.Child_allergies, customer.Emergency_person1_name, customer.Emergency_person1_phone, customer.Emergency_person1_relationchip_to_child, customer.Emergency_person2_name, customer.Emergency_person2_phone, customer.Emergency_person2_relationchip_to_child, customer.Person_authorized_pickup1, customer.Person_authorized_pickup2, CosmoKids.Duration_of_trial_period, customer.End_of_trial_period, customer.Sum_of_payment);

            //Update of DataGridView after inserting data
            Customer_catalog cc = (Customer_catalog)this.Owner;
            new_customer.LoadCustomers(cc.dg2); 
            
            this.Close();           

        }

        private void textBox21_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
            return;            
        }
    }
}
