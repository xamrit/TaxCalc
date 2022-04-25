using System;

namespace TaxCalc.API.Extensions
{
    /// <summary>
    /// <see cref="UriBuilder"/> extensions.
    /// </summary>
    public static class UriBuilderExtensions
    {
        /// <summary>
        /// Extension for making URI parameter building easy.
        /// </summary>
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
