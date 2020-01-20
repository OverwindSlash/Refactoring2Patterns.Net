using System;
using System.Collections.Generic;
using System.Text;

namespace State.PermissionControl.Start
{
    public class SystemProfile
    {
        private bool _unixPermissionRequired = false;
        public bool IsUnixPermissionRequired()
        {
            return _unixPermissionRequired;
        }

        public void SetUnixPermissionRequired(bool unixPermissionRequired)
        {
            this._unixPermissionRequired = unixPermissionRequired;
        }
    }
}
