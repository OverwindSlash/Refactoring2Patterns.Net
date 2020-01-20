using System;
using System.Collections.Generic;
using System.Text;

namespace State.PermissionControl.Finish
{
    public class PermissionRequested : PermissionState
    {
        public PermissionRequested() : base("REQUESTED")
        {
        }

        public override void ClaimedBy(SystemAdmin admin, SystemPermission permission)
        {
            WillBeHandledBy(admin);

            permission.State = PermissionState.CLAIMED;
        }
    }
}
