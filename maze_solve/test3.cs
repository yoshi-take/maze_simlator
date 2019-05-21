using System;
using System.Windows.Forms;

class Sample5
{
    static Form fm;
    static Label lb;
    static Button bt;

    public static void Main()
    {
        fm = new Form();
        fm.Text = "サンプル";
        fm.Width = 200; fm.Height = 100;

        lb = new Label();
        lb.Text = "いらっしゃい";
        lb.Dock = DockStyle.Top;

        bt = new Button();
        bt.Text = "購入";
        bt.Dock = DockStyle.Bottom;
        bt.Click += new EventHandler(bt_Click);

        lb.Parent = fm;
        lb.Parent = fm;
        Application.Run(fm);

    }

    public static void bt_Click(object sender,EventArgs e)
    {
        lb.Text = "ありがとね";
        lb.Enabled = false;
    }

}