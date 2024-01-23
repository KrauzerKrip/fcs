using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fcs.System.Event
{

    public class ActionQueue
    {
        private Queue<Action> _queue = new Queue<Action>();

        public void Enequeue(Action action)
        {
            _queue.Enqueue(action);
        }

        public Action Dequeue(){
            return _queue.Dequeue();
        }
    }
}
