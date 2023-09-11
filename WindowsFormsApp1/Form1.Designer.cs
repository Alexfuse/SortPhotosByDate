namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.input_folder_path = new System.Windows.Forms.TextBox();
            this.button_folder = new System.Windows.Forms.Button();
            this.button_start = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.output_folder_path = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.labelSortedFiles = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // input_folder_path
            // 
            this.input_folder_path.Location = new System.Drawing.Point(50, 12);
            this.input_folder_path.Name = "input_folder_path";
            this.input_folder_path.Size = new System.Drawing.Size(287, 20);
            this.input_folder_path.TabIndex = 0;
            this.input_folder_path.TextChanged += new System.EventHandler(this.FolderPatchTextChanged);
            // 
            // button_folder
            // 
            this.button_folder.Location = new System.Drawing.Point(343, 11);
            this.button_folder.Name = "button_folder";
            this.button_folder.Size = new System.Drawing.Size(87, 20);
            this.button_folder.TabIndex = 1;
            this.button_folder.Text = "Select Folder";
            this.button_folder.UseVisualStyleBackColor = true;
            // 
            // button_start
            // 
            this.button_start.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.button_start.Location = new System.Drawing.Point(343, 75);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(87, 23);
            this.button_start.TabIndex = 4;
            this.button_start.Text = "Start";
            this.button_start.UseVisualStyleBackColor = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 117);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(418, 161);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "To";
            // 
            // output_folder_path
            // 
            this.output_folder_path.Location = new System.Drawing.Point(50, 46);
            this.output_folder_path.Name = "output_folder_path";
            this.output_folder_path.Size = new System.Drawing.Size(287, 20);
            this.output_folder_path.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(343, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 20);
            this.button1.TabIndex = 9;
            this.button1.Text = "Select Folder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.GetDestinationFolder);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(13, 74);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(324, 23);
            this.progressBar1.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(403, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "log";
            // 
            // folderBrowserDialog2
            // 
            this.folderBrowserDialog2.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Files sorted:";
            // 
            // labelSortedFiles
            // 
            this.labelSortedFiles.AutoSize = true;
            this.labelSortedFiles.Location = new System.Drawing.Point(72, 101);
            this.labelSortedFiles.Name = "labelSortedFiles";
            this.labelSortedFiles.Size = new System.Drawing.Size(13, 13);
            this.labelSortedFiles.TabIndex = 13;
            this.labelSortedFiles.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 283);
            this.Controls.Add(this.labelSortedFiles);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.output_folder_path);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.button_folder);
            this.Controls.Add(this.input_folder_path);
            this.Name = "Form1";
            this.Text = "Sort to folder images";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox input_folder_path;
        private System.Windows.Forms.Button button_folder;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox output_folder_path;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelSortedFiles;
    }
}

