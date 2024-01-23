using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fcs.System.Event.Events
{
    internal class CameraCheckAction() : Action
    {
        public override string GetCommand()
        {
            return "camera_check";
        }
    }
}
