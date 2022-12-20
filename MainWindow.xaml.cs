using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace The_Encryptor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileInfo _seleceted_File;
        private Validator _validator = new();
        string msg;


        public MainWindow()
        {
            InitializeComponent();
            if (!GetTrusedFile())
            {
                Application.Current.Shutdown();
            }
        }


        private void Btn_Select_file_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new();
            try
            {
                if (fileDialog.ShowDialog().Value)
                {
                    _seleceted_File = _validator.Validat_File(fileDialog.FileName);
                    txtbl_filePath.Text = _seleceted_File.FullName;
                    Cmd_Start.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show($"Please select a file", "Encryptor", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                msg = _seleceted_File.Extension.ToString() == ".enc"
                    ? "Please Enter the password to decrypt the file"
                    : "Please Enter the Hint, password to Encrypt the file";
            }
            catch (Exception)
            {
                MessageBox.Show($"The file {fileDialog.FileName} {Environment.NewLine} doesn't exist or is currently being used by another program!", "Error Accessing the file", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Cmd_Start_Click(object sender, RoutedEventArgs e)
        {
            PassWordandHints passwordandhints = new PassWordandHints(new Carrier(_seleceted_File, "", msg));
            passwordandhints.ShowDialog();
        }
        private void Cmd_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Btn_Select_file_MouseEnter(object sender, MouseEventArgs e)
        {
            lbl_Status.Content = " Click to select a file to process";
        }

        private void Cmd_Exit_MouseEnter(object sender, MouseEventArgs e)
        {
            lbl_Status.Content = " Click to Exit";
        }

        private void Cmd_Start_MouseEnter(object sender, MouseEventArgs e)
            => lbl_Status.Content = " Click to start processing the selected file ";

        private void Btn_Select_file_MouseLeave(object sender, MouseEventArgs e)
            => lbl_Status.Content = "";

        private void Cmd_Exit_MouseLeave(object sender, MouseEventArgs e) => lbl_Status.Content = "";

        private void Txtbl_filePath_MouseEnter(object sender, MouseEventArgs e)
            => lbl_Status.Content = "Path of the selected file";

        private void Txtbl_filePath_MouseLeave(object sender, MouseEventArgs e)
            => lbl_Status.Content = "";

        internal void Enable_ProgBar()
        {
            lbl_Status.Content = "Working";
            ProgBar_Bar1.IsIndeterminate = true;
            ProgBar_Bar1.Visibility = Visibility.Visible;
        }

        internal void Disable_ProgBar()
        {
            lbl_Status.Content = "";
            ProgBar_Bar1.Visibility = Visibility.Hidden;

        }
        private bool GetTrusedFile()
        {
            bool result = false;
            FileInfo fileInfo = new("Run.dll");
            if (fileInfo.Exists)
            {
                SHA512 sHA = SHA512.Create();
                using FileStream fileStream = new FileStream("Run.dll", FileMode.Open, FileAccess.Read);
                byte[] value = new byte[fileInfo.Length];
                fileStream.Read(value, 0, (int)fileInfo.Length);
                if (sHA.ComputeHash(Encoding.ASCII.GetBytes(AppDomain.CurrentDomain.FriendlyName)).SequenceEqual(value))
                {
                    result = true;
                }
            }
            return result;
        }

        private void MainWindowContainer_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using FileStream fileStream = new FileStream("Run.dll", FileMode.Create, FileAccess.Write);
            SHA512 sHA = SHA512.Create();
            byte[] hashed = sHA.ComputeHash(Encoding.ASCII.GetBytes("FriendlyName"));
            fileStream.Write(hashed, 0, hashed.Length);
        }

        private void Cmd_Start_MouseLeave(object sender, MouseEventArgs e) => lbl_Status.Content = "";

    }
}
