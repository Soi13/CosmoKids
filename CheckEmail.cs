using System;
using System.Text;
using System.Text.RegularExpressions;

namespace CosmoKids
{
    class CheckEmail
    {
        private string em;

        public CheckEmail(string s)
        {
            this.em = s;
        }

        public bool CheckEM()
        {
            //Checking of correction of email
            Regex rgx = new Regex(@"^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$", RegexOptions.IgnoreCase);
            if ((!rgx.IsMatch(em)) && (em.Length != 0))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
