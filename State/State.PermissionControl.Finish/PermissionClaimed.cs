using System;
using System.Collections.Generic;
using System.Text;

namespace State.PermissionControl.Finish
{
    public class PermissionClaimed : PermissionState
    {
        public PermissionClaimed() : base("CLAIMED")
        {
        }

        public override void GrantedBy(SystemAdmin admin, SystemPermission permission)
        {
            if (!permission.Admin.Equals(admin))
                return;

            if (permission.Profile.IsUnixPermissionRequired()
                     && !permission.IsUnixPermissionGranted())
            {
                permission.State = PermissionState.UNIX_REQUESTED;
                NotifyUnixAdminsOfPermissionRequest();
                return;
            }
            permission.State = PermissionState.GRANTED;
            permission.Granted = true;
            NotifyUserOfPermissionRequestResult();
        }

        public override void DeniedBy(SystemAdmin admin, SystemPermission permission)
        {
            if (!permission.Admin.Equals(admin))
                return;

            permission.Granted = false;
            permission.UnixPermissionGranted = false;
            permission.State = PermissionState.DENIED;
            NotifyUserOfPermissionRequestResult();
        }
    }
}
