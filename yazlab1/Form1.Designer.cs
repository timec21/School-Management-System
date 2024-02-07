namespace yazlab1
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			adminScreen1 = new AdminScreen();
			loginScreen1 = new LoginScreen();
			studentScreen1 = new StudentScreen();
			teacherScreen1 = new TeacherScreen();
			SuspendLayout();
			// 
			// adminScreen1
			// 
			adminScreen1.Location = new Point(0, 0);
			adminScreen1.Name = "adminScreen1";
			adminScreen1.Size = new Size(866, 367);
			adminScreen1.TabIndex = 0;
			adminScreen1.Visible = false;
			// 
			// loginScreen1
			// 
			loginScreen1.Location = new Point(0, 0);
			loginScreen1.Name = "loginScreen1";
			loginScreen1.Size = new Size(900, 600);
			loginScreen1.TabIndex = 1;
			loginScreen1.Load += loginScreen1_Load;
			loginScreen1.Visible = true;
			// 
			// studentScreen1
			// 
			studentScreen1.Location = new Point(0, 0);
			studentScreen1.Name = "studentScreen1";
			studentScreen1.Size = new Size(855, 534);
			studentScreen1.TabIndex = 2;
			studentScreen1.Visible = false;
			// 
			// teacherScreen1
			// 
			teacherScreen1.Location = new Point(0, 0);
			teacherScreen1.Name = "teacherScreen1";
			teacherScreen1.Size = new Size(880, 525);
			teacherScreen1.TabIndex = 3;
			teacherScreen1.Visible = false;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 491);
			Controls.Add(studentScreen1);
			Controls.Add(loginScreen1);
			Controls.Add(adminScreen1);
			Controls.Add(teacherScreen1);
			Name = "Form1";
			Text = "Form1";
			ResumeLayout(false);
		}

		#endregion

		private AdminScreen adminScreen1;
		private LoginScreen loginScreen1;
		private StudentScreen studentScreen1;
		private TeacherScreen teacherScreen1;
	}
}