using System;
using System.Collections.Generic;
using System.Text;

namespace BrawlhallaNet
{
    public class BrawlhallaNetException : Exception
    {
        public int StatusCode;
        public string Reason;

        public BrawlhallaNetException(int statusCode, string reason)
        {
            this.StatusCode = statusCode;
            this.Reason = reason;
        }
    }
}
