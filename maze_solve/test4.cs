using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace maze_solve
{
    public partial class test4 : Form
    {
        public test4()
        {
            InitializeComponent();
        }

        private void test4_Load(object sender, EventArgs e)
        {

        }

        // 描画内容
        public class Content
        {
            //行数
            public int NumOfRows { get; set; }

           public int NumOfColumns { get; set; }

               //枡の幅
               public int Width { get; set; }
               //枡の高さ
               public int Height { get; set; }

               //線の太さ
               public int Thickness { get; set; }
               //線の色
               public Color Color { get; set; }

               //妥当な行数か？
               public static bool IsValidNumOfRows(int numOfRows)
               {
                   return numOfRows > 0;
               }

               //妥当な列数か？
               public static bool IsValidNumOfColumns(int numOfColumns)
               {
                   return numOfColumns > 0;
               }

               //妥当な枡の高さか？
               public static bool IsValidHeight(int height)
               {
                   return height > 0;
               }

               //妥当な枡の幅か？
               public static bool IsValidWidth(int width)
               {
                   return width > 0;
               }

               //妥当な線の太さか？
               public static bool IsValidThickness(int thickness)
               {
                   return thickness >= 0;
               }
    
        }
        
        // 範囲を表す
        public class Range2d<T>
        {
            public Range2d(T _x, T _y, T _width, T _height)
            {
                X = _x;
                Y = _y;
                Width = _width;
                Height = _height;
            }

            //左上のX座標
            public T X { get; private set; }
            //左上のY座標
            public T Y { get; private set; }
            //幅
            public T Width { get; private set; }
            //高さ
            public T Height { get; private set; }

        }

        //　描画環境
        public class Context
        {
            // 描画手段
            public Graphics G { get; set; }

            //描画手段
            public Range2d<int> Range { get; set; }
        }

        //既定の描画内容
        private static Content CreateDefaultContent()
        {
            // 既定の行数
            const int numOfRows = 32;
            //既定の列数
            const int numOfColumns =32;

            //既定の枡の幅
            const int width = 40;
            //既定の枡の高さ
            const int height = 40;
            //既定の線の太さ
            const int thickness = 1;
            //既定の線の色の赤成分
            const int red = 0;
            //既定の線の色の緑成分
            const int green = 0;
            //既定の線の色の青成分
            const int blue = 0;

            //既定の線の色
            Color color = Color.FromArgb(red, green, blue);

            //既定の描画内容を作成する
            Content content = new Content();
            //既定の描画内容を設定する
            content.NumOfRows = numOfRows;
            content.NumOfColumns = numOfColumns;
            content.Width = width;
            content.Height = height;
            content.Thickness = thickness;
            content.Color = color;

            //既定の描画内容を返す
            return content;
        }

        //描画を実行
        // 描画内容，描画環境
        private static void Draw(Context context, Content content)
        {
            // 描画手段
            Graphics g = context.G;

            //描画範囲
            Range2d<int> range = context.Range;

            //範囲外に描画されるのを防ぐ
            g.SetClip(new Rectangle(range.X, range.Y, range.Width, range.Height));

            //マスの境界の線を描画しなければならない場合
            if ((content.Thickness != 0) && (content.Color != Color.Empty) && (content.Color != Color.Transparent))
            {
                //枡と枡の境界の全体の幅
                int width = content.Width * content.NumOfColumns + content.Thickness * (content.NumOfColumns + 1);
                //枡と枡の境界の全体の高さ
                int height = content.Height * content.NumOfRows + content.Thickness * (content.NumOfRows + 1);

                using (SolidBrush brush = new SolidBrush(content.Color))
                {
                    //縦線の描画
                    for (int i = 0, x = range.X; i <= content.NumOfColumns; i++, x += content.Thickness + content.Width)
                        g.FillRectangle(brush, x, range.Y, content.Thickness, height);

                    for (int i = 0, y = range.Y; i <= content.NumOfRows; i++, y += content.Thickness + content.Height)
                        g.FillRectangle(brush, range.X, y, width, content.Thickness);

                }
            }

        }

        public static void Main()
        {

            const string title = "テスト";

            const int width = 800;
            const int height = 600;

            using (test4 form = new test4())
            {
                //フォーム名を設定する
                form.Text = title;
                //フォームの大きさを設定する
                form.Width = width;
                form.Height = height;
                //描画を実行しなければならない時
                //将棋盤を描画する
                form.Paint += (sender, e) =>
                {
                    //描画環境を作成する
                    Context context = new Context();
                    context.G = e.Graphics;
                    //描画範囲はフォームのクライアント領域の全面
                    context.Range = new Range2d<int>(0, 0, form.ClientSize.Width, form.ClientSize.Height);

                    //描画内容を作成する
                    Content content = CreateDefaultContent();

                    //描画を実行する
                    Draw(context, content);
                };


                Application.Run(form);
            }

        }

        private void test4_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
