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
using System.Data;
using MySql.Data.MySqlClient;


namespace Register
{
    /// <summary>
    /// Card.xaml 的交互逻辑
    /// </summary>
    public partial class Card : Window
    {
        Friendlist friendlist = null;
        public Card(Friendlist friendlist)
        {
            this.friendlist = friendlist;
            InitializeComponent();
            string connString = "Server=cdb-9lgx1peo.bj.tencentcdb.com;Database=SDChatRoom;uid=root;pwd=rjgzjc666;port=10096;Charset=utf8";
            MySqlConnection connection1 = new MySqlConnection(connString);
            string sqlstr = "SELECT * FROM users WHERE U_Login=\"" + MainWindow.ID + "\";";
            MySqlCommand Mycommand = new MySqlCommand(sqlstr, connection1);
            connection1.Open();
            MySqlDataReader MyReader = Mycommand.ExecuteReader();
            while (MyReader.Read())
            {
                thetouxiang.Source = new BitmapImage(new Uri("pack://application:,,,/Images/head" + MyReader["P_ID"] + ".jpg"));
                zhanghaoshow.Text = MyReader["U_Login"].ToString();
                nicknameshow.Text = MyReader["Nickname"].ToString();
                sexresult.Text = MyReader["Sex"].ToString();
                phoneshow.Text = MyReader["Telephone"].ToString();
                emailshow.Text = MyReader["Email"].ToString();
                occupationshow.Text = MyReader["Occupation"].ToString();
                kongjianshow.Text = nicknameshow.Text + "的空间";
                //you may ask why not "sexshow",because it is used!
            }
            MyReader.Close();
            connection1.Close();
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new CardEdit(friendlist, this);
        }

        /*private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }*/
    }
}