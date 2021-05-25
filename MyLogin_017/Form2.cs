using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MyLogin_017
{
    public partial class MasterProduct : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-5VJ7M3BC;Initial Catalog=MyPractice;Integrated Security=True;");

        public MasterProduct()
        {
            InitializeComponent();
        }

        private void MasterProduct_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select isnull(max (cast (ID as int)), 0) +1 from TB_M_PRODUCT", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            txtID.Text = dt.Rows[0][0].ToString();
            LoadData();
        }

        DataClasses1DataContext db = new DataClasses1DataContext();
        private void button2_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtID.Text);
            string item = txtItem.Text;
            string design = txtDesign.Text;
            string color = cbColor.Text;
            DateTime expiredDate = DateTime.Parse(dateExpired.Text);
            var data = new TB_M_PRODUCT
            {
                ID = id,
                itemName = item,
                color = color,
                design = design,
                expiredDate = expiredDate
            };
            db.TB_M_PRODUCTs.InsertOnSubmit(data);
            db.SubmitChanges();
            MessageBox.Show("Save Successfully");
            txtDesign.Clear();
            txtItem.Clear();
            cbColor.Items.Clear();
            LoadData();
        }

        void LoadData()
        {
            var st = from tb in db.TB_M_PRODUCTs select tb;
            dt.DataSource = st;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var st = from s in db.TB_M_PRODUCTs where s.itemName == txtSearchItem.Text || s.design == txtSearchDesign.Text select s;
            dt.DataSource = st;
        }
    }
}
