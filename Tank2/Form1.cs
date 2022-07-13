using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tank2.Properties;

namespace Tank2
{
    public partial class Form1 : Form
    {
        private Thread t;
        private static Graphics windowG;
        private static Bitmap tempBmp;
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "坦克大战";

            windowG = this.CreateGraphics();

            tempBmp = new Bitmap(450, 450);
            Graphics bmpG= Graphics.FromImage(tempBmp);
            GameFramework.g = bmpG;
            t = new Thread(new ThreadStart(GameMainThread));
            t.Start();
        }


        private static void GameMainThread() {
            //GameFramework
            //控制刷新率，每秒120帧
            int sleepTime = 1000 / 60;

            GameFramework.Start();
            while (true) {
                //临时画布作画
                GameFramework.g.Clear(Color.Black);

                GameFramework.Update();
                windowG.DrawImage(tempBmp, 0, 0);
                Thread.Sleep(sleepTime);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            t.Abort();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            GameObjectManager.KeyDown(e);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            GameObjectManager.KeyUp(e);
        }
    }
}
