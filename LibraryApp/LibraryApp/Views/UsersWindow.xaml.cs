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
    /// public partial class UsersWindow : Page
    public partial class UsersWindow : Page
    {
        public UsersWindow()
    {
        InitializeComponent();
        UsersList.ItemsSource = userList;
        Refresh();
    }

    private void CreateButton_Click(object sender, RoutedEventArgs e)
    {
        using (DataContext context = new DataContext())
        {
            if (UsernameTextBox.Text != "" && PasswordTextBox.Text != "" && EmailTextBox.Text != "")
            {
                var username = UsernameTextBox.Text.Trim();
                var password = PasswordTextBox.Text.Trim();
                var email = EmailTextBox.Text.Trim();

                if (context.Users.Any(user => user.Username != username))
                {
                    if (username != null && username != null && username != null)
                    {
                        context.Users.Add(new DatabaseTables.User() { Username = username, Password = password, Email = email });
                        context.SaveChanges();
                    }
                    Refresh();
                }
                else
                {
                    MessageBox.Show("One of the existing users already contains that username!");
                }
            }
            else
            {
                MessageBox.Show("Every field must be filled");
            }
        }
    }

    /// <summary>
    /// ObjectList of authors
    /// </summary>
    public List<User> userList { get; set; } = new List<User>();

    public void Refresh()
    {
        userList.Clear();
        using (DataContext context = new DataContext())
        {
            foreach (var item in context.Users.ToList())
            {
                userList.Add(new User()
                {
                    ID = item.ID,
                    Username = item.Username,
                    Password = item.Password,
                    Email = item.Email,
                });
            }

        }
        UsersList.Items.Refresh();
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        using (DataContext context = new DataContext())
        {
            User selectedUser = UsersList.SelectedItem as User;

            if (selectedUser != null)
            {
                if (context.Users.Any(user => user.ID == selectedUser.ID))
                {
                    User user = context.Users.Single(x => x.ID == selectedUser.ID);

                    context.Remove(user);
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

            if (UsernameTextBox.Text != "" && PasswordTextBox.Text != "" && EmailTextBox.Text != "")
            {
                User selectedUser = UsersList.SelectedItem as User;

                var username = UsernameTextBox.Text.Trim();
                var password = PasswordTextBox.Text.Trim();
                var email = EmailTextBox.Text.Trim();

                if (context.Users.Any(user => user.Username != username))
                {
                    User user = context.Users.Find(selectedUser.ID);

                    user.Username = username;
                    user.Password = password;
                    user.Email = email;

                    context.SaveChanges();
                    Refresh();
                }
                else
                {
                    MessageBox.Show("User already exists!");
                }
            }
            else
            {
                MessageBox.Show("Every field must be filled");
            }
        }
    }
}
      }
