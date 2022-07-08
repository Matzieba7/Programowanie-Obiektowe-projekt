using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LibraryApp.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            var Username = usernameTxtBox.Text.Trim();
            var Password = passwordTxtBox.Password.Trim();

            using (DataContext context = new DataContext())
            {
                if (context.Users.Any(user => user.Username == Username && user.Password == Password))
                {
                    OpenMainWindow();
                    Close();
                }
                else
                {
                    MessageBox.Show("Wrong credentials!");
                }
            }
        }

        public void OpenMainWindow()
        {
            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}
