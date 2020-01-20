using System;
using System.Collections.Generic;
using System.Text;

namespace State.PermissionControl.Finish
{
    public class PermissionDenied : PermissionState
    {
        public PermissionDenied() : base("DENIED")
        {
        }
    }
}
