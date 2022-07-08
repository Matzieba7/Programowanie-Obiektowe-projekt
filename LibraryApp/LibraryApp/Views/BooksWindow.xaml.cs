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
using System.Text.RegularExpressions;

namespace LibraryApp.Views
{
    /// <summary>
    /// Interaction logic for Books.xaml
    /// </summary>
    public partial class BooksWindow : Page
    {
        public BooksWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            using (DataContext context = new DataContext())
            {
                var name = NameTextBox.Text.Trim();
                var pages = Int32.Parse(PagesTextBox.Text.Trim());
                var authorID = Int32.Parse(AuthorIDTextBox.Text);
                var genreID = Int32.Parse(GenreIDTextBox.Text);

                if (context.Authors.Any(author => author.ID == authorID) && context.Genres.Any(genre => genre.ID == genreID))
                {

                    if (name != null)
                    {
                        context.Books.Add(new DatabaseTables.Book() { Name = name, Pages = pages, AuthorID = authorID, GenreID = genreID });
                        context.SaveChanges();
                    }
                }

            }
        }


        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
