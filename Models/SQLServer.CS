namespace Athena_REST.Models
{
    /// <summary>
    /// This class holds Environmental Variables.
    /// </summary>
    internal class SQLServer
    {
        private const string IT_INTERN_EMAIL = "";
        private const string IT_INTERN_PW = "";

        // API WEB
        private const string API_KEY = "";
        private const string URL_PREFIX = "";

        public static string GetIT_INTERN_EMAIL()
        {
            return IT_INTERN_EMAIL;
        }

        public static string GetIT_INTERN_PW()
        {
            return IT_INTERN_PW;
        }

        public static string GetAPI_KEY()
        {
            return API_KEY;
        }

        public static string GetURL_PREFIX()
        {
            return URL_PREFIX;
        }
    }
}
