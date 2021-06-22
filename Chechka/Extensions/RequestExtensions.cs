using System;
using Microsoft.AspNetCore.Http;

namespace Chechka.Extensions
{
    //Lb7.4.5{
    public static class RequestExtensions
    {
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            return request

            .Headers["x-requested-with"]
            .Equals("XMLHttpRequest");

        }
    }
    //Lb7.4.5}
}
