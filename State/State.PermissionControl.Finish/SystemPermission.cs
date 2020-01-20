namespace State.PermissionControl.Finish
{
    // 在 SystemPermission 类加入了越来越多的真实世界的行为后，其状态改变逻辑将变得晦涩复杂
    // 比如，用户在被授予访问指定软件系统的通用权限之前，必须获得 UNIX 权限

    // 虽然可以试着通过 Extract Method 简化 grantedBy() 等函数，但是这些函数的判断逻辑还是过于复杂
    // 这可以通过 state 模式简化
    public class SystemPermission
    {
        private SystemProfile _profile;
        private readonly SystemUser _requestor;
        private SystemAdmin _admin;
        private bool _granted;
        // UNIX 权限相关的变量
        private bool _unixPermissionGranted;
        private PermissionState _state;

        //public static readonly string REQUESTED = "REQUESTED";
        //public static readonly string CLAIMED = "CLAIMED";
        //public static readonly string GRANTED = "GRANTED";
        //public static readonly string DENIED = "DENIED";
        //// UNIX 权限相关的状态值
        //public static readonly string UNIX_REQUESTED = "UNIX_REQUESTED";
        //public static readonly string UNIX_CLAIMED = "UNIX_CLAIMED";

        public SystemPermission(SystemUser requestor, SystemProfile profile)
        {
            Admin = new SystemAdmin();
            _requestor = requestor;
            Profile = profile;
            if (profile.IsUnixPermissionRequired())
            {
                State = PermissionState.UNIX_REQUESTED;
            }
            else
            {
                State = PermissionState.REQUESTED;
            }
            Granted = false;
            notifyAdminOfPermissionRequest();
        }

        public PermissionState State
        {
            get => _state;
            set => _state = value;
        }

        public SystemAdmin Admin
        {
            get => _admin;
            set => _admin = value;
        }

        public SystemProfile Profile
        {
            get => _profile;
            set => _profile = value;
        }

        public bool UnixPermissionGranted
        {
            get => _unixPermissionGranted;
            set => _unixPermissionGranted = value;
        }

        public bool Granted
        {
            get => _granted;
            set => _granted = value;
        }

        private void notifyAdminOfPermissionRequest()
        {
            // notify code goes here.
        }

        public void ClaimedBy(SystemAdmin admin)
        {
            _state.ClaimedBy(admin, this);
        }

        /*private void WillBeHandledBy(SystemAdmin systemAdmin)
        {
            // handle code goes here.
        }*/

        public void GrantedBy(SystemAdmin admin)
        {
            _state.GrantedBy(admin, this);
        }

        /*private void NotifyUnixAdminsOfPermissionRequest()
        {
            // notify code goes here.
        }

        private void NotifyUserOfPermissionRequestResult()
        {
            // notify code goes here.
        }*/

        public void DeniedBy(SystemAdmin admin)
        {
            _state.DeniedBy(admin, this);
        }
        
        public PermissionState GetState()
        {
            return _state;
        }

        public bool IsGranted()
        {
            return Granted;
        }

        // UNIX 权限相关的变量的 get 函数
        internal bool IsUnixPermissionGranted()
        {
            return UnixPermissionGranted;
        }
    }
}
