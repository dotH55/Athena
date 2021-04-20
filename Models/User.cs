using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Athena_REST.Models
{
    /// <summary>
    /// This class models Application Users.
    /// </summary>
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Initials { get; set; }
        public string Location { get; set; }

        public static User LOGGED_USER;
        public static List<User> USERS = new List<User>();

        private static bool Connectivity_Status = false;
        private static bool IsCONNECTED => Connectivity.NetworkAccess == NetworkAccess.Internet;


        [Newtonsoft.Json.JsonConstructor]
        public User(string web_Tech_Init, string web_Name, string web_Location, string web_Password_Hash)
        {
            Initials = web_Tech_Init;
            Username = web_Name;
            Location = web_Location;
            Password = web_Password_Hash;
        }

        /// <summary>
        /// This method makes an API Request for all the Tech guys.
        /// </summary>
        public static async void Query_Users_REST()
        {
            HttpWebRequest request;
            HttpWebResponse response = null;

            try
            {
                if (!IsCONNECTED)
                {
                    Connectivity_Status = true;
                    return;
                }

                string URLString = SQLServer.GetURL_PREFIX() + "TechList";

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
                            USERS = JsonConvert.DeserializeObject<List<User>>(str);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.StackTrace);
                    Connectivity_Status = true;
                    return;
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

        /// <summary>
        /// This method returns List of Users
        /// </summary>
        /// <returns>Users List</returns>
        public static List<User> GetUsers()
        {
            return USERS;
        }

        /// <summary>
        /// This method returns connection status
        /// </summary>
        /// <returns>Connection Status</returns>
        public static bool GetConnectivity_Status()
        {
            return IsCONNECTED;
        }

        /// <summary>
        /// This method verifies the encrypted password
        /// </summary>
        /// <param name="user_pwd">User Password</param>
        /// /// <param name="saved_pwd">Saved Password</param>
        /// <returns>Boolean Result</returns>
        private bool ComparePassword(string user_pwd, string saved_pwd)
        {
            string HashPass = string.Empty;
            HashPass = CreatePasswordHash(user_pwd);

            int result = string.Compare(HashPass, saved_pwd, true);
            return result == 0;
        }

        /// <summary>
        /// This method creates an Hash
        /// </summary>
        /// <param name="pwd">Password</param>
        /// <returns>Hash Result</returns>
        public string CreatePasswordHash(string pwd)
        {
            //Copy from Ken
            byte[] Data = System.Text.ASCIIEncoding.ASCII.GetBytes(pwd); // System.Text.UTF8Encoding.UTF8.GetBytes(pwd);
            //SHA256Manage
            SHA512Managed sha = new SHA512Managed();
            byte[] HashValue = sha.ComputeHash(Data);
            string result = string.Empty;

            foreach (byte h in HashValue)
            {
                //Have to pad, was dropping leading Zero in hex number, ie 08 was coming back as 8
                result += string.Format("{0:X}", h).PadLeft(2, '0');
            }

            return result;
        }

        /// <summary>
        /// This method checks User's Credentials
        /// </summary>
        /// <param name="e">EventArgs</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>Task Completed!</returns>
        public static async Task<bool> CheckUserCredentials(EventArgs e, string username, string password)
        {
            //Copy from Ken
            bool valid = false;
            GetUsers().ForEach(d =>
            {

                if (d.Initials.Equals(username))
                {
                    if (d.ComparePassword(password, d.Password))
                    {
                        User.LOGGED_USER = d;
                        valid = true;
                    }
                }
            });
            return valid;
        } // CheckUserCredentials
    }  // User
}
