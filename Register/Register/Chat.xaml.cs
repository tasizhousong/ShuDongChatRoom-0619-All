﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class Chat : Window
    {
        IPAddress address = null;
        Socket clientSocket = null;
        IPEndPoint endPoint = null;
        ChatViewModel chatViewModel;

        delegate void ShowMsg(string msg);
        private ShowMsg showMsg;

        delegate void Display(string fromP, string pictureName);
        Display display;

        delegate void DisplayEmoji(string emojiName);
        DisplayEmoji displayEmoji;

        public string sentMessage = "你好，今天天气不错";
        public string username;
        public string Touser { get; set; }
        public Socket ClientSocket { set => clientSocket = value; }

        public Chat(Friend fd, string usr)
        {
            InitializeComponent();
            chatViewModel = ChatViewModel.sharedInstance;
            chatViewModel.chatWindow = this;
            this.DataContext = chatViewModel;
            username = usr;
            //this.FriendList.SelectedItem = null;
            this.FriendList.SelectedItem = chatViewModel.Friends[0];
            this.FriendList.SelectedItem = chatViewModel.Friends[1];
            this.FriendList.SelectedItem = fd;
            showMsg = new ShowMsg(AddMsg);
            display = new Display(receiveEmoji);
            displayEmoji = new DisplayEmoji(SendEmoji);
            //sentMessage = "你好呀，今天天气不错";
            

            //// grid_talk.Background = "";

            //string username = usr;
            //string pwd = "123";
            //// if (!checkInput(username, pwd)) return;
            //connecToServer();
            //string requestLoginMsg = string.Format(@"Login|{0}|{1}", username, pwd);
            //try
            //{
            //    sendMsg(requestLoginMsg);
            //    //初始化登录
            //    initLogin();
            //}
            //catch (Exception ex)
            //{
            //    showDialog("登录失败");
            //    return;
            //}

            //FriendList.SelectedItem = "";

            //StackPanel stp = new StackPanel();
            //stp.Name = "Stack_wangwu";
            //grid_talk.Children.Add(new StackPanel());
        }

        /* private void connecToServer()
         {
             try
             {
                 //创建客户端套接字
                 clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                 address = IPAddress.Parse("47.115.11.25");//127.0.0.1//192.168.1.107//10.242.69.240//172.18.194.215
                 //address = IPAddress.Parse("127.0.0.1");
                 endPoint = new IPEndPoint(address, 22);
                 //endPoint = new IPEndPoint(address, 9000);
                 //连接服务器
                 clientSocket.Connect(endPoint);
             }
             catch (Exception e)
             {
                 string errStr = string.Format("系统提示：出现了错误！\r\n错误信息：{0}\r\n", e.Message);
                 textBox2.AppendText(errStr);
                 return;
             }
         }*/

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
                showDialog("Error:" + ex.Message);
            }
        }

        //private void initLogin()//有问题
        //{
        //    string strRecvMsg = recvMsg();
        //    //theAccount.Content = "接受成功！";
        //    string[] strArray = strRecvMsg.Split('|');
        //    switch (strArray[0])
        //    {
        //        case "login":
        //            if (strArray[1].Equals("succeed"))//登录成功！
        //            {
        //                //string user = userTXB.Text;
        //                //启动客户端与服务器的连接服务
        //                new ClientService(clientSocket, this);
        //                string strSendMsg = "Init|online";
        //                sendMsg(strSendMsg);
        //                showDialog("登录成功！");
        //                //theAcc.Text = user;
        //            }
        //            break;
        //        case "warning":
        //            string warningMsg = this.recvMsg();
        //            showDialog(warningMsg.Split('|')[1]);
        //            clientSocket.Shutdown(SocketShutdown.Both);
        //            clientSocket.Close();
        //            clientSocket = null;
        //            showDialog("登录失败");
        //            break;
        //    }
        //}

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            //string msg = "Close|" + nickName.Text;
            //sendMsg(msg);
            // Environment.Exit(0);
        }

        private void Button_Click_Send(object sender, RoutedEventArgs e)
        {
            // this.sentMessageUserControl.textBox2.Text = this.textBox1.Text;
            string msg = this.textBox1.Text;
            UserControlMessageSent newUserControlMessageSent = new UserControlMessageSent();
            newUserControlMessageSent.textBlock_time.Text = DateTime.Now.ToString();

            SolidColorBrush colorBrush;
            if (chatViewModel.anonymity == true)
            {
                switch (chatViewModel.AnonymousTalkColorIndicator)
                {
                    case 0: colorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#EEEE00")); break;
                    case 1: colorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#FFA500")); break;
                    case 2: colorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#00FF00")); break;
                    case 3: colorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#600060")); break;
                    case 4: colorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#FF80CB")); break;
                    case 5: colorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#1C93EC")); break;
                    default: colorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#808080")); break;
                }
            }
            else
                colorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#808080"));
            newUserControlMessageSent.border_cloud.Background = colorBrush;

            foreach (ScrollViewer item in grid_talkStackPanels.Children)
            {
                if (item.Name == Touser)
                {
                    StackPanel stp = item.Content as StackPanel;
                    stp.Children.Add(newUserControlMessageSent);
                    //item.Children.Add(newUserControlMessageSent);
                    break;
                }
            }
            // this.albery.Children.Add(newUserControlMessageSent);
            newUserControlMessageSent.textBlock_msg.Text = msg;
            this.textBox1.Text = null;
            newUserControlMessageSent.textBlock_msg.TextWrapping = TextWrapping.Wrap;
            // newUserControlMessageSent.textBox2.Foreground = Brushes.White;
            double leftMargin;
            double talkWidth = talkArea.Width.Value;
            double length = msg.Length + 4;
            if (length * 12.86 < (5 / 7.0) * talkWidth)
            {
                leftMargin = talkWidth - length * 12.86;
                //        MessageBox.Show("调整margin");
            }
            else
            {
                //         MessageBox.Show("调整margin2");
                if (msg.Length < 200)
                {
                    leftMargin = 2 / 7.0 * talkWidth;
                    //             MessageBox.Show("调整margin3");
                }
                else
                {
                    //            MessageBox.Show("调整margin4");
                    leftMargin = 0;
                }
            }
            //newUserControlMessageReceived.Margin = new Thickness(0, 0, rightMargin, 0);
            newUserControlMessageSent.HorizontalAlignment = HorizontalAlignment.Right;
            newUserControlMessageSent.border_cloud.Width = talkWidth - leftMargin - 15;
            //newUserControlMessageSent.Margin = new Thickness(250, 0, 0, 0);

            sendMessage(msg);

        }

        void sendMessage(string msg)
        {

            try
            {
                //连接服务器成功发送消息
                //string logininUser = username;
                if (string.IsNullOrEmpty(username))
                {
                    showDialog("您还没登录");
                    return;
                }
                if (string.IsNullOrEmpty(msg))
                {
                    showDialog("发送内容不能为空");
                    return;
                }
                if (string.IsNullOrEmpty(Touser))
                {
                    showDialog("请选择发送对象！");
                    return;
                }

                string sendStrMsg;
                if (Touser != "All")
                    sendStrMsg = "Talk|" + username + "|" + Touser + "|" + chatViewModel.TalkColorIndicator.ToString() + "：" + msg;
                else
                {
                    if (chatViewModel.anonymity == false)
                        sendStrMsg = "Talk|" + username + "|" + Touser + "|" + "All：" + "0：" + chatViewModel.TalkColorIndicator.ToString() + "：" + msg;
                    else
                        sendStrMsg = "Talk|" + chatViewModel.Alias + "|" + Touser + "|" + "All：" + "1：" + chatViewModel.AnonymousTalkColorIndicator.ToString() + "：" + msg;
                }
                sendMsg(sendStrMsg);
                //sendTBX.Text = "";
                //receiveTBX.AppendText("你对" + toUser + "说:" + msg + "\n");
            }
            catch (Exception ex)
            {
                //出现异常，如：没有连接到服务器
                string errorMsg = string.Format(@"发送失败：{0}", ex.Message);
                showDialog(errorMsg + "\r\n");
            }
        }

        public void displayMsg(string msg)
        {

            try
            {
                if (System.Threading.Thread.CurrentThread != this.Dispatcher.Thread)
                {
                    this.Dispatcher.Invoke(showMsg, msg);
                }
            }
            catch (Exception e) { }

        }

        private void AddMsg(string msg)
        {

            string[] msgArray = msg.Split('：');
            msg = msgArray[2];
            if (msgArray[0] != username && msgArray[0] != chatViewModel.Alias)
            {
                UserControlMessageReceived newUserControlMessageReceived = new UserControlMessageReceived();
                if (msgArray[1] != "All")
                {
                    //this.albery.Children.Add(newUserControlMessageReceived);
                    newUserControlMessageReceived.column_0.Width = new GridLength(0);
                    newUserControlMessageReceived.textBlock_time.Text = DateTime.Now.ToString();
                    try
                    {
                        switch (Int32.Parse(msgArray[1]))
                        {
                            case 0: newUserControlMessageReceived.border_cloud.Background = new SolidColorBrush(ColorTranslater.ColorFromHex("#EEEE00")); ; break;
                            case 1: newUserControlMessageReceived.border_cloud.Background = new SolidColorBrush(ColorTranslater.ColorFromHex("#FFA500")); ; break;
                            case 2: newUserControlMessageReceived.border_cloud.Background = new SolidColorBrush(ColorTranslater.ColorFromHex("#00FF00")); ; break;
                            case 3: newUserControlMessageReceived.border_cloud.Background = new SolidColorBrush(ColorTranslater.ColorFromHex("#600060")); ; break;
                            case 4: newUserControlMessageReceived.border_cloud.Background = new SolidColorBrush(ColorTranslater.ColorFromHex("#FF80CB")); ; break;
                            case 5: newUserControlMessageReceived.border_cloud.Background = new SolidColorBrush(ColorTranslater.ColorFromHex("#1C93EC")); ; break;
                            default: newUserControlMessageReceived.border_cloud.Background = new SolidColorBrush(ColorTranslater.ColorFromHex("#1C93EC")); ; break;
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }

                    foreach (ScrollViewer item in grid_talkStackPanels.Children)
                    {
                        if (item.Name == msgArray[0].Trim())
                        {
                            StackPanel stp = item.Content as StackPanel;
                            stp.Children.Add(newUserControlMessageReceived);
                            //item.Children.Add(newUserControlMessageReceived);
                            break;
                        }
                    }
                    foreach (Friend item in chatViewModel.Friends)
                    {
                        if (item.Nickname == msgArray[0].Trim())
                        {
                            item.LastMessage = msg;
                            if (item != FriendList.SelectedItem)
                            {
                                item.NumNewMessages = (Int32.Parse(item.NumNewMessages) + 1).ToString();
                                item.SpotVisibility = Visibility.Visible;
                            }
                            break;
                        }
                    }

                }
                else
                {
                    msg = msgArray[4];
                    newUserControlMessageReceived.column_0.Width = new GridLength(45);
                    SolidColorBrush colorBrush;
                    try
                    {
                        switch (Int32.Parse(msgArray[3]))
                        {
                            case 0: colorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#EEEE00")); break;
                            case 1: colorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#FFA500")); break;
                            case 2: colorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#00FF00")); break;
                            case 3: colorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#600060")); break;
                            case 4: colorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#FF80CB")); break;
                            case 5: colorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#1C93EC")); break;
                            default: colorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#1C93EC")); break;
                        }
                        newUserControlMessageReceived.border_cloud.Background = colorBrush;
                        newUserControlMessageReceived.border_head.Background = colorBrush;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    if (msgArray[2].Trim() == "0")
                        foreach (Friend item in chatViewModel.Friends)
                        {
                            if (item.Nickname == msgArray[0].Trim())
                            {
                                newUserControlMessageReceived.imageBrush_head.ImageSource = item.Head;
                                break;
                            }
                        }
                    else
                        newUserControlMessageReceived.imageBrush_head.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/anonymity.png"));
                    foreach (Friend item in chatViewModel.Friends)
                    {
                        if (item.Nickname == "All")
                        {
                            item.LastMessage = msgArray[0].Trim() + "：" + msg;
                            if (item != FriendList.SelectedItem)
                            {
                                item.NumNewMessages = (Int32.Parse(item.NumNewMessages) + 1).ToString();
                                item.SpotVisibility = Visibility.Visible;
                            }
                            break;
                        }
                    }
                    StackPanel stp = All.Content as StackPanel;
                    stp.Children.Add(newUserControlMessageReceived);
                    //All.Children.Add(newUserControlMessageReceived);
                    newUserControlMessageReceived.textBlock_time.Text = DateTime.Now.ToString() + "  " + msgArray[0].Trim();
                }

                if (msg.Contains("$") && msg.Split('$')[0] == "file")
                {
                    newUserControlMessageReceived.textBlock_msg.Background = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/FIle.png"))
                    };
                    newUserControlMessageReceived.HorizontalAlignment = HorizontalAlignment.Left;
                    newUserControlMessageReceived.border_cloud.Width = 80;
                    newUserControlMessageReceived.border_cloud.Height = 80;
                    newUserControlMessageReceived.gridRow_buttons.Height = new GridLength(20);
                    newUserControlMessageReceived.textBlock_time.Text = "文件名：" + msg.Split('$')[1] + "\n" 
                        + "比特数：" + msg.Split('$')[2] + "\n" + DateTime.Now.ToString();
                }

                else
                {
                    newUserControlMessageReceived.textBlock_msg.Text = msg;
                    newUserControlMessageReceived.textBlock_msg.TextWrapping = TextWrapping.Wrap;
                    // newUserControlMessageSent.textBox2.Foreground = Brushes.White;
                    //newUserControlMessageReceived.Margin = new Thickness(0, 0, 250, 0);
                    double rightMargin;
                    double talkWidth = talkArea.Width.Value;
                    double length = msg.Length + 4;
                    if (length * 12.86 < (5 / 7.0) * talkWidth)
                    {
                        rightMargin = talkWidth - length * 12.86;
                        //            MessageBox.Show("调整margin");
                    }
                    else
                    {
                        //            MessageBox.Show("调整margin2");
                        if (msg.Length < 200)
                        {
                            rightMargin = 2 / 7.0 * talkWidth;
                            //               MessageBox.Show("调整margin3");
                        }
                        else
                        {
                            //               MessageBox.Show("调整margin4");
                            rightMargin = 0;
                        }
                    }
                    //newUserControlMessageReceived.Margin = new Thickness(0, 0, rightMargin, 0);
                    newUserControlMessageReceived.HorizontalAlignment = HorizontalAlignment.Left;
                    newUserControlMessageReceived.border_cloud.Width = talkWidth - rightMargin - 15;
                }
            }

        }

        //显示弹出框
        private void showDialog(string msg)
        {
            MessageBox.Show(msg);
        }

        public void chooseVisibleStackPanel(string name)
        {
            foreach (ScrollViewer item in grid_talkStackPanels.Children)
            {
                if (item.Name == name) item.Visibility = Visibility.Visible;
                else item.Visibility = Visibility.Hidden;
            }
        }

        public void alterAlias(string alias)
        {
            chatViewModel.Alias = alias;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //chatViewModel.Friends[2].Nickname = "静香_改名";
        }


        //private void Button_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    popUp_emoji.IsOpen = false;
        //    popUp_emoji.IsOpen = true;
        //}




        private void Button_Click_Anonymity(object sender, RoutedEventArgs e)
        {
            try
            {
                chatViewModel.anonymity = !chatViewModel.anonymity;
                if (chatViewModel.anonymity == true)
                {
                    chatViewModel.BackgroundColorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#2D2D30"));
                    chatViewModel.ButtonBackgroundColorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#2D2D30"));
                    chatViewModel.TextEditorColorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#9E9E9E"));
                    imageBrush_upperLeftHead.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/anonymity.png"));
                    foreach (Friend item in chatViewModel.Friends)
                    {
                        item.FontColorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#FFFFFFFF"));
                    }
                    if (chatViewModel.AnonymousTalkColorIndicator == null)
                    {
                        Random rand = new Random();
                        int rnum = rand.Next(5);
                        chatViewModel.AnonymousTalkColorIndicator = rnum;
                    }
                    switch (chatViewModel.AnonymousTalkColorIndicator)
                    {
                        case 0: border_anonymityHead.Background = new SolidColorBrush(ColorTranslater.ColorFromHex("#EEEE00")); ; break;
                        case 1: border_anonymityHead.Background = new SolidColorBrush(ColorTranslater.ColorFromHex("#FFA500")); ; break;
                        case 2: border_anonymityHead.Background = new SolidColorBrush(ColorTranslater.ColorFromHex("#00FF00")); ; break;
                        case 3: border_anonymityHead.Background = new SolidColorBrush(ColorTranslater.ColorFromHex("#600060")); ; break;
                        case 4: border_anonymityHead.Background = new SolidColorBrush(ColorTranslater.ColorFromHex("#FF80CB")); ; break;
                        default: break;
                    }
                    if (chatViewModel.Alias == null)
                    {
                        sendMsg("AliasRqt|" + username);
                        //while (true)
                        //{
                        //    if (chatViewModel.Alias != null)
                        //        break;
                        //}
                    }
                    textBlock_nickName.Text = chatViewModel.Alias;
                }
                else
                {
                    chatViewModel.BackgroundColorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#FF1C93EC"));
                    chatViewModel.ButtonBackgroundColorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#FF1C93EC"));
                    chatViewModel.TextEditorColorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#FF1C93EC"));
                    imageBrush_upperLeftHead.ImageSource = new BitmapImage(new Uri("https://c-ssl.duitang.com/uploads/item/202004/12/20200412144215_sdhzl.jpg")); foreach (Friend item in chatViewModel.Friends)
                    {
                        item.FontColorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#FF000000"));
                    }
                    textBlock_nickName.Text = username;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("匿名异常：" + ex.Message);
            }
        }

        private void TextBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text == "" || tb.Text == null)
            {
                chatViewModel.PresentedFriends = chatViewModel.Friends;
            }
            else
            {
                System.Collections.ObjectModel.ObservableCollection<Friend> candidates = new System.Collections.ObjectModel.ObservableCollection<Friend>();
                foreach (Friend item in chatViewModel.Friends)
                {
                    if (item.Nickname.Contains(tb.Text))
                        candidates.Add(item);
                }
                chatViewModel.PresentedFriends = candidates;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            System.Windows.Forms.DialogResult result = openFileDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                //textBox1.Text = openFileDialog.FileName;
                string filePath = openFileDialog.FileName;
                showFile(System.IO.Path.GetFileName(filePath));
                Thread thread = new Thread(new ParameterizedThreadStart(sendFileToServer));
                thread.IsBackground = true;
                thread.Start(filePath);
            }

        }

        void sendFileToServer(object fp)
        {
            string filePath = fp as string;
            string fileName = System.IO.Path.GetFileName(filePath);
            try
            {
                using (FileStream fileReader = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    string fileLength = fileReader.Length.ToString();
                    sendMessage("file$" + fileName + "$" + fileLength);
                    Socket fileSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    //IPAddress iP = IPAddress.Parse("127.0.0.1");
                    //IPEndPoint endPoint = new IPEndPoint(iP, 9000);
                    IPAddress iP = IPAddress.Parse("47.115.11.25");//127.0.0.1//192.168.1.107//47.115.11.25//172.18.194.215
                    IPEndPoint endPoint = new IPEndPoint(iP, 22);
                    fileSocket.Connect(endPoint);
                    //MessageBox.Show(fileSocket.LocalEndPoint.ToString());
                    string fileRequest = "File|" + username + "|" + Touser + "|request|" + fileName + "|" + fileLength;
                    fileSocket.Send(Encoding.UTF8.GetBytes(fileRequest));
                    int fileBufferSize = 1024;
                    byte[] messageBuffer = new byte[32];
                    fileSocket.Receive(messageBuffer);
                    string message = Encoding.UTF8.GetString(messageBuffer);

                    if (message.Contains("ok"))
                    {
                        byte[] fileBuffer = new byte[fileBufferSize];
                        int read, sent;
                        long sentInTotal = 0;
                        while ((read = fileReader.Read(fileBuffer, 0, fileBufferSize)) != 0)
                        {
                            sent = 0;
                            while ((sent += fileSocket.Send(fileBuffer, sent, read, SocketFlags.None)) < read)
                            {
                                sentInTotal += (long)sent;
                            }
                            sentInTotal += (long)sent;
                        }
                        MessageBox.Show(sentInTotal.ToString() + "比特文件发送完毕");
                    }
                    fileSocket.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void Button_Click_3(object sender, RoutedEventArgs e)
        //{
        //    Socket recvFileSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //    IPAddress iP = IPAddress.Parse("127.0.0.1");
        //    IPEndPoint endPoint = new IPEndPoint(iP, 9000);
        //    recvFileSocket.Connect(endPoint);
        //    Friend selectedFriend = FriendList.SelectedItem as Friend;
        //    string fileName = selectedFriend.LastMessage.Split('*')[0];
        //    string fileLength = selectedFriend.LastMessage.Split('*')[1];
        //    string fileMessage = "File|" + username + "|" + Touser + "|ok|" + fileName;
        //    recvFileSocket.Send(Encoding.UTF8.GetBytes(fileMessage));
        //    int fileBufferSize = 1024;
        //    using (FileStream fileWriter = new FileStream(System.IO.Path.Combine(@".\", fileName), FileMode.Create, FileAccess.Write))
        //    {
        //        int receivedInTotal = 0, received;
        //        byte[] fileBuffer = new byte[fileBufferSize];
        //        while (receivedInTotal < Int32.Parse(fileLength))
        //        {
        //            received = recvFileSocket.Receive(fileBuffer, 0, fileBufferSize, SocketFlags.None);
        //            fileWriter.Write(fileBuffer, 0, received);
        //            fileWriter.Flush();
        //            receivedInTotal += received;
        //        }
        //    }
        //    MessageBox.Show("文件接收完毕");
        //}

        public void showFile(string fileName)
        {
            UserControlMessageSent newUserControlMessageSent = new UserControlMessageSent();
            newUserControlMessageSent.textBlock_time.Text = fileName + "\n" + DateTime.Now.ToString();

            foreach (ScrollViewer item in grid_talkStackPanels.Children)//grid_talkStackPanels 是 null
            {
                if (item.Name == Touser)
                {
                    StackPanel stp = item.Content as StackPanel;
                    stp.Children.Add(newUserControlMessageSent);
                    //item.Children.Add(newUserControlMessageSent);
                    break;
                }
            }
            // this.albery.Children.Add(newUserControlMessageSent);
            newUserControlMessageSent.textBlock_msg.Background = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/File.png"))
            };
            newUserControlMessageSent.HorizontalAlignment = HorizontalAlignment.Right;
            newUserControlMessageSent.border_cloud.Width = 80;
            newUserControlMessageSent.border_cloud.Height = 80;
            //newUserControlMessageSent.Margin = new Thickness(250, 0, 0, 0);
        }



        //emoji相关




        //接收表情 

        public void displayP(string fromP, string PictureName)
        {
            //MessageBox.Show("来到了displayP");
            try
            {
                if (System.Threading.Thread.CurrentThread != this.Dispatcher.Thread)
                {
                    this.Dispatcher.Invoke(display, fromP, PictureName);
                }
            }
            catch (Exception e) { }

        }
        private void receiveEmoji(string fromP, string pictureName)
        {
            if (fromP != username)
            {
                //MessageBox.Show("来到了receiveEmoji");
                UserControlMessageReceived newUserControlMessageReceived = new UserControlMessageReceived();
                if (fromP != "All")
                {
                    //this.albery.Children.Add(newUserControlMessageReceived);
                    newUserControlMessageReceived.column_0.Width = new GridLength(0);
                    newUserControlMessageReceived.textBlock_time.Text = DateTime.Now.ToString();


                    foreach (ScrollViewer item in grid_talkStackPanels.Children)
                    {
                        if (item.Name == fromP.Trim())
                        {
                            StackPanel stp = item.Content as StackPanel;
                            stp.Children.Add(newUserControlMessageReceived);
                            //item.Children.Add(newUserControlMessageReceived);
                            break;
                        }
                    }
                    foreach (Friend item in chatViewModel.Friends)
                    {
                        if (item.Nickname == fromP.Trim())
                        {
                            item.LastMessage = "【" + pictureName + "】";
                            if (item != FriendList.SelectedItem)
                            {
                                item.NumNewMessages = (Int32.Parse(item.NumNewMessages) + 1).ToString();
                                item.SpotVisibility = Visibility.Visible;
                            }
                            break;
                        }
                    }

                }
                else
                {
                    newUserControlMessageReceived.column_0.Width = new GridLength(45);
                    foreach (Friend item in chatViewModel.Friends)
                    {
                        if (item.Nickname == fromP.Trim())
                        {
                            newUserControlMessageReceived.imageBrush_head.ImageSource = item.Head;
                            break;
                        }
                    }
                    foreach (Friend item in chatViewModel.Friends)
                    {
                        if (item.Nickname == "All")
                        {
                            item.LastMessage = fromP.Trim() + "：【" + pictureName + "】";
                            if (item != FriendList.SelectedItem)
                            {
                                item.NumNewMessages = (Int32.Parse(item.NumNewMessages) + 1).ToString();
                                item.SpotVisibility = Visibility.Visible;
                            }
                            break;
                        }
                    }
                    StackPanel stp = All.Content as StackPanel;
                    stp.Children.Add(newUserControlMessageReceived);
                    //All.Children.Add(newUserControlMessageReceived);
                    newUserControlMessageReceived.textBlock_time.Text = DateTime.Now.ToString() + "  " + fromP.Trim();
                }

                newUserControlMessageReceived.textBlock_msg.Background = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/" + pictureName))
                };
                newUserControlMessageReceived.HorizontalAlignment = HorizontalAlignment.Left;
                newUserControlMessageReceived.border_cloud.Width = 80;
                newUserControlMessageReceived.border_cloud.Height = 80;
            }
        }





        //发送表情

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            //popUp_emoji.IsOpen = false;
            //popUp_emoji.IsOpen = true;
            if (grid_talk.Children.Contains(Page_Change))
            {
                UserControlEmojiWindow emojiWindow = new UserControlEmojiWindow(this);
                Page_Change.Content = new Frame()
                {
                    Content = emojiWindow
                };
            }
            else
            {
                grid_talk.Children.Add(Page_Change);
                UserControlEmojiWindow emojiWindow = new UserControlEmojiWindow(this);
                Page_Change.Content = new Frame()
                {
                    Content = emojiWindow
                };
            }

        }


        public void CloseEmojiWindow()
        {
            grid_talk.Children.Remove(Page_Change);
        }

        //发送表情包
        public void SendEmoji(string pictureName)
        {
            try
            {
                string sendStrMsg;
                if (Touser != "All")
                    sendStrMsg = "Talk|" + username + "|" + Touser + "|emoji*" + pictureName;
                else
                    sendStrMsg = "Talk|" + username + "|" + "All" + "|emoji*" + pictureName;
                sendMsg(sendStrMsg);
                //sendTBX.Text = "";
                //receiveTBX.AppendText("你对" + toUser + "说:" + msg + "\n");
            }
            catch (Exception ex)
            {
                //出现异常，如：没有连接到服务器
                string errorMsg = string.Format(@"发送失败：{0}", ex.Message);
                showDialog(errorMsg + "\r\n");
            }
            showEmoji(pictureName);
        }

        public void showEmoji(string emojiName)
        {
            UserControlMessageSent newUserControlMessageSent = new UserControlMessageSent();
            newUserControlMessageSent.textBlock_time.Text = DateTime.Now.ToString();

            foreach (ScrollViewer item in grid_talkStackPanels.Children)//grid_talkStackPanels 是 null
            {
                if (item.Name == Touser)
                {
                    StackPanel stp = item.Content as StackPanel;
                    stp.Children.Add(newUserControlMessageSent);
                    //item.Children.Add(newUserControlMessageSent);
                    break;
                }
            }
            // this.albery.Children.Add(newUserControlMessageSent);
            newUserControlMessageSent.textBlock_msg.Background = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/" + emojiName))
            };
            newUserControlMessageSent.HorizontalAlignment = HorizontalAlignment.Right;
            newUserControlMessageSent.border_cloud.Width = 80;
            newUserControlMessageSent.border_cloud.Height = 80;
            //newUserControlMessageSent.Margin = new Thickness(250, 0, 0, 0);
        }

        private void Button_Click_Emoji(object sender, RoutedEventArgs e)
        {
            CloseEmojiWindow();
        }
    }
}
