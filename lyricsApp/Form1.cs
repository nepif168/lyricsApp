using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lyricsApp
{
    public partial class Form1 : Form
    {
        private string receiveURL = "";

        public Form1()
        {
            InitializeComponent();
            this.AcceptButton = button1;
        }

        //サイトへ飛ぶがクリックされたらサイトへ飛ぶ
        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.form1 = this; //form2のメンバーに代入
            f.Show();
        }

        //URL入力されてHTMLをDL
        private void button1_Click(object sender, EventArgs e)
        {
            Control();
        }

        private void Control()
        {
            string lyrics;  //歌詞を入れる変数

            string html = ReturnHTML(textBox1.Text);    //引数のURLからHTMLを返す
            if (html == null) return;   //URLが正しくない場合はnullが帰る

            lyrics = ExtractingLyric(html); //HTMLから歌詞を抽出し返す

            lyrics = System.Web.HttpUtility.HtmlDecode(lyrics); //数値文字の変換

            lyrics = lyrics.Replace("<br>", "\r\n");    //HTMLの改行をWindowsの改行に変換(CRLF) クリップボードにコピーした際改行がされないため\nではない

            if (checkBox1.Checked) Clipboard.SetText(lyrics);   //チェックボックスにチェックが入ってたら歌詞をコピー

            richTextBox1.Text = lyrics; //テキストボックスに入れる
        }

        //歌詞の抽出
        private string ExtractingLyric(string html)
        {
            string lyrics;

            string firstString = "var lyrics = '";
            string endString = "';";

            lyrics = html.Remove(0, html.IndexOf(firstString) + firstString.Length);
            lyrics = lyrics.Remove(lyrics.IndexOf(endString));

            return lyrics;
        }

        //URLからHTMLを返す
        private string ReturnHTML(string url)
        {
            string URL = url;

            if (URL.IndexOf("http://www.kasi-time.com/item-") < 0)
            {
                MessageBox.Show("URLが正しくない可能性があります");
                return null;
            }

            WebClient wc = new WebClient();

            Stream st = wc.OpenRead(URL);

            Encoding enc = Encoding.GetEncoding("utf-8");
            StreamReader sr = new StreamReader(st, enc);
            string html = sr.ReadToEnd();
            sr.Close();
            st.Close();

            return html;
        }

        //プロパティ
        public string ReceiveURL
        {
            //URLが入ったら自動でControlを実行
            set
            {
                receiveURL = value;
                textBox1.Text = receiveURL;
                Control();
            }
            get
            {
                return receiveURL;
            }
        }
    }
}
