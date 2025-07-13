using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms.Design;

namespace ArchiveModel
{
    public partial class CreateArchive : Form
    {
        public CreateArchive(String filePath, String latestDate, String parentDirectory, String projectTitle)
        {
            InitializeComponent();
            rlArchiveDate.Text = latestDate;
            rlProjectTitle.Text = projectTitle;
            rlSourceName = filePath;
            rlParentDirectory = parentDirectory;
        }

        private void CreateArchive_Load(object sender, EventArgs e)
        {

        }

        private void rlCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rlArchiveButton_Click(object sender, EventArgs e)
        {
            String today = DateTime.Now.ToString("yyyy-MM-dd");
            String createDirPath = Path.Combine(rlParentDirectory, today + " - " + rlProjectTitle.Text);
            string sourceName = rlSourceName;
            string fileName = Path.GetFileName(rlSourceName);
            string destName = Path.Combine(createDirPath, fileName);
            if (!Directory.Exists(createDirPath))
            {
                try
                {
                    Directory.CreateDirectory(createDirPath);
                    File.Copy(sourceName, destName, true);

                }
                catch (UnauthorizedAccessException)
                {
                    LaunchElevatedAction($"New-Item -ItemType Directory -Path '{createDirPath}'");
                    LaunchElevatedAction($"Copy-Item -Force -Path '{sourceName}' -Destination '{destName}'");
                }
                catch (Exception)
                {
                    MessageBox.Show("An error occurred while copying the file. Please check the file path and permissions.", "Error");
                    return;
                }
                MessageBox.Show("Project Archive Created");
                this.Close();
            }
            else
            {
                DialogResult result = MessageBox.Show("Archive " + today + " - " + rlProjectTitle.Text + " already exists. Press OK to Overwrite", "Archive Exists",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    try
                    {
                        File.Copy(sourceName, destName, true);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        LaunchElevatedAction($"Copy-Item -Force -Path '{sourceName}' -Destination '{destName}'");
                    }
                    catch (Exception) {
                        MessageBox.Show("An error occurred while copying the file. Please check the file path and permissions.", "Error");
                        return;
                    }

                    MessageBox.Show("Project Archive Created");
                    this.Close();
                   
                }
                else
                {
                    MessageBox.Show("Archiving was canceled.", "Status");
                }
            }
        }

        private void rlListArchives_Click(object sender, EventArgs e)
        {
            MessageBox.Show("I'll finish this later!", "Not Implemented");
        }

        public static void LaunchElevatedAction(string actionCommand)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"-Command \"{actionCommand}\"",
                Verb = "runas",
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            try
            {
                Process.Start(psi);
            }
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("You must run with elevated privileges to perform this operation.");
            }
        }

    }
}
