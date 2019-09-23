using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Configuration;
using System.Windows.Forms;

namespace CRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // Строка подключения к Базе Данных
        readonly static string MyConnetion = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;
        // метод чистки строк
        void Clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
        int number;
        // кнопка добавления
        void Button1_Click(object sender, EventArgs e)
        {
            using(SqlConnection sqlConnection = new SqlConnection(MyConnetion))
            {
                string query = $"insert into Testing (Name, Status) values ('{textBox2.Text}','{textBox3.Text}')";
                try
                {
                    sqlConnection.Open();
                    SqlCommand command = new SqlCommand(query, sqlConnection);
                    number += command.ExecuteNonQuery();
                    MessageBox.Show("Добавление данных прошло успешно!", "Уведомление!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    Clear();
                    Referesh();
                }
                catch(Exception ex) { MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
        // кнопка удаления
        void Button2_Click(object sender, EventArgs e)
        {
            using(SqlConnection sqlConnection = new SqlConnection(MyConnetion))
            {
                try
                {
                    string query = $"delete from Testing where ID='{textBox1.Text}'";
                    sqlConnection.Open();
                    SqlCommand command = new SqlCommand(query, sqlConnection);
                    number += command.ExecuteNonQuery();
                    MessageBox.Show("Удаление данных прошло успешно!", "Уведомление!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    Clear();
                    Referesh();
                }
                catch(Exception ex) { MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
        // кнопка обновления
        void Button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(MyConnetion))
            {
                sqlConnection.Open();
                string query = $"update Testing set Name='{textBox2.Text}', Status='{textBox3.Text}' WHERE id='{textBox1.Text}'";
                SqlCommand command = new SqlCommand(query, sqlConnection);
                number += command.ExecuteNonQuery();
                MessageBox.Show("Изменение данных прошло успешно!", "Уведомление!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Clear();
                Referesh();
            }
        }
        // показать данные в таблице
        void ПоказатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Size = new Size(611, 247);
        }
        // скрыть данные в таблице
        void СкрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Size = new Size(310, 230);
        }
        // загрузка данных
        public new DataTable Select()
        {
            DataTable data = new DataTable();
            try
            {
                using(SqlConnection sqlConnection = new SqlConnection(MyConnetion))
                {
                    sqlConnection.Open();
                    string query = "select * from Testing";
                    SqlCommand command = new SqlCommand(query, sqlConnection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(data);
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            return data;
        }
        // обновления данных
        void Referesh()
        {
            DataTable data = Select();
            dataGridView1.DataSource = data;
        }
        // вывод данных
        void Form1_Load(object sender, EventArgs e)
        {
            Referesh();
        }
    }
}
