using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class ColorTranslater
    {
        public static Color ColorFromHex(string strHxColor)
        {
            try
            {
                if (strHxColor.Length != 7 && strHxColor.Length != 9)
                {
                    return Color.FromArgb(1, 1, 1, 1); //默认为白色
                }
                else if (strHxColor.Length == 7)
                {
                    return Color.FromArgb(Byte.Parse("FF",
    System.Globalization.NumberStyles.AllowHexSpecifier), Byte.Parse(strHxColor.Substring(1, 2),
    System.Globalization.NumberStyles.AllowHexSpecifier),
    Byte.Parse(strHxColor.Substring(3, 2), System.Globalization.NumberStyles.AllowHexSpecifier),
    Byte.Parse(strHxColor.Substring(5, 2), System.Globalization.NumberStyles.AllowHexSpecifier));
                }
                else
                {
                    return Color.FromArgb(Byte.Parse(strHxColor.Substring(1, 2),
   System.Globalization.NumberStyles.AllowHexSpecifier),
   Byte.Parse(strHxColor.Substring(3, 2), System.Globalization.NumberStyles.AllowHexSpecifier),
   Byte.Parse(strHxColor.Substring(5, 2), System.Globalization.NumberStyles.AllowHexSpecifier),
   Byte.Parse(strHxColor.Substring(7, 2), System.Globalization.NumberStyles.AllowHexSpecifier));
                }
            }
            catch
            {
                return Color.FromArgb(1, 1, 1, 1); //出现异常则设为默认值白色
            }
        }
    }

    public class Friend /*: INotifyPropertyChanged*/ : BindableBase
    {

        string nickname;
        public string Nickname
        {
            get { return nickname; }
            set => SetProperty(ref nickname, value);
        }
        public BitmapImage Head { get; set; }
        public Color BackGroundColor
        {
            get { return BackGroundBrush.Color; }
            set { BackGroundBrush = new SolidColorBrush(value); }
        }
        private SolidColorBrush BackGroundBrush;
        public SolidColorBrush BackGround { get { return BackGroundBrush; } set { BackGroundBrush = value; } }

        string lastMessage = "我们已经是朋友了，开始聊天吧。";
        public string LastMessage { get { return lastMessage; } set { SetProperty(ref lastMessage, value); RaisePropertyChanged(); } }

        string numNewMessages = "0";
        public string NumNewMessages
        {
            get { return numNewMessages.ToString(); }
            set { SetProperty(ref numNewMessages, value); }

            //event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
            //{
            //    add
            //    {
            //        throw new NotImplementedException();
            //    }

            //    remove
            //    {
            //        throw new NotImplementedException();
            //    }
            //}
        }

        Visibility spotVisibility = Visibility.Hidden;
        public Visibility SpotVisibility { get { return spotVisibility; } set { SetProperty(ref spotVisibility, value); } }

        SolidColorBrush fontColorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#FFFFFFFF"));
        public SolidColorBrush FontColorBrush
        {
            get
            {
                return fontColorBrush;
            }
            set => SetProperty(ref fontColorBrush, value);
        }

        public int rnum;    //用于匿名功能，决定匿名聊天气泡颜色和匿名头像背景颜色

    }

    public class ChatViewModel : BindableBase
    {


        public DelegateCommand<object> MouseRightButtonDownCommand { get; set; }
        public DelegateCommand FriendlistCloseCommand { get; set; }
        public string usr;

        public DelegateCommand<object> SelectItemChangedCommand { get; set; }
        public DelegateCommand ChatCloseCommand { get; set; }

        static public ChatViewModel sharedInstance = new ChatViewModel();

        public Chat chatWindow;
        public Friendlist friendlistWindow;

        public bool anonymity = false;

        SolidColorBrush backgroundColorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#FF1C93EC"));
        public SolidColorBrush BackgroundColorBrush
        {
            get
            {
                return backgroundColorBrush;
            }
            set
            {
                SetProperty(ref backgroundColorBrush, value);
            }
        }

        SolidColorBrush buttonBackgroundColorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#FF1C93EC"));
        public SolidColorBrush ButtonBackgroundColorBrush{
            get
            {
                return buttonBackgroundColorBrush;
            }
            set
            {
                SetProperty(ref buttonBackgroundColorBrush, value);
            }
        }

        SolidColorBrush buttonForegroundColorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#FFFFFFFF"));
        public SolidColorBrush ButtonForegroundColorBrush
        {
            get
            {
                return buttonForegroundColorBrush;
            }
            set
            {
                SetProperty(ref buttonForegroundColorBrush, value);
            }
        }

        SolidColorBrush textEditorColorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#FFB5CEE0"));
        public SolidColorBrush TextEditorColorBrush
        {
            get
            {
                return textEditorColorBrush;
            }
            set
            {
                SetProperty(ref textEditorColorBrush, value);
            }
        }


        private ObservableCollection<Friend> friends;

        public ObservableCollection<Friend> Friends
        {
            get { return friends; }
            set { friends = value; }
        }

        private ObservableCollection<Friend> presentedFriends;
        public ObservableCollection<Friend> PresentedFriends
        {
            get { return presentedFriends; }
            set { SetProperty(ref presentedFriends, value); }
        }

        private ObservableCollection<Friend> presentedFriends_2;
        public ObservableCollection<Friend> PresentedFriends_2
        {
            get { return presentedFriends_2; }
            set { SetProperty(ref presentedFriends_2, value); }
        }

        private BitmapImage head;

        public BitmapImage Head
        {
            get
            {
                Friend slt = chatWindow.FriendList.SelectedItem as Friend;
                return slt.Head;
            }
            set { SetProperty(ref head, value); }
        }


        private string nickname;
        public string Nickname
        {
            get
            {
                Friend slt = chatWindow.FriendList.SelectedItem as Friend;
                return slt.Nickname;
            }
            set { SetProperty(ref nickname, value); }
        }

        string alias;
        public string Alias
        {
            get
            {
                return alias;
            }
            set
            {
                SetProperty(ref alias, value);
            }
        }

        public int TalkColorIndicator { get; set; } = 5;
        public int? AnonymousTalkColorIndicator { get; set; }




        //数据库相关

        string connString = "Server=cdb-9lgx1peo.bj.tencentcdb.com;Database=SDChatRoom;uid=root;pwd=rjgzjc666;port=10096;Charset=utf8";
        MySqlConnection connection1;
        string sqlstr;
        MySqlCommand Mycommand;
        MySqlDataReader MyReader;
        //声明三个变量
        string address = "";
        public string Address
        {
            get { return address; }
            set { SetProperty(ref address, value); }
        }
        string phone = "";
        public string Phone
        {
            get { return phone; }
            set { SetProperty(ref phone, value); }
        }
        string email = "";
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        string personalSignature = "";
        public string PersonalSignature
        {
            get { return personalSignature; }
            set { SetProperty(ref personalSignature, value); }
        }



        public ChatViewModel()
            {
            //friends = new ObservableCollection<Friend>();
            //friends.Add(new Friend() { Nickname = "All", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head8.jpg")), BackGroundColor = ColorTranslater.ColorFromHex("#FF1C93EC") });
            //friends.Add(new Friend() { Nickname = "albery", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head2.jpg")), BackGroundColor = ColorTranslater.ColorFromHex("#FF1C93EC") });
            //friends.Add(new Friend() { Nickname = "吴健豪", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head7.jpg")), BackGroundColor = ColorTranslater.ColorFromHex("#FFFFFFFF") });
            //friends.Add(new Friend() { Nickname = "吴茜", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head3.jpg")), BackGroundColor = ColorTranslater.ColorFromHex("#FFFFFFFF") });
            //friends.Add(new Friend() { Nickname = "李光华", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head4.jpg")), BackGroundColor = ColorTranslater.ColorFromHex("#FF1C93EC") });
            //friends.Add(new Friend() { Nickname = "于永瑞", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head5.jpg")), BackGroundColor = ColorTranslater.ColorFromHex("#FF1C93EC") });
            //friends.Add(new Friend() { Nickname = "潘童欣", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head1.jpg")), BackGroundColor = ColorTranslater.ColorFromHex("#FF1C93EC") });
            //friends.Add(new Friend() { Nickname = "李芊颖", Head = new BitmapImage(new Uri("pack://application:,,,/Images/head6.jpg")), BackGroundColor = ColorTranslater.ColorFromHex("#FF1C93EC") });

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


            presentedFriends_2 = friends;
            presentedFriends = friends;

                FriendlistCloseCommand = new DelegateCommand(() =>
                {

                    //friendlist.Quit();
                    string msg = "Close|" + friendlistWindow.Myname.Text;
                    friendlistWindow.sendMsg(msg);
                    Application.Current.Shutdown();

                });

                MouseRightButtonDownCommand = new DelegateCommand<object>((p) =>
                {

                    //MessageBox.Show("MouseRightButtonDownCommand");

                    ListView lv = p as ListView;
                    Friend friend = lv.SelectedItem as Friend;
                    //Head = friend.Head;
                    //Nickname = friend.Nickname;

                    Chat chat = friendlistWindow.Chat;
                    friendlistWindow.BindChatSocket(chat);
                    chat.Show();
                    chat.FriendList.SelectedItem = Friends[0];
                    chat.FriendList.SelectedItem = Friends[1];
                    chat.FriendList.SelectedItem = lv.SelectedItem;

                });


                ChatCloseCommand = new DelegateCommand(() =>
                {

                    Application.Current.Shutdown();

                });

                SelectItemChangedCommand = new DelegateCommand<object>((p) =>
                {

                    ListView lv = p as ListView;
                    if (lv.SelectedItem != null)
                    {
                        Friend friend = lv.SelectedItem as Friend;

                        Head = friend.Head;
                        Nickname = friend.Nickname;
                        friend.NumNewMessages = 0.ToString();
                        friend.SpotVisibility = Visibility.Hidden;




                        connString = "Server=cdb-9lgx1peo.bj.tencentcdb.com;Database=SDChatRoom;uid=root;pwd=rjgzjc666;port=10096;Charset=utf8";
                        connection1 = new MySqlConnection(connString);
                        sqlstr = "SELECT * FROM users WHERE U_Login=\"" + friend.Nickname + "\";";
                        Mycommand = new MySqlCommand(sqlstr, connection1);
                        connection1.Open();
                        MyReader = Mycommand.ExecuteReader();
                        while (MyReader.Read())
                        {
                            Address = MyReader["Address"].ToString();
                            Phone = MyReader["Telephone"].ToString();
                            Email = MyReader["Email"].ToString();
                            PersonalSignature = MyReader["PersonalSignature"].ToString();
                        }
                        MyReader.Close();
                        connection1.Close();





                        if (friend.Nickname != "All")
                        {
                            chatWindow.button_anonymity.Visibility = Visibility.Hidden;
                            BackgroundColorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#FF1C93EC"));
                            ButtonBackgroundColorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#FF1C93EC"));
                            TextEditorColorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#FF1C93EC"));
                            chatWindow.imageBrush_upperLeftHead.ImageSource = new BitmapImage(new Uri("https://c-ssl.duitang.com/uploads/item/202004/12/20200412144215_sdhzl.jpg"));
                            chatWindow.textBlock_nickName.Text = usr;
                        }
                        else
                        {
                            chatWindow.button_anonymity.Visibility = Visibility.Visible;
                            if (anonymity == true)
                            {
                                BackgroundColorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#2D2D30"));
                                ButtonBackgroundColorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#2D2D30"));
                                TextEditorColorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#9E9E9E"));
                                chatWindow.imageBrush_upperLeftHead.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/anonymity.png"));
                                foreach (Friend item in Friends)
                                {
                                    item.FontColorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#FFFFFFFF"));
                                }
                                chatWindow.textBlock_nickName.Text = Alias;
                            }
                            else
                            {
                                BackgroundColorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#FF1C93EC"));
                                ButtonBackgroundColorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#FF1C93EC"));
                                TextEditorColorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#FF1C93EC"));
                                chatWindow.imageBrush_upperLeftHead.ImageSource = new BitmapImage(new Uri("https://c-ssl.duitang.com/uploads/item/202004/12/20200412144215_sdhzl.jpg"));
                                foreach (Friend item in Friends)
                                {
                                    item.FontColorBrush = new SolidColorBrush(ColorTranslater.ColorFromHex("#FF000000"));
                                }
                                chatWindow.textBlock_nickName.Text = usr;
                            }
                        }

                        chatWindow.chooseVisibleStackPanel(Nickname);
                        chatWindow.Touser = friend.Nickname;
                    }
                    //mainWindow

                });
            }


        }
    }

