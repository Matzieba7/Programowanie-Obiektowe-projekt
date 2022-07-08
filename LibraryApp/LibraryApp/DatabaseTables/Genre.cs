using System.ComponentModel.DataAnnotations;

namespace LibraryApp.DatabaseTables
{
    public class Genre
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}