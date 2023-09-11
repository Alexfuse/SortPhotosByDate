using MetadataExtractor;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    
    public partial class Form1 : Form
    {
        static readonly string[] Formats = { "yyyy:MM:dd HH:mm:ss.fff", "yyyy:MM:dd HH:mm:ss" };

        private static volatile int filesMovedCounter = 0;
        public Form1()
        {
            InitializeComponent();
            labelSortedFiles.Text = "0";
            button_folder.Click += GetFolder;
            button_start.Click += StartSorting;
            button_start.Enabled = false;
            progressBar1.Minimum = 0;
            progressBar1.Step = 1;
        }
        
        // get input folder
        void GetFolder (object sende, EventArgs e )
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            folderBrowserDialog1.ShowNewFolderButton = false;
            
            input_folder_path.Text = folderBrowserDialog1.SelectedPath;
            button_start.Enabled = true;
            
        }

        //set destination folder
        private void GetDestinationFolder(object sender, EventArgs e)
        {
            if (folderBrowserDialog2.ShowDialog() == DialogResult.Cancel)
                return;
            folderBrowserDialog2.ShowNewFolderButton = true;
            output_folder_path.Text = folderBrowserDialog2.SelectedPath;
            button_start.Enabled = true;
        }

        async void StartSorting(object sende, EventArgs args)
        {
            progressBar1.Value = 0;
            filesMovedCounter = 0;
            richTextBox1.Clear();

            if (!System.IO.Directory.Exists(input_folder_path.Text))
            {
                MessageBox.Show("Folder doesn't exist!");
                return;
            }
            input_folder_path.ReadOnly = true;
            if (output_folder_path.Text.Trim().Length == 0)
            {
                MessageBox.Show("Select otput folder!");
                return;
            }
            
            if (!System.IO.Directory.Exists(output_folder_path.Text))
            {
                createDestinationFolder();
            }

                
            DirectoryInfo rootDirectory = new DirectoryInfo(@"" + input_folder_path.Text);
            if (rootDirectory.GetFilesByExtensions(".jpg", ".png", ".jpeg").Count() == 0 && rootDirectory.GetDirectories().Length == 0)
            {
                MessageBox.Show("Folder is empty!\nPlease choose another folder.");
                input_folder_path.ReadOnly = false;
                return;
            }
            List<string> allImages = System.IO.Directory
            .EnumerateFiles(rootDirectory.FullName)
            .Where(file => file.ToLower().EndsWith("jpg") || file.ToLower().EndsWith("png"))
            .ToList();

            progressBar1.Maximum = allImages.Count;
            await Task.Run(() => ProcessFiles(rootDirectory, richTextBox1));
            input_folder_path.ReadOnly = false;
            output_folder_path.ReadOnly = false;
            MessageBox.Show("Done");
        }

        async void ProcessFiles(DirectoryInfo directory, RichTextBox richTextBox)
        {
            List<Task> listTasks = new List<Task>();
            PermissionSet permissions = new PermissionSet(PermissionState.None);
            permissions.AddPermission(new FileIOPermission(FileIOPermissionAccess.Read, directory.FullName));
            if (permissions.IsSubsetOf(AppDomain.CurrentDomain.PermissionSet))
            {
                IEnumerable<FileInfo> imageFiles = directory.GetFilesByExtensions(".jpg", ".png", ".jpeg");
                if (imageFiles.Count() != 0)
                {
                    richTextBox.Invoke((MethodInvoker)delegate { richTextBox.AppendText($"Files loaded: {imageFiles.Count()} from {directory.FullName}" + Environment.NewLine);  });
                    foreach (FileInfo file in imageFiles)
                    {
                        Tag dateTimeTag = GetDateTimeTagFromFile(file);

                        if (dateTimeTag == null)
                        {
                            richTextBox.Invoke((MethodInvoker)delegate { richTextBox.AppendText($"ERROR: The file is missing a date-time field in the metadata {file.Name}" + Environment.NewLine); });
                            continue;
                        }

                        try
                        {
                            DateTime myDate = DateTime.ParseExact(dateTimeTag.Description, Formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
                            createDirectory(output_folder_path.Text + "\\" + myDate.Year + "\\" + myDate.Month);

                            try
                            {
                                File.Move(file.FullName, output_folder_path.Text + "\\" + myDate.Year + "\\" + myDate.Month + "\\" + file.Name);
                            }
                            catch (IOException e)
                            {
                                richTextBox.Invoke((MethodInvoker)delegate {
                                    richTextBox.AppendText($"ERROR: {e.Message}" + Environment.NewLine);
                                    richTextBox.AppendText($"ERROR: Can't move file {file.Name}. File was copied" + Environment.NewLine);
                                });
                                File.Copy(file.FullName, output_folder_path.Text + "\\" + myDate.Year + "\\" + myDate.Month + "\\" + file.Name);
                            }
                            Interlocked.Increment(ref filesMovedCounter);
                            labelSortedFiles.Invoke((MethodInvoker)delegate { labelSortedFiles.Text = filesMovedCounter.ToString(); });
                        }
                        catch (IOException e)
                        {
                            richTextBox.Invoke((MethodInvoker)delegate {
                                richTextBox.AppendText($"ERROR: {e.Message}" + Environment.NewLine);
                                richTextBox.AppendText($"ERROR: ERROR: {dateTimeTag.Description}" + Environment.NewLine);
                            });
                        }
                        progressBar1.Invoke((MethodInvoker)delegate { progressBar1.PerformStep(); });
                    }
                }


                foreach (DirectoryInfo d in directory.GetDirectories())
                {
                    Task task = Task.Run(() => ProcessFiles(d, richTextBox));
                    listTasks.Add(task);
                }
            } 
            else
            {
                richTextBox.Invoke((MethodInvoker)delegate { richTextBox.AppendText("ERROR: no access to directory" + Environment.NewLine); });
            }
            Task.WhenAll(listTasks);
        }

        private void FolderPatchTextChanged(object sender, EventArgs e)
        {
            if (input_folder_path.Text.Trim().Length > 0)
            {
                button_start.Enabled = true;
            }
            else
            {
                button_start.Enabled = false;
                
            }
            
        }

        void createDestinationFolder()
        {
            try
            {
                System.IO.Directory.CreateDirectory(output_folder_path.Text);
                output_folder_path.ReadOnly = true;
            }
            catch (IOException e)
            {
                richTextBox1.Text += $"ERROR: {e.Message}\n";
                richTextBox1.Text += $"ERROR: Can't create new folder for sorted files\n";
                MessageBox.Show("Can't create copy folder");
                return;
            }
        }

        void createDirectory(string path)
        {
            try
            {
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);
            }
            catch (IOException e)
            {
                richTextBox1.Text += $"ERROR: {e.Message}\n";
                MessageBox.Show("Can't create month folder");
                return;
            }
        }

        Tag GetDateTimeTagFromFile(FileInfo file) {
            Tag dateTimeTag = null;
            try
            {
                FileStream fs = File.OpenRead(file.FullName);
                IReadOnlyList<MetadataExtractor.Directory> metaDirectories = ImageMetadataReader.ReadMetadata(fs);
                fs.Close();

                MetadataExtractor.Directory directory = metaDirectories.Where(m => !m.HasError)
                    .SingleOrDefault(m => m.Tags.Any(t => t.Name.Equals("Date/Time")))
                    ?? throw new Exception("Metadata not found");
                dateTimeTag = directory.Tags
                .SingleOrDefault(t => t.Name.Equals("Date/Time")) ?? throw new Exception("Tag not found");
            }
            catch (Exception e)
            {
                richTextBox1.Text += $"ERROR: {e.Message}\n";
            }
            return dateTimeTag;
        }

    }
}
