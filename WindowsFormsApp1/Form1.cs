using MetadataExtractor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            button_folder.Click += GetFolder;
            button_start.Click += StartSorting;
            button_start.Enabled = false;
        }
        
        // получаем выбранную директорию
        void GetFolder (object sende, EventArgs e )
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            folderBrowserDialog1.ShowNewFolderButton = false;
            
            folder_patch.Text = folderBrowserDialog1.SelectedPath;
            button_start.Enabled = true;
            
        }

        //указываем куда складываем файлы
        private void GetDestinationFolder(object sender, EventArgs e)
        {
            if (folderBrowserDialog2.ShowDialog() == DialogResult.Cancel)
                return;
            folderBrowserDialog2.ShowNewFolderButton = true;
            textBox_copy_to.Text = folderBrowserDialog2.SelectedPath;
            button_start.Enabled = true;
        }

        void StartSorting(object sende, EventArgs args)
        {
            progressBar1.Value = 0;
            int filesMovedCounter = 0;
            
            string[] formats = { "yyyy:MM:dd HH:mm:ss.fff", "yyyy:MM:dd HH:mm:ss" };
            if (System.IO.Directory.Exists(folder_patch.Text))
            {
                folder_patch.ReadOnly = true;
                if (textBox_copy_to.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Select copy folder");
                    return;
                }
                if (System.IO.Directory.Exists(textBox_copy_to.Text))
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(textBox_copy_to.Text);
                        textBox_copy_to.ReadOnly = true;
                    }
                    catch (System.IO.IOException e)
                    {
                        richTextBox1.Text += $"ERROR: {e.Message}\n";
                        richTextBox1.Text += $"ERROR: Can't create new folder for sorted files\n";
                        MessageBox.Show("Can't create copy folder");
                    }
                }

                 
                DirectoryInfo d = new DirectoryInfo(@"" + folder_patch.Text);
                var Files = d.GetFilesByExtensions(".jpg", ".png", ".jpeg");
                if(Files.Count() == 0)
                {
                    MessageBox.Show("Wow!\nSo much empty there.\nWhere is youe files dude?");
                    folder_patch.ReadOnly = false;
                    return;
                }
                progressBar1.Minimum = 0;
                progressBar1.Maximum = Files.Count();
                progressBar1.Step = 1;
                richTextBox1.Text += $"Files loaded: {Files.Count()}\n";
                foreach (FileInfo file in Files)
                {
                    FileStream fs = File.OpenRead(file.FullName);
                    try
                    {
                        var metaDirectories = ImageMetadataReader.ReadMetadata(fs);
                        fs.Close();
                        foreach (var metaData in metaDirectories)
                        {
                            if (metaData.HasError)
                            {
                                foreach (var error in metaData.Errors)
                                    richTextBox1.Text += $"ERROR: metaData.HasError {error}\n";
                                richTextBox1.Text += $"ERROR: file {file.Name}\n";
                            }
                            else
                            {
                                Tag tag = metaData.Tags.Where(i => i.Name == "Date/Time").SingleOrDefault();
                                if(tag != null)
                                {
                                    DateTime myDate = new DateTime();
                                    try
                                    {
                                        myDate = DateTime.ParseExact(tag.Description, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);// 
                                        try
                                        {
                                            if (!System.IO.Directory.Exists(textBox_copy_to.Text + "\\" + myDate.Year))
                                                System.IO.Directory.CreateDirectory(textBox_copy_to.Text + "\\" + myDate.Year);
                                        }
                                        catch (System.IO.IOException e)
                                        {
                                            richTextBox1.Text += $"ERROR: {e.Message}\n";
                                            MessageBox.Show("Can't create year folder");
                                            return;
                                        }

                                        try
                                        {
                                            if (!System.IO.Directory.Exists(textBox_copy_to.Text + "\\" + myDate.Year + "\\" + myDate.Month))
                                                System.IO.Directory.CreateDirectory(textBox_copy_to.Text + "\\" + myDate.Year + "\\" + myDate.Month);
                                        }
                                        catch (System.IO.IOException e)
                                        {
                                            richTextBox1.Text += $"ERROR: {e.Message}\n";
                                            MessageBox.Show("Can't create month folder");
                                            return;
                                        }
                                        try
                                        {
                                            File.Move(file.FullName, textBox_copy_to.Text + "\\" + myDate.Year + "\\" + myDate.Month + "\\" + file.Name);
                                            filesMovedCounter++;
                                        }
                                        catch (System.IO.IOException e)
                                        {
                                            richTextBox1.Text += $"ERROR: {e.Message}\n";
                                            richTextBox1.Text += $"ERROR: Can't move file {file.Name}. File was copied \n";
                                            File.Copy(file.FullName, textBox_copy_to.Text + "\\" + myDate.Year + "\\" + myDate.Month + "\\" + file.Name);
                                        }
                                    }
                                    catch (System.IO.IOException e)
                                    {
                                        richTextBox1.Text += $"ERROR: {e.Message}\n";
                                        richTextBox1.Text += $"ERROR: {tag.Description}\n";
                                        MessageBox.Show("Can't parse datetime");
                                    }
                                }
                            }

                        }

                    }
                    catch (System.IO.IOException e)
                    {
                        richTextBox1.Text += $"ERROR: {e.Message}\n";
                        richTextBox1.Text += $"ERROR: Can't read file {file.Name}\n";

                    }
                    
                    
                    
                    progressBar1.PerformStep();
                }
                richTextBox1.Text += $"Files sorted & moved: {filesMovedCounter}\n";
                folder_patch.ReadOnly = false;
                textBox_copy_to.ReadOnly = false;
                MessageBox.Show("Done");

            }
            else
            {
                MessageBox.Show("Folder doesn't exist!");
            }
        }

        private void FolderPatchTextChanged(object sender, EventArgs e)
        {
            if(folder_patch.Text.Trim().Length > 0)
            {
                button_start.Enabled = true;
            }
            else
            {
                button_start.Enabled = false;
                
            }
            
        }

        
    }
}
