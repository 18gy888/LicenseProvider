using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace 计网项目三客户端
{
    class Client
    {
        private static IPEndPoint ip2 = new IPEndPoint(IPAddress.Parse("192.168.0.100"), 8910);
        private static IPEndPoint ip3 = new IPEndPoint(IPAddress.Parse("192.168.0.100"), 8888);
        private byte[] msg;

        public Client()
        {
            msg = new byte[1024];
        }

        public void SendSerialNumber(UdpClient client)
        {
            msg = Encoding.UTF8.GetBytes("4186387651");
            client.Send(msg, msg.Length, ip2);
        }

        public string ReceiveAndSendToken(UdpClient client)
        {
            msg = client.Receive(ref ip2);
            string s1 = Encoding.UTF8.GetString(msg);
            msg = Encoding.UTF8.GetBytes("1." + Encoding.UTF8.GetString(msg));
            client.Send(msg, msg.Length, ip3);
            return s1;
        }

        public void ReceiveResult(UdpClient client)
        {
            msg = client.Receive(ref ip3);
            //弹窗显示结果
            if (Encoding.UTF8.GetString(msg)=="true")
            {
                MessageBox.Show("授权成功！", "授权提示");
            }
            else
            {
                MessageBox.Show("授权失败！", "授权提示");
            }
        }
    }
}
