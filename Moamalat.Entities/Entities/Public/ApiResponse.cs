using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moamalat.Entities
{
    public class ApiResponse
    {
        public string? DataObject { get; set; }
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
        public string? Source { get; set; }
        public string? ExceptionType { get; set; }
        public bool isConnected { get; set; } = true;

    }
    public class ApiRequest
    {
        public string? Content { get; set; }
    }
	 public class PaginationResult<T>
		{
			public IEnumerable<T>? Items { get; set; }=Enumerable.Empty<T>();
        public int PageSize { get; set; }
			public int PageNumber { get; set; }
			public int StartIndex { get; set; }
			public int EndIndex { get; set; }
			public int RecordsCount { get; set; }
			public string? WhereClause { get; set; }
			public string? Search { get; set; }
			public string? SortingList { get; set; }
            public string? FilterExpression { get; set; }


    }
}
