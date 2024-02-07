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
using NpgsqlTypes;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using Newtonsoft.Json;
using System.Collections;
using System.Reflection.PortableExecutable;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace yazlab1
{
	public partial class TeacherScreen : UserControl
	{
		public int identificationid;
		private string userName;
		private string userPassword;
		private string courseInfo;
		private string studentinfo;
		private int studentid;
		private string courseName;
		private string courseid;
		private int teacher_quota;
		private string coursecode;
		private List<Tuple<int, double>> studentssum = new List<Tuple<int, double>>();
		private List<Tuple<int, double>> studentssum2 = new List<Tuple<int, double>>();
		public LoginScreen loginScreen = new LoginScreen();
		public User user;

		NpgsqlConnection connection = new NpgsqlConnection("server=localHost; port=5432; Database=yazlab1; user ID=postgres; password=admin");
		public TeacherScreen()
		{
			InitializeComponent();
			comboBox1.Name = "Mesajlar";
		}

		private void TeacherScreen_Load(object sender, EventArgs e)
		{
			button1.Visible = false;
			button2.Visible = false;
			button3.Visible = false;
			button6.Visible = false;

			listBox1.Visible = false;
			button8.Visible = false;
			button9.Visible = false;
			listBox3.Visible = false;
			button10.Visible = false;
			button1.Visible = true;
			button2.Visible = true;
			button3.Visible = true;
			checkedListBox2.Visible = false;
			button11.Visible = false;
			textBox1.Visible = false;
			button13.Visible = false;
			button14.Visible = false;
			comboBox1.Visible = false;
			button15.Visible = false;
			button17.Visible = false;
			checkedListBox3.Visible = false;
			button16.Visible = false;
			button18.Visible = false;
			comboBox2.Visible = false;
			label1.Visible = false;
			textBox2.Visible = false;
			button19.Visible = false;

			richTextBox2.Visible = false;
			listBox2.Visible = false;
			comboBox3.Visible = false;
			button21.Visible = false;

			userName = user.username;
			userPassword = user.password;

			checkedListBox2.Items.Clear();
			listBox1.Visible = false;
			connection.Open();
			string selectQuery = "SELECT identification_number FROM teachers WHERE username = @userName AND password = @userPassword";

			using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
			{
				command.Parameters.AddWithValue("userName", userName);
				command.Parameters.AddWithValue("userPassword", userPassword);

				using (NpgsqlDataReader reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						identificationid = reader.GetInt32(0);
					}
				}
			}
			connection.Close();





		}


		private void button1_Click(object sender, EventArgs e)  ///////TALEP EDEN ÖĞRENCİLERİ GÖSTERME
		{
			listBox1.Items.Clear();
			button1.Visible = false;
			button2.Visible = false;
			button3.Visible = false;
			button6.Visible = false;

			richTextBox2.Visible = false;
			listBox2.Visible = false;
			comboBox3.Visible = false;
			button21.Visible = false;

			listBox1.Visible = false;
			button8.Visible = false;
			button9.Visible = false;
			listBox3.Visible = false;
			button10.Visible = false;
			button1.Visible = true;
			button2.Visible = true;
			button3.Visible = true;
			checkedListBox2.Visible = false;
			button11.Visible = false;
			textBox1.Visible = false;
			button13.Visible = false;
			button14.Visible = false;
			comboBox1.Visible = false;
			button15.Visible = false;
			button17.Visible = false;
			checkedListBox3.Visible = false;
			button16.Visible = false;
			button18.Visible = false;
			comboBox2.Visible = false;
			label1.Visible = false;
			textBox2.Visible = false;

			studentssum.Clear();
			listBox1.Visible = false;
			button5.Visible = true;
			listBox1.Items.Clear();
			listBox1.Text = null;
			checkedListBox2.Visible = false;
			button10.Visible = false;
			button14.Visible = false;

			connection.Open();
			string query = "SELECT demanded_lectures, student_id, name, surname FROM students";


			List<JObject> jsonList = new List<JObject>();
			using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
			using (NpgsqlDataReader reader = cmd.ExecuteReader())
			{
				while (reader.Read())
				{
					if (!reader.IsDBNull(0)) // JSON verisi boş değilse
					{
						string json = reader.GetString(0);
						JArray demandedlecturesArray = JArray.Parse(json);
						foreach (var item in demandedlecturesArray)
						{
							string courseinfo = item["course_info"].ToString();

							// teachers_id'yi bir tamsayıya dönüştürün
							int teachersId = Convert.ToInt32(item["teachers_id"]);

							if (teachersId == identificationid)
							{
								if (item["agreement_status"].ToString() == "Talep Edildi")
								{
									listBox1.Items.Add(reader["student_id"] + " " + reader["name"] + " " + reader["surname"] + "-" + courseinfo);

								}
							}
						}
					}
				}

				connection.Close();
				listBox1.Visible = true;

			}
		}
		private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			button8.Visible = true;


		}

		private void button8_Click(object sender, EventArgs e)
		{
			button1.Visible = false;
			button2.Visible = false;
			button3.Visible = false;
			button6.Visible = false;

			listBox1.Visible = false;
			button8.Visible = false;
			button9.Visible = false;
			listBox3.Visible = false;
			button10.Visible = false;
			button1.Visible = true;
			button2.Visible = true;
			button3.Visible = true;
			checkedListBox2.Visible = false;
			button11.Visible = false;
			textBox1.Visible = false;
			button13.Visible = false;
			button14.Visible = false;
			comboBox1.Visible = false;
			button15.Visible = false;
			button17.Visible = false;
			checkedListBox3.Visible = false;
			button16.Visible = false;
			button18.Visible = false;
			comboBox2.Visible = false;
			label1.Visible = false;
			textBox2.Visible = false;

			button8.Visible = false;
			button1.Visible = true;
			button2.Visible = true;
			button3.Visible = true;
			richTextBox2.Visible = false;
			listBox2.Visible = false;
			comboBox3.Visible = false;
			button21.Visible = false;



		}



		private void button5_Click(object sender, EventArgs e)
		{
			button1.Visible = false;
			button2.Visible = false;
			button3.Visible = false;
			button6.Visible = false;

			listBox1.Visible = false;
			button8.Visible = false;
			button9.Visible = false;
			listBox3.Visible = false;
			button10.Visible = false;
			button1.Visible = true;
			button2.Visible = true;
			button3.Visible = true;
			checkedListBox2.Visible = false;
			button11.Visible = false;
			textBox1.Visible = false;
			button13.Visible = false;
			button14.Visible = false;
			comboBox1.Visible = false;
			button15.Visible = false;
			button17.Visible = false;
			checkedListBox3.Visible = false;
			button16.Visible = false;
			button18.Visible = false;
			comboBox2.Visible = false;
			label1.Visible = false;
			textBox2.Visible = false;

			listBox1.Visible = false;
			label1.Visible = true;
			comboBox2.Visible = true;
			textBox2.Visible = true;
			button17.Visible = true;
			checkedListBox3.Visible = true;
			button16.Visible = true;
			button18.Visible = true;
			richTextBox2.Visible = false;
			listBox2.Visible = false;
			comboBox3.Visible = false;
			button21.Visible = false;

			HashSet<string> courses = new HashSet<string>();


			string query = "SELECT transcript  FROM students";


			List<JObject> jsonList = new List<JObject>();


			connection.Open();
			using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
			using (NpgsqlDataReader reader = cmd.ExecuteReader())
			{
				while (reader.Read())
				{
					if (!reader.IsDBNull(0)) // JSON verisi boş değilse
					{
						string json = reader.GetString(0);
						JArray demandedlecturesArray = JArray.Parse(json);
						foreach (var item in demandedlecturesArray)
						{
							string courseinfo = item["course_code"].ToString();
							string coursename = item["course_name"].ToString();
							string course = courseinfo + " " + coursename;

							courses.Add(course);


						}


					}
				}

			}
			connection.Close();
			foreach (var item in courses)
			{
				comboBox2.Items.Add(item);
			}






		}

		private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			button9.Visible = true;
			button14.Visible = true;
			button15.Visible = true;
			richTextBox2.Visible = false;
			listBox2.Visible = false;
			comboBox3.Visible = false;
			button21.Visible = false;
			comboBox1.Visible = true;
		}

		private void button9_Click(object sender, EventArgs e)
		{
			button1.Visible = false;
			button2.Visible = false;
			button3.Visible = false;
			button6.Visible = false;
			richTextBox2.Visible = false;
			listBox2.Visible = false;
			comboBox3.Visible = false;
			button21.Visible = false;
			listBox1.Visible = false;
			button8.Visible = false;
			button9.Visible = false;
			listBox3.Visible = false;
			button10.Visible = false;
			button1.Visible = true;
			button2.Visible = true;
			button3.Visible = true;
			checkedListBox2.Visible = false;
			button11.Visible = false;
			textBox1.Visible = false;
			button13.Visible = false;
			button14.Visible = false;
			comboBox1.Visible = false;
			button15.Visible = false;
			button17.Visible = false;
			checkedListBox3.Visible = false;
			button16.Visible = false;
			button18.Visible = false;
			comboBox2.Visible = false;
			label1.Visible = false;
			textBox2.Visible = false;

			button9.Visible = false;
			listBox1.Items.Clear();
			listBox1.Text = null;
			connection.Open();
			using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT name FROM students", connection))
			using (NpgsqlDataReader reader = cmd.ExecuteReader())
			{
				while (reader.Read())
				{
					listBox1.Items.Add(reader["name"]);
				}
			}
			listBox1.Visible = true;
			connection.Close();
		}

		private void button2_Click(object sender, EventArgs e)    ////////TALEBİ KABUL EDİLMEYEN ÖĞRENCİLERİ GÖSTERME
		{
			button1.Visible = false;
			button2.Visible = false;
			button3.Visible = false;
			button6.Visible = false;

			listBox1.Visible = false;
			button8.Visible = false;
			button9.Visible = false;
			listBox3.Visible = false;
			button10.Visible = false;
			button1.Visible = true;
			richTextBox2.Visible = false;
			listBox2.Visible = false;
			comboBox3.Visible = false;
			button21.Visible = false;
			button2.Visible = true;
			button3.Visible = true;
			checkedListBox2.Visible = false;
			button11.Visible = false;
			textBox1.Visible = false;
			button13.Visible = false;
			button14.Visible = false;
			comboBox1.Visible = false;
			button15.Visible = false;
			button17.Visible = false;
			checkedListBox3.Visible = false;
			button16.Visible = false;
			button18.Visible = false;
			comboBox2.Visible = false;
			label1.Visible = false;
			textBox2.Visible = false;

			studentssum.Clear();
			listBox1.Visible = false;
			button6.Visible = false;

			button5.Visible = true;
			listBox1.Items.Clear();
			listBox1.Text = null;
			button11.Visible = false;
			button14.Visible = true;


			string query = "SELECT demanded_lectures, student_id, name, surname FROM students";


			List<JObject> jsonList = new List<JObject>();
			List<string> lectures = new List<string>(); connection.Open();
			using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT lectures FROM teachers WHERE identification_number = @identificationid", connection))
			{
				cmd.Parameters.AddWithValue("identificationid", identificationid);
				using (NpgsqlDataReader reader = cmd.ExecuteReader())
				{


					while (reader.Read())
					{

						if (!reader.IsDBNull(0)) // JSON verisi boş değilse
						{
							string jsonStr = reader[0].ToString();
							JArray lecturesArray = JArray.Parse(jsonStr);

							foreach (JToken lecture in lecturesArray)
							{
								lectures.Add(lecture["course_code"].ToString());
							}
						}
					}
				}
			}
			connection.Close();
			connection.Open();
			using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
			using (NpgsqlDataReader reader = cmd.ExecuteReader())
			{
				while (reader.Read())
				{
					if (!reader.IsDBNull(0)) // JSON verisi boş değilse
					{
						string json = reader.GetString(0);
						JArray demandedlecturesArray = JArray.Parse(json);
						foreach (var item in demandedlecturesArray)
						{
							string courseinfo = item["course_info"].ToString();
							int index = courseinfo.IndexOf(" ");
							courseinfo = courseinfo.Substring(0, index);

							// teachers_id'yi bir tamsayıya dönüştürün
							int teachersId = Convert.ToInt32(item["teachers_id"]);

							if (teachersId != identificationid)
							{
								foreach (var lecture in lectures)
								{
									if (item["agreement_status"].ToString() == "Talep Edildi" && lecture == courseinfo)
									{
										courseinfo = item["course_info"].ToString();
										listBox1.Items.Add(reader["student_id"] + " " + reader["name"] + " " + reader["surname"] + " " + courseinfo);

									}
								}

							}
						}
					}
				}
			}

			connection.Close();

			listBox1.Visible = true;
			connection.Close();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			button1.Visible = false;
			button2.Visible = false;
			button3.Visible = false;
			richTextBox2.Visible = false;
			listBox2.Visible = false;
			comboBox3.Visible = false;
			button21.Visible = false;
			button6.Visible = false;

			listBox1.Visible = false;
			button8.Visible = false;
			button9.Visible = false;
			listBox3.Visible = false;
			button10.Visible = false;
			button1.Visible = true;
			button2.Visible = true;
			button3.Visible = true;
			checkedListBox2.Visible = false;
			button11.Visible = false;
			textBox1.Visible = false;
			button13.Visible = false;
			button14.Visible = false;
			comboBox1.Visible = false;
			button15.Visible = false;
			button17.Visible = false;
			checkedListBox3.Visible = false;
			button16.Visible = false;
			button18.Visible = false;
			comboBox2.Visible = false;
			label1.Visible = false;
			textBox2.Visible = false;

			listBox1.Visible = false;
			button6.Visible = false;

			button5.Visible = true;
			listBox1.Items.Clear();
			listBox1.Text = null;
			studentssum.Clear();
			button14.Visible = true;
			button11.Visible = false;

			string query = "SELECT demanded_lectures, student_id, name, surname FROM students";


			List<JObject> jsonList = new List<JObject>();


			connection.Open();
			using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
			using (NpgsqlDataReader reader = cmd.ExecuteReader())
			{
				while (reader.Read())
				{
					if (reader.IsDBNull(0)) // JSON verisi boş değilse
					{

						listBox1.Items.Add(reader["student_id"] + " " + reader["name"] + " " + reader["surname"]);

					}
				}
			}


			connection.Close();
			listBox1.Visible = true;
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			button6.Visible = true;

			button11.Visible = true;

			string str = listBox1.SelectedItem.ToString();
			int index = str.IndexOf(" ");
			studentinfo = str.Substring(0, index);
			studentid = Convert.ToInt32(studentinfo);
			int index2 = str.IndexOf("-") + 1;
			coursecode = str.Substring(index2);


		}

		private void button6_Click(object sender, EventArgs e)
		{
			button1.Visible = false;
			button2.Visible = false;
			button3.Visible = false;
			button6.Visible = false;
			richTextBox2.Visible = false;
			listBox2.Visible = false;
			comboBox3.Visible = false;
			button21.Visible = false;
			listBox1.Visible = false;
			button8.Visible = false;
			button9.Visible = false;
			listBox3.Visible = false;
			button10.Visible = false;
			button1.Visible = true;
			button2.Visible = true;
			button3.Visible = true;
			checkedListBox2.Visible = false;
			button11.Visible = false;
			textBox1.Visible = false;
			button13.Visible = false;
			button14.Visible = false;
			comboBox1.Visible = false;
			button15.Visible = false;
			button17.Visible = false;
			checkedListBox3.Visible = false;
			button16.Visible = false;
			button18.Visible = false;
			comboBox2.Visible = false;
			label1.Visible = false;
			textBox2.Visible = false;
			button4.Visible = false;
			button12.Visible = false;

			listBox1.Visible = false;
			button1.Visible = false;
			button2.Visible = false;
			button3.Visible = false;
			button5.Visible = false;

			connection.Open();
			using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT transcript FROM students WHERE student_id = @studentid", connection))
			{
				cmd.Parameters.AddWithValue("studentid", studentid);
				using (NpgsqlDataReader reader = cmd.ExecuteReader())
				{

					while (reader.Read())
					{
						if (!reader.IsDBNull(0)) // JSON verisi boş değilse
						{
							string jsonStr = reader[0].ToString();
							JArray transcriptArray = JArray.Parse(jsonStr);

							foreach (JToken transcript in transcriptArray)
							{
								listBox3.Items.Add(transcript["course_code"] + " " + transcript["course_name"] + " " + transcript["course_grade"]);
							}
						}
					}
				}
				connection.Close();
				listBox3.Visible = true;
				button19.Visible = true;


			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			button1.Visible = false;
			button2.Visible = false;
			button3.Visible = false;
			button6.Visible = false;

			listBox1.Visible = false; richTextBox2.Visible = false;
			listBox2.Visible = false;
			comboBox3.Visible = false;
			button21.Visible = false;
			button8.Visible = false;
			button9.Visible = false;
			listBox3.Visible = false;
			button10.Visible = false;
			button1.Visible = true;
			button2.Visible = true;
			button3.Visible = true;
			checkedListBox2.Visible = false;
			button11.Visible = false;
			textBox1.Visible = false;
			button13.Visible = false;
			button14.Visible = false;
			comboBox1.Visible = false;
			button15.Visible = false;
			button17.Visible = false;
			checkedListBox3.Visible = false;
			button16.Visible = false;
			button18.Visible = false;
			comboBox2.Visible = false;
			label1.Visible = false;
			textBox2.Visible = false;

			checkedListBox2.Items.Clear();

			listBox1.Visible = false;
			button10.Visible = true;
			connection.Open();

			string selectQuery = "SELECT identification_number FROM teachers WHERE username = @userName AND password = @userPassword";

			using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
			{
				command.Parameters.AddWithValue("userName", userName);
				command.Parameters.AddWithValue("userPassword", userPassword);

				using (NpgsqlDataReader reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						identificationid = reader.GetInt32(0);
					}
				}
			}
			connection.Close();

			connection.Open();
			using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT lectures FROM teachers WHERE identification_number = @identificationid", connection))
			{
				cmd.Parameters.AddWithValue("identificationid", identificationid);
				using (NpgsqlDataReader reader = cmd.ExecuteReader())
				{

					while (reader.Read())
					{

						if (!reader.IsDBNull(0)) // JSON verisi boş değilse
						{
							string jsonStr = reader[0].ToString();
							JArray lecturesArray = JArray.Parse(jsonStr);

							foreach (JToken lecture in lecturesArray)
							{
								if (lecture["lecture_status"].ToString() == "0")
								{
									checkedListBox2.Items.Add(lecture["course_code"].ToString() + " " + lecture["course_name"].ToString());
								}
								else if (lecture["lecture_status"].ToString() == "1")
								{
									checkedListBox2.Items.Add(lecture["course_code"].ToString() + " " + lecture["course_name"].ToString() + "  Açık");
								}
							}
						}
					}
				}
			}
			connection.Close();
			checkedListBox2.Visible = true;
		}

		private void button10_Click(object sender, EventArgs e)
		{
			button1.Visible = false;
			button2.Visible = false;
			button3.Visible = false;
			button6.Visible = false;
			richTextBox2.Visible = false;
			listBox2.Visible = false;
			comboBox3.Visible = false;
			button21.Visible = false;
			listBox1.Visible = false;
			button8.Visible = false;
			button9.Visible = false;
			listBox3.Visible = false;
			button10.Visible = false;
			button1.Visible = true;
			button2.Visible = true;
			button3.Visible = true;
			checkedListBox2.Visible = false;
			button11.Visible = false;
			textBox1.Visible = false;
			button13.Visible = false;
			button14.Visible = false;
			comboBox1.Visible = false;
			button15.Visible = false;
			button17.Visible = false;
			checkedListBox3.Visible = false;
			button16.Visible = false;
			button18.Visible = false;
			comboBox2.Visible = false;
			label1.Visible = false;
			textBox2.Visible = false;

			connection.Open();
			foreach (var checkeditem in checkedListBox2.CheckedItems)
			{
				courseid = checkeditem.ToString().Split(" ")[0];

				NpgsqlCommand command = new NpgsqlCommand("SELECT lectures FROM teachers WHERE identification_number = @identificationid", connection);
				command.Parameters.AddWithValue("identificationid", identificationid);////////////////////////DÜZELT
				NpgsqlDataReader reader = command.ExecuteReader();
				{
					if (reader.Read())
					{
						string jsonStr = reader[0].ToString();
						JArray lecturesArray = JArray.Parse(jsonStr);

						if (lecturesArray != null)
						{
							bool updated = false;

							foreach (var item in lecturesArray)
							{
								if (item["course_code"].ToString() == courseid)
								{
									item["lecture_status"] = "1";
									updated = true; // Öğe güncellendi
								}
							}

							if (updated)
							{
								// JSON verilerini PostgreSQL veritabanına geri yazın
								string updatedJson = lecturesArray.ToString();
								string updateQuery = @"
                                                    UPDATE teachers
                                                    SET lectures = @jsonLectures
                                                    WHERE identification_number = @identificationid";
								connection.Close();
								connection.Open();
								using (NpgsqlCommand cmd = new NpgsqlCommand(updateQuery, connection))
								{
									cmd.Parameters.AddWithValue("jsonLectures", NpgsqlTypes.NpgsqlDbType.Jsonb, updatedJson);
									cmd.Parameters.AddWithValue("identificationid", identificationid);

									cmd.ExecuteNonQuery();
								}
							}
						}
					}
				}
				MessageBox.Show("Ders Eklendi");
			}
			connection.Close();





		}

		private void button11_Click(object sender, EventArgs e)
		{
			connection.Open();


			NpgsqlCommand command = new NpgsqlCommand("SELECT demanded_lectures FROM students WHERE student_id = @studentid", connection);
			command.Parameters.AddWithValue("studentid", studentid);
			NpgsqlDataReader reader = command.ExecuteReader();
			{
				if (reader.Read())
				{
					string jsonStr = reader[0].ToString();
					JArray lecturesArray = JArray.Parse(jsonStr);

					if (lecturesArray != null)
					{
						bool updated = false;

						foreach (var item in lecturesArray)
						{
							if (item["teachers_id"].ToString() == identificationid.ToString() && item["course_info"].ToString() == coursecode && item["agreement_status"].ToString() == "Talep Edildi")
							{
								item["agreement_status"] = "Kabul Edildi";
								updated = true; // Öğe güncellendi
												//  button11.Enabled = false;
							}
						}

						if (updated)
						{
							// JSON verilerini PostgreSQL veritabanına geri yazın
							string updatedJson = lecturesArray.ToString();
							string updateQuery = @"
                    UPDATE students
                    SET demanded_lectures = @jsonLectures
                    WHERE student_id = @studentid";
							connection.Close();

							connection.Open();
							using (NpgsqlCommand cmd = new NpgsqlCommand(updateQuery, connection))
							{
								cmd.Parameters.AddWithValue("jsonLectures", NpgsqlTypes.NpgsqlDbType.Jsonb, updatedJson);
								cmd.Parameters.AddWithValue("studentid", studentid);

								cmd.ExecuteNonQuery();
							}
							button10.Enabled = false;
						}
						if (updated)
						{

							NpgsqlCommand cmd2 = new NpgsqlCommand("SELECT quota FROM teachers WHERE identification_number = @identificationid", connection);
							cmd2.Parameters.AddWithValue("identificationid", identificationid);
							NpgsqlDataReader reader2 = cmd2.ExecuteReader();

							if (reader2.Read())
							{
								teacher_quota = reader2.GetInt32(0);
							}

							reader2.Close();


							string updateQuery = @"
                    UPDATE teachers
                    SET quota = @newquota
                    WHERE identification_number = @identificationid";
							connection.Close();
							connection.Open();
							using (NpgsqlCommand cmd = new NpgsqlCommand(updateQuery, connection))
							{
								cmd.Parameters.AddWithValue("newquota", (teacher_quota - 1));
								cmd.Parameters.AddWithValue("identificationid", identificationid);

								cmd.ExecuteNonQuery();
							}
							button10.Enabled = false;
						}
						connection.Close();
					}
				}
			}
			button1_Click(sender, e);
		}





		private void checkedListBox2_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			string[] item = checkedListBox2.SelectedItem.ToString().Split(" ");
			bool isOpened = item[item.Length - 1].Equals("Açık");
			if (isOpened)
			{
				e.NewValue = CheckState.Unchecked;
			}

		}

		private void button12_Click_1(object sender, EventArgs e) ////////////// İLGİ ALANI EKLE
		{

			button1.Visible = false;
			button2.Visible = false;
			button3.Visible = false;
			button6.Visible = false; richTextBox2.Visible = false;
			listBox2.Visible = false;
			comboBox3.Visible = false;
			button21.Visible = false;

			listBox1.Visible = false;
			button8.Visible = false;
			button9.Visible = false;
			listBox3.Visible = false;
			button10.Visible = false;
			button1.Visible = true;
			button2.Visible = true;
			button3.Visible = true;
			checkedListBox2.Visible = false;
			button11.Visible = false;
			textBox1.Visible = false;
			button13.Visible = false;
			button14.Visible = false;
			comboBox1.Visible = false;
			button15.Visible = false;
			button17.Visible = false;
			checkedListBox3.Visible = false;
			button16.Visible = false;
			button18.Visible = false;
			comboBox2.Visible = false;
			label1.Visible = false;
			textBox2.Visible = false;


			textBox1.Visible = true;
			button13.Visible = true;
		}

		private void button13_Click(object sender, EventArgs e) //////////// EKLE (İLGİ ALANI)
		{


			// Veritabanı bağlantısını açın



			connection.Open();
			bool isFirstCourse = true;
			NpgsqlCommand cmd = new NpgsqlCommand("SELECT interest_areas FROM teachers WHERE identification_number = @identificationid", connection);
			cmd.Parameters.AddWithValue("identificationid", identificationid);
			NpgsqlDataReader reader = cmd.ExecuteReader();
			{

				while (reader.Read())
				{
					string jsonStr = reader[0].ToString();
					JArray Array = JArray.Parse(jsonStr);
					if (Array != null) // JSON verisi boş değilse
					{
						isFirstCourse = false;
					}
				}
			}
			connection.Close();
			connection.Open();
			string updateQuery;
			string new_interest = textBox1.Text.ToString();


			string jsonInterest = $@"[{{""interest_area"": ""{new_interest}""}}]";

			if (isFirstCourse)
			{
				updateQuery = @"
			UPDATE teachers
			SET interest_areas = @jsonInterest
			WHERE identification_number = @identificationid";
				isFirstCourse = false;
			}
			else
			{
				updateQuery = @"
			UPDATE teachers
			SET interest_areas = interest_areas || @jsonInterest
			WHERE identification_number = @identificationid";
			}
			using (NpgsqlCommand command = new NpgsqlCommand(updateQuery, connection))
			{
				command.Parameters.AddWithValue("jsonInterest", NpgsqlTypes.NpgsqlDbType.Jsonb, jsonInterest);
				command.Parameters.AddWithValue("identificationid", identificationid);///////*** girilen id yap DÜZELT*************

				command.ExecuteNonQuery();
			}

			connection.Close();
			MessageBox.Show("ilgi alanı Eklendi");

		}

		private void button14_Click(object sender, EventArgs e) /////////TALEPTE BULUN
		{
			comboBox1.Visible = true;
			button15.Visible = true;

			connection.Open();
			using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT lectures FROM teachers WHERE identification_number = @identificationid", connection))
			{
				cmd.Parameters.AddWithValue("identificationid", identificationid);
				using (NpgsqlDataReader reader = cmd.ExecuteReader())
				{

					while (reader.Read())
					{

						if (!reader.IsDBNull(0)) // JSON verisi boş değilse
						{
							string jsonStr = reader[0].ToString();
							JArray lecturesArray = JArray.Parse(jsonStr);

							foreach (JToken lecture in lecturesArray)
							{


								if (lecture["lecture_status"].ToString() == "1")
								{
									comboBox1.Items.Add(lecture["course_code"].ToString() + " " + lecture["course_name"].ToString());
								}
							}
						}
					}
				}

				connection.Close();
			}
		}

		private void button15_Click(object sender, EventArgs e) //////////////ONAYLA
		{
			courseInfo = comboBox1.SelectedItem.ToString();
			courseid = courseInfo.ToString().Split(" ")[0];
			string teachersName = "";
			connection.Open();
			NpgsqlCommand cmd2 = new NpgsqlCommand("SELECT name, surname FROM teachers WHERE identification_number = @identificationid", connection);
			cmd2.Parameters.AddWithValue("identificationid", identificationid);
			NpgsqlDataReader reader2 = cmd2.ExecuteReader();
			{

				while (reader2.Read())
				{
					teachersName = reader2["name"].ToString() + " " + reader2["surname"].ToString();

				}
			}
			connection.Close();
			connection.Open();
			bool isFirstValue = true;
			NpgsqlCommand cmd = new NpgsqlCommand("SELECT demanded_lectures FROM students WHERE student_id = @studentid", connection);
			cmd.Parameters.AddWithValue("studentid", studentid);
			NpgsqlDataReader reader = cmd.ExecuteReader();
			{
				while (reader.Read())
				{
					if (!reader.IsDBNull(0)) // JSON verisi boş değilse
					{
						isFirstValue = false;
					}
				}
			}
			connection.Close();
			connection.Open();
			string updateQuery;




			string jsondemanded = $@"[{{""demander"": ""hoca"", ""course_info"": ""{courseInfo}"", ""teachers_id"": ""{identificationid}"", ""teachers_name"": ""{teachersName}"", ""agreement_status"": ""Talep Edildi""}}]";

			if (isFirstValue)
			{
				updateQuery = @"
			UPDATE students
			SET demanded_lectures = @jsondemanded
			WHERE student_id = @studentid";
				isFirstValue = false;
			}
			else
			{
				updateQuery = @"
			UPDATE students
			SET demanded_lectures = demanded_lectures  || @jsondemanded
			WHERE student_id = @studentid";
			}
			using (NpgsqlCommand command = new NpgsqlCommand(updateQuery, connection))
			{
				command.Parameters.AddWithValue("jsondemanded", NpgsqlTypes.NpgsqlDbType.Jsonb, jsondemanded);
				command.Parameters.AddWithValue("studentid", studentid);///////*** girilen id yap DÜZELT*************

				command.ExecuteNonQuery();
			}

			connection.Close();


		}

		private void button16_Click(object sender, EventArgs e)
		{
			connection.Open();
			string query = "SELECT demanded_lectures, student_id, name, surname FROM students";


			List<JObject> jsonList = new List<JObject>();
			using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
			using (NpgsqlDataReader reader = cmd.ExecuteReader())
			{
				while (reader.Read())
				{
					if (!reader.IsDBNull(0)) // JSON verisi boş değilse
					{
						string json = reader.GetString(0);
						JArray demandedlecturesArray = JArray.Parse(json);
						foreach (var item in demandedlecturesArray)
						{
							string courseinfo = item["course_info"].ToString();

							// teachers_id'yi bir tamsayıya dönüştürün
							int teachersId = Convert.ToInt32(item["teachers_id"]);

							if (teachersId != identificationid)
							{
								if (item["agreement_status"].ToString() == "Talep Edildi")
								{
									//listBox1.Items.Add(reader["student_id"] + " " + reader["name"] + " " + reader["surname"] + "-" + courseinfo);
									int ss = Convert.ToInt32(reader["student_id"]);
									Tuple<int, double> tuple = new Tuple<int, double>(ss, 0);
									studentssum.Add(tuple);
								}
							}
						}
					}
					if (reader.IsDBNull(0)) // JSON verisi boş değilse
					{
						// listBox1.Items.Add(reader["student_id"] + " " + reader["name"] + " " + reader["surname"] + "-" + courseinfo);
						int ss = Convert.ToInt32(reader["student_id"]);
						Tuple<int, double> tuple = new Tuple<int, double>(ss, 0);
						studentssum.Add(tuple);
					}
				}

				connection.Close();
				//  listBox1.Visible = true;

			}
			richTextBox2.Visible = false;
			listBox2.Visible = false;
			comboBox3.Visible = false;
			button21.Visible = false;
			checkedListBox2.Visible = false;
			textBox2.Visible = false;
			label1.Visible = false;
			button17.Visible = false;
			checkedListBox3.Visible = false;
			Tuple<int, double> tuple2 = new Tuple<int, double>(0, 0);
			foreach (var student in studentssum)
			{
				double sum = 0;
				List<JObject> jsonList2 = new List<JObject>();
				connection.Open();
				string query2 = "SELECT transcript  FROM students WHERE student_id = @studentid";
				using (NpgsqlCommand cmd = new NpgsqlCommand(query2, connection))
				{
					cmd.Parameters.AddWithValue("studentid", student.Item1);
					using (NpgsqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							if (!reader.IsDBNull(0)) // JSON verisi boş değilse
							{
								foreach (var checkeditem in checkedListBox3.Items)
								{
									string criteria_course = checkeditem.ToString().Split(" ")[0];
									string json = reader.GetString(0);
									JArray transcriptArray = JArray.Parse(json);
									int stucount = 0;
									sum = 0;
									foreach (var item in transcriptArray)
									{
										if (item["course_code"].ToString() == criteria_course)
										{
											stucount++;
											if (item["course_grade"].ToString() == "AA")
											{
												sum = (double.Parse(textBox2.Text) * 4) + sum;
											}
											else if (item["course_grade"].ToString() == "BA")
											{
												string text = textBox2.Text;
												sum = (double.Parse(text) * 3.5) + sum;
											}
											else if (item["course_grade"].ToString() == "BB")
											{
												sum = (double.Parse(textBox2.Text) * 3) + sum;
											}
											else if (item["course_grade"].ToString() == "CB")
											{
												sum = (double.Parse(textBox2.Text) * 2.5) + sum;
											}
											else if (item["course_grade"].ToString() == "CC")
											{
												sum = (double.Parse(textBox2.Text) * 2) + sum;
											}
											else if (item["course_grade"].ToString() == "DC")
											{
												sum = (double.Parse(textBox2.Text) * 1.5) + sum;
											}
											else if (item["course_grade"].ToString() == "DD")
											{
												sum = (double.Parse(textBox2.Text) * 1) + sum;
											}
											else if (item["course_grade"].ToString() == "FF")
											{
												sum = (double.Parse(textBox2.Text) * 0) + sum;
											}

										}
										if (stucount == 0)
										{
											// studentssum.Remove(student);
										}


									}
								}
							}
						}
					}

					listBox1.Visible = true;
					listBox3.Visible = false;
					connection.Close();
					double stu2 = student.Item2;
					Tuple<int, double> tuple = new Tuple<int, double>(student.Item1, stu2 + sum);
					studentssum2.Add(tuple);
				}

			}

			for (int i = 0; i < studentssum2.Count; i++)
			{
				for (int j = 0; j < studentssum2.Count - 1; j++)
				{
					if (studentssum2[j].Item2 < studentssum2[j + 1].Item2)
					{
						tuple2 = studentssum2[j];
						studentssum2[j] = studentssum2[j + 1];
						studentssum2[j + 1] = tuple2;
					}
				}
			}
			//foreach (var students in studentssum2)
			//{
			//    listBox1.Items.Add(students.Item1 + "," + students.Item2);
			//}


			listBox1.Items.Clear();

			foreach (var student in studentssum2)
			{

				connection.Open();
				string query3 = "SELECT name, surname FROM students WHERE student_id = @studentid";
				using (NpgsqlCommand cmd = new NpgsqlCommand(query3, connection))
				{
					cmd.Parameters.AddWithValue("studentid", student.Item1);
					using (NpgsqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{

							listBox1.Items.Add(student.Item1 + " " + reader["name"] + " " + reader["surname"] + " " + student.Item2);


						}


					}
				}
				connection.Close();
			}
		}


		private void button17_Click(object sender, EventArgs e)
		{
			checkedListBox3.Items.Add(comboBox2.Text.ToString() + " " + "x" + " " + textBox2.Text);
		}



		private void button18_Click(object sender, EventArgs e)
		{
			List<object> itemsToRemove = new List<object>();

			foreach (var checkeditem in checkedListBox3.CheckedItems)
			{
				itemsToRemove.Add(checkeditem);
			}

			foreach (var itemToRemove in itemsToRemove)
			{
				checkedListBox3.Items.Remove(itemToRemove);
			}
		}

		private void button19_Click(object sender, EventArgs e)
		{
			button1.Visible = false;
			button2.Visible = false;
			button3.Visible = false;
			button6.Visible = false;
			richTextBox2.Visible = false;
			listBox2.Visible = false;
			comboBox3.Visible = false;
			button21.Visible = false;

			listBox1.Visible = false;
			button8.Visible = false;
			button9.Visible = false;
			listBox3.Visible = false;
			button10.Visible = false;
			button1.Visible = true;
			button2.Visible = true;
			button3.Visible = true;
			button4.Visible = true;
			button12.Visible = true;
			checkedListBox2.Visible = false;
			button11.Visible = false;
			textBox1.Visible = false;
			button13.Visible = false;
			button14.Visible = false;
			comboBox1.Visible = false;
			button15.Visible = false;
			button17.Visible = false;
			checkedListBox3.Visible = false;
			button16.Visible = false;
			button18.Visible = false;
			comboBox2.Visible = false;
			label1.Visible = false;
			textBox2.Visible = false;

			listBox3.Items.Clear();
			listBox3.Visible = false;
			listBox1.Visible = true;
			button19.Visible = false;

		}

		private void button20_Click(object sender, EventArgs e)
		{
			TeacherScreen_Load(sender, e);
			this.Visible = false;
			loginScreen.Visible = true;
		}

		private void button7_Click(object sender, EventArgs e)
		{
			TeacherScreen_Load(sender, e);

			richTextBox2.Visible = true;
			listBox2.Visible = true;
			comboBox3.Visible = true;
			button21.Visible = true;
			comboBox3.Items.Clear();
			comboBox3.Name = "Mesajlar";
			comboBox3.Text = "Mesajlar";

			connection.Open();
			using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT student_id, name, surname FROM students", connection))
			using (NpgsqlDataReader reader = cmd.ExecuteReader())
			{
				while (reader.Read())
				{
					string formattedTeacher = $"{reader["student_id"]} - {reader["name"]} {reader["surname"]}";
					comboBox3.Items.Add(formattedTeacher);
				}
			}
			connection.Close();

			connection.Open();
			using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT character_limit FROM collage WHERE collage_id = 1", connection))
			using (NpgsqlDataReader reader = cmd.ExecuteReader())
			{
				while (reader.Read())
				{
					richTextBox2.MaxLength = int.Parse(reader["character_limit"].ToString());
				}
			}
			connection.Close();
		}

		void getMessages()
		{
			if (comboBox3.Name == "Mesajlar" && comboBox3.Items.Count != 0)
			{
				string[] student = comboBox3.Text.Split("-");
				connection.Open();
				listBox2.Items.Clear();

				string selectQuery = "SELECT sent_messages FROM teachers where identification_number = @identification_number ";

				using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
				{
					command.Parameters.AddWithValue("identification_number", user.id);

					using (NpgsqlDataReader reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							string sentMessages = reader["sent_messages"].ToString();

							var jsonArray = Newtonsoft.Json.JsonConvert.DeserializeObject(sentMessages) as Newtonsoft.Json.Linq.JArray;

							string formattedSentMessages = "";

							if (jsonArray != null)
							{
								foreach (var item in jsonArray)
								{

									string sent = item["sent"].ToString();
									string text = item["text"].ToString();
									string studentId = item["student_id"].ToString();
									string studentName = item["student_name"].ToString();

									if (int.Parse(sent) == 0 && int.Parse(studentId) == int.Parse(student[0].Trim()))
									{
										formattedSentMessages = $"{user.name} {user.surname} : {text}";
										listBox2.Items.Add(formattedSentMessages);
									}
									else if (int.Parse(sent) == 1 && int.Parse(studentId) == int.Parse(student[0].Trim()))
									{
										formattedSentMessages = $"{studentId} - {studentName} : {text}";
										listBox2.Items.Add(formattedSentMessages);
									}
								}
							}
						}
					}
				}
				connection.Close();
			}

		}
		private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
		{
			getMessages();
		}

		private void button21_Click(object sender, EventArgs e)
		{

			//gönder
			if (comboBox1.Text != "Mesajlar")
			{

				string[] student = comboBox3.Text.Split("-");

				connection.Open();

				bool isFirstCourse = true;
				string updateQuery;

				using (NpgsqlCommand command = new NpgsqlCommand("SELECT sent_messages FROM teachers WHERE identification_number = @identificationNumber", connection))
				{
					command.Parameters.AddWithValue("identificationNumber", user.id);

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

				string message = richTextBox2.Text;
				string sent = "0";
				string identificationNumber = user.id.ToString();
				string teacherName = userName + " " + user.surname;
				string studentid = student[0].Trim();
				string studentName = student[1];

				string jsonLecture = $@"[{{""sent"": ""{sent}"", ""text"": ""{message.Replace("\n", "\\n")}"", ""student_id"": ""{studentid}"", ""student_name"": ""{studentName}""}}]";

				if (isFirstCourse)
				{
					updateQuery = @"
						UPDATE teachers
						SET sent_messages = @jsonLecture
						WHERE identification_number = @identificationNumber";
					isFirstCourse = false;
				}
				else
				{
					updateQuery = @"
						UPDATE teachers
						SET sent_messages = sent_messages || @jsonLecture
						WHERE identification_number = @identificationNumber";
				}
				using (NpgsqlCommand command = new NpgsqlCommand(updateQuery, connection))
				{
					command.Parameters.AddWithValue("jsonLecture", NpgsqlTypes.NpgsqlDbType.Jsonb, jsonLecture);
					command.Parameters.AddWithValue("identificationNumber", int.Parse(identificationNumber));

					command.ExecuteNonQuery();
				}
				connection.Close();
				richTextBox2.Text = "";
				getMessages();
			}
		}
	}
}





