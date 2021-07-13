using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TokenMaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static string GetMD5EncryptionString(string content, Encoding encoding)
        {
            using (MD5 md5 = MD5.Create())
            {
                StringBuilder md5Str = new StringBuilder();

                byte[] hashBytes = md5.ComputeHash(encoding.GetBytes(content));

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    md5Str.Append(hashBytes[i].ToString("X2"));
                }

                return md5Str.ToString();
            }
        }

        /// <summary>
        /// 32位MD5加密算法
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <returns>加密后的32位MD5值</returns>
        public static string GetMD5EncryptionString32(string str)
        {
            byte[] b = Encoding.Default.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
            {
                ret += b[i].ToString("x").PadLeft(2, '0');
            }
            return ret;
        }

        private void UpdateEBSTestToken(object sender, RoutedEventArgs e)
        {
            string dateTimeStr = DateTime.Now.AddMinutes(1).ToString("yyyyMMddHHmm");
            string token = GetMD5EncryptionString(dateTimeStr + ConfigurationManager.AppSettings["EBSHttpKeyTest"].ToString(), Encoding.UTF8);

            this.EBSTestTokenTextBox.Text = token;
            Clipboard.SetDataObject(token);

        }

        private void UpdateEBSToken(object sender, RoutedEventArgs e)
        {

            string dateTimeStr = DateTime.Now.AddMinutes(1).ToString("yyyyMMddHHmm");
            string token = GetMD5EncryptionString(dateTimeStr + ConfigurationManager.AppSettings["EBSHttpKey"].ToString(), Encoding.UTF8);

            this.EBSTokenTextBox.Text = token;
            Clipboard.SetDataObject(token);
        }
    }
}
