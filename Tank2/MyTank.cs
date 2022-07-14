using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tank2.Properties;

namespace Tank2
{
    internal class MyTank:Movething
    {
        public bool IsMoving { get; set; }
        public int HP { get; set; }
        private int originalX;
        private int originalY;
        public MyTank(int x,int y,int speed) { 
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            originalX = x;
            originalY = y;
            this.BitmapUp = Resources.MyTankUp;
            this.BitmapDown = Resources.MyTankDown;
            this.BitmapLeft = Resources.MyTankLeft;
            this.BitmapRight = Resources.MyTankRight;
            this.Dir = Direction.Up;
            IsMoving = false;
            HP = 2;

        }
        private void MoveCheck()
        {
            #region//检查有没有超出窗体边界
            if (Dir == Direction.Up)
            {
                if (Y - Speed < 0)
                {
                    IsMoving = false; return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + Speed + Height > 450)
                {
                    IsMoving = false; return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X - Speed < 0)
                {
                    IsMoving = false; return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Speed + Width > 450)
                {
                    IsMoving = false; return;
                }
            }
            #endregion

            Rectangle rect=GetRectangle();
            switch (Dir) {
                case Direction.Up:
                    rect.Y -= Speed;
                    break;
                case Direction.Down:
                    rect.Y += Speed;
                    break;
                case Direction.Left:
                    rect.X -= Speed;
                    break;
                case Direction.Right:
                    rect.X += Speed;
                    break;
            }

            //检查有没有和其他元素发生碰撞
            if (GameObjectManager.IsColliedWall(rect) !=null) { 
                IsMoving=false; return;
            } ;
            if (GameObjectManager.IsColliedSteel(rect) != null)
            {
                IsMoving = false; return;
            };
            if (GameObjectManager.IsColliedBoss(rect))
            {
                IsMoving = false; return;
            };
        }
        private void Move()
        {
            if (!IsMoving) return;
            switch (Dir)
            {
                case Direction.Up:
                    Y -= Speed;
                    break;
                case Direction.Down:
                    Y += Speed;
                    break;
                case Direction.Left:
                    X -= Speed;
                    break;
                case Direction.Right:
                    X += Speed;
                    break;
            }

        }

        public override void Update()
        {
            MoveCheck();
            Move();
            base.Update();
        }
        public  void KeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode) { 
                case Keys.W:
                    Dir = Direction.Up;
                    IsMoving = true;
                    break;
                case Keys.S:
                    Dir = Direction.Down;
                    IsMoving = true;
                    break;
                case Keys.A:
                    Dir = Direction.Left;
                    IsMoving = true;
                    break;
                case Keys.D:
                    Dir = Direction.Right;
                    IsMoving = true;
                    break;
                case Keys.Space:
                    //发射子弹
                    Attack();
                    
                    
                    break;
            }
        }
        private void Attack() {
            int x = this.X;
            int y = this.Y;
            switch (Dir)
            {
                case Direction.Up:
                    x = x + Width / 2;
                    break;
                case Direction.Down:
                    x = x + Width / 2;
                    y=y + Height;
                    break;
                case Direction.Left:
                    y = y + Height / 2;
                    break;
                case Direction.Right:
                    x = x + Width;
                    y = y + Height/2;
                    break;
            }
            GameObjectManager.CreateBullet(x,y,Dir,Tag.MyTank);
        }

        public void TakeDamage() {
            HP--;
            if (HP == 0) {
                X = originalX;
                Y = originalY;
                HP = 2;
            }
        }
        public  void KeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    IsMoving = false;
                    break;
                case Keys.S:
                    IsMoving = false;
                    break;
                case Keys.A:
                    IsMoving = false;
                    break;
                case Keys.D:
                    IsMoving = false;
                    break;
            }
        }

        

       

    }
}
