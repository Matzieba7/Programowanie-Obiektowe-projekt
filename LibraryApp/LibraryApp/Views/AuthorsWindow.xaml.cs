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
    public partial class AuthorsWindow : Page
    {

        public AuthorsWindow()
        {
            InitializeComponent();
            AuthorList.ItemsSource = authorList;
            Refresh();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            using (DataContext context = new DataContext())
            {
                if (FullNameTextBox.Text != "")
                {
                    var fullName = FullNameTextBox.Text.Trim();

                    if (context.Authors.Any(author => author.FullName != fullName))
                    {
                        if (fullName != null)
                        {
                            context.Authors.Add(new DatabaseTables.Author() { FullName = fullName });
                            context.SaveChanges();
                        }
                        Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Author already exists!");
                    }
                }
                else
                {
                    MessageBox.Show("Author name must be provided");
                }
            }
        }

        /// <summary>
        /// ObjectList of authors
        /// </summary>
        public List<Author> authorList { get; set; } = new List<Author>();

        public void Refresh()
        {
            authorList.Clear();
            using (DataContext context = new DataContext())
            {
                foreach (var item in context.Authors.ToList())
                {
                    authorList.Add(new Author()
                    {
                        ID = item.ID,
                        FullName = item.FullName,
                    });
                }

            }
            AuthorList.Items.Refresh();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (DataContext context = new DataContext())
            {
                Author selectedAuthor = AuthorList.SelectedItem as Author;

                if (selectedAuthor != null)
                {
                    if (context.Books.Any(book => book.AuthorID == selectedAuthor.ID))
                    {
                        MessageBox.Show("You cannot delete one of the books' author!");
                    }
                    else
                    {
                        Author author = context.Authors.Single(x => x.ID == selectedAuthor.ID);

                        context.Remove(author);
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

                if (FullNameTextBox.Text != "")
                {
                    Author selectedAuthor = AuthorList.SelectedItem as Author;

                    var fullName = FullNameTextBox.Text.Trim();

                    if (context.Authors.Any(author => author.FullName != fullName))
                    {
                        Author author = context.Authors.Find(selectedAuthor.ID);

                        author.FullName = fullName;

                        context.SaveChanges();
                        Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Author already exists!");
                    }
                }
                else
                {
                    MessageBox.Show("Author's name must be provided");
                }
            }
    }
}
    }
