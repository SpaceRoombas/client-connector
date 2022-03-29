using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConnector.messages
{
    class GameMap : MappedMessage
    {
        GameMap()
        {
            this.MessageType = this.GetType();
        }
    }
}
