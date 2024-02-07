namespace yazlab1
{
	partial class StudentScreen
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
			openFileDialog1 = new OpenFileDialog();
			button1 = new Button();
			richTextBox1 = new RichTextBox();
			button2 = new Button();
			button3 = new Button();
			button4 = new Button();
			comboBox1 = new ComboBox();
			checkedListBox1 = new CheckedListBox();
			button5 = new Button();
			button6 = new Button();
			button7 = new Button();
			button8 = new Button();
			button9 = new Button();
			listBox1 = new ListBox();
			richTextBox2 = new RichTextBox();
			button10 = new Button();
			SuspendLayout();
			// 
			// openFileDialog1
			// 
			openFileDialog1.FileName = "openFileDialog1";
			// 
			// button1
			// 
			button1.Location = new Point(716, 142);
			button1.Name = "button1";
			button1.Size = new Size(104, 60);
			button1.TabIndex = 0;
			button1.Text = "Transkript Ekle";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// richTextBox1
			// 
			richTextBox1.Location = new Point(3, 61);
			richTextBox1.Name = "richTextBox1";
			richTextBox1.ReadOnly = true;
			richTextBox1.Size = new Size(676, 443);
			richTextBox1.TabIndex = 1;
			richTextBox1.Text = "";
			// 
			// button2
			// 
			button2.Location = new Point(716, 76);
			button2.Name = "button2";
			button2.Size = new Size(104, 60);
			button2.TabIndex = 2;
			button2.Text = "Transkript Görüntüle";
			button2.UseVisualStyleBackColor = true;
			button2.Click += button2_Click;
			// 
			// button3
			// 
			button3.Location = new Point(716, 10);
			button3.Name = "button3";
			button3.Size = new Size(104, 60);
			button3.TabIndex = 3;
			button3.Text = "Mesajlar";
			button3.UseVisualStyleBackColor = true;
			button3.Click += button3_Click;
			// 
			// button4
			// 
			button4.Location = new Point(716, 208);
			button4.Name = "button4";
			button4.Size = new Size(104, 60);
			button4.TabIndex = 4;
			button4.Text = "Açılan Dersler";
			button4.UseVisualStyleBackColor = true;
			button4.Click += button4_Click;
			// 
			// comboBox1
			// 
			comboBox1.FormattingEnabled = true;
			comboBox1.Location = new Point(14, 15);
			comboBox1.Name = "comboBox1";
			comboBox1.Size = new Size(305, 28);
			comboBox1.TabIndex = 5;
			comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
			// 
			// checkedListBox1
			// 
			checkedListBox1.FormattingEnabled = true;
			checkedListBox1.Location = new Point(3, 61);
			checkedListBox1.Name = "checkedListBox1";
			checkedListBox1.Size = new Size(707, 400);
			checkedListBox1.TabIndex = 6;
			checkedListBox1.Visible = false;
			// 
			// button5
			// 
			button5.Location = new Point(716, 274);
			button5.Name = "button5";
			button5.Size = new Size(104, 60);
			button5.TabIndex = 7;
			button5.Text = "Ders Ekle";
			button5.UseVisualStyleBackColor = true;
			button5.Click += button5_Click;
			// 
			// button6
			// 
			button6.Location = new Point(333, 6);
			button6.Name = "button6";
			button6.Size = new Size(151, 49);
			button6.TabIndex = 8;
			button6.Text = "Talep Edilen Dersler";
			button6.UseVisualStyleBackColor = true;
			button6.Click += button6_Click;
			// 
			// button7
			// 
			button7.Location = new Point(611, 6);
			button7.Name = "button7";
			button7.Size = new Size(99, 49);
			button7.TabIndex = 9;
			button7.Text = "Talep İptal";
			button7.UseVisualStyleBackColor = true;
			button7.Click += button7_Click;
			// 
			// button8
			// 
			button8.Location = new Point(487, 6);
			button8.Name = "button8";
			button8.Size = new Size(118, 49);
			button8.TabIndex = 10;
			button8.Text = "Onaylanan Dersler";
			button8.UseVisualStyleBackColor = true;
			button8.Click += button8_Click;
			// 
			// button9
			// 
			button9.Location = new Point(716, 340);
			button9.Name = "button9";
			button9.Size = new Size(104, 60);
			button9.TabIndex = 11;
			button9.Text = "Çıkış";
			button9.UseVisualStyleBackColor = true;
			button9.Click += button9_Click;
			// 
			// listBox1
			// 
			listBox1.FormattingEnabled = true;
			listBox1.ItemHeight = 20;
			listBox1.Location = new Point(3, 61);
			listBox1.Name = "listBox1";
			listBox1.Size = new Size(410, 384);
			listBox1.TabIndex = 12;
			listBox1.Visible = false;
			// 
			// richTextBox2
			// 
			richTextBox2.Location = new Point(416, 63);
			richTextBox2.Name = "richTextBox2";
			richTextBox2.Size = new Size(291, 205);
			richTextBox2.TabIndex = 13;
			richTextBox2.Text = "";
			richTextBox2.Visible = false;
			richTextBox2.TextChanged += richTextBox2_TextChanged;
			// 
			// button10
			// 
			button10.Location = new Point(487, 274);
			button10.Name = "button10";
			button10.Size = new Size(151, 49);
			button10.TabIndex = 14;
			button10.Text = "Gönder";
			button10.UseVisualStyleBackColor = true;
			button10.Visible = false;
			button10.Click += button10_Click;
			// 
			// StudentScreen
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(button10);
			Controls.Add(richTextBox2);
			Controls.Add(listBox1);
			Controls.Add(button9);
			Controls.Add(button8);
			Controls.Add(button7);
			Controls.Add(button6);
			Controls.Add(button5);
			Controls.Add(checkedListBox1);
			Controls.Add(comboBox1);
			Controls.Add(button4);
			Controls.Add(button3);
			Controls.Add(button2);
			Controls.Add(richTextBox1);
			Controls.Add(button1);
			Name = "StudentScreen";
			Size = new Size(855, 534);
			Load += StudentScreen_Load;
			VisibleChanged += StudentScreen_VisibleChanged;
			ResumeLayout(false);
		}

		#endregion

		private OpenFileDialog openFileDialog1;
		private Button button1;
		private RichTextBox richTextBox1;
		private Button button2;
		private Button button3;
		private Button button4;
		private ComboBox comboBox1;
		private CheckedListBox checkedListBox1;
		private Button button5;
		private Button button6;
		private Button button7;
		private Button button8;
		private Button button9;
		private ListBox listBox1;
		private RichTextBox richTextBox2;
		private Button button10;
	}
}
