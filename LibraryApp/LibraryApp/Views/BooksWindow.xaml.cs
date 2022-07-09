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
using LibraryApp.DatabaseTables;

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
            BooksList.ItemsSource = booksList;
            Refresh();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            using (DataContext context = new DataContext())
            {
                if (AuthorIDTextBox.Text.Trim() != "" && GenreIDTextBox.Text.Trim() != "")
                {
                    var name = NameTextBox.Text.Trim();
                    var pages = Int32.Parse(PagesTextBox.Text.Trim());
                    var authorID = Int32.Parse(AuthorIDTextBox.Text.Trim());
                    var genreID = Int32.Parse(GenreIDTextBox.Text.Trim());

                    if (context.Authors.Any(author => author.ID == authorID) && context.Genres.Any(genre => genre.ID == genreID))
                    {

                        if (name != null)
                        {
                            context.Books.Add(new DatabaseTables.Book() { Name = name, Pages = pages, AuthorID = authorID, GenreID = genreID });
                            context.SaveChanges();
                        }
                        Refresh();
                    }
                    else
                    {
                        MessageBox.Show("AuthorID or GenreID do not exist!");
                    }
                }
                else
                {
                    MessageBox.Show("AuthorID and GenreID must be provided");
                }

            }
        }

        public List<Book> booksList { get; set; } = new List<Book>();

        public void Refresh()
        {
            booksList.Clear();
            using (DataContext context = new DataContext())
            {
                foreach (var item in context.Books.ToList())
                {
                    booksList.Add(new Book()
                    {
                        ID = item.ID,
                        Name = item.Name,
                        Pages = item.Pages,
                        AuthorID = item.AuthorID,
                        GenreID = item.GenreID,
                    }); ;
                }

            }
            BooksList.Items.Refresh();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {            
            using (DataContext context = new DataContext())
            {
                Book selectedBook = BooksList.SelectedItem as Book;

                if (selectedBook != null)
                {
                    Book book = context.Books.Single(x => x.ID == selectedBook.ID);

                    context.Remove(book);
                    context.SaveChanges();
                    Refresh();

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

                if (AuthorIDTextBox.Text.Trim() != "" && GenreIDTextBox.Text.Trim() != "")
                {
                    Book selectedBook = BooksList.SelectedItem as Book;

                    var name = NameTextBox.Text.Trim();
                    var pages = Int32.Parse(PagesTextBox.Text.Trim());
                    var authorID = Int32.Parse(AuthorIDTextBox.Text);
                    var genreID = Int32.Parse(GenreIDTextBox.Text);

                    if (context.Authors.Any(author => author.ID == authorID) && context.Genres.Any(genre => genre.ID == genreID))
                    {

                        Book book = context.Books.Find(selectedBook.ID);

                        book.Name = name;
                        book.Pages = pages;
                        book.AuthorID = authorID;
                        book.GenreID = genreID;


                        context.SaveChanges();
                        Refresh();
                    }
                    else
                    {
                        MessageBox.Show("AuthorID or GenreID do not exist!");
                    }
                }
                else
                {
                    MessageBox.Show("AuthorID and GenreID must be provided");
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
