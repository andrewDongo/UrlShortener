using System;
using System.Collections.Generic;

namespace UrlShortening.Domain.Model
{
    /// <summary>
    /// Response object for execution of an operation.
    /// Boolean value indicates if operation suceeded or failed.
    /// Errors contains list of operation specific error messages.
    /// Model contains result set of operation.
    /// </summary>
    public class Response
    {
        public Response()
        {
            Succeeded = true;
            Errors = new List<string>();
        }

        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }
        public Object Model { get; set; }
    }
}