
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moamalat.Shared
{
    public class PageHistoryState 
    {
        private Stack<string> previousPages { get; set; }
       // private Stack<string> previousPages;
        private Stack<string> nextPages { get; set; }
        //private readonly string errorPageUrl;
        public PageHistoryState()
        {
            previousPages=new Stack<string>();
            nextPages = new();
        }
        public void AddPageToHistory(string pageName)
        {
            previousPages.Push(pageName);
        }
        public string GetBackPage()
        {
            if (previousPages.TryPeek(out string url) && !string.IsNullOrWhiteSpace(url))
            {
                // If moved to the next page check
                if (previousPages.Count > 1)
                {
                    // Pop the current page
                    nextPages.Push(previousPages.Pop());
                    // Pop the previous page -> "/"
                    url = previousPages.Pop();
                    return url;
                }
            }

            // If stack is empty redirect to the error page
            return "empty";
        }
        public string GetGoForwardPage()
        {
            if (nextPages.TryPop(out string url) && !string.IsNullOrWhiteSpace(url))
                return url;

            // If stack is empty redirect to the error page
            return "/profile";
        }

        public bool CanGoForward() => nextPages.Any();
        public bool CanGoBack() => previousPages.Count > 1;

    }
}
