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
    public partial class main : Form
    {        
        public main()
        {
            InitializeComponent();     
        }

        private void main_Resize(object sender, EventArgs e)
        {
            Form main = new Form();
            label1.Left = this.Width / 2 - 150;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Form_daycare form_daycare = new Form_daycare();
            //form_daycare.ShowDialog();

            Customer_catalog customer_catalog = new Customer_catalog();
            customer_catalog.ShowDialog();
        }
               
    }
}
