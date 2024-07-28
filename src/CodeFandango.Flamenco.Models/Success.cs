using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFandango.Flamenco.Models
{
    public class Success
    {

        public Success(bool successful)
        {
            Successful = successful;
        }

        public Success(bool successful, string? statusMessage)
        {
            Successful = successful;
            StatusMessage = statusMessage;
        }

        public Success(bool successful, string? statusMessage, int httpStatusCode)
        {
            Successful = successful;
            StatusMessage = statusMessage;
            HttpStatusCode = httpStatusCode;
        }

        public string? StatusMessage { get; set; }
        public bool Successful { get; set; } 
        public int HttpStatusCode { get; set; }
    }

    public class Success<T> : Success
    {
        public Success(bool successful) : base(successful)
        {
        }

        public Success(bool successful, string? statusMessage) : base(successful, statusMessage)
        {
        }

        public Success(bool successful, string? statusMessage, int httpStatusCode) : base(successful, statusMessage, httpStatusCode)
        {
        }

        public Success(T model): base(true)
        {
            Model = model;
        }

        public Success(bool successful, T model) : base(successful)
        {
            Model = model;
        }

        public Success(bool successful, string? statusMessage, T model) : base(successful, statusMessage)
        {
            Model = model;
        }

        public Success(bool successful, string? statusMessage, int httpStatusCode, T model) : base(successful, statusMessage, httpStatusCode)
        {
            Model = model;
        }

        public T? Model { get; set; }

    }

}
