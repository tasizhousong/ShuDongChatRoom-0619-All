using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Register
{
    public class ClientService
    {
        private Socket socket;
        private Thread talkThread;
        private Chat clientForm = null;
        private Friendlist friendlist = null;
        public ClientService(Socket socket, Chat clientForm)
        {
            this.socket = socket;
            this.clientForm = clientForm;
            talkThread = new Thread(watchMsg);
            talkThread.IsBackground = true;
            talkThread.Start();
        }
        public ClientService(Socket socket, Friendlist friendlist)
        {
            this.socket = socket;
            this.friendlist = friendlist;
            talkThread = new Thread(watchMsg);
            talkThread.IsBackground = true;
            talkThread.Start();
        }

        private void watchMsg()
        {
            try
            {
                while (true)
                {
                    byte[] recvMsgByte = new byte[1024 * 1024 * 2];
                    int length = socket.Receive(recvMsgByte);
                    if (length > 0)
                    {
                        string strRecvMsg = Encoding.UTF8.GetString(recvMsgByte, 0, length);
                        handleMsg(strRecvMsg);
                    }
                }
            }
            catch (Exception e)
            {
                //appendMsg("错误：" + e.Message + "\n");
                System.Windows.MessageBox.Show("watchMsg错误：" + e.Message);
                return;
            }
        }
        private void handleMsg(string msg)
        {
            string recvMsgStr = msg.ToString();
            string[] recvArray = recvMsgStr.Split('|');

            
            switch (recvArray[0])
            {
                case "Alias":
                    changeAlias(recvArray[1]);
                    break;
                case "talk":
                    //发送给聊天
                    //System.Windows.MessageBox.Show("收到了talk信息");
                    string[] receiveP = recvArray[1].Split('：');
                    if (receiveP[1].Contains("*"))
                    {
                        string[] receivePandM = receiveP[1].Split('*');
                        if (receivePandM[0] == "picture" || receivePandM[0] == "emoji")
                            displayPicture(receiveP[0], receivePandM[1]);
                        else
                            appendMsg(recvArray[1]);
                    }
                    else
                        appendMsg(recvArray[1]);
                    break;
                case "online":
                    addToComboBox(recvMsgStr);
                    break;
                case "Close":
                    clientLeave(recvArray[1]);
                    break;
                case "warning":
                    //appendMsg("错误提示：" + recvArray[1] + "\n");
                    System.Windows.MessageBox.Show("服务器发来：" + msg);
                    break;
            }
        }

        private void changeAlias(string alias)
        {
            this.clientForm.alterAlias(alias);
        }
        private void clientLeave(string who)
        {
            //  this.clientForm.removeFromToList(who);
        }
        #region ----------------------------调用委托向界面中添加消息
        private void addToComboBox(string str)
        {
            //   this.clientForm.addItemToList(str);
        }
        private void appendMsg(string msg)
        {
            //   this.clientForm.DisplayMsg(msg);
            this.clientForm.displayMsg(msg);
        }
        private void displayPicture(string receivePerson, string pictureName)
        {
            //System.Windows.MessageBox.Show("来到了ClientService.displayPicture");
            this.clientForm.displayP(receivePerson, pictureName);
        }
        #endregion
    }
}

