using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Data;
using MySql.Data.MySqlClient;

namespace Register
{
    class FriendlistViewModel : BindableBase
    {

        Friendlist friendlist = null;
        public DelegateCommand<object> MouseRightButtonDownCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }
        public string usr;

        public FriendlistViewModel (Friendlist frdlst, string user)
        {
            //friends = new ObservableCollection<Friend>();
            //friends.Add(new Friend() { Nickname = "Mark", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head1.jpg")) });
            //friends.Add(new Friend() { Nickname = "Mary", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head2.jpg")) });
            //friends.Add(new Friend() { Nickname = "静香", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head3.jpg")) });
            //friends.Add(new Friend() { Nickname = "小夫", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head4.jpg")) });
            //friends.Add(new Friend() { Nickname = "饼藏", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head5.jpg")) });
            //friends.Add(new Friend() { Nickname = "玉子", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head6.jpg")) });

            //friends = new ObservableCollection<Friend>();
            //friends.Add(new Friend() { Nickname = "albery", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head1.jpg")), BackGroundColor = ColorTranslater.ColorFromHex("#FF1C93EC") });
            //friends.Add(new Friend() { Nickname = "wangwu", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head2.jpg")), BackGroundColor = ColorTranslater.ColorFromHex("#FFFFFFFF") });
            //friends.Add(new Friend() { Nickname = "静香", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head3.jpg")), BackGroundColor = ColorTranslater.ColorFromHex("#FFFFFFFF") });
            //friends.Add(new Friend() { Nickname = "wangwu2", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head4.jpg")), BackGroundColor = ColorTranslater.ColorFromHex("#FF1C93EC") });
            //friends.Add(new Friend() { Nickname = "饼藏", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head5.jpg")), BackGroundColor = ColorTranslater.ColorFromHex("#FF1C93EC") });
            ////friends.Add(new Friend() { Nickname = "玉子", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head6.jpg")), BackGroundColor = ColorTranslater.ColorFromHex("#FF1C93EC") });
            //friends.Add(new Friend() { Nickname = "All", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head6.jpg")), BackGroundColor = ColorTranslater.ColorFromHex("#FF1C93EC") });
            friendlist = frdlst;
            usr = user;


            string connString = "Server=cdb-9lgx1peo.bj.tencentcdb.com;Database=SDChatRoom;uid=root;pwd=rjgzjc666;port=10096;Charset=utf8";
            MySqlConnection connection1 = new MySqlConnection(connString);
            string sqlstr = "SELECT * FROM users ;";
            MySqlCommand Mycommand = new MySqlCommand(sqlstr, connection1);
            connection1.Open();
            MySqlDataReader MyReader = Mycommand.ExecuteReader();
            friends = new ObservableCollection<Friend>();
            friends.Add(new Friend() { Nickname = "All", Head = new BitmapImage(new Uri("pack://application:,,,/Images/headqun.jpg")), BackGroundColor = ColorTranslater.ColorFromHex("#FF1C93EC") });
            while (MyReader.Read())
            {
                friends.Add(new Friend() { Nickname = MyReader["NickName"].ToString(), Head = new BitmapImage(new Uri("pack://application:,,,/Images/head" + MyReader["P_ID"] + ".jpg")), BackGroundColor = ColorTranslater.ColorFromHex("#FF1C93EC") });
            }
            MyReader.Close();
            connection1.Close();
            Console.Read();


            CloseCommand = new DelegateCommand(() => {

                //friendlist.Quit();
                Application.Current.Shutdown();

            });

            MouseRightButtonDownCommand = new DelegateCommand<object>((p) => {

                //MessageBox.Show("MouseRightButtonDownCommand");
 
                ListView lv = p as ListView;
                Friend friend = lv.SelectedItem as Friend;
                Head = friend.Head;
                Nickname = friend.Nickname;

                Chat chat = friendlist.Chat;
                friendlist.BindChatSocket(chat);
                chat.Show();

            });
        }

        private ObservableCollection<Friend> friends;

        public ObservableCollection<Friend> Friends
        {
            get { return friends; }
            set { friends = value; }
        }




        private BitmapImage head;

        public BitmapImage Head
        {
            get { return head; }
            set { SetProperty(ref head, value); }
        }


        private string nickname;
        public string Nickname
        {
            get { return nickname; }
            set { SetProperty(ref nickname, value); }
        }

    }
}
