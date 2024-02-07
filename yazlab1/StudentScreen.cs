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
using Spire.Pdf;
using Newtonsoft.Json;
using System.Data.Common;
using System.Text.Json.Nodes;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Microsoft.VisualBasic;
using System.Reflection;

namespace yazlab1
{
	public partial class StudentScreen : UserControl
	{

		NpgsqlConnection connection = new NpgsqlConnection("server=localHost; port=5432; Database=yazlab1; user ID=postgres; password=admin");
		public UserControl loginScreen;
		public User user;

		public StudentScreen()
		{
			InitializeComponent();
		}

		static List<string[]> course_data = new List<string[]>();
		void isTranscriptUploaded()
		{
			connection.Open();

			string selectQuery = "SELECT transcript FROM students WHERE student_id = @studentId";

			using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
			{
				command.Parameters.AddWithValue("studentId", user.id);

				using (NpgsqlDataReader reader = command.ExecuteReader())
				{
					if (reader.Read())
						if (reader.IsDBNull(0))
						{
							foreach (Control control in this.Controls)
							{
								if (control is Button)
								{
									Button button = (Button)control;
									button.Enabled = false;
								}
							}
							button1.Enabled = true;
							reader.Close();
						}
				}
			}
			connection.Close();
		}

		void GetInterestAreas()
		{
			List<string[]> area_data = new List<string[]>();

			comboBox1.Items.Clear();
			comboBox1.Items.Add("Tüm Dersler");
			comboBox1.Name = "Dersler";
			comboBox1.SelectedIndex = 0;

			string selectQuery = "SELECT interest_areas FROM teachers";

			connection.Open();

			using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
			{
				using (NpgsqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						string interestAreas = reader["interest_areas"].ToString();

						var jsonArray = Newtonsoft.Json.JsonConvert.DeserializeObject(interestAreas) as Newtonsoft.Json.Linq.JArray;

						string formattedInterestAreas = "";

						if (jsonArray != null)
						{
							foreach (var item in jsonArray)
							{
								string interestArea = item["interest_area"].ToString();

								if (!area_data.Any(x => x[0] == interestArea))
								{
									area_data.Add(new string[] { interestArea });
									comboBox1.Items.Add(interestArea);
								}
							}
						}
					}
				}
			}

			connection.Close();

		}

