using System;
using System.Collections.Generic;
using System.Text;

namespace State.PermissionControl.Finish
{
    public class UnixPermissionRequested : PermissionState
    {
        public UnixPermissionRequested() : base("UNIX_REQUESTED")
        {
        }

        public override void ClaimedBy(SystemAdmin admin, SystemPermission permission)
        {
            WillBeHandledBy(admin);

            permission.State = PermissionState.UNIX_CLAIMED;
        }
    }
}
