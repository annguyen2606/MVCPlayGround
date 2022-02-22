using System.ComponentModel.DataAnnotations;

namespace MVCPlayground.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemType Type { get; set; }
    }

    public enum ItemType
    {
        TypeOne,
        TypeTwo,
        TypeThree
    }
}
