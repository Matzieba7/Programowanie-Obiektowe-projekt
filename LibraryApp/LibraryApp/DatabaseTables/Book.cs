using System.ComponentModel.DataAnnotations;

namespace LibraryApp.DatabaseTables
{
    public class Book
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Pages { get; set; }
        public  int AuthorID { get; set; }
        public int GenreID { get; set; }
    }
}