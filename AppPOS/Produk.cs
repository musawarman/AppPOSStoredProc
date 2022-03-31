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

namespace AppPOS
{
    public partial class Produk : Form
    {
        public Produk()
        {
            InitializeComponent();
        }

        public void Display()
        {
            using (SqlConnection Koneksi = new SqlConnection(sqlConnect.Connect))
            {
                Koneksi.Open();
                SqlDataAdapter getData = new SqlDataAdapter("SELECT * FROM TBProduk Order By ID Desc", Koneksi);
                getData.SelectCommand.ExecuteNonQuery();
                DataTable dt = new DataTable();
                getData.Fill(dt);

                dgData.DataSource = dt;
            }

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection Connect = new SqlConnection(sqlConnect.Connect))
                {
                    Connect.Open();
                    SqlCommand sqlInsert = new SqlCommand("Exec spINSERTPRODUCT @pName, @Desc, @Price, @Stock", Connect);
                    sqlInsert.Parameters.AddWithValue("@pName", txtName.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@Desc", txtDesc.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@Price", txtPrice.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@Stock", txtStock.Text.Trim());
                    sqlInsert.ExecuteNonQuery();
                    MessageBox.Show("Data tersimpan...");
                    using (SqlConnection Koneksi = new SqlConnection(sqlConnect.Connect))
                    {
                        Koneksi.Open();
                        SqlDataAdapter getData = new SqlDataAdapter("SELECT * FROM TBProduk Order By ID Desc", Koneksi);
                        getData.SelectCommand.ExecuteNonQuery();
                        DataTable dt = new DataTable();
                        getData.Fill(dt);

                        dgData.DataSource = dt;
                    }


                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection Connect = new SqlConnection(sqlConnect.Connect))
                {
                    Connect.Open();
                    SqlDataAdapter sqlInsert = new SqlDataAdapter("Exec spGetData @pName", Connect);
                    sqlInsert.SelectCommand.Parameters.AddWithValue("@pName", txtSearch.Text.Trim());
                    sqlInsert.SelectCommand.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    sqlInsert.Fill(dt);

                    dgData.DataSource = dt;
             
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Produk_Load(object sender, EventArgs e)
        {
            Display();
        }
    }
}
