using Athena_REST.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Essentials;

// Bundle Id: com.DSI.AthenaRest
// App Id: com.DSI.AthenaRest

namespace Athena_REST.Services
{
    public class MockDataStore : IDataStore<SvcCall>
    {
        // Declare Global Variables
        /// <value>Is it a Service or IT Call?</value>
        private static bool IsIT_Call;
        /// <value>Service Contact Signature</value>
        private static Stream Signature;
        /// <value>Service Contact</value>
        private static string Signee;
        /// <value>User's Device Latitue Coordonate</value>
        private static double USER_CURR_LATITUDE;
        /// <value>User's Device Longitude Coordonate</value>
        private static double USER_CURR_LONGITUDE;
        /// <value>This static variable Holds the selected Svc from the ItemPage</value>
        private static SvcCall SvcSELECTED;
        /// <value>This List holds the result of parts lookup queried from Database</value>
        private static List<Part> SvcLookup_Parts;
        /// <value>List of Parts</value>
        private static List<Part> SvcSOLUTION_PARTS;
        /// <value>This List holds all Svc queried from Database</value>
        private static List<SvcCall> SvcCALLS;

        /// <value>List of service history</value>
        private static List<JobClassModel> SvcHISTORY_LIST;
        /// <value>Internet Connectivity Boolean</value>
        public static bool IsCONNECTED => Connectivity.NetworkAccess == NetworkAccess.Internet;

        // Regular Expression for verifying phone numbers
        //private static readonly string PHONE_NUMBER_REGEX = @"^(?<countryCode>[\+][1-9]{1}[0-9]{0,2}\s)?(?<areaCode>0?[1-9]\d{0,4})(?<number>\s[1-9][\d]{5,12})(?<extension>\sx\d{0,4})?$";
        private static readonly string PHONE_NUMBER_REGEX = @"^(\d{3}\.?\d{3}\.?\d{4}?){1}";


        /// <summary>
        /// The Main <c>Work</c> Horse.
        /// This class handles most of the heavy work of Athena.
        /// The following list Contains the major function of this class
        /// <list type="bullet">
        /// <item>
        /// <term>Service Call (Svc)</term>
        /// <description>Methods that Retrieve, edit & sort Service Calls.
        /// </description>
        /// </item>
        /// <item>
        /// <term>Solution Parts</term>
        /// <description>Functions to Retrieve, Add & delete Solution Parts</description>
        /// </item>
        /// <item>
        /// <term>Debugging</term>
        /// <description>Function with the sole purpose to debug</description>
        /// </item>
        /// <item>
        /// <term>Service History</term>
        /// <description>Functions to retrieve and process Service Calls</description>
        /// </item>
        /// </list>
        /// </summary>
        public MockDataStore()
        {
            // Initialize Variables
            Initialize_Variables();

            // Query Database
            Query_Svc_REST().ConfigureAwait(true);
        } // MockDataStore

        private void Initialize_Variables()
        {
            SvcSELECTED = new SvcCall();
            SvcCALLS = new List<SvcCall>();
            SvcSOLUTION_PARTS = new List<Part>();
            SvcHISTORY_LIST = new List<JobClassModel>();
        }

        /// <summary>
        /// This method provides access to SvcCalls
        /// </summary>
        /// <returns>List of SvcCalls</returns>
        public static List<SvcCall> GetSvcCalls()
        {
            return SvcCALLS;
        }

        public async Task<bool> AddItemAsync(SvcCall item)
        {
            SvcCALLS.Add(item);

            return await Task.FromResult(true);
        }

        public static void AddSvcAsync(SvcCall item)
        {
            SvcCALLS.Add(item);
            // return await Task.FromResult(true);
        }

