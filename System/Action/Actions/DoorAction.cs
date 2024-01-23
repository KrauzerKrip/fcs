using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fcs.System.Event.Events
{

    public enum DoorEventType {
        Open, 
        Close
    }

    public class DoorAction (DoorEventType type) : Action
    {
        private readonly DoorEventType _type = type;


        public override string GetCommand()
        {

            string typeStr = _type.ToString().ToLowerInvariant();

            return $"door {typeStr}";
        }
    }
}
