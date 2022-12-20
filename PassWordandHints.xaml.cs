using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace The_Encryptor
{
    /// <summary>
    /// Interaction logic for PassWordandHints.xaml
    /// </summary>
    public partial class PassWordandHints : Window
    {
        public string Hint_String { get; set; }
        public string Password_String { get; set; }




        public string lbl_title_text { get; set; }
        public FileInfo FileName { get; set; }
        private Validator validator = new Validator();
        public readonly bool Encrypting;
        Carrier carrier;
        TheProcess process;



        public PassWordandHints(Carrier _carrier)
        {
            carrier = _carrier;
            DataContext = this;
            InitializeComponent();

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (!carrier.Encrypting)
            {
                process = new TheProcess();
                SHA512 sHA = SHA512.Create();
                var HashedPassandhint = process.Get_HashedPassword_and_hint(process.ReadByteFromtheFile(carrier));
                txt_hint.Text = Encoding.ASCII.GetString(HashedPassandhint[1]);
                carrier.HashedPassword = HashedPassandhint[0];
            }


            SetTextBoxText();

        }



        private async void Cmd_OK_Click(object sender, RoutedEventArgs e)
        {


            txt_hint.GetBindingExpression(TextBox.TextProperty).UpdateSource();






            if (!carrier.Encrypting)
            {
                //                  <<   DECREPTING   
                SHA512 sHA = SHA512.Create();
                byte[] hashedpassword = sHA.ComputeHash(Encoding.ASCII.GetBytes(Pbox_password.Password));
                if (carrier.HashedPassword.SequenceEqual(hashedpassword))
                {
                    var _form = Application.Current.Windows[0] as MainWindow;
                    _form.Enable_ProgBar();
                    this.Hide();
                    await process.StartDec(carrier);
                    _form.Disable_ProgBar();
                    MessageBox.Show($"Decryption {carrier.FileInfo.Name} process is done successfully!", "Encryptor", MessageBoxButton.OK, MessageBoxImage.Information);

                    Disable_Start_Buton();
                }
                else
                {
                    MessageBox.Show($"The Password you enter is not correct.", "Encryptor", MessageBoxButton.OK, MessageBoxImage.Hand);
                    Disable_Start_Buton();
                }
                Close();
            }
            else
            {

                //                  <<    ENCRYPTING
                if (!validator.Validat_Hint(txt_hint.Text))
                {
                    MessageBox.Show("The Hint text can't be less then 8 characters length !!!!! ");
                    return;
                }
                if (!validator.Validat_PassWord(Pbox_password.Password))
                {
                    MessageBox.Show("The Password can't be less then 8 characters length !!!!! ");
                    return;
                }
                var _form = Application.Current.Windows[0] as MainWindow;

                _form.Enable_ProgBar();
                this.Hide();


                var process2 = new TheProcess(Pbox_password.Password.ToString(), txt_hint.Text);
                await process2.StartEnc(carrier);
                _form.Disable_ProgBar();
                MessageBox.Show($"Encrypting {carrier.FileInfo.Name} process is done successfully!", "Encryptor", MessageBoxButton.OK, MessageBoxImage.Information);
                carrier.FileInfo.Delete();
                Close();
            }
            Disable_Start_Buton();
        }
        private static void Disable_Start_Buton()
        {
            var mainwindows = Application.Current.Windows[0] as MainWindow;
            mainwindows.Cmd_Start.IsEnabled = false;
        }
        //   private void Start_Encrypting(Carrier carrier)
        //   {
        // process = new TheProcess(Pbox_password.Password.ToString(), txt_hint.Text);
        // process.StartEnc(carrier);
        ///   }
        private void SetTextBoxText()
        {
            Tb_Title.Inlines.Add(new Run($"{ carrier.Msg } "));
            Tb_Title.Inlines.Add(Environment.NewLine);
            Tb_Title.Inlines.Add(new Run($"{carrier.FileInfo.FullName}") { Foreground = Brushes.White });
        }

        private void Cmd_Cancel_Click(object sender, RoutedEventArgs e)
        {
            var mainwindows = Application.Current.Windows[0] as MainWindow;
            //mainwindows.Cmd_Start.IsEnabled = false;
            this.Close();
        }
    }
}
