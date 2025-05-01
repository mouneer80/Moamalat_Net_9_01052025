using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moamalat.Entities
{
    public class Operation
    {
        public PhoneData ExtractPhoneData(string PhoneNumber)
        {
            PhoneData pResult = new PhoneData();
            int len = PhoneNumber.Length;

            if (len == 8)
            {
                pResult.CountryCode = 968;
                pResult.PhoneNumber = PhoneNumber;

            }
            else
            if (len > 8)
            {
                pResult.CountryCode = Convert.ToInt32(PhoneNumber.Substring(0, 3));
                pResult.PhoneNumber = PhoneNumber.Substring(3, PhoneNumber.Length - 3);


            }
            else
            {
                pResult.PhoneNumber = "";

            }

            if (pResult.PhoneNumber.StartsWith("9") || pResult.PhoneNumber.StartsWith("7"))
                pResult.PhoneType = 1;
            else
                pResult.PhoneType = 2;

            return pResult;
        }

        public int? DateToNumber(DateTime? pDate)
        {
            int? result = null;
            if (pDate != null)
                result = Convert.ToInt32(((DateTime)pDate).ToString("yyyyMMdd"));
            if (result == 19000101)
                result = null;
            return result;

        }
        public decimal? DateTimeToNumber(DateTime? pDate)
        {
            decimal? result = null;
            if (pDate != null)
                result = Convert.ToDecimal(((DateTime)pDate).ToString("yyyyMMddHHmm"));

            if (result == 190001011200)
                result = null;

            return result;
        }

        public DateTime NumberToDate(string Datestr)
        {
            DateTime ResultDate = new DateTime();
            // ResultDate.Subtract(DateTime.now);

            if (Datestr.Length == 8)
                ResultDate = DateTime.ParseExact(Datestr, "yyyyMMdd", CultureInfo.InvariantCulture);

            if (Datestr.Length == 12)
                ResultDate = DateTime.ParseExact(Datestr, "yyyyMMddHHmm", CultureInfo.InvariantCulture);



            return ResultDate;
        }
    }
}
