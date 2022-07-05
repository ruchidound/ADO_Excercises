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
using System.Configuration;

namespace ADO_Excercises
{
    public partial class FrmConnected : Form
    {
        public FrmConnected()
        {
            InitializeComponent();
        }
        private SqlConnection con = null;
        private SqlCommand cmd = null;
        private SqlDataReader reader = null;

        private void FrmConnected_Load(object sender, EventArgs e)
        {
            using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["TrainingConnection"].ConnectionString))
            {
                using (cmd = new SqlCommand("Select Stud_Code,Stud_Name,Dept_Code,Stud_Dob,Address from Student_master", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    using (reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            TxtStudentCode.Text = reader["Stud_Code"].ToString();
                            TxtName.Text = reader["Stud_Name"].ToString();
                            TxtDCode.Text = reader["Dept_Code"].ToString();
                            TxtDOB.Text = reader["Stud_Dob"].ToString();
                            txtAddress.Text = reader["Address"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No Record");

                        }
                    }
                }
            }
        }
        //Search Student by StudentCode
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["TrainingConnection"].ConnectionString))
            {
                using (cmd = new SqlCommand("Select Stud_Code,Stud_Name,Dept_Code,Stud_Dob,Address from Student_master where Stud_Code=@Student_Code ", con))
                {
                    cmd.Parameters.AddWithValue("@Student_Code", TxtStudentCode.Text);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    using (reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            TxtName.Text = reader["Stud_Name"].ToString();
                            TxtDCode.Text = reader["Dept_Code"].ToString();
                            TxtDOB.Text = reader["Stud_Dob"].ToString();
                            txtAddress.Text = reader["Address"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No Record Found");
                            this.ClearTest();
                        }

                    }
                }
            }
        }
        //Reset Form
        private void BtnReset_Click(object sender, EventArgs e)
        {
            this.ClearTest();
        }
        public void ClearTest()
        {
            TxtStudentCode.Text = "";
            TxtName.Text = "";
            TxtDCode.Text = "";
            TxtDOB.Text = "";
            txtAddress.Text = "";
            TxtStudentCode.Focus();
        }

        //Insert the Records
        private void BtnInsert_Click(object sender, EventArgs e)
        {
            using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["TrainingConnection"].ConnectionString))
            {
                using (cmd = new SqlCommand("usp_InsertStudentDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Stud_Code", TxtStudentCode.Text);
                    cmd.Parameters.AddWithValue("@Stud_Name", TxtName.Text);
                    cmd.Parameters.AddWithValue("@Dept_Code", TxtDCode.Text);
                    cmd.Parameters.AddWithValue("@Stud_Dob", TxtDOB.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("New Student Record Created!!");
        }

        //Update the Records
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["TrainingConnection"].ConnectionString))
            {
                using (cmd = new SqlCommand("usp_UpdateDCodeAndAddressBySCode", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Dept_Code", TxtDCode.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@Stud_Code", TxtStudentCode.Text);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("New Student Records Updated!!");
        }

        //Delete the Records
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["TrainingConnection"].ConnectionString))
            {
                using (cmd = new SqlCommand("Delete from Student_master where Stud_Code=@Stud_Code", con))
                {
                    cmd.Parameters.AddWithValue("@Stud_Code", TxtStudentCode.Text);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("New Student Records Has Been Deleted!!");
        }
    }
}