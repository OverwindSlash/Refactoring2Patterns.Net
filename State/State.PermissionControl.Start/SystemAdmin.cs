using System;
using System.Collections.Generic;
using System.Text;

namespace State.PermissionControl.Start
{
    public class SystemAdmin
    {
        public override bool Equals(object obj)
        {
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
