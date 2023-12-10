using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Extensions;

public static class UriBuilderExtensions
{
    public static UriBuilder AddQueryParameters(this UriBuilder uriBuilder, NameValueCollection queryParams)
    {
        if (queryParams == null)
        {
            throw new ArgumentNullException(nameof(queryParams));
        }

        NameValueCollection existingParams = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

        foreach (string key in queryParams.AllKeys)
        {
            existingParams[key] = queryParams[key];
        }
        uriBuilder.Query = existingParams.ToString();

        return uriBuilder;
    }

    public static UriBuilder AddQueryParameter(this UriBuilder uriBuilder, string key, string value)
    {
        NameValueCollection queryParams = new NameValueCollection
        {
            { key, value }
        };

        return AddQueryParameters(uriBuilder, queryParams);
    }
}
