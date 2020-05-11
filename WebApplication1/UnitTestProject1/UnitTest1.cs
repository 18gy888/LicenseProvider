using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebApplication1;

namespace UnitTestProject1
{
    [TestClass]
    public class Test
    {
        [TestMethod("д�뵽�ļ�")]
        public void Write()
        {
            UserRuntime userRuntime = new UserRuntime();
            userRuntime.WriteToFile();
        }

        [TestMethod("���ļ���ȡ")]
        public void Read()
        {
            UserRuntime userRuntime = new UserRuntime();
            userRuntime.ReadFromFile();
        }

        [TestMethod("��ȷ�û�")]
        public void AU()
        {
            UserRuntime userRuntime = new UserRuntime();
            UserInfo user;
            user.expiringTime = DateTime.Now.AddDays(1);
            user.userID = Guid.NewGuid();
            user.userName = "songrunhan";

            Assert.IsTrue(userRuntime.AuthorizeUser(user));
        }

        [TestMethod("�����û�")]
        public void AU_E1()
        {
            UserRuntime userRuntime = new UserRuntime();
            UserInfo user;
            user.expiringTime = DateTime.Now.AddDays(1);
            user.userID = Guid.NewGuid();
            user.userName = "badgay";

            Assert.IsTrue(userRuntime.AuthorizeUser(user));
        }

        [TestMethod("�����û�")]
        public void AU_E2()
        {
            UserRuntime userRuntime = new UserRuntime();
            UserInfo user;
            user.expiringTime = DateTime.Now.AddDays(1);
            user.userName = "verybadgay";
            for (int i = 0; i < 10; ++i)
            {
                user.userID = Guid.NewGuid();
                userRuntime.AuthorizeUser(user);
            }
            user.userID = Guid.NewGuid();

            Assert.IsFalse(userRuntime.AuthorizeUser(user));
        }

    }
}
