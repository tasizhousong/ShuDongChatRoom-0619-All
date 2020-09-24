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
    /// CardEdit.xaml 的交互逻辑
    /// </summary>

    public partial class CardEdit : Page
    {
        string lujing = null;
        Friendlist friendlist = null;
        Card card1 = null;
        public CardEdit(Friendlist friendlist, Card card1)
        {
            InitializeComponent();
            //touxiang.Source = new BitmapImage(new Uri("pack://application:,,,/Images/head" + MyReader["P_ID"] + ".jpg"));

            string connString = "Server=cdb-9lgx1peo.bj.tencentcdb.com;Database=SDChatRoom;uid=root;pwd=rjgzjc666;port=10096;Charset=utf8";
            MySqlConnection connection1 = new MySqlConnection(connString);
            string sqlstr = "SELECT * FROM users WHERE U_Login=\"" + MainWindow.ID + "\";";
            MySqlCommand Mycommand = new MySqlCommand(sqlstr, connection1);
            connection1.Open();
            MySqlDataReader MyReader = Mycommand.ExecuteReader();
            while (MyReader.Read())
            {
                nicknamein.Text = MyReader["NickName"].ToString();
                phonein.Text = MyReader["Telephone"].ToString();
                emailin.Text = MyReader["Email"].ToString();
                occupationin.Text = MyReader["Occupation"].ToString();
                addressin.Text = MyReader["Address"].ToString();
                touxiang.Background = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/head" + MyReader["P_ID"] + ".jpg"))
                };
            }
            MyReader.Close();
            connection1.Close();

            this.friendlist = friendlist;
            this.card1 = card1;

            zhanghaoshowinedit.Text = MainWindow.ID;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (lujing != null)
            {
                friendlist.myh.Background = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(lujing))
                };
            }
            //card1.thetouxiang.ImageSource = new BitmapImage(new Uri(lujing));
            string thenickname = nicknamein.Text;
            string thesex = "";
            if (RadComplete.IsChecked == true)
            {
                thesex = "男";
            }
            else
            {
                thesex = "女";
            }
            string thephone = phonein.Text;
            string theemail = emailin.Text;
            string theoccupation = occupationin.Text;
            string theaddress = addressin.Text;

            string connString = "Server=cdb-9lgx1peo.bj.tencentcdb.com;Database=SDChatRoom;uid=root;pwd=rjgzjc666;port=10096;Charset=utf8";

            MySqlConnection connection1 = new MySqlConnection(connString);
            string sqlstr = "UPDATE users SET NickName=\"" + thenickname + "\",Sex=\"" + thesex + "\",Telephone=\"" + thephone + "\",Email=\"" + theemail + "\",Occupation=\"" + theoccupation + "\",Address=\"" + theaddress + "\" WHERE U_Login=\"" + MainWindow.ID + "\";";
            MySqlCommand Mycommand = new MySqlCommand(sqlstr, connection1);
            connection1.Open();
            int cntInsert = Mycommand.ExecuteNonQuery();
            connection1.Close();
            (this.Parent as Window).Close();
        }

        private void Touxiang_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            System.Windows.Forms.DialogResult result = openFileDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)  //下面大括号里实现打开文件后要做的事
            {
                string touxiangpath = openFileDialog.FileName; //获取选中的文件全名(包括路径)
                string connString = "Server=cdb-9lgx1peo.bj.tencentcdb.com;Database=SDChatRoom;uid=root;pwd=rjgzjc666;port=10096;Charset=utf8";
                MySqlConnection connection1 = new MySqlConnection(connString);
                string sqlstr = "UPDATE users SET P_ID=\"" + "1" + "\" WHERE U_Login=\"" + MainWindow.ID + "\";";
                MySqlCommand Mycommand = new MySqlCommand(sqlstr, connection1);
                connection1.Open();
                int cntInsert = Mycommand.ExecuteNonQuery();
                connection1.Close();
           
                touxiang.Background = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(openFileDialog.FileName))
                };
                lujing = openFileDialog.FileName;
            }
        }
    }
}