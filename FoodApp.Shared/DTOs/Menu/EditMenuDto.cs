
namespace FoodApp.Shared.DTOs.Menu
{
    public class EditMenuDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public List<int> ItemIds { get; set; } = new List<int>();

    }
}
