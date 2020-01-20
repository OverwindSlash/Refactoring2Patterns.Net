namespace State.PermissionControl.Start
{
    // 在 SystemPermission 类加入了越来越多的真实世界的行为后，其状态改变逻辑将变得晦涩复杂
    // 比如，用户在被授予访问指定软件系统的通用权限之前，必须获得 UNIX 权限

    // 虽然可以试着通过 Extract Method 简化 grantedBy() 等函数，但是这些函数的判断逻辑还是过于复杂
    // 这可以通过 state 模式简化
    public class SystemPermission
    {
        private readonly SystemProfile _profile;
        private readonly SystemUser _requestor;
        private readonly SystemAdmin _admin;
        private bool _granted;
        // UNIX 权限相关的变量
        private bool _unixPermissionGranted;
        private string _state;

        public static readonly string REQUESTED = "REQUESTED";
        public static readonly string CLAIMED = "CLAIMED";
        public static readonly string GRANTED = "GRANTED";
        public static readonly string DENIED = "DENIED";
        // UNIX 权限相关的状态值
        public static readonly string UNIX_REQUESTED = "UNIX_REQUESTED";
        public static readonly string UNIX_CLAIMED = "UNIX_CLAIMED";

        public SystemPermission(SystemUser requestor, SystemProfile profile)
        {
            _admin = new SystemAdmin();
            _requestor = requestor;
            _profile = profile;
            if (profile.IsUnixPermissionRequired())
            {
                _state = UNIX_REQUESTED;
            }
            else
            {
                _state = REQUESTED;
            }
            _granted = false;
            notifyAdminOfPermissionRequest();
        }

        private void notifyAdminOfPermissionRequest()
        {
            // notify code goes here.
        }

        public void ClaimedBy(SystemAdmin admin)
        {
            #region original process logic
            //if (!state.Equals(REQUESTED))
            //    return;
            //willBeHandledBy(admin);
            //state = CLAIMED; 
            #endregion

            // 添加 UNIX 权限相关逻辑
            if (!_state.Equals(REQUESTED) && !_state.Equals(UNIX_REQUESTED))
                return;
            WillBeHandledBy(admin);
            if (_state.Equals(REQUESTED))
                _state = CLAIMED;
            else if (_state.Equals(UNIX_REQUESTED))
                _state = UNIX_CLAIMED;

        }

        private void WillBeHandledBy(SystemAdmin systemAdmin)
        {
            // handle code goes here.
        }

        public void DeniedBy(SystemAdmin admin)
        {
            #region original process logic
            //if (!state.Equals(CLAIMED))
            //    return;
            //if (!this.admin.Equals(admin))
            //    return;
            //granted = false;
            //state = DENIED;
            //notifyUserOfPermissionRequestResult(); 
            #endregion

            // 添加 UNIX 权限相关逻辑
            if (!_state.Equals(CLAIMED) && !_state.Equals(UNIX_CLAIMED))
                return;
            if (!this._admin.Equals(admin))
                return;
            _granted = false;
            _unixPermissionGranted = false;
            _state = DENIED;
            NotifyUserOfPermissionRequestResult();
        }

        private void NotifyUserOfPermissionRequestResult()
        {
            // notify code goes here.
        }

        public void GrantedBy(SystemAdmin admin)
        {
            #region original process logic
            //if (!state.Equals(CLAIMED))
            //    return;
            //if (!this.admin.Equals(admin))
            //    return;
            //state = GRANTED;
            //granted = true;
            //notifyUserOfPermissionRequestResult(); 
            #endregion

            // 添加 UNIX 权限相关逻辑
            if (!_state.Equals(CLAIMED) && !_state.Equals(UNIX_CLAIMED))
                return;
            if (!_admin.Equals(admin))
                return;

            if (_profile.IsUnixPermissionRequired() && _state.Equals(UNIX_CLAIMED))
                _unixPermissionGranted = true;
            else if (_profile.IsUnixPermissionRequired() && !IsUnixPermissionGranted())
            {
                _state = UNIX_REQUESTED;
                NotifyUnixAdminsOfPermissionRequest();
                return;
            }
            _state = GRANTED;
            _granted = true;
            NotifyUserOfPermissionRequestResult();

        }

        private void NotifyUnixAdminsOfPermissionRequest()
        {
            // notify code goes here.
        }

        public string GetState()
        {
            return _state;
        }

        public bool IsGranted()
        {
            return _granted;
        }

        // UNIX 权限相关的变量的 get 函数
        private bool IsUnixPermissionGranted()
        {
            return _unixPermissionGranted;
        }
    }
}
