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

namespace Register
{
    /// <summary>
    /// UserControlEmojiWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlEmojiWindow : UserControl
    {
        Chat clientForm;

        public UserControlEmojiWindow(Chat clientForm)
        {
            this.clientForm = clientForm;
            InitializeComponent();
        }

        private void clauseElementClicked(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ImageBrush imb = btn.Background as ImageBrush;
            string emojiPath = imb.ImageSource.ToString();
            string emojiName = System.IO.Path.GetFileName(emojiPath);
            //MessageBox.Show(emojiName);
            displayPictureEmoji(emojiName);
            closeEmoji();
        }


        private void displayPictureEmoji(string pictureName)
        {
            this.clientForm.SendEmoji(pictureName);
        }

        private void closeEmoji()
        {
            this.clientForm.CloseEmojiWindow();
        }

    }
}

