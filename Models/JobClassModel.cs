
namespace Athena_REST.Models
{
    /// <summary>
    /// This class is used to process Svc History.
    /// </summary>
    public class JobClassModel
    {
        public string Invoice_Number { get; set; }
        public string Call_Number { get; set; }
        public string Problem { get; set; }
        public string Solution { get; set; }
        public string Black_Meter { get; set; }
        public string Color_Meter { get; set; }
        public string DateTimeCompleted { get; set; }
        public string Solution_Parts { get; set; }

        public JobClassModel() { }

        [Newtonsoft.Json.JsonConstructor]
        public JobClassModel(string web_InvoiceNumber, string web_CallNumber, string web_Problem, string web_Solution,
            string web_BlackMeter, string web_ColorMeter, string web_DateTimeCompleted, string web_Solution_Parts)
        {
            Invoice_Number = web_InvoiceNumber;
            Call_Number = web_CallNumber;
            Problem = web_Problem;
            Solution = web_Solution;
            Black_Meter = web_BlackMeter;
            Color_Meter = web_ColorMeter;
            DateTimeCompleted = web_DateTimeCompleted;
            Solution_Parts = web_Solution_Parts;
        }
    }
}
