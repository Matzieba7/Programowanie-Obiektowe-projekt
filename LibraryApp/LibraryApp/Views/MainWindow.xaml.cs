using LibraryApp.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryApp
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

        private void bookButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new BooksWindow());
        }

        private void authorButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new AuthorsWindow());
        }

        private void genreButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new GenresWindow());
        }

        private void userButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new UsersWindow());

        }
    }
}
