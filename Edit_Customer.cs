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
