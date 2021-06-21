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
using System.IO;

namespace BillingSystem
{
    public partial class view_employee : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-NQN7RPE;Initial Catalog=Billing_system;Integrated Security=True;Pooling=False");

        string pwd = Class1.GetRandomPassword(20);
        string wanted_path;
        DialogResult result;

        public view_employee()
        {
            InitializeComponent();
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void View_employee_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            disp_data();
        }

        public void disp_data()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Refresh();
          
            int i = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from employee";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;


            Bitmap img;
            DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
            imageCol.HeaderText = "employee image";
            imageCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            imageCol.Width = 100;
            dataGridView1.Columns.Add(imageCol);

            foreach (DataRow dr in dt.Rows)
            {
                img = new Bitmap(@"..\..\" + dr["Eimage"].ToString());
                dataGridView1.Rows[i].Cells[9].Value = img;
                dataGridView1.Rows[i].Height = 100;
                i = i + 1;
            }
            con.Close();
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from employee where id=" + i + "";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    name.Text = dr["Ename"].ToString();
                    address.Text = dr["Eaddress"].ToString();
                    contact.Text = dr["Econtact"].ToString();
                    email.Text = dr["Eemail"].ToString();
                    dateTimePicker1.Text = dr["Eentrydate"].ToString();
                    leavedate.Text = dr["Eleavedate"].ToString();
                    status.Text = dr["Estatus"].ToString();

                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            result = openFileDialog1.ShowDialog();
            openFileDialog1.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files(*.png)|*.png|JPG Files(*.jpg)|*.jpg|GIF Files(*.gif)|*.gif";
        }

       


        private void Button2_Click(object sender, EventArgs e)
        {
                int i;
                i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
                string img_path;
                File.Copy(openFileDialog1.FileName, wanted_path + "\\employee_image\\" + pwd + ".jpg");
                img_path = "employee_image\\" + pwd + ".jpg";
                try
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update employee set Ename='" + name.Text + "',Eaddress='" + address.Text + "',Econtact='" + contact.Text + "',Eemail='" + email.Text + "',Eimage='"+ img_path.ToString() +"',EentryDate='" + dateTimePicker1.Value + "',EleaveDate='" + leavedate.Text + "',Estatus='" + status.Text + "' where id=" + i + "";
                    cmd.ExecuteNonQuery();
                    disp_data();
                    MessageBox.Show("Record updated successfully!!");
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            int i;
            i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete employee where id=" + i + "";
                cmd.ExecuteNonQuery();
                disp_data();
                MessageBox.Show("Record deleted successfully!!");
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }

        private void TextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                dataGridView1.Columns.Clear();
                dataGridView1.Refresh();

                con.Open();
                int i = 0;
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from employee where Ename like('%"+ textBox1.Text +"%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;


                Bitmap img;
                DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
                imageCol.HeaderText = "employee image";
                imageCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                imageCol.Width = 100;
                dataGridView1.Columns.Add(imageCol);

                foreach (DataRow dr in dt.Rows)
                {
                    img = new Bitmap(@"..\..\" + dr["Eimage"].ToString());
                    dataGridView1.Rows[i].Cells[9].Value = img;
                    dataGridView1.Rows[i].Height = 100;
                    i = i + 1;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}