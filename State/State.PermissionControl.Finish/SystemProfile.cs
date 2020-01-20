namespace State.PermissionControl.Finish
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
