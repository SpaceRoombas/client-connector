using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConnector.messages
{
    public class InternalErrorMessage
    {
        public string message;

        public InternalErrorMessage(string message)
        {
            this.message = message;
        }
    }
}
