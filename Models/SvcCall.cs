using System;

namespace Athena_REST.Models
{
    /// <summary>
    /// This class models DSI Service Calls (SvcCall or Svc).
    /// </summary>
    public class SvcCall
    {
        /// <value>Svc Id</value>
        public string Id { get; set; }
        /// <value>Svc Rowguid in DSI Database</value>
        public string Rowguid { get; set; }
        /// <value>Svc SerialNumber</value>
        public string SerialNumber { get; set; }
        /// <value>Svc ControlID</value>
        public string ControlID { get; set; }
        /// <value>Svc SvcCallID</value>
        public string SvcCallID { get; set; }
        /// <value>Svc Machine Model </value>
        public string Model { get; set; }
        /// <value>Svc Name</value>
        public string SvcName { get; set; }
        /// <value>Svc Address 1</value>
        public string SvcAddr1 { get; set; }
        /// <value>Svc Address 2</value>
        public string SvcAddr2 { get; set; }
        /// <value>Svc City</value>
        public string SvcCity { get; set; }
        /// <value>Svc State </value>
        public string SvcState { get; set; }
        /// <value>Svc Zip</value>
        public string SvcZip { get; set; }
        /// <value>Svc Contact Name</value>
        public string ServiceContact { get; set; }
        /// <value>Svc Contact Phone Number</value>
        public string ServicePhone { get; set; }
        /// <value>Svc Date & Time Received</value>
        public string DateTimeIn { get; set; }
        /// <value>Svc Problem</value>
        public string Problem { get; set; }
        /// <value>Svc Notes</value>
        public string Notes { get; set; }
        /// <value>Svc Status(INP -> In progress, RS -> Reschedule, NEW)</value>
        public string Status { get; set; }
        /// <value>Svc Contact Number</value>
        public string CallNumber { get; set; }
        /// <value>Svc was taken by</value>
        public string TakenBy { get; set; }
        /// <value>Svc Distance from user's current location</value>
        public double SvcDistance { get; set; }
        /// <value>Svc Latitude Coordinate</value>
        public double SvcLatitude { get; set; }
        /// <value>Svc Longitude Coordinate</value>
        public double SvcLongitude { get; set; }
        /// <value>Svc Full Address(Ex: 133 Herman St, Athens GA 30044)</value>
        public string SvcFullAddress { get; set; }
        /// <value>Svc Solution to Problem</value>
        public string SvcSolution { get; set; }
        /// <value>Svc Black Meter</value>
        public string SvcBWMeter { get; set; }
        /// <value>Svc Color Meter </value>
        public string SvcColorMeter { get; set; }
        /// <value>Distance when user claimed Svc</value>
        public string SvcTimeBegin { get; set; }
        /// <value>Time when Technician stopped working on Svc</value>
        public string SvcTimeEnd { get; set; }
        /// <value>Svc County</value>
        public string SvcCounty { get; set; }
        /// <value>Technician who completed Call</value>
        public string Tech_Initials { get; set; }
        /// <value>Svc Date</value>
        public string SvcDate { get; set; }
        /// <value>Svc Time</value>
        public string SvcTime { get; set; }
        /// <value>Svc </value>
        public string SvcNew_Problem { get; set; }
        public string PayType { get; set; }

        // Constructors
        public SvcCall()
        {
            Id = Guid.NewGuid().ToString();
            Notes = null;
            Model = null;
            SvcZip = null;
            Status = null;
            TakenBy = null;
            SvcName = null;
            Problem = null;
            Rowguid = null;
            SvcCity = null;
            SvcAddr1 = null;
            SvcAddr2 = null;
            SvcState = null;
            SvcCounty = null;
            SvcCallID = null;
            ControlID = null;
            CallNumber = null;
            SvcBWMeter = "0";
            SvcTimeEnd = null;
            DateTimeIn = null;
            SvcSolution = "";
            SvcTimeBegin = null;
            ServicePhone = null;
            SerialNumber = null;
            SvcColorMeter = "0";
            Tech_Initials = null;
            ServiceContact = null;
            SvcNew_Problem = "";
            PayType = "";

        }

        [Newtonsoft.Json.JsonConstructor]
        public SvcCall(string web_Rowguid, string web_SerialNumber, string web_ControlID, string web_SvcCallID, string web_Model,
            string web_SvcName, string web_SvcAddr1, string web_SvcAddr2, string web_SvcCity, string web_SvcState, string web_SvcZip,
            string web_SvcCounty, string web_ServiceContact, string web_ServicePhone, DateTime web_DateTimeIn, string web_Problem,
            string web_Notes, string web_Status, string web_CallNumber, string web_TakenBy, string web_SvcTimeBegin,
            string web_SvcTimeEnd, string web_GivenTo, string web_Tech_Initials, string web_PayType)
        {

            Id = Guid.NewGuid().ToString();
            Notes = web_Notes;
            Model = web_Model;
            SvcZip = web_SvcZip;
            Status = web_Status;
            TakenBy = web_TakenBy;
            SvcDate = web_DateTimeIn.ToString("MM/dd/yyyy");
            SvcName = web_SvcName;
            SvcName = web_SvcName;
            SvcTime = web_DateTimeIn.ToString("HH:MM");
            Problem = web_Problem;
            Rowguid = web_Rowguid;
            SvcCity = web_SvcCity;
            SvcAddr1 = web_SvcAddr1;
            SvcAddr2 = web_SvcAddr2;
            SvcState = web_SvcState;
            SvcCounty = web_SvcCounty;
            SvcCallID = web_SvcCallID;
            ControlID = web_ControlID;
            CallNumber = web_CallNumber;
            SvcBWMeter = "0";
            SvcTimeEnd = web_SvcTimeEnd;
            DateTimeIn = web_DateTimeIn.ToString();
            SvcSolution = "";
            SvcTimeBegin = web_SvcTimeBegin;
            ServicePhone = web_ServicePhone;
            SerialNumber = web_SerialNumber;
            SvcColorMeter = "0";
            Tech_Initials = web_GivenTo;
            SvcNew_Problem = "";
            ServiceContact = web_ServiceContact;
            // Convert.ToString(rdr["DateTimeIn"]).Substring(12,5);
            SvcFullAddress = web_SvcAddr1 + " " + web_SvcAddr2 + ", " + web_SvcCity + " " + web_SvcState + " " + web_SvcZip;
            PayType = web_PayType;
        }
    }
}