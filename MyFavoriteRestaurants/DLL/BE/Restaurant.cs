using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace DLL.BE
{
    public class Restaurant
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Describing { get; set; }
        public string Webside { get; set; }
        public string ImagePath { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Food> Foods { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
