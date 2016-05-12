using System;
using System.IO;
using System.Net;

namespace MODEL_Main
{
    public class FTPConnector
    {
        #region Objects And Variables

        private WebClient FtpClient = null;
        private FtpWebRequest FtpWebRequest;
        private string Username = String.Empty;
        private string Password = String.Empty;

        #endregion Objects And Variables

        #region Constructor

        public FTPConnector()
        {
            this.FtpClient = new WebClient();
        }

        #endregion Constructor

        #region Connect

        public void Connect(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        #endregion Connect

        #region CreateDirectory

        public void CreateDirecroty(string ftpAddressPath, string directoryName)
        {
            this.FtpWebRequest = (FtpWebRequest)FtpWebRequest.Create(ftpAddressPath + directoryName);
            this.FtpWebRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
            this.FtpWebRequest.Credentials = new NetworkCredential(this.Username, this.Password);

            this.FtpWebRequest.GetResponse();
        }

        #endregion CreateDirectory

        #region RemoveDirectory

        public void RemoveDirectory(string ftpAddressPath, string directoryName)
        {
            this.FtpWebRequest = (FtpWebRequest)FtpWebRequest.Create(ftpAddressPath + directoryName);
            this.FtpWebRequest.Method = WebRequestMethods.Ftp.RemoveDirectory;
            this.FtpWebRequest.Credentials = new NetworkCredential(this.Username, this.Password);

            this.FtpWebRequest.GetResponse();
        }

        #endregion RemoveDirectory

        #region DownloadData

        public void DownloadData(string ftpAddressPath, string fileName)
        {
            try
            {
                this.FtpClient.Credentials = new NetworkCredential(this.Username, this.Password);
                byte[] FileData = FtpClient.DownloadData(ftpAddressPath + fileName);

                FileStream FileStream;
                DirectoryInfo DirInfo = new DirectoryInfo(Environment.CurrentDirectory + "\\FTP\\");
                if (!DirInfo.Exists)
                    DirInfo.Create();
                FileStream = File.Create(Environment.CurrentDirectory + "\\FTP\\" + fileName);
                FileStream.Write(FileData, 0, FileData.Length);
                FileStream.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string DownloadDataVoip(string ftpAddressPath, string fileName, string pathYear)
        {
            try
            {
                this.FtpClient.Credentials = new NetworkCredential(this.Username, this.Password);
                byte[] FileData = FtpClient.DownloadData(ftpAddressPath + fileName);
                FileStream FileStream;
                DirectoryInfo DirInfo = new DirectoryInfo(Environment.CurrentDirectory + "\\" + pathYear);
                if (!DirInfo.Exists)
                    DirInfo.Create();
                FileStream = File.Create(Environment.CurrentDirectory + "\\" + pathYear + fileName);
                FileStream.Write(FileData, 0, FileData.Length);
                FileStream.Close();
                return "yes";
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode ==
                    FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    return "not file";
                }
                else
                {
                    return "error";
                }
            }
        }

        #endregion DownloadData

        #region UploadData

        public void UploadData(string ftpAddressPath, string locationFile, string folder)
        {
            try
            {
                FtpWebRequest reqFTP = null;
                Stream ftpStream = null;

                string[] subDirs = folder.Split('/');

                foreach (string subDir in subDirs)
                {
                    try
                    {
                        ftpAddressPath = ftpAddressPath + subDir + "/";
                        reqFTP = (FtpWebRequest)FtpWebRequest.Create(ftpAddressPath);
                        reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                        reqFTP.UseBinary = true;
                        reqFTP.Credentials = new NetworkCredential(this.Username, this.Password);
                        FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                        ftpStream = response.GetResponseStream();
                        ftpStream.Close();
                        response.Close();
                    }
                    catch (WebException ex)
                    {
                        FtpWebResponse response = (FtpWebResponse)ex.Response;
                        if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                        {
                            //Does not exist
                        }
                        else
                        {
                            throw ex;
                        }
                    }
                }

                FileInfo Finfo = new FileInfo(locationFile);
                this.FtpWebRequest = (FtpWebRequest)FtpWebRequest.Create(ftpAddressPath + Finfo.Name);
                this.FtpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;
                // this.FtpWebRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                this.FtpWebRequest.Credentials = new NetworkCredential(this.Username, this.Password);

                Stream FtpStream = FtpWebRequest.GetRequestStream();
                FileStream Files = File.OpenRead(locationFile);

                int LengthBuffer = 1024;
                int ByteRead = 0;
                byte[] Buffer = new byte[LengthBuffer];

                do
                {
                    ByteRead = Files.Read(Buffer, 0, LengthBuffer);
                    FtpStream.Write(Buffer, 0, ByteRead);
                } while (ByteRead != 0);

                Files.Close();
                FtpStream.Close();
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode ==
                    FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                }
                else
                {
                    throw ex;
                }
            }
        }

        #endregion UploadData

        #region RemoveFile

        public void RemoveFile(string ftpAddressPath, string fileName)
        {
            try
            {
                this.FtpWebRequest = (FtpWebRequest)WebRequest.Create(ftpAddressPath + fileName);
                this.FtpWebRequest.Credentials = new NetworkCredential(this.Username, this.Password);
                this.FtpWebRequest.Method = WebRequestMethods.Ftp.DeleteFile;

                this.FtpWebRequest.GetResponse();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion RemoveFile

        #region 

        public bool CheckFileExist(string ftpAddressPath, string FileName)
        {
            FtpWebRequest ftpRequest = null;
            FtpWebResponse ftpResponse = null;
            bool IsExists = true;
            try
            {
                FileInfo fileInf = new FileInfo(FileName);
                //ftpRequest = (FtpWebRequest)WebRequest.Create(ftpURL + "/" + ftpDirectory + "/" + FileName);
                ftpRequest = (FtpWebRequest)WebRequest.Create(ftpAddressPath + fileInf.Name);
                ftpRequest.Credentials = new NetworkCredential(this.Username, this.Password);
                ftpRequest.Method = WebRequestMethods.Ftp.GetFileSize;
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                ftpResponse.Close();
                ftpRequest = null;
            }
            catch (Exception ex)
            {
                IsExists = false;
            }
            return IsExists;
        }

        #endregion

        #region Close

        public void Close()
        {
            this.FtpClient.Dispose();
            this.FtpClient = null;

            this.FtpWebRequest = null;
        }

        #endregion Close
    }
}
