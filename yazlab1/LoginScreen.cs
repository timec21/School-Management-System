using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using Spire.Pdf.Exporting.XPS.Schema;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace yazlab1
{

	public partial class LoginScreen : UserControl
	{
		public UserControl adminScreen;
		public UserControl studentScreen;
		public UserControl teacherScreen;
		public User user;

		NpgsqlConnection connection = new NpgsqlConnection("server=localHost; port=5432; Database=yazlab1; user ID=postgres; password=admin");

		public LoginScreen()
		{
			InitializeComponent();
			user = new User();
		}

		private void LoginScreen_Load(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			user.username = textBox1.Text;
			user.password = textBox2.Text;

			if (checkBox1.Checked)
			{


				string query = "SELECT * FROM collage WHERE admin_username = @username AND admin_password = @password";

				connection.Open();

				using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
				{
					cmd.Parameters.AddWithValue("username", user.username);
					cmd.Parameters.AddWithValue("password", user.password);

					using (NpgsqlDataReader reader = cmd.ExecuteReader())
					{
						if (!reader.Read())
						{
							MessageBox.Show("Eksik veya Yanlış Giriş Yapıldı");
							connection.Close();
							return;
						}
						connection.Close();
					}
				}

				adminScreen.Visible = true;
				this.Visible = false;
				checkBox2.Checked = false;
				checkBox3.Checked = false;
			}
			if (checkBox2.Checked)
			{
				string query = "SELECT * FROM teachers WHERE username = @username AND password = @password";

				connection.Open();

				using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
				{
					cmd.Parameters.AddWithValue("username", user.username);
					cmd.Parameters.AddWithValue("password", user.password);

					using (NpgsqlDataReader reader = cmd.ExecuteReader())
					{
						if (!reader.Read())
						{
							MessageBox.Show("Eksik veya Yanlış Giriş Yapıldı");
							connection.Close();
							return;
						}
						else
						{
							user.name = reader["name"].ToString();
							user.surname = reader["surname"].ToString();
							user.id = int.Parse(reader["identification_number"].ToString());
						}
						connection.Close();
					}
				}

				this.Visible = false;
				teacherScreen.Visible = true;
				checkBox1.Checked = false;
				checkBox3.Checked = false;

			}
			if (checkBox3.Checked)
			{
				string query = "SELECT * FROM students WHERE username = @username AND password = @password";

				connection.Open();

				using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
				{
					cmd.Parameters.AddWithValue("username", user.username);
					cmd.Parameters.AddWithValue("password", user.password);

					using (NpgsqlDataReader reader = cmd.ExecuteReader())
					{
						if (!reader.Read())
						{
							MessageBox.Show("Eksik veya Yanlış Giriş Yapıldı");
							connection.Close();
							return;
						}
						else
						{
							user.name = reader["name"].ToString();
							user.surname = reader["surname"].ToString();
							user.id = int.Parse(reader["student_id"].ToString());
						}
						connection.Close();
					}
				}

				studentScreen.Visible = true;
				this.Visible = false;
				checkBox1.Checked = false;
				checkBox2.Checked = false;
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox1.Checked)
			{
				checkBox2.Checked = false;
				checkBox3.Checked = false;
			}
		}

		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox2.Checked)
			{
				checkBox1.Checked = false;
				checkBox3.Checked = false;
			}
		}

		private void checkBox3_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox3.Checked)
			{
				checkBox2.Checked = false;
				checkBox1.Checked = false;
			}
		}

		private void LoginScreen_VisibleChanged(object sender, EventArgs e)
		{
			if (!this.Visible)
			{
				checkBox1.Checked = false;
				checkBox2.Checked = false;
				checkBox3.Checked = false;
				textBox1.Text = "";
				textBox2.Text = "";
			}
		}
	}
	public class User
	{
		public string username;
		public string password;
		public string name;
		public string surname;
		public int id;
	}
}