        /// <summary>
        /// This method Updates Svc
        /// </summary>
        /// <param name="item"> New SvcCalls</param>
        /// <returns>Successfull Completing</returns>
        public async Task<bool> UpdateItemAsync(SvcCall item)
        {
            SvcCall oldItem = SvcCALLS.Where((SvcCall arg) => arg.Id == item.Id).FirstOrDefault();
            SvcCALLS.Remove(oldItem);
            SvcCALLS.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            SvcCall oldItem = SvcCALLS.Where((SvcCall arg) => arg.Id == id).FirstOrDefault();
            SvcCALLS.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<SvcCall> GetItemAsync(string id)
        {
            return await Task.FromResult(SvcCALLS.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<SvcCall>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(SvcCALLS);
        }

        public async Task<IEnumerable<Part>> GetPartsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(SvcLookup_Parts);
        }

        public static List<Part> GetLookup_Parts()
        {
            return SvcLookup_Parts;
        }

        /// <summary>
        /// This method returns User's Current Latitude
        /// </summary>
        /// <returns>User's Current Latitude</returns>
        private static double GetCurrent_Latitude()
        {
            return USER_CURR_LATITUDE;
        }

        /// <summary>
        /// This method returns User's Current Longitute
        /// </summary>
        /// <returns>User's Current Longitude</returns>
        private static double GetCurrent_Longitute()
        {
            return USER_CURR_LONGITUDE;
        }

        /// <summary>
        /// This method sets the user current coordinates
        /// </summary>
        private static async void SetCurrent_Coordinates()
        {
            if (IsCONNECTED)
            {
                try
                {
                    Location location = await Geolocation.GetLocationAsync();
                    if (location == null)
                    {
                        USER_CURR_LATITUDE = 0;
                        USER_CURR_LONGITUDE = 0;
                    }
                    else
                    {
                        USER_CURR_LATITUDE = location.Latitude;
                        USER_CURR_LONGITUDE = location.Longitude;
                    }

                }
                catch (Exception ex)
                {

                    Debug.WriteLine(ex.StackTrace);
                    //await Application.Current.MainPage.DisplayAlert("Athena", "Error: SetCurrent_Coordinates", "Ok");
                    //await AlertUser("Error", "SetCurrent_Coordinates");
                }
            }
            else
            {
                Debug.WriteLine("Error: Internet Connectivity");
                //await Application.Current.MainPage.DisplayAlert(, "OK");
                //await Application.Current.MainPage.DisplayAlert("Athena", "Make sure you are connected to the Internet", "Ok");
                //await AlertUser("Connectivity Error", "Make sure you are connected to the Internet");
            }
        } // SetCurrent_Coordinates




        /// <summary>
        /// This method does the following:
        /// 1. Set user coordinates
        /// 2. Clear SvcCalls
        /// 3. Queries database
        /// 4. Sort SvcCalls
        /// Prints to the Debugger
        /// </summary>
        public static async Task<bool> Query_Svc_REST()
        {
            // Set user coordinates
            SetCurrent_Coordinates();
            // Queries database
            if (await Query_REST().ConfigureAwait(true))
            {
                // Sort SvcCalls
                Sort_Svc();
                return true;
            }
            else
            {
                return false;
            }
        } // Query_Svc_SQL


        /// <summary>
        /// This method Re-assigns SvcSelected
        /// </summary>
        /// <param name="svc">List of SvcCalls</param>
        public static void SetSvcSelected(SvcCall svc)
        {
            SvcSELECTED = svc;
        }

        /// <summary>
        /// This method gives access to SvcSolution_Parts
        /// </summary>
        public static List<Part> GetSvcSolution_Parts()
        {
            return SvcSOLUTION_PARTS;
        }

        /// <summary>
        /// This method retrieves a solution part
        /// </summary>
        /// /// <param name="index">index in SvcSolution_Parts</param>
        public static Part GetSvcSolution_Part(int index)
        {
            return SvcSOLUTION_PARTS[index];
        }

        /// <summary>
        /// This method adds a new part to solution part
        /// </summary>
        /// <param name="id">Part ID</param>
        /// <param name="qty">Part Qty</param>
        public static void Add_SvcSolution_Part(string id, string qty)
        {
            bool duplicate = false;
            foreach (Part x in SvcSOLUTION_PARTS)
            {
                if (x.ID.Equals(id))
                {
                    duplicate = true;
                }
            }

            if (!duplicate)
            {
                SvcSOLUTION_PARTS.Add(new Part(id, qty));
            }
        }

        /// <summary>
        /// This method removes a Svc part from solution
        /// </summary>
        /// /// <param name="index">Index in SvcSolution_Parts</param>
        public static void Delete_SvcSolution_Part(int index)
        {
            SvcSOLUTION_PARTS.RemoveAt(index);
        }

        /// <summary>
        /// This method gives access to SvcSelected
        /// </summary>
        /// <returns>Selected SvcCall in the ListView</returns>
        public static SvcCall GetSvcSelected()
        {
            return SvcSELECTED;
        }

        /// <summary>
        /// This method swaps two values in a list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="j"></param>
        /// <param name="i"></param>
        private static void Swap<T>(IList<T> list, int j, int i)
        {
            T tmp = list[j];
            list[j] = list[i];
            list[i] = tmp;
        }

        /// <summary>
        /// This method uses bubble sort to sort SvcCalls by distance
        /// from Current location
        /// </summary>
        public static void Sort_Svc()
        {
            // Sort by distance
            BubbleSort_Svc_By_Distance();
            // Sort SR by status
            // Bring Items IN_PROGRESS... to the top of the list
            // This way the user can see where each others are
            BubbleSort_Svc_By_Status();
            // Group INP
            BubbleSort_Svc_Helper();
        }

        /// <summary>
        /// This method uses bubble sort to sort SvcCalls by distance
        /// from Current location
        /// </summary>
        private static void BubbleSort_Svc_By_Distance()
        {
            int i, j;
            bool KeepIterating = true;
            while (KeepIterating)
            {
                KeepIterating = false;
                for (i = 0; i < SvcCALLS.Count - 1; i++)
                {
                    // Last i elements are already in place  
                    for (j = 0; j < SvcCALLS.Count - i - 1; j++)
                    {
                        if (SvcCALLS[j].SvcDistance > SvcCALLS[j + 1].SvcDistance)
                        {
                            Swap<SvcCall>(SvcCALLS, j, j + 1);
                            KeepIterating = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method uses bubble sort to sort SvcCalls by Status
        /// Order: INP, NEW, RS
        /// from Current location
        /// </summary>
        private static void BubbleSort_Svc_By_Status()
        {
            int i, j;
            bool KeepIterating = true;
            while (KeepIterating)
            {
                KeepIterating = false;
                for (i = 0; i < SvcCALLS.Count - 1; i++)
                {
                    // Last i elements are already in place  
                    for (j = 0; j < SvcCALLS.Count - i - 1; j++)
                    {
                        if ((SvcCALLS[j].Status.Equals("NEW") && SvcCALLS[j + 1].Status.Equals("INP"))
                            || (SvcCALLS[j].Status.Equals("RS") && SvcCALLS[j + 1].Status.Equals("NEW"))
                            || (SvcCALLS[j].Status.Equals("RS") && SvcCALLS[j + 1].Status.Equals("INP")))
                        {
                            Swap<SvcCall>(SvcCALLS, j, j + 1);
                            KeepIterating = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method uses bubble sort to sort SvcCalls by Status
        /// Order: INP, NEW, RS
        /// from Current location
        /// </summary>
        private static void BubbleSort_Svc_Helper()
        {
            for (int u = 0; u < User.GetUsers().Count; u++)
            {
                for (int s = 0; s < SvcCALLS.Count; s++)
                {
                    if (SvcCALLS[s].Status.Equals("INP") && SvcCALLS[s].Tech_Initials.Equals(User.GetUsers()[u].Initials))
                    {
                        SvcCall temp = SvcCALLS[s];
                        SvcCALLS.RemoveAt(s);
                        SvcCALLS.Insert(0, temp);
                    }
                }
            }
        }

        /// <summary>
        /// This method sorts SvcCalls by bringing up to the top
        /// all the INP the user has open.
        /// Order: INP...
        /// from Current location
        /// </summary>
        public static void Sort_Svc_By_Logged_User()
        {
            for (int x = 1; x < SvcCALLS.Count; x++)
            {
                if (SvcCALLS[x].Tech_Initials.Equals(User.LOGGED_USER.Initials))
                {
                    SvcCall temp = SvcCALLS[x];
                    SvcCALLS.RemoveAt(x);
                    SvcCALLS.Insert(0, temp);
                }
            }

        }

        /// <summary>
        /// This method sorts Svcs by DateTimeIn in a Descendant order
        /// </summary>
        public static void Sort_Svc_By_DateTimeIn()
        {
            SvcCALLS = SvcCALLS.OrderBy(x => x.DateTimeIn).ToList();
            SvcCALLS.Reverse();
            Sort_Svc_By_New();
        }

        /// <summary>
        /// This method uses bubble sort to sort SvcCalls by Date_Time_In.
        /// Order: NEW, INP, RS
        /// from Current location
        /// </summary>
        private static void Sort_Svc_By_New()
        {
            int i, j;
            bool KeepIterating = true;
            while (KeepIterating)
            {
                KeepIterating = false;
                for (i = 0; i < SvcCALLS.Count - 1; i++)
                {
                    // Last i elements are already in place  
                    for (j = 0; j < SvcCALLS.Count - i - 1; j++)
                    {
                        if ((SvcCALLS[j].Status.Equals("INP") && SvcCALLS[j + 1].Status.Equals("NEW"))
                            || (SvcCALLS[j].Status.Equals("RS") && SvcCALLS[j + 1].Status.Equals("NEW"))
                            || (SvcCALLS[j].Status.Equals("NEW") && SvcCALLS[j + 1].Status.Equals("NEW") && (DateTime.Compare(DateTime.Parse(SvcCALLS[j].DateTimeIn), DateTime.Parse(SvcCALLS[j + 1].DateTimeIn)) < 0)))
                        {
                            Swap<SvcCall>(SvcCALLS, j, j + 1);
                            KeepIterating = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method returns SvcHISTORY_LIST.
        /// </summary>
        /// <returns>Svc HISTORY_LIST</returns>
        public static List<JobClassModel> GetSvc_History()
        {
            return SvcHISTORY_LIST;
        }

        /// <summary>
        /// This method clears the List containing SvcSolution Parts.
        /// </summary>
        public static void ClearSvcSolution_Parts()
        {
            SvcSOLUTION_PARTS = new List<Part>();
        }

        /// <summary>
        /// This method is used to process parts from the Svc History function.
        /// Two database entries belong to the same Svc if they share the same 
        /// Invoice_Number & Call_Number
        /// </summary>
        /// <param name="svcs">Database Entries</param>
        /// <param name="newSvc">Database Entry</param>
        private static void Append_Svc_Parts(List<JobClassModel> svcs, JobClassModel newSvc)
        {
            bool duplicate = false;
            foreach (JobClassModel x in svcs)
            {
                if (x.Invoice_Number.Equals(newSvc.Invoice_Number) && x.Call_Number.Equals(newSvc.Call_Number))
                {
                    duplicate = true;
                    x.Solution_Parts += "\n" + newSvc.Solution_Parts;
                }
            }
            if (!duplicate)
            {
                svcs.Add(newSvc);
            }
        }

        /// <summary>
        /// This Task calculates distance for each SvcCall.
        /// It first retrieves latitude & longitude from Svc address
        /// then processes to calculating the distance from the user's
        /// current location.
        /// </summary>
        public static async Task GetSvc_Distances()
        {
            foreach (SvcCall x in SvcCALLS)
            {
                string destination = x.SvcFullAddress;
                IEnumerable<Location> destinationLocation = await Geocoding.GetLocationsAsync(destination);
                if (destinationLocation != null)
                {
                    //var sourceLocations = sourceLocation?.FirstOrDefault();
                    Location destinationLocations = destinationLocation?.FirstOrDefault();
                    Location sourceCoordinates = new Location(Services.MockDataStore.GetCurrent_Latitude(), Services.MockDataStore.GetCurrent_Longitute());
                    Location destinationCoordinates = new Location(destinationLocations.Latitude, destinationLocations.Longitude);
                    x.SvcDistance = (int)Location.CalculateDistance(destinationCoordinates, sourceCoordinates, DistanceUnits.Miles);
                }
            }
            Services.MockDataStore.Sort_Svc();
        }

        /// <summary>
        /// This method claims a SvcCall
        /// Makes an API request call to update: @pSvcCallID, @pGivenTo, @pTimeGiven, @pLocation
        /// Make sure Svc_Selected has been updated before making any API request
        /// After making a request, make sure to update the whole system 
        /// </summary>
        public static async Task<int> Claim_Svc_REST(bool claiming)
        {
            HttpWebRequest request;
            HttpWebResponse response = null;
            string URL_String;
            int result = 0;

            try
            {
                if (!IsCONNECTED)
                {
                    return -1;
                }

                if (claiming)
                {
                    URL_String = SQLServer.GetURL_PREFIX() + "UpdateDispatch?SvcCallID=" + SvcSELECTED.SvcCallID
                    + "&&GivenTo=" + User.LOGGED_USER.Initials + "&&TimeGiven=" + DateTime.Now.ToString() + "&&Location="
                    + User.LOGGED_USER.Location + "&&Status=" + SvcSELECTED.Status;
                }
                else
                {
                    URL_String = SQLServer.GetURL_PREFIX() + "UpdateDispatch?SvcCallID=" + SvcSELECTED.SvcCallID
                    + "&&GivenTo=" + "" + "&&TimeGiven=" + DateTime.Now.ToString() + "&&Location=" + User.LOGGED_USER.Location + "" + "&&Status=" + SvcSELECTED.Status;
                }

                //This reads the file
                request = (HttpWebRequest)WebRequest.Create(URL_String);
                request.Method = "GET";
                request.Timeout = 12000;
                request.Headers["APIKey"] = SQLServer.GetAPI_KEY();

                try
                {
                    HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

                    using (resp)
                    {
                        if (resp.StatusCode == HttpStatusCode.OK)
                        {
                            StreamReader rd = new StreamReader(resp.GetResponseStream());
                            // Get result
                            // -1: Fail
                            result = rd.Read();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.StackTrace);
                }
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// This method is used to save a note to a Svc.
        /// @pNotes is updated
        /// </summary>
        /// <returns>Successful Task completion</returns>
        public static async Task<int> Save_SvcNotes_REST()
        {
            HttpWebRequest request;
            HttpWebResponse response = null;
            int result = 0;

            try
            {
                if (!IsCONNECTED)
                {
                    return -1;
                }

                string URL_String = SQLServer.GetURL_PREFIX() + "UpdateSvcCallNote?Notes=" + SvcSELECTED.Notes
                    + "&&rowguid=" + SvcSELECTED.Rowguid;

                //This reads the file
                request = (HttpWebRequest)WebRequest.Create(URL_String);
                request.Method = "GET";
                request.Timeout = 12000;
                request.Headers["APIKey"] = SQLServer.GetAPI_KEY();

                try
                {
                    HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

                    using (resp)
                    {
                        if (resp.StatusCode == HttpStatusCode.OK)
                        {
                            StreamReader rd = new StreamReader(resp.GetResponseStream());
                            // Get Result
                            result = rd.Read();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.StackTrace);
                    Debug.WriteLine(ex.StackTrace);
                    Debug.WriteLine(ex.StackTrace);
                    Debug.WriteLine(ex.StackTrace);

                }
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

            return result;
        }

        /// <summary>
        /// This method retrieves issued parts from Database.
        /// Makes a REST_API Request with Svc_CallID & Tech_Initials as parameters.
        /// </summary>
        public static async Task<int> Retrieve_Issued_Parts_REST()
        {
            HttpWebRequest request;
            HttpWebResponse response = null;

            try
            {
                if (!IsCONNECTED)
                {
                    return -1;
                }
                //String URLString = SQLServer.GetURL_PREFIX() + "IssuedParts";
                string URLString = SQLServer.GetURL_PREFIX() + "PartsForCall?SvcCallID=" + SvcSELECTED.SvcCallID
                    + "&Tech_Init=" + User.LOGGED_USER.Initials;
                //String URLString = URL_Prefix + "activecalls?Location=Athens";

                //This reads the file
                request = (HttpWebRequest)WebRequest.Create(URLString);
                request.Method = "GET";
                request.Timeout = 12000;
                request.Headers["APIKey"] = SQLServer.GetAPI_KEY();

                try
                {
                    HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
                    using (resp)
                    {
                        if (resp.StatusCode == HttpStatusCode.OK)
                        {
                            StreamReader rd = new StreamReader(resp.GetResponseStream());
                            string str = rd.ReadToEnd();
                            // Get Solution parts
                            SvcSOLUTION_PARTS = JsonConvert.DeserializeObject<List<Part>>(str);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.StackTrace);
                }
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return 1;
        }

        /// <summary>
        /// This method retrieves SvcCall History.
        /// Makes a REST_API Request with Svc CallID as parameter.
        /// </summary>
        public static async Task Retrieve_Svc_History_REST()
        {
            HttpWebRequest request;
            HttpWebResponse response = null;

            try
            {
                string URLString = SQLServer.GetURL_PREFIX() + "SvcHistory?SvcCallID=" + SvcSELECTED.SvcCallID;

                //This reads the file
                request = (HttpWebRequest)WebRequest.Create(URLString);
                request.Method = "GET";
                request.Timeout = 12000;
                request.Headers["APIKey"] = SQLServer.GetAPI_KEY();

                try
                {
                    HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

                    using (resp)
                    {
                        if (resp.StatusCode == HttpStatusCode.OK)
                        {
                            StreamReader streamReader = new StreamReader(resp.GetResponseStream());
                            // Get result
                            string result = streamReader.ReadToEnd();
                            // Get a Temp JobClassModels
                            Debug.WriteLine("Result: " + result);

                            List<JobClassModel> temp_SvcHISTORY_LIST = JsonConvert.DeserializeObject<List<JobClassModel>>(result);
                            Debug.WriteLine("Result Count: " + temp_SvcHISTORY_LIST.Count);
                            // Initialize List
                            SvcHISTORY_LIST = new List<JobClassModel>();
                            // Process JobClassModels
                            //int i = 0;
                            foreach (JobClassModel x in temp_SvcHISTORY_LIST)
                            {
                                // This method places parts to appropriate Svc
                                Append_Svc_Parts(SvcHISTORY_LIST, x);
                                //Debug.WriteLine("***Item Number: " + x.Invoice_Number + " ***Call Number: " + x.Call_Number + " ***Problem: " + x.Problem + " ***Solution: " + x.Solution + " ***Black Meter: " + x.Black_Meter + " ***Color Meter" + x.Color_Meter + " ***DateTime: " + x.DateTimeCompleted + " ***Part: " + x.Solution_Parts);
                                //i++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.StackTrace);
                }
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
        }

        public static void Sign_Svc(Stream signature, string serviceContact, bool isITCall)
        {
            Signature = signature;
            Signee = serviceContact;
            IsIT_Call = isITCall;
        }

        /// <summary>
        /// This method complets a SvcCall
        /// Makes an REST_API Request Call to update: @pSvcCallID, @pLocation, @pSolution, @pDateTimeStarted
        /// @pStatus, @pCallNumber, @pDateTimeCompleted, @pBlackMeter, @pPart_Qty_List, @pNew_Problem, @pAck_Signature, @pIT_Call int
        /// </summary>
        public static async Task<int> Complet_Svc_REST()
        {
            HttpWebRequest request;
            HttpWebResponse response = null;
            int result = 0;

            try
            {
                string partList = "";

                if (SvcSOLUTION_PARTS.Count != 0)
                {
                    foreach (Part x in SvcSOLUTION_PARTS)
                    {
                        partList += x.ID + "," + x.Quantity + "|";
                    }
                    // Delete the last comment
                    partList = partList.Substring(0, partList.Length - 1);
                }

                string URLString = SQLServer.GetURL_PREFIX() + "UpdateSvcOrderDetail?SvcCallID=" + SvcSELECTED.SvcCallID + "&&Location=" + User.LOGGED_USER.Location + "&&Solution=" + RemoveSymbols(SvcSELECTED.SvcSolution)
                    + "&&TimeStarted=" + SvcSELECTED.SvcTimeBegin + "&&TimeCompleted=" + SvcSELECTED.SvcTimeEnd + "&&BW=" + SvcSELECTED.SvcBWMeter + "&&Color=" + SvcSELECTED.SvcColorMeter + "&&Status=" + SvcSELECTED.Status
                    + "&&CallNumber=" + SvcSELECTED.CallNumber + "&&Tech_Init=" + SvcSELECTED.Tech_Initials + "&&PartsList=" + partList + "&&NewProblem=;" + SvcSELECTED.SvcNew_Problem + "&&Ack_Signature=" + Signee + "&&IT_Call=";

                if (IsIT_Call)
                {
                    URLString = URLString + "1";
                }
                else
                {
                    URLString = URLString + "0";
                }

                //This reads the file
                request = (HttpWebRequest)WebRequest.Create(URLString);
                request.Method = "GET";
                request.Timeout = 12000;
                request.Headers["APIKey"] = SQLServer.GetAPI_KEY();

                try
                {
                    HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();

                    using (webResponse)
                    {
                        if (webResponse.StatusCode == HttpStatusCode.OK)
                        {
                            StreamReader streamReader = new StreamReader(webResponse.GetResponseStream());
                            // Get Result
                            // -1: Fail
                            // +1: Success
                            result = streamReader.Read();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.StackTrace);
                }
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// This function queries DSI open service calls.
        /// It uses Location as the only parameter.
        /// </summary>
        /// <returns>String without symbols</returns>
        private static async Task<bool> Query_REST()
        {
            HttpWebRequest request;
            HttpWebResponse response = null;

            try
            {
                string URLString = SQLServer.GetURL_PREFIX() + "activecalls?Location=" + User.LOGGED_USER.Location;

                //This reads the file
                request = (HttpWebRequest)WebRequest.Create(URLString);
                request.Method = "GET";
                request.Timeout = 12000;
                request.Headers["APIKey"] = SQLServer.GetAPI_KEY();

                try
                {
                    HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

                    using (resp)
                    {
                        if (resp.StatusCode == HttpStatusCode.OK)
                        {
                            StreamReader rd = new StreamReader(resp.GetResponseStream());
                            string result = rd.ReadToEnd();
                            // Get Svcs
                            SvcCALLS = JsonConvert.DeserializeObject<List<SvcCall>>(result);
                            // Calculate Distances
                            await GetSvc_Distances();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.StackTrace);
                }
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            } // final
            return await Task.FromResult(true);
        } // Query


        /// <summary>
        /// This method queries Parts from database using 
        /// Part_To_Lookup & Location as parameters
        /// </summary>
        /// <returns>Task Completed Successfully</returns>
        public static async Task<bool> Query_Parts_REST(string part_To_Lookup)
        {
            HttpWebRequest request;
            HttpWebResponse response = null;

            try
            {
                string URLString = SQLServer.GetURL_PREFIX() + "PartsLookup?ItemNumber=" + part_To_Lookup
                    + "&&Location=" + User.LOGGED_USER.Location;

                //This reads the file
                request = (HttpWebRequest)WebRequest.Create(URLString);
                request.Method = "GET";
                request.Timeout = 12000;
                request.Headers["APIKey"] = SQLServer.GetAPI_KEY();

                try
                {
                    HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

                    using (resp)
                    {
                        if (resp.StatusCode == HttpStatusCode.OK)
                        {
                            StreamReader rd = new StreamReader(resp.GetResponseStream());
                            string result = rd.ReadToEnd();
                            // Get Svcs
                            SvcLookup_Parts = new List<Part>();
                            SvcLookup_Parts = JsonConvert.DeserializeObject<List<Part>>(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.StackTrace);
                }
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            } // final
            return await Task.FromResult(true);
        } // Query_Svc_SQL

        /// <summary>
        /// This method records a CallID value to be 
        /// looked up.
        /// </summary>
        /// <param name="callID">Call ID</param>
        public static void SetSvcCallID(string callID)
        {
            SvcSELECTED.SvcCallID = callID;
        }

        /// <summary>
        /// This methods removes symbols from a string.
        /// It is used to remove symbols from an Svc solution 
        /// before completion.
        /// </summary>
        /// <param name="str">String with symbols</param>
        /// <returns>String without symbols</returns>
        private static string RemoveSymbols(string str)
        {
            str = new string((from c in str
                              where char.IsWhiteSpace(c) || char.IsLetterOrDigit(c)
                              select c
                   ).ToArray());
            return str;
        }

        public static string GetPHONE_NUMBER_REGEX()
        {
            return PHONE_NUMBER_REGEX;
        }

        /// <summary>
        /// This method updates a SvcContact & Phonr
        /// Makes an REST_API Request Call to update: @pSvcAddr1, @pSvcAddr2, @pSvcCity, @pSvcState
        /// @pSvcZip, @pServiceContact, @pServicePhone, @pRowID
        /// </summary>
        public static async Task<int> Update_Contact_Svc_REST()
        {

            HttpWebRequest request;
            HttpWebResponse response = null;
            int result = 0;

            try
            {
                string URLString = SQLServer.GetURL_PREFIX() + "UpdateContact?SvcAddr1=" + SvcSELECTED.SvcAddr1
                    + "&&SvcAddr2=" + SvcSELECTED.SvcAddr2 + "&&SvcCity=" + SvcSELECTED.SvcCity + "&&SvcState=" + SvcSELECTED.SvcState
                    + "&&SvcZip=" + SvcSELECTED.SvcZip + "&&ServiceContact=" + SvcSELECTED.ServiceContact + "&&ServicePhone="
                    + SvcSELECTED.ServicePhone + "&&RowID=" + SvcSELECTED.Rowguid;

                //This reads the file
                request = (HttpWebRequest)WebRequest.Create(URLString);
                request.Method = "GET";
                request.Timeout = 12000;
                request.Headers["APIKey"] = SQLServer.GetAPI_KEY();

                try
                {
                    HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();

                    using (webResponse)
                    {
                        if (webResponse.StatusCode == HttpStatusCode.OK)
                        {
                            StreamReader streamReader = new StreamReader(webResponse.GetResponseStream());
                            // Get Result
                            // -1: Fail
                            // +1: Success
                            result = streamReader.Read();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.StackTrace);
                }
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// This method allows users to create SvcCalls
        /// This REST_API Call has the following paramenters: @pEquipID, @pNewProblem, @pSvcCity, @pTech_Init
        /// @pLocation
        /// </summary>
        public static async Task<int> Create_Svc_REST(string controlID, string newProblem)
        {

            HttpWebRequest request = null;
            HttpWebResponse response = null;
            int result = 0;

            try
            {
                string URLString = SQLServer.GetURL_PREFIX() + "UpdateCreateSvcCall?EquipID=" + controlID
                    + "&&NewProblem=" + newProblem + "&&Tech_Init=" + User.LOGGED_USER.Initials + "&&Location=" + User.LOGGED_USER.Location;

                //This reads the file
                request = (HttpWebRequest)WebRequest.Create(URLString);
                request.Method = "GET";
                request.Timeout = 12000;
                request.Headers["APIKey"] = SQLServer.GetAPI_KEY();

                try
                {
                    HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();

                    using (webResponse)
                    {
                        if (webResponse.StatusCode == HttpStatusCode.OK)
                        {
                            StreamReader streamReader = new StreamReader(webResponse.GetResponseStream());
                            // Get Result
                            // -1: Fail
                            // +1: Success
                            result = streamReader.Read();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.StackTrace);
                }
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// This method brings all Svc in the 
        /// provided City to the top of the list
        /// from Current location
        /// </summary>
        public static void Sort_City(string city)
        {
            for (int s = 0; s < SvcCALLS.Count; s++)
            {
                if (SvcCALLS[s].SvcCity.ToUpper().Equals(city.ToUpper()))
                {
                    SvcCall temp = SvcCALLS[s];
                    SvcCALLS.RemoveAt(s);
                    SvcCALLS.Insert(0, temp);
                }
            }
        }
    } // MockDataStore
} // namespace Athena.Services