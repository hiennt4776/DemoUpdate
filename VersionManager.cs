using System;
using System.Collections.Generic;

using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;

using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Xml;
using System.Threading;
using System.Xml.Linq;
using System.IO.Compression;
using System.Security.Principal;
using Microsoft.Win32;

namespace DemoUpdate
{
    public class VersionManager
    {
        static string url = "https://localhost:44385/File/Download?fileName=version.xml";

        public static bool IsCrystalReportsInstalled()
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


        public static string GetCurrentVersion()
        {

            string currentVersion = Application.ProductVersion;
            return currentVersion;

        }
        public static string GetLatestVersionFromServer()
        {

            string latestVersion = "";
            int k = 1;
            do
            {
                try
                {
                    k = 1;
                    WebClient web = new WebClient();
                    string xmlString = web.DownloadString(url);
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlString);
                    XmlNode versionNode = xmlDoc.SelectSingleNode("/version/latest");
                    latestVersion = versionNode.InnerText;
                }
                catch (WebException ex)

                {

                    frmMessageBoxConnect messageBox = new frmMessageBoxConnect(ex.ToString());
                    DialogResult result = messageBox.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        k = 0;
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        k = 1;
                    }

                    //string message = "This is the message to display.";
                    //string detail = "This is the detail to display.";

                    //  MessageBox.Show(message, "Message Box Title", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, detail);

                    //if (result == DialogResult.OK)
                    //{
                    //    // Perform the desired action when the user clicks the OK button.
                    //}

                    //string shortSentence = "Check Connection to update new version";
                    //string longParagraph = ex.ToString();


                    //string message = "This is the message to display.";
                    //string detail = "This is the detail to display.";


                    //if (result == DialogResult.OK)
                    //{
                    //    // Perform the desired action when the user clicks the OK button.
                    //}
                    //MessageBox.Show(ex.ToString(), "GetLatestVersionFromServer()");
                }
            }
            while (k == 0);



