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
            //Regex rgx = new Regex(@"^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$", RegexOptions.IgnoreCase);
            Regex rgx = new Regex(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", RegexOptions.IgnoreCase);
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
