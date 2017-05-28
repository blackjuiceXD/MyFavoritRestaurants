using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace DLL.BE
{
    public class Food
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Describing { get; set; }
        public int Rating { get; set; }
        public string ImagePath { get; set; }

        [ForeignKey(typeof(Restaurant))]
        public int RestaurantId { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
