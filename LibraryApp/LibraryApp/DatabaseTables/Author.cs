using System.ComponentModel.DataAnnotations;

namespace LibraryApp.DatabaseTables
{
    public class Author
    {
        [Key]
        public int ID { get; set; }
        public string FullName { get; set; }
    }
}