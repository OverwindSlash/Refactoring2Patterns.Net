using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace State.PermissionControl.Finish.Tests
{
    [TestClass]
    public class SystemPermissionFinishTest
    {
        [TestMethod]
        public void TestClaimedBy()
        {
            SystemUser user = new SystemUser();
            SystemProfile profile = new SystemProfile();
            SystemPermission permission = new SystemPermission(user, profile);
            Assert.AreEqual(PermissionState.REQUESTED, permission.GetState());

            SystemAdmin admin = new SystemAdmin();
            permission.ClaimedBy(admin);
            Assert.AreEqual(PermissionState.CLAIMED, permission.GetState());
            Assert.AreEqual(false, permission.IsGranted());
        }

        [TestMethod]
        public void TestClaimedByWithUnixConcerned()
        {
            SystemUser user = new SystemUser();
            SystemProfile profile = new SystemProfile();
            profile.SetUnixPermissionRequired(true);
            SystemPermission permission = new SystemPermission(user, profile);
            Assert.AreEqual(PermissionState.UNIX_REQUESTED, permission.GetState());

            SystemAdmin admin = new SystemAdmin();
            permission.ClaimedBy(admin);
            Assert.AreEqual(PermissionState.UNIX_CLAIMED, permission.GetState());
            Assert.AreEqual(false, permission.IsGranted());
        }

        [TestMethod]
        public void TestGrantedBy()
        {
            SystemUser user = new SystemUser();
            SystemProfile profile = new SystemProfile();
            SystemPermission permission = new SystemPermission(user, profile);
            Assert.AreEqual(PermissionState.REQUESTED, permission.GetState());

            SystemAdmin admin = new SystemAdmin();
            permission.ClaimedBy(admin);
            Assert.AreEqual(PermissionState.CLAIMED, permission.GetState());
            Assert.AreEqual(false, permission.IsGranted());

            permission.GrantedBy(admin);
            Assert.AreEqual(PermissionState.GRANTED, permission.GetState());
            Assert.AreEqual(true, permission.IsGranted());
        }

        [TestMethod]
        public void TestGrantedByWithUnixConcerned()
        {
            SystemUser user = new SystemUser();
            SystemProfile profile = new SystemProfile();
            profile.SetUnixPermissionRequired(true);
            SystemPermission permission = new SystemPermission(user, profile);
            Assert.AreEqual(PermissionState.UNIX_REQUESTED, permission.GetState());

            SystemAdmin admin = new SystemAdmin();
            permission.ClaimedBy(admin);
            Assert.AreEqual(PermissionState.UNIX_CLAIMED, permission.GetState());
            Assert.AreEqual(false, permission.IsGranted());

            permission.GrantedBy(admin);
            Assert.AreEqual(PermissionState.GRANTED, permission.GetState());
            Assert.AreEqual(true, permission.IsGranted());
        }

        [TestMethod]
        public void TestDeniedBy()
        {
            SystemUser user = new SystemUser();
            SystemProfile profile = new SystemProfile();
            SystemPermission permission = new SystemPermission(user, profile);
            Assert.AreEqual(PermissionState.REQUESTED, permission.GetState());

            SystemAdmin admin = new SystemAdmin();
            permission.ClaimedBy(admin);
            Assert.AreEqual(PermissionState.CLAIMED, permission.GetState());
            Assert.AreEqual(false, permission.IsGranted());

            permission.DeniedBy(admin);
            Assert.AreEqual(PermissionState.DENIED, permission.GetState());
            Assert.AreEqual(false, permission.IsGranted());
        }

        [TestMethod]
        public void TestDeniedByWithUnixConcerned()
        {
            SystemUser user = new SystemUser();
            SystemProfile profile = new SystemProfile();
            profile.SetUnixPermissionRequired(true);
            SystemPermission permission = new SystemPermission(user, profile);
            Assert.AreEqual(PermissionState.UNIX_REQUESTED, permission.GetState());

            SystemAdmin admin = new SystemAdmin();
            permission.ClaimedBy(admin);
            Assert.AreEqual(PermissionState.UNIX_CLAIMED, permission.GetState());
            Assert.AreEqual(false, permission.IsGranted());

            permission.GrantedBy(admin);
            Assert.AreEqual(PermissionState.GRANTED, permission.GetState());
            Assert.AreEqual(true, permission.IsGranted());
        }
    }
}
