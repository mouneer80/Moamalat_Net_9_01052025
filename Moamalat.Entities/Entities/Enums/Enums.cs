using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moamalat.Entities
{
    public enum DataTableType
    {
       MINISTRY,
       BUDGET,
       BANK,
       BANK_BRANCH,
       REGION,
       WILAYAT,

    }

    public enum SelectionType
    {
        NONE,
        SINGLE,
        MULTIPLE
    }

    public enum SearchCategory
    {
        SEARCH_CR,
        SEARCH_BENEFICIARY,
        SEARCH_CIVILID
    }
	
	public enum RunningPlatform
     {
      ANDROID,
      IOS,
      WINDOWS,
      MACCATALYST,
      WEB

      }
}
