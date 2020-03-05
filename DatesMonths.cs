using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoKids
{
    class DatesMonths
    {       
        public int GetCountMonth(DateTime cur_date)
        {           
            DateTime end_year_month = new DateTime(DateTime.Now.Year, 12, 31);
            int res_count_month = ((end_year_month.Year - cur_date.Year) * 12) + end_year_month.Month - cur_date.Month;

            return res_count_month;
        }
    }
}
