namespace yazlab1
{
	partial class LoginScreen
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			button1 = new Button();
			label1 = new Label();
			textBox1 = new TextBox();
			textBox2 = new TextBox();
			label2 = new Label();
			checkBox1 = new CheckBox();
			checkBox2 = new CheckBox();
			checkBox3 = new CheckBox();
			SuspendLayout();
			// 
			// button1
			// 
			button1.Location = new Point(360, 277);
			button1.Name = "button1";
			button1.Size = new Size(108, 44);
			button1.TabIndex = 0;
			button1.Text = "Giriş";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(262, 184);
			label1.Name = "label1";
			label1.Size = new Size(92, 20);
			label1.TabIndex = 1;
			label1.Text = "Kullanıcı Adı";
			// 
			// textBox1
			// 
			textBox1.Location = new Point(360, 184);
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(125, 27);
			textBox1.TabIndex = 2;
			// 
			// textBox2
			// 
			textBox2.Location = new Point(360, 228);
			textBox2.Name = "textBox2";
			textBox2.Size = new Size(125, 27);
			textBox2.TabIndex = 4;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(295, 228);
			label2.Name = "label2";
			label2.Size = new Size(39, 20);
			label2.TabIndex = 3;
			label2.Text = "Şifre";
			// 
			// checkBox1
			// 
			checkBox1.AutoSize = true;
			checkBox1.Location = new Point(271, 139);
			checkBox1.Name = "checkBox1";
			checkBox1.Size = new Size(83, 24);
			checkBox1.TabIndex = 5;
			checkBox1.Text = "Yönetici";
			checkBox1.UseVisualStyleBackColor = true;
			checkBox1.CheckedChanged += checkBox1_CheckedChanged;
			// 
			// checkBox2
			// 
			checkBox2.AutoSize = true;
			checkBox2.Location = new Point(371, 139);
			checkBox2.Name = "checkBox2";
			checkBox2.Size = new Size(66, 24);
			checkBox2.TabIndex = 6;
			checkBox2.Text = "Hoca";
			checkBox2.UseVisualStyleBackColor = true;
			checkBox2.CheckedChanged += checkBox2_CheckedChanged;
			// 
			// checkBox3
			// 
			checkBox3.AutoSize = true;
			checkBox3.Location = new Point(457, 139);
			checkBox3.Name = "checkBox3";
			checkBox3.Size = new Size(83, 24);
			checkBox3.TabIndex = 7;
			checkBox3.Text = "Öğrenci";
			checkBox3.UseVisualStyleBackColor = true;
			checkBox3.CheckedChanged += checkBox3_CheckedChanged;
			// 
			// LoginScreen
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(checkBox3);
			Controls.Add(checkBox2);
			Controls.Add(checkBox1);
			Controls.Add(textBox2);
			Controls.Add(label2);
			Controls.Add(textBox1);
			Controls.Add(label1);
			Controls.Add(button1);
			Name = "LoginScreen";
			Size = new Size(893, 451);
			Load += LoginScreen_Load;
			VisibleChanged += LoginScreen_VisibleChanged;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button button1;
		private Label label1;
		private TextBox textBox1;
		private TextBox textBox2;
		private Label label2;
		private CheckBox checkBox1;
		private CheckBox checkBox2;
		private CheckBox checkBox3;
	}
}
