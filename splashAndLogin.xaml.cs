using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Media.Animation;

[assembly: ObfuscateAssembly(false)]
[assembly: DisablePrivateReflection]
namespace The_Encryptor
{
    /// <summary>
    /// Interaction logic for splashAndLogin.xaml
    /// </summary>

    public partial class SplashAndLogin : Window
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public SplashAndLogin()
        {
            InitializeComponent();
            Groupbox1.Opacity = 0;
        }
        private void Groupbox1_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void backgroundPic_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void SplashAndLogIn_Loaded(object sender, RoutedEventArgs e)
        {
            #region Fade in

            Storyboard storyboard = new();
            TimeSpan duration = new(0, 0, 3);


            DoubleAnimation animation = new();

            animation.From = 0.0;
            animation.BeginTime = duration;
            animation.To = 1.0;
            animation.Duration = duration;

            Storyboard.SetTargetName(animation, Groupbox1.Name);
            Storyboard.SetTargetProperty(animation, new PropertyPath(OpacityProperty));

            storyboard.Children.Add(animation);


            storyboard.Begin(this);

            #endregion
        }
        public void Cmd_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void CmdLoging_Click(object sender, RoutedEventArgs e)
        {
            KeyFileOperations keyFile = new();
            if (KeyFileOperations.GenrateFile(txtBoxUserName.Text, txtBoxPassWord.Password) == KeyFileOperations.GetKeyFileString())
            {

                // MessageBox.Show("Right login info "+Application.Current.Windows[0].Name );
                GenrateTrusted_file();
                MainWindow mainWindow = new();
                Hide();
                mainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Wrong login info");
                Close();
            }
        }
        private static void GenrateTrusted_file()
        {
            AppDomain fff = AppDomain.CurrentDomain;
            Console.WriteLine(fff.FriendlyName);
            using FileStream fileStream = new("Run.dll", FileMode.Create, FileAccess.Write);
            SHA512 sHA = SHA512.Create();
            byte[] hashed = sHA.ComputeHash(Encoding.ASCII.GetBytes(fff.FriendlyName));
            fileStream.Write(hashed, 0, hashed.Length);
        }
        private void Cmd_dump_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
