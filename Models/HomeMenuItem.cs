namespace Athena_REST.Models
{
    public enum MenuItemType
    {
        Browse,
        Feedback,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
        public string Icon { get; set; }
    }
}
