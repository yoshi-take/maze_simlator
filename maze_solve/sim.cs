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


    public partial class sim : Form
    {

        const int X_offset = 10;
        const int Y_offset = 10;

        public static void Main()
        {
            Application.Run(new sim());
        }

        public sim()
        {
            InitializeComponent();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void panel1_Paint(object sender, PaintEventArgs e)
        {
            Context context = new Context();
            context.G = e.Graphics;
            context.Range = new Range2d<int>(0, 0, panel1.Size.Width, panel1.Size.Height);
            //Content content = CreateDefaultContent();
            Content content = GridContent();
            //描画を実行
            DrawGrid(context, content);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        /* === 以下は壁の描画関係 === */
        /*壁*/
        public class Wall_Button:System.Windows.Forms.Button
        {
        
            public Wall_Button(int x, int y)
            {
                this.BackColor      = Color.Black;    // 黒

                // サイズ
                this.Width          = 10;        // 横       
                this.Height         = 1;       // 縦

                // 位置

                //クリックしたときに行うこと
                this.Click += new EventHandler(bt_Click);

            }

            public void bt_Click(Object sender, EventArgs e)
            {

            }

        }

        /* === 以下は描画関係 === */
        /* 描画内容 */
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

            //妥当な行数か？(※32マスを超えないようにする)
            public static bool IsValidNumOfRows(int numOfRows)
            {
                return ((numOfRows > 0)&&(numOfRows<33));
            }
            //妥当な列数か？(※32マスを超えないようにする)
            public static bool IsValidNumOfColumns(int numOfColumns)
            {
                return ((numOfColumns > 0) && (numOfColumns < 33));
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

        /* 範囲を表す */
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

        /* 描画環境 */
        public class Context
        {
            // 描画手段
            public Graphics G { get; set; }
            //描画範囲
            public Range2d<int> Range { get; set; }
        }

        /* 既定の描画内容 */
        public static Content CreateDefaultContent()
        {
            // 既定の行数
            const int numOfRows = 16;
            //既定の列数
            const int numOfColumns = 16;

            //既定の枡の幅
            const int width = 28;
            //既定の枡の高さ
            const int height = 28;
            //既定の線の太さ
            const int thickness = 1;
            //既定の線の色の赤成分
            const int red = 255;
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

        /* 描画処理 */
        public static void Draw(Context context, Content content)
        {
            //手段
            Graphics g = context.G;
            //描画範囲
            Range2d<int> range = context.Range;

            //描画範囲外に描画されないようにする
          //  g.SetClip(new Rectangle(range.X, range.Y, range.Width, range.Height));

            //枡と枡の境界の全体の幅
            int width = content.Width * content.NumOfColumns + content.Thickness * (content.NumOfColumns + 1);
            //枡と枡の境界の全体の高さ
            int height = content.Height * content.NumOfRows + content.Thickness * (content.NumOfRows + 1);

            using (SolidBrush brush = new SolidBrush(content.Color))
            {
                //縦線の描画
                for (int i = 0, x = range.X ; 
                        i <= content.NumOfColumns ;
                        i++, x += content.Thickness + content.Width)
                            
                            g.FillRectangle( brush , x , range.Y , content.Thickness , height );    //ブラシ，X座標，Y座標，幅，高さ　
                
                //横線の描画
                for (int i = 0, y = range.Y;
                        i <= content.NumOfRows; 
                        i++, y += content.Thickness + content.Height)
                    
                            g.FillRectangle( brush , range.X , y , width , content.Thickness );      //ブラシ，X座標，Y座標，幅，高さ
            }

        }

        /* 柱を描画する */
        public static void DrawGrid(Context context, Content content)
        {
            //手段
            Graphics g = context.G;
            //描画範囲
            Range2d<int> range = context.Range;

            //描画範囲外に描画されないようにする
            //  g.SetClip(new Rectangle(range.X, range.Y, range.Width, range.Height));

            //枡と枡の境界の全体の幅
            int width = content.Width * content.NumOfColumns + content.Thickness * (content.NumOfColumns + 1);
            //枡と枡の境界の全体の高さ
            int height = content.Height * content.NumOfRows + content.Thickness * (content.NumOfRows + 1);

            using (SolidBrush brush = new SolidBrush(content.Color))
            {               
                // 下方向
                for (int i = 1; i < content.NumOfRows ; i++)
                {
                    //右方向
                    for (int j = 1; j < content.NumOfColumns ; j++)
                    {
                        //縦線の描画
                        g.FillRectangle( brush ,X_offset + j * content.Width ,Y_offset + i * content.Height-5 , content.Thickness, 10);
                    
                        //横線の描画
                        g.FillRectangle( brush ,X_offset + j * content.Width-5 ,Y_offset + i * content.Height , 10,content.Thickness );
                    }
                }

               // g.DrawRectangle(brush, X_offset, Y_offset, X_offset , Y_offset);
                Pen redPen = new Pen(Color.Red,1);
                Rectangle rect = new Rectangle(X_offset, Y_offset, content.Width * content.NumOfColumns, content.Height * content.NumOfRows);
                g.DrawRectangle(redPen,rect);
            }
        }
        
        /* 柱の描画内容 */
        public static Content GridContent()
        {
            // 既定の行数
            const int numOfRows = 16;
            //既定の列数
            const int numOfColumns = 16;

            //既定の枡の幅
            const int width = 28;
            //既定の枡の高さ
            const int height = 28;
            //既定の線の太さ
            const int thickness = 1;

            //既定の線の色
            const int red = 255;        // 赤
            const int green = 0;        // 緑
            const int blue = 0;          // 青 
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
