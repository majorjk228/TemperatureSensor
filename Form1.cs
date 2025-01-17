using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace TemperatureSensor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            const string api = "ca88e8c853d9c9e518c3bfe1f4ff3cae";

            string url = $"https://api.openweathermap.org/data/2.5/weather?q=Perm&appid={api}&units=metric";
            try
            {
                using (WebClient client = new WebClient())
                {
                    string response = client.DownloadString(url);

                    // ������ json
                    var json = JObject.Parse(response);
                    string temp = json["main"]["temp"].ToString();
                    label3.Text = temp;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ftpServer = "ftp://185.195.27.15:21";
            string remoteFile = @"/test.txt";
            string localFile = @"D:\dt\trr.txt";
            string userName = "ftpuser";
            string password = "Zxcvbnm12345";

            Encoding encoding = Encoding.UTF8; // Specify UTF-8 encoding

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpServer + "/" + remoteFile);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(userName, password);

            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                using (Stream responseStream = response.GetResponseStream())
                using (FileStream localFileStream = File.Create(localFile))
                using (StreamWriter localStreamWriter = new StreamWriter(localFileStream, encoding)) // Use StreamWriter for UTF-8 encoding
                {
                    byte[] buffer = new byte[4096];
                    int contentLength = (int)response.ContentLength;
                    int bytesRead = 0;
                    int totalBytesRead = 0;

                    do
                    {
                        bytesRead = responseStream.Read(buffer, 0, buffer.Length);
                        localStreamWriter.Write(encoding.GetString(buffer, 0, bytesRead)); // Decode bytes to string with UTF-8 encoding
                        totalBytesRead += bytesRead;

                        // ��������� ProgressBar
                        //int progressValue = (int)((double)totalBytesRead / contentLength * 100);
                        //progressBar1.Value = Math.Min(progressValue, 100); // Set to 100 if overflow occurs
                    } while (bytesRead != 0);
                }

                // ������� ������ StreamReader ��� ������ �����
                using (StreamReader reader = new StreamReader(localFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {                                              
                        // ��������� ������ � ����������
                        string myVariable = line;
                        label3.Text = myVariable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("������ ��� �������� �����: " + ex.Message);
            }

            
        }
    }
}
