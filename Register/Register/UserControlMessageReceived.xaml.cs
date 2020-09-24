using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

namespace Register
{
    /// <summary>
    /// UserControlMessageReceived.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlMessageReceived : UserControl
    {
        public UserControlMessageReceived()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Socket recvFileSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //IPAddress iP = IPAddress.Parse("127.0.0.1");
                //IPEndPoint endPoint = new IPEndPoint(iP, 9000);
                IPAddress iP = IPAddress.Parse("47.115.11.25");//127.0.0.1//192.168.1.107//47.115.11.25//172.18.194.215
                IPEndPoint endPoint = new IPEndPoint(iP, 22);
                recvFileSocket.Connect(endPoint);
                Friend selectedFriend = ChatViewModel.sharedInstance.chatWindow.FriendList.SelectedItem as Friend;
                string[] textArray = textBlock_time.Text.Split('\n');
                string fileName = textArray[0].Split('：')[1];
                string fileLength = textArray[1].Split('：')[1];
                string fileMessage = "File|" + ChatViewModel.sharedInstance.chatWindow.username + "|" + ChatViewModel.sharedInstance.chatWindow.Touser + "|ok|" + fileName;
                recvFileSocket.Send(Encoding.UTF8.GetBytes(fileMessage));
                int fileBufferSize = 1024;
                using (FileStream fileWriter = new FileStream(System.IO.Path.Combine(@".\", fileName), FileMode.Create, FileAccess.Write))
                {
                    int receivedInTotal = 0, received;
                    byte[] fileBuffer = new byte[fileBufferSize];
                    while (receivedInTotal < Int32.Parse(fileLength))
                    {
                        received = recvFileSocket.Receive(fileBuffer, 0, fileBufferSize, SocketFlags.None);
                        fileWriter.Write(fileBuffer, 0, received);
                        fileWriter.Flush();
                        receivedInTotal += received;
                    }
                }
                MessageBox.Show("文件接收完毕");
            }
            catch(Exception ex)
            {
                MessageBox.Show("接受文件出错，错误信息：" + ex.Message);
            }
        }
    }
}
