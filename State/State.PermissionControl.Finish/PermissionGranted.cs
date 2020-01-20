using System;
using System.Collections.Generic;
using System.Text;

namespace State.PermissionControl.Finish
{
    public class PermissionGranted : PermissionState
    {
        public PermissionGranted() : base("GRANTED")
        {
        }
    }
}
