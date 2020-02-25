using System;

namespace CosmoKids
{
    abstract class Client
    {
        private const string provider_name = "COSMOKIDS ACADEMY LLC";
        private const string provider_address = "1984 Bernice Way, San Jose, CA, 95124";
        private const string provider_phone = "+1 (408) 667 2985";
        private const string provider_email = "cosmokids.academy@gmail.com";
        private string client_surname;
        private string client_name;
        private string client_date_birth;
        private int res;

        public static string Provider_name
        {
            get { return provider_name; }
        }

        public static string Provider_address
        {
            get { return provider_address; }
        }

        public static string Provider_phone
        {
            get { return provider_phone; }
        }

        public static string Provider_email
        {
            get { return provider_email; }
        }

        public string Client_surname
        {
            get { return client_surname; }
            set { client_surname = value; }
        }

        public string Client_name
        {
            get { return client_name; }
            set { client_name = value; }
        }

        public string Client_date_birth
        {
            get { return client_date_birth; }
            set { client_date_birth = value; }
        }

        //Method of calculating age of client
        public string Get_age(string dt_brth)
        {
            DateTime today = DateTime.Now.Date;
            DateTime dt_birth = DateTime.Parse(dt_brth);
            res = (today - dt_birth).Days / 365;
            return res.ToString();
        }

    }
}
