using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;

namespace Register
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        IPAddress address = null;
        Socket clientSocket = null;
        IPEndPoint endPoint = null;
        public static string ID = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConnecToServer()
        {
            try
            {
                //创建客户端套接字
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                address = IPAddress.Parse("47.115.11.25");//127.0.0.1//192.168.1.107//47.115.11.25//172.18.194.215
                endPoint = new IPEndPoint(address, 22);
                //address = IPAddress.Parse("127.0.0.1");//127.0.0.1//192.168.1.107//47.115.11.25//172.18.194.215
                //endPoint = new IPEndPoint(address, 9000);
                //连接服务器
                clientSocket.Connect(endPoint);
            }
            catch (Exception e)
            {
                string errStr = string.Format("系统提示：出现了错误！\r\n错误信息：{0}\r\n", e.Message);
                //receiveTBX.AppendText(errStr);
                MessageBox.Show(errStr);
                return;
            }
        }

        // 获取服务器发送过来的数据
        private string recvMsg()
        {
            try
            {
                byte[] receveMsg = new byte[1024 * 1024 * 2];
                int length = clientSocket.Receive(receveMsg);
                return Encoding.UTF8.GetString(receveMsg, 0, length);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        // 描述：发送数据到服务器进行处理
        private void sendMsg(string msg)
        {
            try
            {
                clientSocket.Send(Encoding.UTF8.GetBytes(msg));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }
        private void initLogin()
        {
            string strRecvMsg = recvMsg();
            //MessageBox.Show("接收成功");
            //theAccount.Content = "接受成功！";
            string[] strArray = strRecvMsg.Split('|');
            MessageBox.Show("分解成功");
            switch (strArray[0])
            {
                case "login":
                    if (strArray[1].Equals("succeed"))//登录成功！
                    {
                        ID = txtName.Text;
                        Friendlist friendlist = new Friendlist(clientSocket, txtName.Text);
                        //string user = userTXB.Text;
                        //启动客户端与服务器的连接服务
                        new ClientService(clientSocket, friendlist.Chat);//将此socket传给chat
                        string strSendMsg = "Init|online";
                        sendMsg(strSendMsg);
                        MessageBox.Show("登录成功！");
                        friendlist.Show();
                        //friendlist.Myname.Text = txtName.Text;
                        this.Close();
                    }
                    break;
                case "warning":
                    string warningMsg = this.recvMsg();
                   // MessageBox.Show(warningMsg.Split('|')[1]);
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    clientSocket = null;
                    MessageBox.Show("登录失败！用户名或密码错误");
                    break;
            }
        }

        private void NameTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
       
       
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = txtName.Text;
            string pwd = pwdInfo.Password;
            ConnecToServer();
            string requestLoginMsg = string.Format(@"Login|{0}|{1}", username, pwd);
            sendMsg(requestLoginMsg);
            initLogin();
           
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            Register_ register = new Register_();
            register.Show();
            this.Close();
        }
    }
}
