using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConnector.messages
{
    public interface ICarrierPigeon
    {
        string GetPayloadType();
        string GetMessageType();

        void SetPayloadObject(object payload);
    }
}
