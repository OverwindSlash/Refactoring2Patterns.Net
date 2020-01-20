using System;
using System.Collections.Generic;
using System.Text;

namespace State.PermissionControl.Finish
{
    public class PermissionState
    {
        private string _state;

        //public static PermissionState REQUESTED = new PermissionState("REQUESTED");
        //public static PermissionState CLAIMED = new PermissionState("CLAIMED");
        //public static PermissionState GRANTED = new PermissionState("GRANTED");
        //public static PermissionState DENIED = new PermissionState("DENIED");
        //public static PermissionState UNIX_REQUESTED = new PermissionState("UNIX_REQUESTED");
        //public static PermissionState UNIX_CLAIMED = new PermissionState("UNIX_CLAIMED");

        public static PermissionState REQUESTED = new PermissionRequested();
        public static PermissionState CLAIMED = new PermissionClaimed();
        public static PermissionState GRANTED = new PermissionGranted();
        public static PermissionState DENIED = new PermissionDenied();
        public static PermissionState UNIX_REQUESTED = new UnixPermissionRequested();
        public static PermissionState UNIX_CLAIMED = new UnixPermissionClaimed();

        public PermissionState(string state)
        {
            _state = state;
        }

        public virtual void ClaimedBy(SystemAdmin admin, SystemPermission permission)
        {
            // do nothing.
        }

        protected void WillBeHandledBy(SystemAdmin systemAdmin)
        {
            // handle code goes here.
        }

        public virtual void GrantedBy(SystemAdmin admin, SystemPermission permission)
        {
            // do nothing.
        }

        protected void NotifyUnixAdminsOfPermissionRequest()
        {
            // notify code goes here.
        }

        protected void NotifyUserOfPermissionRequestResult()
        {
            // notify code goes here.
        }

        public virtual void DeniedBy(SystemAdmin admin, SystemPermission permission)
        {
            // do nothing.
        }
    }
}
