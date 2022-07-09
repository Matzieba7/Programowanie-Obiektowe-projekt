using LibraryApp.DatabaseTables;
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

namespace LibraryApp.Views
{
    /// <summary>
    /// Interaction logic for Books.xaml
    /// </summary>
    public partial class GenresWindow : Page
    {

        public GenresWindow()
        {
            InitializeComponent();
            GenreList.ItemsSource = genreList;
            Refresh();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            using (DataContext context = new DataContext())
            {
                if (NameTextBox.Text != "")
                {
                    var name = NameTextBox.Text.Trim();

                    if (context.Genres.Any(genre => genre.Name != name))
                    {
                        if (name != null)
                        {
                            context.Genres.Add(new DatabaseTables.Genre() { Name = name });
                            context.SaveChanges();
                        }
                        Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Genre already exists!");
                    }
                }
                else
                {
                    MessageBox.Show("Genre name must be provided");
                }
            }
        }

        /// <summary>
        /// ObjectList of authors
        /// </summary>
        public List<Genre> genreList { get; set; } = new List<Genre>();

        public void Refresh()
        {
            genreList.Clear();
            using (DataContext context = new DataContext())
            {
                foreach (var item in context.Genres.ToList())
                {
                    genreList.Add(new Genre()
                    {
                        ID = item.ID,
                        Name = item.Name,
                    });
                }

            }
            GenreList.Items.Refresh();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (DataContext context = new DataContext())
            {
                Genre selectedGenre = GenreList.SelectedItem as Genre;

                if (selectedGenre != null)
                {
                    if (context.Books.Any(book => book.GenreID == selectedGenre.ID))
                    {
                        MessageBox.Show("You cannot delete one of the books' genre!");
                    }
                    else
                    {
                        Genre genre = context.Genres.Single(x => x.ID == selectedGenre.ID);

                        context.Remove(genre);
                        context.SaveChanges();
                        Refresh();                        
                    }

                }
                else
                {
                    MessageBox.Show("You must select a row!");
                }
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            using (DataContext context = new DataContext())
            {

                if (NameTextBox.Text != "")
                {
                    Genre selectedGenre = GenreList.SelectedItem as Genre;

                    var name = NameTextBox.Text.Trim();

                    if (context.Books.Any(author => author.Name != name))
                    {
                        Genre genre = context.Genres.Find(selectedGenre.ID);

                        genre.Name = name;

                        context.SaveChanges();
                        Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Genre already exists!");
                    }
                }
                else
                {
                    MessageBox.Show("Genre's name must be provided");
                }
            }
    }
}
    }
