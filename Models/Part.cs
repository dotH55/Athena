
namespace Athena_REST.Models
{
    /// <summary>
    /// This class is used multiple times throught out the 
    /// project with different constructors. It is used for:
    /// Retrieving Parts Issued (string ItemNumber, string Qty)
    /// Adding New Parts        (string ItemNumber, string Qty)
    /// SvcCall History         (string ID, string Quantity, string Description)
    /// Parts Lookup.           (string web_ItemNumber, string web_Qty, string web_Description, string web_Retail, string web_Area, string web_Bin, string web_Tech, string web_Location)
    /// </summary>

    public class Part
    {
        public string ID { get; set; }
        public string Bin { get; set; }
        public string Tech { get; set; }
        public string Area { get; set; }
        public string Retail { get; set; }
        public string Quantity { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Tech_Initials { get; set; }

        [Newtonsoft.Json.JsonConstructor]
        public Part(string web_ItemNumber, string web_Qty, string web_Description,
            string web_Retail, string web_Area, string web_Bin, string web_Tech, string web_Location)
        {
            ID = web_ItemNumber;
            Bin = web_Bin;
            Tech = web_Tech;
            Area = web_Area;
            Retail = web_Retail;
            Location = web_Location;
            Quantity = web_Qty;
            Description = web_Description;
        }

        public Part(string ItemNumber, string Qty)
        {
            ID = ItemNumber;
            Quantity = Qty;
        }

        public Part(string ID, string Quantity, string Description)
        {
            this.ID = ID;
            this.Quantity = Quantity;
            this.Description = Description;
        }
    }
}
