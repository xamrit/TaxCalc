using System;

namespace TaxCalc.API.Extensions
{
    public static class UriBuilderExtensions
    {
        public static UriBuilder AppendParameter(this UriBuilder uri, string queryToAppend)
        {
            if (uri.Query != null && uri.Query.Length > 1)
                uri.Query = uri.Query.Substring(1) + "&" + queryToAppend;
            else
                uri.Query = queryToAppend;

            return uri;
        }
    }
}