            return latestVersion;
        }


        public static void DownloadAndInstallerNewVersion()
        {
            //string latestVersion = "";

            try
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
                    Thread.Sleep(20000);
                    Process.Start(@".\DemoUpdate.exe");
                }



            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.ToString(), "DownloadAndInstallerNewVersion()");
            }
        }

        public static void DownloadNewVersion()
        {
            try
            {
                WebClient web = new WebClient();
                string xmlString = web.DownloadString(url);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlString);

                string downloadUrl = xmlDoc.SelectSingleNode("/version/url").InnerText;
                string fileNamelatestVersion = xmlDoc.SelectSingleNode("/version/fileName").InnerText;

                WebClient client = new WebClient();
                client.DownloadFile(downloadUrl, fileNamelatestVersion);
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.ToString(), "DownloadNewVersion");
            }
        }

        public static void ChangeName()
        {
            // MessageBox.Show("begin ChangeName");
            Process procDestruct = new Process();
            string strName = "destruct.bat";
            string strPath = Path.Combine(Directory.GetCurrentDirectory(), strName);
            string strExe = new FileInfo(Application.ExecutablePath).Name;

            StreamWriter swDestruct = new StreamWriter(strPath);

            swDestruct.WriteLine("@echo off");
            swDestruct.WriteLine("rename " + "\"" + "DemoUpdate.exe" + "\"" + " " + "\"" + "DemoUpdateOldVersion.exe" + "\"");
            swDestruct.WriteLine("start DemoUpdateOldVersion.exe");
            //swDestruct.WriteLine("runas /user:Administrator " + "\"" + "DemoUpdateOldVersion.exe" + "\"");

            //swDestruct.WriteLine("if exist \"" + strExe + "\"" + " goto Repeat");
            // swDestruct.WriteLine("del \"" + strName + "\"");
            swDestruct.Close();

            procDestruct.StartInfo.FileName = "destruct.bat";

            procDestruct.StartInfo.CreateNoWindow = true;
            procDestruct.StartInfo.UseShellExecute = false;
            //MessageBox.Show("begin try");
            try
            {
                // MessageBox.Show("before procDestruct.Start()");
                procDestruct.Start();
                // MessageBox.Show("after procDestruct.Start()");
                Application.Exit();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());

            }
            Application.Exit();

        }

        public static void InstallNewVersion(string appObjectVersion)
        {

            WebClient webClient = new WebClient();
            try
            {
                string zipPath = appObjectVersion;
                string extractPath = @".\";
                ZipFile.ExtractToDirectory(zipPath, extractPath);
                File.Delete(appObjectVersion);
                Process.Start(@".\DemoUpdate.exe");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "InstallNewVersion");
            }
        }

        public static void SelfDestruct()
        {
            //Thread.Sleep(10000);
            Process procDestruct = new Process();
            string strName = "destruct.bat";
            string strPath = Path.Combine(Directory
               .GetCurrentDirectory(), strName);
            string strExe = new FileInfo(Application.ExecutablePath).Name;

            StreamWriter swDestruct = new StreamWriter(strPath);

            swDestruct.WriteLine("attrib \"" + strExe + "\"" + " -a -s -r -h");
            swDestruct.WriteLine(":Repeat");
            swDestruct.WriteLine("del " + "\"" + strExe + "\"");
            swDestruct.WriteLine("if exist \"" + strExe + "\"" + " goto Repeat");
            swDestruct.WriteLine("del \"" + strName + "\"");
            swDestruct.Close();

            procDestruct.StartInfo.FileName = "destruct.bat";

            procDestruct.StartInfo.CreateNoWindow = true;
            procDestruct.StartInfo.UseShellExecute = false;
            procDestruct.Start();
            Application.Exit();

            //try
            //{

            //    procDestruct.Start();


            //}
            //catch (Exception)
            //{

            //    Application.Exit();

            //}
        }


        public static void DeleteFileOldVersion()
        {

            try
            {
                string exePath = Assembly.GetExecutingAssembly().Location;
                string folderPath = Path.GetDirectoryName(exePath);
                Process process1 = new Process();
                process1.StartInfo.FileName = "cmd.exe";
                process1.StartInfo.Arguments = $"/c del /s /q /f \"{folderPath}\\*.* \"";
                process1.StartInfo.UseShellExecute = false;
                process1.StartInfo.RedirectStandardOutput = true;
                process1.Start();

                process1.WaitForExit();

                Process process2 = new Process();
                process2.StartInfo.FileName = "cmd.exe";
                process2.StartInfo.Arguments = $"/c for /d %d in (\"{folderPath}\\* \") do rd /s /q \"%d\"";
                process2.StartInfo.UseShellExecute = false;
                process2.StartInfo.RedirectStandardOutput = true;
                process2.Start();

                process2.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "DeleteFileOldVersion");
            }

        }

        public static void CheckUpdate()
        {
            string latestVersion = GetLatestVersionFromServer();
            string currentVersion = GetCurrentVersion();
            string currentExecutable = Process.GetCurrentProcess().ProcessName;

            bool isAdministrator = false;
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            isAdministrator = principal.IsInRole(WindowsBuiltInRole.Administrator);

            if (currentExecutable.Equals("DemoUpdateOldVersion"))
            {
                // MessageBox.Show("DemoUpdateOldVersion");
                if (!isAdministrator)
                {
                    // MessageBox.Show(" if isAdministrator ");

                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    startInfo.Verb = "runas";
                    try
                    {
                        //MessageBox.Show(" try isAdministrator ");
                        Process.Start(startInfo);
                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                DeleteFileOldVersion();
                //VersionManager.DownloadAndInstallerNewVersion();
                Application.Run(new frmDownLoadAndInstall());
                SelfDestruct();
                Application.Exit();
            }
            else
            {

                if ((string.Compare(latestVersion, currentVersion) > 0) && !isAdministrator)
                {
                    if (MessageBox.Show("A new version is available. Would you like to download it now?", "Update Available", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (!isAdministrator)
                        {
                            //MessageBox.Show(" if isAdministrator ");

                            ProcessStartInfo startInfo = new ProcessStartInfo();
                            startInfo.FileName = System.Reflection.Assembly.GetExecutingAssembly().Location;
                            startInfo.Verb = "runas";
                            try
                            {
                                // MessageBox.Show(" try isAdministrator ");
                                Process.Start(startInfo);
                                return;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }

                    }

                    else
                    {
                        Application.Run(new Form1());
                    }
                }
                else if ((string.Compare(latestVersion, currentVersion) > 0) && isAdministrator)
                {

                    ChangeName();

                }
                else
                {
                    Application.Run(new Form1());
                }


            }


        }
    }
}
