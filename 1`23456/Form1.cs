using _1_23456._123;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1_23456
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
                try
                {
                    Model1 context = new Model1();
                    List<Faculty> listFalcultys = context.Faculties.ToList(); //l y các khoa
                    List<Student> listStudent = context.Students.ToList(); //l y sinh viên
                    FillFalcultyCombobox(listFalcultys);
                    BindGrid(listStudent);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
//Hàm binding list có tên hi n th là tên khoa, giá tr là Mã khoa
private void FillFalcultyCombobox(List<Faculty> listFalcultys)
        {
            this.comboBox1.DataSource = listFalcultys;
            this.comboBox1.DisplayMember = "FacultyName";
            this.comboBox1.ValueMember = "FacultyID";
        }
        //Hàm binding gridView t list sinh viên
        private void BindGrid(List<Student> listStudent)
        {
            dataGridView1.Rows.Clear();
            foreach (var item in listStudent)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = item.StudentID;
                dataGridView1.Rows[index].Cells[1].Value = item.FullName;
                dataGridView1.Rows[index].Cells[2].Value = item.Faculty.FacultyName;
                dataGridView1.Rows[index].Cells[3].Value = item.AverageScore;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) { 
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value?.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value?.ToString();
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value?.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value?.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (Model1 context = new Model1())
                {
                    Student newStudent = new Student
                    {
                        StudentID = int.Parse(textBox1.Text),
                        FullName = textBox2.Text,
                        AverageScore = decimal.Parse(textBox3.Text),
                        FacultyID = (int)comboBox1.SelectedValue
                    };

                    context.Students.Add(newStudent);
                    context.SaveChanges();

                    // Refresh DataGridView
                    BindGrid(context.Students.ToList());
                    MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            try
            {
                using (Model1 context = new Model1())
                {
                    int studentID = int.Parse(textBox1.Text);
                    Student dbStudent = context.Students.FirstOrDefault(s => s.StudentID == studentID);

                    if (dbStudent != null)
                    {
                        dbStudent.FullName = textBox2.Text;
                        dbStudent.AverageScore = decimal.Parse(textBox3.Text);
                        dbStudent.FacultyID = (int)comboBox1.SelectedValue;

                        context.SaveChanges();

                        // Refresh DataGridView
                        BindGrid(context.Students.ToList());
                        MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (Model1 context = new Model1())
                {
                    int studentID = int.Parse(textBox1.Text);
                    Student dbStudent = context.Students.FirstOrDefault(s => s.StudentID == studentID);

                    if (dbStudent != null)
                    {
                        context.Students.Remove(dbStudent);
                        context.SaveChanges();

                        // Refresh DataGridView
                        BindGrid(context.Students.ToList());
                        MessageBox.Show("Xóa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
    

