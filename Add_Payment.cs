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
    public partial class Add_Payment : Form
    {
        public Add_Payment()
        {
            InitializeComponent();
            cb1 = comboBox1;
            tb1 = textBox1;
            dt1 = dateTimePicker1;
            tb2 = textBox2;
            lb1 = label1;
        }

        public ComboBox cb1 { get; set; }
        public TextBox tb1 { get; set; }
        public DateTimePicker dt1 { get; set; }
        public TextBox tb2 { get; set; }
        public Label lb1 { get; set; }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Add_Payment_Load(object sender, EventArgs e)
        {
            ConnectDB cb = new ConnectDB();
            cb.GetKindPay(comboBox1);            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Input only numbers
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Customer_catalog ct = (Customer_catalog)this.Owner;
            
            int idd_payment = (int)ct.dg3.CurrentRow.Cells[0].Value;
            int idd_customer = (int)ct.dg2.CurrentRow.Cells[0].Value;

            if ((textBox1.Text.Length == 0) || (textBox1.Text == "0.00"))
            {
                MessageBox.Show("Incorrect amount.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox1.Text.Length == 0)
            {
                MessageBox.Show("You haven't choosen kind of payment.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
                        
            ConnectDB cd = new ConnectDB();
            cd.AddPayment(idd_customer, textBox1.Text, dateTimePicker1.Value.ToShortDateString(), comboBox1.Text, textBox2.Text);
            cd.LoadPayments(idd_customer, ct.dg3);

            this.Close();            
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
