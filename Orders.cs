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
    public partial class Orders : Form
    {
        public Orders()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Orders_Load(object sender, EventArgs e)
        {
            Customer_catalog cc = (Customer_catalog)this.Owner;

            int idd = (int)cc.dg2.CurrentRow.Cells[0].Value;

            ConnectDB cdb = new ConnectDB();
            cdb.LoadOrders(idd, dataGridView1);
        }
    }
}
