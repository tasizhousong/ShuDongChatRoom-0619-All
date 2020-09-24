using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;
using System.Data;
using MySql.Data.MySqlClient;

namespace Register
{
    /// <summary>
    /// Friendlist.xaml 的交互逻辑
    /// </summary>
    public partial class Friendlist : Window
    {
        //MouseButtonEventHandler ListView_MouseRightButtonDown;
        Socket clientSocket = null;
        Chat chat;
        string tempTxt2 = string.Empty;    //定义一个全局变量，用来存储获取焦点之前 TextBox 的值

        public Chat Chat { get => chat; }  //封装chat

        public Friendlist(Socket skt, string user)
        {
            InitializeComponent();


            string connString = "Server=cdb-9lgx1peo.bj.tencentcdb.com;Database=SDChatRoom;uid=root;pwd=rjgzjc666;port=10096;Charset=utf8";
            MySqlConnection connection1 = new MySqlConnection(connString);
            string sqlstr = "SELECT * FROM users WHERE U_Login=\"" + user + "\";";
            MySqlCommand Mycommand = new MySqlCommand(sqlstr, connection1);
            connection1.Open();
            MySqlDataReader MyReader = Mycommand.ExecuteReader();
            while (MyReader.Read())
            {
                gexingqianming.Text = MyReader["PersonalSignature"].ToString();
                Myname.Text = MyReader["NickName"].ToString();
                myh.Background = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/head" + MyReader["P_ID"] + ".jpg"))
                };
            }
            MyReader.Close();
            connection1.Close();
            //设置myname的值


            ChatViewModel viewModel = ChatViewModel.sharedInstance;
            viewModel.usr = user;
            viewModel.friendlistWindow = this;
            this.DataContext = viewModel;
            clientSocket = skt;
            chat = new Chat(viewModel.Friends[0], user);
            //this.FriendList.AddHandler(ListView.MouseRightButtonDownEvent, new MouseButtonEventHandler(this.ListView_MouseRightButtonDown), true); ;
        }

        private void NavBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Searchbox_GotFocus(object sender, RoutedEventArgs e)   //获取焦点执行的事件
        {
            TextBox Searchbox = sender as TextBox;
            tempTxt2 = Searchbox.Text;
            Searchbox.Text = string.Empty;
            Searchbox.Background = Brushes.White;          //获取焦点后，将文本框的背景色改成白色
            Searchbox.BorderBrush = Brushes.Transparent;

        }

        public void sendMsg(string msg)
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

        public void Quit()
        {
            string msg = "Close|" + Myname.Text;
            sendMsg(msg);
            Environment.Exit(0);
        }

        private void Searchbox_LostFocus(object sender, RoutedEventArgs e)   //丢失焦点之后，该处理的事件
        {
            if (Searchbox.Text == string.Empty)
            {
                Searchbox.Text = tempTxt2;
            }

        }

        private void ScrollViewer_AccessKeyPressed(object sender, AccessKeyPressedEventArgs e)
        {
            //ListView lv = new ListView();
            //lv.MouseRightButtonDown
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //string msg = "Close|" + Myname.Text;
            //sendMsg(msg);
        }

        public void BindChatSocket(Chat chat)
        {
            chat.ClientSocket = clientSocket;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            sendMsg("AliasRqt|init");
        }

        private void Searchbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //TextBox tb = sender as TextBox;
            //if (tb.Text == "" || tb.Text == null || tb.Text == "  搜索 : 联系人、讨论组、群、企业" || tb.Text == " ")
            //{
            //    ChatViewModel.sharedInstance.PresentedFriends_2 = ChatViewModel.sharedInstance.Friends;
            //}
            //else
            //{
            //    System.Collections.ObjectModel.ObservableCollection<Friend> candidates = new System.Collections.ObjectModel.ObservableCollection<Friend>();
            //    foreach (Friend item in ChatViewModel.sharedInstance.Friends)
            //    {
            //        if (item.Nickname.Contains(tb.Text))
            //            candidates.Add(item);
            //    }
            //    ChatViewModel.sharedInstance.PresentedFriends_2 = candidates;
            //}
        }

        private void Head_Click(object sender, RoutedEventArgs e)
        {
            //this.Content = new Card();
            Card card = new Card(this);
            card.Show();
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (gexingqianming.Text != "编辑个性签名")
            {
                string connString = "Server=cdb-9lgx1peo.bj.tencentcdb.com;Database=SDChatRoom;uid=root;pwd=rjgzjc666;port=10096;Charset=utf8";
                MySqlConnection connection1 = new MySqlConnection(connString);
                string sqlstr = "UPDATE users SET PersonalSignature=\"" + gexingqianming.Text + "\" WHERE U_Login=\"" + MainWindow.ID + "\";";
                MySqlCommand Mycommand = new MySqlCommand(sqlstr, connection1);
                connection1.Open();
                int cntInsert = Mycommand.ExecuteNonQuery();
                Console.WriteLine(cntInsert);
                connection1.Close();
                Console.Read();
            }
        }
    }
}
