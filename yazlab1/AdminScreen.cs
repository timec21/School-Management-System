using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using Spire.Pdf;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace yazlab1
{
	public partial class AdminScreen : UserControl
	{
		NpgsqlConnection connection = new NpgsqlConnection("server=localHost; port=5432; Database=yazlab1; user ID=postgres; password=admin");
		public UserControl loginScreen;
		public AdminScreen()
		{
			InitializeComponent();
			GetTranscript();
		}

		private void AdminScreen_Load(object sender, EventArgs e)
		{
			textBox1.Text = null;
			textBox2.Text = null;
			textBox3.Text = null;
			textBox4.Text = null;
			textBox5.Text = null;
		}
		void GetTranscript()
		{
			PdfDocument pdfDocument = new PdfDocument();
			pdfDocument.LoadFromFile("C:\\Users\\isgor\\Downloads\\transkript.pdf");

			string textData = "";

			foreach (PdfPageBase page in pdfDocument.Pages)
			{
				string text = page.ExtractText();
				textData += text;
			}

			pdfDocument.Close();

			TranscriptSplit(textData);
		}


		static List<string[]> course_data = new List<string[]>();

		List<string> grade = new List<string> { "AA", "BA", "BB", "CB", "CC", "DC", "DD", "FD", "FF" };

		List<string[]> RandomTranscriptGenerate()
		{
			List<string[]> randomTranscript = new List<string[]>();

			Random random = new Random();

			for (int i = 0; i < 50; i++)
			{
				int index = random.Next(course_data.Count);
				string[] selectedCourse = course_data[index];

				bool isDuplicate = false;

				foreach (string[] course in randomTranscript)
				{
					if (course[0] == selectedCourse[0] && course[1] == selectedCourse[1])
					{
						isDuplicate = true;
						break;
					}
				}

				if (!isDuplicate)
				{
					int randomIndex = random.Next(grade.Count);
					string randomGrade = grade[randomIndex];
					selectedCourse[2] = randomGrade;
					randomTranscript.Add(selectedCourse);
				}
			}

			return randomTranscript;
		}
		void TranscriptSplit(string text)
		{
			string[] splittedLines = text.Split('\n');
			decimal gpa = 0;
			for (int i = 0; i < splittedLines.Length; i++)
			{
				if (splittedLines[i].EndsWith("(Comment)\r"))
				{
					int x = i + 1;
					while (!splittedLines[x].TrimStart().StartsWith("DNO"))
					{
						List<string> course = new List<string>();
						string[] a = splittedLines[x].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
						course.Add(a[0]);
						List<string> courseNameParts = new List<string>();
						for (int j = 1; j < a.Length; j++)
						{
							if (a[j] != "Z" && a[j] != "S")
							{
								courseNameParts.Add(a[j]);
							}
							else
							{
								break;
							}
						}
						string courseName = string.Join(" ", courseNameParts);
						course.Add(courseName);
						course.Add(a[a.Length - 3]);
						course_data.Add(course.ToArray());
						x += 2;
					}
				}
			}
		}
		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox2.Checked)
			{
				button3.Visible = true;
				button4.Visible = true;
				button5.Visible = false;

				numericUpDown1.Visible = false;

				comboBox1.Visible = true;

				label1.Visible = true;
				label2.Visible = true;
				label3.Visible = true;
				label4.Visible = true;
				label5.Visible = true;
				label6.Visible = false;
				label7.Visible = false;
				textBox1.Visible = true;
				textBox2.Visible = true;
				textBox3.Visible = true;
				textBox4.Visible = true;
				textBox5.Visible = true;
				checkBox1.Checked = false;
				checkBox3.Checked = false;
				checkBox4.Checked = false;

				checkBox5.Visible = false;

				label1.Location = new Point(21, 31);
				label5.Location = new Point(256, 32);
				textBox5.TextAlign = HorizontalAlignment.Left;
				textBox1.TextAlign = HorizontalAlignment.Left;

				comboBox1.Items.Clear();
				comboBox1.Text = null;
				comboBox1.Items.Add("Kayıt");
				label1.Text = "Ad";
				label5.Text = "Kullanıcı Adı";
				comboBox1.SelectedIndex = 0;

				connection.Open();
				using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT identification_number, name, surname FROM teachers", connection))
				using (NpgsqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						string formattedTeacher = $"{reader["identification_number"]} {reader["name"]} {reader["surname"]}";
						comboBox1.Items.Add(formattedTeacher);
					}
				}
				connection.Close();
			}
		}

		private void label4_Click(object sender, EventArgs e)
		{

		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e) //student
		{
			if (checkBox1.Checked)
			{
				button3.Visible = true;
				button4.Visible = true;
				button5.Visible = false;

				numericUpDown1.Visible = false;

				comboBox1.Visible = true;

				label1.Visible = true;
				label2.Visible = true;
				label3.Visible = false;
				label4.Visible = true;
				label5.Visible = true;
				label6.Visible = false;
				label7.Visible = false;
				textBox1.Visible = true;
				textBox2.Visible = true;
				textBox3.Visible = false;
				textBox4.Visible = true;
				textBox5.Visible = true;
				checkBox2.Checked = false;
				checkBox3.Checked = false;
				checkBox4.Checked = false;

				checkBox5.Visible = false;

				label1.Location = new Point(21, 31);
				label5.Location = new Point(256, 32);
				textBox5.TextAlign = HorizontalAlignment.Left;
				textBox1.TextAlign = HorizontalAlignment.Left;

				comboBox1.Items.Clear();
				comboBox1.Text = null;
				comboBox1.Items.Add("Kayıt");
				label1.Text = "Ad";
				label5.Text = "Kullanıcı Adı";
				comboBox1.SelectedIndex = 0;

				connection.Open();
				using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT student_id, name, surname FROM students", connection))
				using (NpgsqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						string formattedStudent = $"{reader["student_id"]} {reader["name"]} {reader["surname"]}";
						comboBox1.Items.Add(formattedStudent);
					}
				}
				connection.Close();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (checkBox1.Checked && textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && textBox4.Text.Length > 0 && textBox5.Text.Length > 0) //student
			{

				connection.Open();

				using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO students (name, surname, username, password, collage_id) VALUES (@name, @surname, @username, @password, 1)"))
				{
					cmd.Connection = connection;

					cmd.Parameters.AddWithValue("@name", textBox1.Text);
					cmd.Parameters.AddWithValue("@surname", textBox2.Text);
					cmd.Parameters.AddWithValue("@username", textBox5.Text);
					cmd.Parameters.AddWithValue("@password", textBox4.Text);

					cmd.ExecuteNonQuery();
				}
				connection.Close();

				MessageBox.Show("Kayıt Başarılı");
			}
			else if (checkBox2.Checked && textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && int.TryParse(textBox3.Text, out int n) && textBox4.Text.Length > 0 && textBox5.Text.Length > 0) //teacher
			{
				connection.Open();

				using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO teachers (name, surname, quota, username, password, collage_id) VALUES (@name, @surname, @quota, @username, @password, 1)"))
				{
					cmd.Connection = connection;

					cmd.Parameters.AddWithValue("@name", textBox1.Text);
					cmd.Parameters.AddWithValue("@surname", textBox2.Text);
					cmd.Parameters.AddWithValue("@quota", int.Parse(textBox3.Text));
					cmd.Parameters.AddWithValue("@username", textBox5.Text);
					cmd.Parameters.AddWithValue("@password", textBox4.Text);

					cmd.ExecuteNonQuery();
				}
				connection.Close();

				MessageBox.Show("Kayıt Başarılı");
			}
			else if (checkBox3.Checked && textBox1.Text.Length > 0 && textBox5.Text.Length > 0) //lectures
			{
				string[] parts = comboBox1.SelectedItem.ToString().Split(' ');

				if (parts.Length >= 1)
				{
					if (int.TryParse(parts[0], out int teacherId))
					{
						connection.Open();

						bool isFirstCourse = true;
						string updateQuery;

						using (NpgsqlCommand command = new NpgsqlCommand("SELECT lectures FROM teachers WHERE identification_number = @teacherid", connection))
						{
							command.Parameters.AddWithValue("teacherId", teacherId);

							using (NpgsqlDataReader reader = command.ExecuteReader())
							{
								if (reader.Read())
								{
									if (reader.IsDBNull(0))
									{
										isFirstCourse = true;
									}
									else
										isFirstCourse = false;
								}
								reader.Close();
							}
						}

						string couseCode = textBox1.Text;
						string courseName = textBox5.Text;
						string lectureStatus = "0";


						string jsonLecture = $@"[{{""course_code"": ""{couseCode}"", ""course_name"": ""{courseName}"", ""lecture_status"": ""{lectureStatus}""}}]";

						if (isFirstCourse)
						{
							updateQuery = @"
						UPDATE teachers
						SET lectures = @jsonLecture
						WHERE identification_number = @teacherId";
							isFirstCourse = false;
						}
						else
						{
							updateQuery = @"
						UPDATE teachers
						SET lectures = lectures || @jsonLecture
						WHERE identification_number = @teacherId";
						}
						using (NpgsqlCommand command = new NpgsqlCommand(updateQuery, connection))
						{
							command.Parameters.AddWithValue("jsonLecture", NpgsqlTypes.NpgsqlDbType.Jsonb, jsonLecture);
							command.Parameters.AddWithValue("teacherId", teacherId);

							command.ExecuteNonQuery();
						}

						connection.Close();
						MessageBox.Show("Kayıt Başarılı");
						return;
					}
				}
			}
			else if (checkBox4.Checked && textBox1.Text.Length > 0 && textBox5.Text.Length > 0 && int.TryParse(textBox1.Text, out int characterLimit) && int.TryParse(textBox5.Text, out int demandLimit))
			{
				connection.Open();

				using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT COUNT(*) FROM collage WHERE collage_id = 1", connection))
				{
					long existingRecordCount = (long)cmd.ExecuteScalar();

					if (existingRecordCount > 0)
					{
						// Mevcut bir kayıt var, bu nedenle güncelleme yapmalıyız
						using (NpgsqlCommand updateCmd = new NpgsqlCommand("UPDATE collage SET character_limit = @characterLimit, demand_limit = @demandLimit, teacher_select_limit = @teacherSelectLimit WHERE collage_id = 1", connection))
						{
							updateCmd.Parameters.AddWithValue("@characterLimit", characterLimit);
							updateCmd.Parameters.AddWithValue("@demandLimit", demandLimit);
							updateCmd.Parameters.AddWithValue("@teacherSelectLimit", checkBox5.Checked);

							updateCmd.ExecuteNonQuery();
						}
					}
					else
					{
						// collage_id = 1 olan bir kayıt yok, yeni bir kayıt eklemeliyiz
						using (NpgsqlCommand insertCmd = new NpgsqlCommand("INSERT INTO collage (collage_id, character_limit, demand_limit, teacher_select_limit, admin_username, admin_password) VALUES (1, @characterLimit, @demandLimit, @teacherSelectLimit, 'Admin', 'admin')", connection))
						{
							insertCmd.Parameters.AddWithValue("@characterLimit", characterLimit);
							insertCmd.Parameters.AddWithValue("@demandLimit", demandLimit);
							insertCmd.Parameters.AddWithValue("@teacherSelectLimit", checkBox5.Checked);

							insertCmd.ExecuteNonQuery();
						}
					}
				}

				connection.Close();

				MessageBox.Show("Kayıt Başarılı");
				return;
			}
			else
			{
				MessageBox.Show("Eksik veya Hatalı Giriş Yapıldı");
				return;
			}
			comboBox1.SelectedIndex = 0;
			checkBox1_CheckedChanged(sender, new EventArgs());
			checkBox2_CheckedChanged(sender, new EventArgs());
		}

		private void button3_Click(object sender, EventArgs e)
		{
			string[] parts;
			int selectedIndex = comboBox1.SelectedIndex;

			if (checkBox1.Checked && comboBox1.SelectedItem != null && textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && textBox4.Text.Length > 0 && textBox5.Text.Length > 0) //student
			{
				parts = comboBox1.SelectedItem.ToString().Split(' ');

				if (parts.Length >= 1)
				{
					if (int.TryParse(parts[0], out int studentId))
					{
						connection.Open();
						using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE students SET name = @name, surname = @surname, username = @username, password = @password WHERE student_id = @id", connection))
						{
							cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, studentId);
							cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Text, textBox1.Text);
							cmd.Parameters.AddWithValue("@surname", NpgsqlDbType.Text, textBox2.Text);
							cmd.Parameters.AddWithValue("@username", NpgsqlDbType.Text, textBox5.Text);
							cmd.Parameters.AddWithValue("@password", NpgsqlDbType.Text, textBox4.Text);

							cmd.ExecuteNonQuery();

						}
						connection.Close();
					}
				}

				if (checkBox2.Checked && comboBox1.SelectedItem != null && textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && int.TryParse(textBox3.Text, out int n) && textBox4.Text.Length > 0 && textBox5.Text.Length > 0) //teacher
				{
					parts = comboBox1.SelectedItem.ToString().Split(' ');

					if (parts.Length >= 1)
					{
						if (int.TryParse(parts[0], out int teacherId))
						{
							connection.Open();
							using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE teachers SET name = @name, surname = @surname, quota = @quota, username = @username, password = @password WHERE identification_number = @id", connection))
							{
								cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, teacherId);
								cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Text, textBox1.Text);
								cmd.Parameters.AddWithValue("@surname", NpgsqlDbType.Text, textBox2.Text);
								cmd.Parameters.AddWithValue("@quota", NpgsqlDbType.Integer, int.Parse(textBox3.Text));
								cmd.Parameters.AddWithValue("@username", NpgsqlDbType.Text, textBox5.Text);
								cmd.Parameters.AddWithValue("@password", NpgsqlDbType.Text, textBox4.Text);

								cmd.ExecuteNonQuery();

							}
							connection.Close();
						}
					}
				}
			}
			checkBox1_CheckedChanged(sender, new EventArgs());
			checkBox2_CheckedChanged(sender, new EventArgs());
			comboBox1.SelectedIndex = selectedIndex;
		}

		private void button4_Click(object sender, EventArgs e)
		{
			string[] parts;
			if (checkBox1.Checked && comboBox1.SelectedItem != null && textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && textBox4.Text.Length > 0 && textBox5.Text.Length > 0) //student
			{
				parts = comboBox1.SelectedItem.ToString().Split(' ');

				if (parts.Length >= 1)
				{
					if (int.TryParse(parts[0], out int studentId))
					{
						connection.Open();
						using (NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM students WHERE student_id = @id", connection))
						{
							cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, studentId);
							cmd.ExecuteNonQuery();
						}
						connection.Close();
					}
				}
			}

			if (checkBox2.Checked && comboBox1.SelectedItem != null && textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && int.TryParse(textBox3.Text, out int n) && textBox4.Text.Length > 0 && textBox5.Text.Length > 0) //teacher
			{
				parts = comboBox1.SelectedItem.ToString().Split(' ');

				if (parts.Length >= 1)
				{
					if (int.TryParse(parts[0], out int teacherId))
					{
						connection.Open();
						using (NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM teachers WHERE identification_number = @id", connection))
						{
							cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, teacherId);
							cmd.ExecuteNonQuery();
						}
						connection.Close();
					}
				}
			}
			comboBox1.SelectedIndex = 0;
			checkBox1_CheckedChanged(sender, new EventArgs());
			checkBox2_CheckedChanged(sender, new EventArgs());
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			button3.Enabled = true;
			if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "Kayıt")
			{
				textBox1.Text = "";
				textBox2.Text = "";
				textBox3.Text = "";
				textBox5.Text = "";
				textBox4.Text = "";
			}
			else if (comboBox1.SelectedItem != null && checkBox1.Checked)
			{
				string[] parts = comboBox1.SelectedItem.ToString().Split(' ');

				if (parts.Length >= 1)
				{
					if (int.TryParse(parts[0], out int studentId))
					{
						connection.Open();
						using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT name, surname, username, password FROM students WHERE student_id = @id", connection))
						{
							cmd.Parameters.AddWithValue("@id", studentId);
							using (NpgsqlDataReader reader = cmd.ExecuteReader())
							{
								if (reader.Read())
								{
									textBox1.Text = reader["name"].ToString();
									textBox2.Text = reader["surname"].ToString();
									textBox5.Text = reader["username"].ToString();
									textBox4.Text = reader["password"].ToString();
								}
							}
						}
						connection.Close();
					}
				}


			}
			else if (comboBox1.SelectedItem != null && checkBox2.Checked)
			{
				string[] parts = comboBox1.SelectedItem.ToString().Split(' ');

				if (parts.Length >= 1)
				{
					if (int.TryParse(parts[0], out int teacherId))
					{
						connection.Open();
						using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT name, surname, quota, username, password FROM teachers WHERE identification_number = @id", connection))
						{
							cmd.Parameters.AddWithValue("@id", teacherId);
							using (NpgsqlDataReader reader = cmd.ExecuteReader())
							{
								if (reader.Read())
								{
									textBox1.Text = reader["name"].ToString();
									textBox2.Text = reader["surname"].ToString();
									textBox3.Text = reader["quota"].ToString();
									textBox5.Text = reader["username"].ToString();
									textBox4.Text = reader["password"].ToString();
								}
							}
						}
						connection.Close();
					}
				}
			}
		}

		private void checkBox3_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox3.Checked)
			{
				foreach (Control control in this.Controls)
				{
					if (control is TextBox)
					{
						TextBox textBox = (TextBox)control;
						textBox.Text = "";
					}
				}

				button3.Visible = false;
				button4.Visible = false;
				button5.Visible = false;

				comboBox1.Visible = true;

				numericUpDown1.Visible = false;

				label1.Visible = true;
				label2.Visible = false;
				label3.Visible = false;
				label4.Visible = false;
				label5.Visible = true;
				label7.Visible = false;
				textBox1.Visible = true;
				textBox2.Visible = false;
				textBox3.Visible = false;
				textBox4.Visible = false;
				textBox5.Visible = true;
				checkBox1.Checked = false;
				checkBox2.Checked = false;
				checkBox4.Checked = false;

				checkBox5.Visible = false;

				label1.Location = new Point(21, 31);
				label5.Location = new Point(256, 32);
				textBox5.TextAlign = HorizontalAlignment.Left;
				textBox1.TextAlign = HorizontalAlignment.Left;

				label1.Text = "Ders Kodu";
				label5.Text = "Ders Adı";
				label6.Visible = true;

				comboBox1.Items.Clear();
				comboBox1.Text = null;

				connection.Open();
				using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT identification_number, name, surname FROM teachers", connection))
				using (NpgsqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						string formattedTeacher = $"{reader["identification_number"]} {reader["name"]} {reader["surname"]}";
						comboBox1.Items.Add(formattedTeacher);
					}
				}
				connection.Close();
			}

		}

		private void button2_Click(object sender, EventArgs e)
		{
			foreach (Control control in this.Controls)
			{
				if (control is Label)
				{
					Label label = (Label)control;
					label.Visible = false;
				}
				else if (control is TextBox)
				{
					TextBox textBox = (TextBox)control;
					textBox.Visible = false;
				}
			}

			checkBox1.Checked = false;
			checkBox2.Checked = false;
			checkBox3.Checked = false;
			checkBox4.Checked = false;

			checkBox5.Visible = false;
			comboBox1.Visible = false;

			button1.Visible = true;
			button2.Visible = true;
			button3.Visible = true;
			button4.Visible = true;

			button5.Visible = false;

			numericUpDown1.Value = 0;
			numericUpDown1.Visible = false;

			comboBox1.SelectedItem = null;
			comboBox1.Items.Clear();
			loginScreen.Visible = true;
			this.Visible = false;
		}

		private void checkBox4_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox4.Checked)
			{
				foreach (Control control in this.Controls)
				{
					if (control is TextBox)
					{
						TextBox textBox = (TextBox)control;
						textBox.Text = "";
					}
				}
				comboBox1.Items.Clear();
				comboBox1.Text = null;

				button3.Visible = false;
				button4.Visible = false;
				button5.Visible = true;

				numericUpDown1.Visible = true;
				numericUpDown1.Maximum = 50;

				label2.Visible = false;
				label3.Visible = false;
				label4.Visible = false;
				label6.Visible = false;
				label7.Visible = true;
				textBox2.Visible = false;
				textBox3.Visible = false;
				textBox4.Visible = false;
				comboBox1.Visible = false;

				checkBox1.Checked = false;
				checkBox2.Checked = false;
				checkBox3.Checked = false;

				checkBox5.Visible = true;

				label1.Visible = true;
				label1.Text = "Mesaj Karakter\r\n        Limit";
				label1.Location = new Point(15, 20);
				textBox1.Visible = true;
				textBox5.Visible = true;
				label5.Visible = true;
				label5.Text = "Ders Başına Hoca\r\n    Seçim Limiti";
				label5.Location = new Point(244, 20);
				textBox5.TextAlign = HorizontalAlignment.Center;
				textBox1.TextAlign = HorizontalAlignment.Center;

				connection.Open();
				using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM collage WHERE collage_id = 1", connection))
				{
					using (NpgsqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							// Mevcut bir kayıt var, bu nedenle mevcut kayıdın özelliklerini alalım
							int characterLimit = reader.GetInt32(reader.GetOrdinal("character_limit"));
							int demandLimit = reader.GetInt32(reader.GetOrdinal("demand_limit"));
							bool teacherSelectLimit = reader.GetBoolean(reader.GetOrdinal("teacher_select_limit"));

							textBox1.Text = characterLimit.ToString();
							textBox5.Text = demandLimit.ToString();
							checkBox5.Checked = teacherSelectLimit;
						}
						else
						{
							textBox1.Text = null;
							textBox5.Text = "1";
							checkBox5.Checked = false;
						}
					}
				}
				connection.Close();

			}
		}


		private void button5_Click(object sender, EventArgs e)
		{

			connection.Open();
			for (int i = 0; i < (int)numericUpDown1.Value; i++)
			{
				Random random = new Random();

				string randomName = "Student" + random.Next(1000);

				string randomSurname = "Surname" + random.Next(1000);

				double randomGPA = Math.Round(random.NextDouble() * 4.0, 2);

				string randomUsername = "username" + random.Next(1000);

				string randomPassword = "password" + random.Next(1000);


				string insertQuery = "INSERT INTO students (name, surname, gpa, username, password, collage_id, transcript) VALUES (@name, @surname, @gpa, @username, @password, @collage_id, @transcript) RETURNING student_id";


				List<string[]> randomTranscript = RandomTranscriptGenerate();

				string courseCode = randomTranscript[0][0];
				string courseName = randomTranscript[0][1];
				string courseGrade = randomTranscript[0][2];
				int studentId;

				string jsonTranscript = $@"[{{""course_code"": ""{courseCode}"", ""course_name"": ""{courseName}"", ""course_grade"": ""{courseGrade}""}}]";



				using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, connection))
				{
					cmd.Parameters.AddWithValue("name", randomName);
					cmd.Parameters.AddWithValue("surname", randomSurname);
					cmd.Parameters.AddWithValue("gpa", randomGPA);
					cmd.Parameters.AddWithValue("username", randomUsername);
					cmd.Parameters.AddWithValue("password", randomPassword);
					cmd.Parameters.AddWithValue("collage_id", 1);

					cmd.Parameters.AddWithValue("transcript", NpgsqlTypes.NpgsqlDbType.Jsonb, jsonTranscript);

					studentId = (int)cmd.ExecuteScalar();

				}
				for (int j = 1; j < randomTranscript.Count; j++)
				{
					string updateQuery = @"
										UPDATE students
										SET transcript = transcript || @jsonTranscript
										WHERE student_id = @studentId";

					courseCode = randomTranscript[j][0];
					courseName = randomTranscript[j][1];
					courseGrade = randomTranscript[j][2];

					jsonTranscript = $@"[{{""course_code"": ""{courseCode}"", ""course_name"": ""{courseName}"", ""course_grade"": ""{courseGrade}""}}]";

					using (NpgsqlCommand command = new NpgsqlCommand(updateQuery, connection))
					{
						string courseJson = JsonConvert.SerializeObject(randomTranscript[j]);
						command.Parameters.AddWithValue("jsonTranscript", NpgsqlTypes.NpgsqlDbType.Jsonb, jsonTranscript);
						command.Parameters.AddWithValue("studentId", studentId);

						command.ExecuteNonQuery();
					}
				}
			}

			connection.Close();

			MessageBox.Show(numericUpDown1.Value + " Öğrenci Eklendi");
		}
	}
}