		void GetTranscript(string path)
		{


			PdfDocument pdfDocument = new PdfDocument();
			pdfDocument.LoadFromFile(path);

			string textData = "";

			foreach (PdfPageBase page in pdfDocument.Pages)
			{
				string text = page.ExtractText();
				textData += text;
			}

			pdfDocument.Close();

			TranscriptSplit(textData);
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

					int startIndex = splittedLines[x].IndexOf("GNO:");
					startIndex += 4;
					int endIndex = splittedLines[x].IndexOf(' ', startIndex);
					gpa = decimal.Parse(splittedLines[x].Substring(startIndex, endIndex - startIndex), new CultureInfo("en-US"));
				}
			}

			if (gpa != 0)
			{
				connection.Open();

				string updateQuery = "UPDATE students SET gpa = @gpa WHERE student_id = @studentId";

				using (NpgsqlCommand command = new NpgsqlCommand(updateQuery, connection))
				{
					command.Parameters.Add(new NpgsqlParameter("@gpa", gpa));
					command.Parameters.Add(new NpgsqlParameter("@studentId", user.id));
					command.ExecuteNonQuery();
				}
				connection.Close();
			}
		}
		private void button1_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();
			openFileDialog1.Filter = "PDF Dosyaları|*.pdf|Tüm Dosyalar|*.*";

			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				GetTranscript(openFileDialog1.FileName);
				connection.Open();

				bool isFirstCourse = true;
				foreach (string[] course in course_data)
				{
					string updateQuery;
					string courseCode = course[0];
					string courseName = course[1];
					string courseGrade = course[2];

					string jsonTranscript = $@"[{{""course_code"": ""{courseCode}"", ""course_name"": ""{courseName}"", ""course_grade"": ""{courseGrade}""}}]";

					if (isFirstCourse)
					{
						updateQuery = @"
						UPDATE students
						SET transcript = @jsonTranscript
						WHERE student_id = @studentId";
						isFirstCourse = false;
					}
					else
					{
						updateQuery = @"
						UPDATE students
						SET transcript = transcript || @jsonTranscript
						WHERE student_id = @studentId";
					}
					using (NpgsqlCommand command = new NpgsqlCommand(updateQuery, connection))
					{
						command.Parameters.AddWithValue("jsonTranscript", NpgsqlTypes.NpgsqlDbType.Jsonb, jsonTranscript);
						command.Parameters.AddWithValue("studentId", user.id);

						command.ExecuteNonQuery();
					}
				}
				connection.Close();

				foreach (Control control in this.Controls)
				{
					if (control is Button)
					{
						Button button = (Button)control;
						button.Enabled = true;
					}
				}

				string secilenDosyaYolu = openFileDialog1.FileName;
				MessageBox.Show("Transkript Başarıyla Eklendi ( " + secilenDosyaYolu + " )");
			}

		}

		private void button2_Click(object sender, EventArgs e)
		{
			GetInterestAreas();
			richTextBox1.Visible = true;
			checkedListBox1.Visible = false;
			richTextBox2.Visible = false;
			listBox1.Visible = false;
			button10.Visible = false;
			connection.Open();

			string selectQuery = "SELECT transcript FROM students WHERE student_id = @studentId";

			using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
			{
				command.Parameters.AddWithValue("studentId", user.id);

				using (NpgsqlDataReader reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						string transcript = reader["transcript"].ToString();

						var jsonArray = Newtonsoft.Json.JsonConvert.DeserializeObject(transcript) as Newtonsoft.Json.Linq.JArray;

						string formattedTranscript = "";

						if (jsonArray != null)
						{
							foreach (var item in jsonArray)
							{
								string courseCode = item["course_code"].ToString();
								string courseName = item["course_name"].ToString();
								string courseGrade = item["course_grade"].ToString();
								formattedTranscript += $"{courseCode}, {courseName}, {courseGrade}\r\n";
							}
						}
						richTextBox1.Text = formattedTranscript;
					}
				}
			}

			connection.Close();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			comboBox1.Visible = true;
			checkedListBox1.Items.Clear();
			checkedListBox1.Visible = true;
			richTextBox1.Visible = false;
			richTextBox2.Visible = false;
			listBox1.Visible = false;
			button7.Enabled = false;
			button10.Visible = false;
			button5.Enabled = true;
			button5.Text = "Ders Ekle";

			if (comboBox1.Items[0].ToString() != "Tüm Dersler")
				GetInterestAreas();

			if (comboBox1.SelectedItem == "Tüm Dersler")
			{
				connection.Open();

				string selectQuery = "SELECT identification_number, name, surname, lectures FROM teachers ";

				using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
				{
					using (NpgsqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							string identificationNumber = reader["identification_number"].ToString();
							string name = reader["name"].ToString();
							string surname = reader["surname"].ToString();
							string lectures = reader["lectures"].ToString();

							var jsonArray = Newtonsoft.Json.JsonConvert.DeserializeObject(lectures) as Newtonsoft.Json.Linq.JArray;

							string formattedLectures = "";

							if (jsonArray != null)
							{
								foreach (var item in jsonArray)
								{
									string courseCode = item["course_code"].ToString();
									string courseName = item["course_name"].ToString();
									int lectureStatus = int.Parse(item["lecture_status"].ToString());
									if (lectureStatus == 1)
									{
										formattedLectures = $"{identificationNumber} - {name} {surname} - {courseCode} {courseName}";
										checkedListBox1.Items.Add(formattedLectures);
									}
								}
							}
						}
					}
				}
				connection.Close();
			}
			else
			{
				connection.Open();
				string selectedValue = comboBox1.SelectedItem.ToString();

				string selectQuery = "SELECT identification_number, name, surname, lectures FROM teachers WHERE EXISTS (SELECT 1 FROM jsonb_array_elements(interest_areas) AS item WHERE item->>'interest_area' = @selectedValue);";

				using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
				{
					command.Parameters.AddWithValue("selectedValue", selectedValue);

					using (NpgsqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							string identificationNumber = reader["identification_number"].ToString();
							string name = reader["name"].ToString();
							string surname = reader["surname"].ToString();
							string lectures = reader["lectures"].ToString();

							var jsonArray = Newtonsoft.Json.JsonConvert.DeserializeObject(lectures) as Newtonsoft.Json.Linq.JArray;

							string formattedLectures = "";

							if (jsonArray != null)
							{
								foreach (var item in jsonArray)
								{
									string courseCode = item["course_code"].ToString();
									string courseName = item["course_name"].ToString();
									formattedLectures = $"{identificationNumber} - {name} {surname} - {courseCode} {courseName}";
									checkedListBox1.Items.Add(formattedLectures);
								}
							}
						}
					}
				}

				connection.Close();
			}
		}




		private void button5_Click(object sender, EventArgs e)
		{
			if (button5.Text == "Ders Ekle")
			{
				connection.Open();

				bool isFirstCourse = true;
				string updateQuery;
				int demandLimit = 1;
				bool teacherSelectLimit = true;

				using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM collage WHERE collage_id = 1", connection))
				{
					using (NpgsqlDataReader reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							demandLimit = (int)reader["demand_limit"];
							teacherSelectLimit = (bool)reader["teacher_select_limit"];
						}
						reader.Close();
					}
				}

				using (NpgsqlCommand command = new NpgsqlCommand("SELECT demanded_lectures FROM students WHERE student_id = @studentId", connection))
				{
					command.Parameters.AddWithValue("studentId", user.id);

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

				foreach (object selectedItem in checkedListBox1.CheckedItems)
				{
					string selectedText = selectedItem.ToString();

					string[] parts = selectedText.Split('-');

					string identificationNumber = parts[0].Trim();
					string teacherName = parts[1].Trim();
					string courseInfo = parts[2].Trim();
					string agreementStatus = "Talep Edildi";
					string demander = "Öğrenci";

					string jsonLecture = $@"[{{""teachers_id"": ""{identificationNumber}"", ""teachers_name"": ""{teacherName}"", ""course_info"": ""{courseInfo}"", ""agreement_status"": ""{agreementStatus}"", ""demander"": ""{demander}""}}]";



					string selectQuery = "SELECT demanded_lectures FROM students WHERE student_id = @studentId";

					using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
					{
						command.Parameters.AddWithValue("studentId", user.id);

						using (NpgsqlDataReader reader = command.ExecuteReader())
						{
							if (!isFirstCourse && reader.Read())
							{
								string demandedLectures = reader["demanded_lectures"].ToString();

								var jsonArray = Newtonsoft.Json.JsonConvert.DeserializeObject(demandedLectures) as Newtonsoft.Json.Linq.JArray;

								int count = jsonArray.Count(item => item["course_info"].ToString() == courseInfo);

								bool exists = jsonArray.Any(item =>
															item["teachers_id"].ToString() == identificationNumber &&
															item["teachers_name"].ToString() == teacherName &&
															item["course_info"].ToString() == courseInfo);
								bool teacherExist = jsonArray.Any(item => item["teachers_id"].ToString() == identificationNumber);


								if (exists)
								{
									MessageBox.Show("Ders Talep Listesinde Bulunuyor " + "( " + selectedText + " )");
									continue;
								}
								else if (demandLimit <= count)
								{
									MessageBox.Show("Aynı Ders En Fazla " + demandLimit + " Hocadan Talep Edilebilir" + "( " + selectedText + " )");
									continue;
								}
								else if (teacherExist)
								{
									if (teacherSelectLimit)
									{
										MessageBox.Show(" Aynı Hocadan Birden Fazla Ders Talep Edilemez " + "( " + selectedText + " )");
										continue;
									}
								}


							}
							reader.Close();
						}
					}



					if (isFirstCourse)
					{
						updateQuery = @"
						UPDATE students
						SET demanded_lectures = @jsonLecture
						WHERE student_id = @studentId";
						isFirstCourse = false;
					}
					else
					{
						updateQuery = @"
						UPDATE students
						SET demanded_lectures = demanded_lectures || @jsonLecture
						WHERE student_id = @studentId";
					}
					using (NpgsqlCommand command = new NpgsqlCommand(updateQuery, connection))
					{
						command.Parameters.AddWithValue("jsonLecture", NpgsqlTypes.NpgsqlDbType.Jsonb, jsonLecture);
						command.Parameters.AddWithValue("studentId", user.id);

						command.ExecuteNonQuery();
					}
					MessageBox.Show(selectedText + " Talep Edildi");

				}
				connection.Close();

				foreach (object checkedItem in checkedListBox1.CheckedItems.OfType<object>().ToList())
				{
					checkedListBox1.SetItemChecked(checkedListBox1.Items.IndexOf(checkedItem), false);
				}

			}
			else if (button5.Text == "Hoca Talep Onayı")
			{
				foreach (object selectedItem in checkedListBox1.CheckedItems)
				{
					string selectedText = selectedItem.ToString();

					string[] parts = selectedText.Split('-');

					if (parts.Length == 4 && parts[3] == " Hoca Talebi")
					{
						string demander = "Hoca";
						string courseInfo = parts[2].Trim();
						string teachersId = parts[0].Trim();
						string teachersName = parts[1].Trim();

						connection.Open();
						string selectQuery = "SELECT demanded_lectures FROM students WHERE student_id = @studentId";

						using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
						{
							command.Parameters.AddWithValue("studentId", user.id);

							using (NpgsqlDataReader reader = command.ExecuteReader())
							{
								if (reader.Read())
								{
									string demandedLectures = reader["demanded_lectures"].ToString();

									var jsonArray = Newtonsoft.Json.JsonConvert.DeserializeObject(demandedLectures) as Newtonsoft.Json.Linq.JArray;

									for (int i = 0; i < jsonArray.Count; i++)
									{
										var item = jsonArray[i];
										var itemDemander = item["demander"]?.ToString();
										var itemCourseInfo = item["course_info"]?.ToString();
										var itemTeachersId = item["teachers_id"]?.ToString();
										var itemTeachersName = item["teachers_name"]?.ToString();
										var itemAgreementStatus = item["agreement_status"]?.ToString();

										if (itemDemander == demander &&
											itemCourseInfo == courseInfo &&
											itemTeachersId == teachersId &&
											itemTeachersName == teachersName &&
											itemAgreementStatus == "Talep Edildi")
										{
											jsonArray.RemoveAt(i);

											var newItem = new JObject();
											newItem["demander"] = demander;
											newItem["course_info"] = courseInfo;
											newItem["teachers_id"] = teachersId;
											newItem["teachers_name"] = teachersName;
											newItem["agreement_status"] = "Kabul Edildi";

											jsonArray.Add(newItem);

											break;
										}
									}
									reader.Close();

									string updatedDemandedLectures = jsonArray.ToString();

									string updateQuery = "UPDATE students SET demanded_lectures = @demandedLectures WHERE student_id = @studentId";

									using (NpgsqlCommand command1 = new NpgsqlCommand(updateQuery, connection))
									{
										command1.Parameters.AddWithValue("studentId", user.id);
										command1.Parameters.Add(new NpgsqlParameter("demandedLectures", NpgsqlDbType.Jsonb) { Value = updatedDemandedLectures });

										command1.ExecuteNonQuery();
									}
								}
							}
						}
						connection.Close();

						MessageBox.Show(selectedText + " Kabul Edildi");
					}
				}


				if (checkedListBox1.CheckedItems.Count == 0)
					return;

				button6_Click(sender, e);
			}
		}

		private void button6_Click(object sender, EventArgs e)
		{
			comboBox1.Visible = false;
			checkedListBox1.Items.Clear();
			checkedListBox1.Visible = true;
			richTextBox1.Visible = false;
			listBox1.Visible = false;
			richTextBox2.Visible = false;
			button5.Enabled = true;
			button7.Enabled = true;
			button10.Visible = false;
			button5.Text = "Hoca Talep Onayı";
			connection.Open();

			string selectQuery = "SELECT demanded_lectures FROM students WHERE student_id = @studentId";

			using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
			{
				command.Parameters.AddWithValue("studentId", user.id);

				using (NpgsqlDataReader reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						string demandedLectures = reader["demanded_lectures"].ToString();

						var jsonArray = Newtonsoft.Json.JsonConvert.DeserializeObject(demandedLectures) as Newtonsoft.Json.Linq.JArray;

						string formattedDemandedLectures = "";

						if (jsonArray != null)
						{
							foreach (var item in jsonArray)
							{
								string courseInfo = item["course_info"].ToString();
								string teachersId = item["teachers_id"].ToString();
								string teachersName = item["teachers_name"].ToString();
								string agreementStatus = item["agreement_status"].ToString();
								string demander = item["demander"].ToString();

								if (agreementStatus == "Talep Edildi" && demander == "Öğrenci")
								{
									formattedDemandedLectures = $"{teachersId} - {teachersName} - {courseInfo} - {agreementStatus}";
									checkedListBox1.Items.Add(formattedDemandedLectures);
								}
								else if (agreementStatus == "Talep Edildi" && demander == "Hoca")
								{
									formattedDemandedLectures = $"{teachersId} - {teachersName} - {courseInfo} - Hoca Talebi";
									checkedListBox1.Items.Add(formattedDemandedLectures);
								}
							}
						}
					}
				}
			}
			connection.Close();
		}

		private void button7_Click(object sender, EventArgs e)
		{
			checkedListBox1.Visible = true;
			richTextBox1.Visible = false;


			string updateQuery;

			foreach (object selectedItem in checkedListBox1.CheckedItems)
			{
				string selectedText = selectedItem.ToString();

				string[] parts = selectedText.Split('-');

				string identificationNumber = parts[0].Trim();
				string teacherName = parts[1].Trim();
				string courseInfo = parts[2].Trim();
				string agreementStatus = parts[3].Trim();

				if (agreementStatus == "Talep Edildi")
				{
					connection.Open();

					string jsonDemandedLectures = $@"[{{""course_info"": ""{courseInfo}"",""teachers_id"": ""{identificationNumber}"", ""teachers_name"": ""{teacherName}"", ""agreement_status"": ""{agreementStatus}""}}]";

					updateQuery = @"UPDATE students
							SET demanded_lectures = (
							SELECT jsonb_agg(item)
							FROM jsonb_array_elements(demanded_lectures) AS item
							WHERE NOT (
							(item->>'course_info' = @courseInfo)
							AND
							(item->>'teachers_id' = @teacherId)))
							WHERE student_id =  @studentId;";

					using (NpgsqlCommand command = new NpgsqlCommand(updateQuery, connection))
					{
						command.Parameters.AddWithValue("jsonDemandedLectures", NpgsqlTypes.NpgsqlDbType.Jsonb, jsonDemandedLectures);
						command.Parameters.AddWithValue("courseInfo", courseInfo);
						command.Parameters.AddWithValue("teacherId", identificationNumber);
						command.Parameters.AddWithValue("studentId", user.id);

						command.ExecuteNonQuery();
					}

					connection.Close();
					MessageBox.Show(courseInfo + " İptal Edildi");
				}
				else if (agreementStatus == "Hoca Talebi")
				{
					MessageBox.Show("Hoca Talebi İptal Edilemez");
				}
				else
				{
					return;
				}

			}

			if (checkedListBox1.CheckedItems.Count == 0)
				return;

			button6_Click(sender, e);
		}

		private void button8_Click(object sender, EventArgs e)
		{
			comboBox1.Visible = false;
			checkedListBox1.Items.Clear();
			checkedListBox1.Visible = true;
			richTextBox1.Visible = false;
			richTextBox2.Visible = false;
			listBox1.Visible = false;
			button10.Visible = false;
			button5.Enabled = false;
			button7.Enabled = false;
			connection.Open();

			string selectQuery = "SELECT demanded_lectures FROM students WHERE student_id = @studentId";


			using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
			{
				command.Parameters.AddWithValue("studentId", user.id);

				using (NpgsqlDataReader reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						string demandedLectures = reader["demanded_lectures"].ToString();

						var jsonArray = Newtonsoft.Json.JsonConvert.DeserializeObject(demandedLectures) as Newtonsoft.Json.Linq.JArray;

						string formattedDemandedLectures = "";

						if (jsonArray != null)
						{
							foreach (var item in jsonArray)
							{

								string courseInfo = item["course_info"].ToString();
								string teachersId = item["teachers_id"].ToString();
								string teachersName = item["teachers_name"].ToString();
								string agreementStatus = item["agreement_status"].ToString();

								if (agreementStatus == "Kabul Edildi")
								{
									formattedDemandedLectures = $"{teachersId} - {teachersName} - {courseInfo} - {agreementStatus}";
									checkedListBox1.Items.Add(formattedDemandedLectures);
								}
							}
						}
					}
				}
			}

			connection.Close();
		}

		private void button9_Click(object sender, EventArgs e)
		{

			richTextBox1.Clear();
			richTextBox2.Clear();
			listBox1.Items.Clear();
			checkedListBox1.Items.Clear();
			comboBox1.Name = "";
			checkedListBox1.Visible = false;
			comboBox1.SelectedItem = null;
			comboBox1.Items.Clear();
			richTextBox2.Visible = false;
			listBox1.Visible = false;
			button10.Visible = false;
			this.Visible = false;
			loginScreen.Visible = true;
		}

		private void StudentScreen_VisibleChanged(object sender, EventArgs e)
		{
			if (this.Visible)
			{
				GetInterestAreas();
				isTranscriptUploaded();
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			comboBox1.Visible = true;
			listBox1.Visible = true;
			richTextBox2.Visible = true;
			button10.Visible = true;
			richTextBox1.Visible = false;
			checkedListBox1.Visible = false;
			comboBox1.Items.Clear();
			comboBox1.Name = "Mesajlar";
			comboBox1.Text = "Mesajlar";

			connection.Open();
			using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT identification_number, name, surname FROM teachers", connection))
			using (NpgsqlDataReader reader = cmd.ExecuteReader())
			{
				while (reader.Read())
				{
					string formattedTeacher = $"{reader["identification_number"]} - {reader["name"]} {reader["surname"]}";
					comboBox1.Items.Add(formattedTeacher);
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

		private void richTextBox2_TextChanged(object sender, EventArgs e)
		{

		}
		void getMessages()
		{
			if (comboBox1.Name == "Mesajlar" && comboBox1.Items.Count != 0)
			{
				connection.Open();
				listBox1.Items.Clear();
				string selectQuery = "SELECT sent_messages FROM teachers where identification_number = @identification_number ";

				string[] teacherInfo = comboBox1.SelectedItem.ToString().Split("-");

				using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
				{
					command.Parameters.AddWithValue("identification_number", int.Parse(teacherInfo[0]));

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

									if (int.Parse(sent) == 0 && int.Parse(studentId) == user.id)
									{
										formattedSentMessages = $"{comboBox1.Text} : {text}";
										listBox1.Items.Add(formattedSentMessages);
									}
									else if (int.Parse(sent) == 1 && int.Parse(studentId) == user.id)
									{
										formattedSentMessages = $"{studentId} - {studentName} : {text}";
										listBox1.Items.Add(formattedSentMessages);
									}

								}
							}
						}
					}
				}
				connection.Close();
			}

		}
		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			getMessages();
			if (comboBox1.Name == "Dersler" && comboBox1.SelectedIndex != 0)
				button4_Click(sender, e);

		}
		private void button10_Click(object sender, EventArgs e)
		{
			//gönder
			if (comboBox1.Text != "Mesajlar")
			{
				connection.Open();

				bool isFirstCourse = true;
				string updateQuery;

				using (NpgsqlCommand command = new NpgsqlCommand("SELECT sent_messages FROM teachers WHERE identification_number = @identificationNumber", connection))
				{
					command.Parameters.AddWithValue("identificationNumber", int.Parse(comboBox1.Text.Split("-")[0].Trim()));

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
				string sent = "1";
				string identificationNumber = comboBox1.Text.Split("-")[0].Trim();
				string teacherName = comboBox1.Text.Split("-")[1].Trim();
				string studentid = user.id.ToString();//************************DÜZELTTTTTTTTT
				string studentName = user.name + " " + user.surname;

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

		private void StudentScreen_Load(object sender, EventArgs e)
		{

		}
	}
}
