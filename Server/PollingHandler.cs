using Fcs.System.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fcs.Server
{
    public class PollingHandler(ActionQueue eventQueue)
    {
        private readonly ActionQueue _eventQueue = eventQueue;
        public string PollEvent()
        {
            try
            {
                System.Event.Action @event = _eventQueue.Dequeue();
                return @event.GetCommand();
            }
            catch (InvalidOperationException)
            {
                return "";
            }
        }
    }
}
