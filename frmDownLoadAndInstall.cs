using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DemoUpdate
{
    public partial class frmDownLoadAndInstall : Form
    {
        public string url = "https://localhost:44385/File/Download?fileName=version.xml";


        public bool IsCrystalReportsInstalled()
        {
            const string registryKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (var key = Registry.LocalMachine.OpenSubKey(registryKey))
            {
                if (key == null)
                {
                    return false;
                }

                foreach (var subkeyName in key.GetSubKeyNames())
                {
                    using (var subkey = key.OpenSubKey(subkeyName))
                    {
                        if (subkey == null)
                        {
                            continue;
                        }

                        var displayName = subkey.GetValue("DisplayName") as string;
                        if (!string.IsNullOrEmpty(displayName) && displayName.Contains("Crystal Reports"))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public frmDownLoadAndInstall()
        {
            InitializeComponent();

        }


        private void frmDownLoadAndInstall_Shown(object sender, EventArgs e)
        {

            DownLoadAndExtract();

        }

        private string getLinkFileDownLoad()
        {
            WebClient web = new WebClient();
            string xmlString = web.DownloadString(url);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            return xmlDoc.SelectSingleNode("/version/fileName").InnerText;
        }

        private void DownLoadAndExtract()
        {

            lblStatus.Text = "Download";
            WebClient web = new WebClient();
            string xmlString = web.DownloadString(url);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            string downloadUrl = xmlDoc.SelectSingleNode("/version/url").InnerText;
            string fileNamelatestVersion = xmlDoc.SelectSingleNode("/version/fileName").InnerText;

            WebClient client = new WebClient();

            // Add event handlers for the download progress
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);
            client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadCompletedCallback);

            // Start the download
            client.DownloadFileAsync(new Uri(downloadUrl), fileNamelatestVersion);

            //client.DownloadFile(downloadUrl, fileNamelatestVersion);

        }
        private void Extract(string fileNamelatestVersion)
        {
            lblStatus.Text = "Extract";

            string zipPath = fileNamelatestVersion;
            long totalSize = 0;
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    totalSize += entry.Length;
                }
            }
            string extractPath = @".\";
            ZipFile.ExtractToDirectory(zipPath, extractPath);

            long extractedSize = 0;
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    // Extract the current file
                    try
                    {
                        //entry.ExtractToFile(Path.Combine(extractPath, entry.FullName));




                        // Update the progress bar and percent label
                        extractedSize += entry.Length;
                        int progress = (int)((double)extractedSize / totalSize * 100);

                        progressBar1.Value = progress;
                        lblPercent.Text = progress + "%";

                        // Update the label with the current file name
                        lblCurrentFile.Text = "Extracting: " + entry.FullName;

                        // Allow the UI to update
                        Application.DoEvents();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }


                }
            }

            progressBar1.Value = 100;
            lblPercent.Text = "100%";
            lblCurrentFile.Text = "Extraction complete.";

            File.Delete(fileNamelatestVersion);




            //progressBar1.Value = 100;
            //lblPercent.Text = "100%";
            //lblCurrentFile.Text = "Extraction complete.";

            ////File.Delete(fileNamelatestVersion);

        }

        private void DownloadAndExtraOld()
        {
            WebClient web = new WebClient();
            string xmlString = web.DownloadString(url);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            string downloadUrl = xmlDoc.SelectSingleNode("/version/url").InnerText;
            string fileNamelatestVersion = xmlDoc.SelectSingleNode("/version/fileName").InnerText;
            WebClient client = new WebClient();
            client.DownloadFile(downloadUrl, fileNamelatestVersion);


            string zipPath = fileNamelatestVersion;
            string extractPath = @".\";
            ZipFile.ExtractToDirectory(zipPath, extractPath);
            File.Delete(fileNamelatestVersion);



            if (IsCrystalReportsInstalled())
            {
                Process.Start(@".\DemoUpdate.exe");
            }
            else
            {
                Process.Start(@".\CR13SP28MSI32_0-10010309.MSI");
                Thread.Sleep(30000);
                Process.Start(@".\DemoUpdate.exe");
            }
        }

        private void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            // Update the progress bar
            progressBar1.Value = e.ProgressPercentage;

            // Update the label with the download progress in percent
            lblPercent.Text = e.ProgressPercentage + "%";
        }

        private void DownloadCompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            // Reset the progress bar and label
            progressBar1.Value = 100;
            lblPercent.Text = "100%";

            // Show a message box to indicate that the download has completed
            //MessageBox.Show("Download completed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Extract(getLinkFileDownLoad());
            this.Close();
            checkAndSAP();

        }

        private void checkAndSAP()
        {
            if (IsCrystalReportsInstalled())
            {
                Process.Start(@".\DemoUpdate.exe");
            }
            else
            {
                Process.Start(@".\CR13SP28MSI32_0-10010309.MSI");
                Thread.Sleep(20000);
                Process.Start(@".\DemoUpdate.exe");
            }
        }





    }
}

