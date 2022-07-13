using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank2
{
    internal class EnemyTank:Movething
    {
        Random r = new Random();
        public EnemyTank(int x, int y, int speed,Bitmap bmpDown, Bitmap bmpUp,Bitmap bmpLeft,Bitmap bmpRight)
        {
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            this.BitmapUp = bmpUp;
            this.BitmapDown = bmpDown;
            this.BitmapLeft = bmpLeft;
            this.BitmapRight = bmpRight;
            this.Dir = Direction.Down;

        }

        private void MoveCheck()
        {
            #region//检查有没有超出窗体边界
            if (Dir == Direction.Up)
            {
                if (Y - Speed < 0)
                {
                    ChangeDirection(); return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + Speed + Height > 450)
                {
                    ChangeDirection(); return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X - Speed < 0)
                {
                    ChangeDirection(); return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Speed + Width > 450)
                {
                    ChangeDirection(); return;
                }
            }
            #endregion

            //检查有没有和其他元素发生碰撞
            Rectangle rect = GetRectangle();
            switch (Dir)
            {
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
            if (GameObjectManager.IsColliedWall(rect) != null)
            {
                ChangeDirection(); return;
            };
            if (GameObjectManager.IsColliedSteel(rect) != null)
            {
                ChangeDirection(); return;
            };
            if (GameObjectManager.IsColliedBoss(rect))
            {
                ChangeDirection(); return;
            };

        }
        private void ChangeDirection() { 
            while (true) {
                Direction dir = (Direction)r.Next(0, 4);
                if (dir == Dir){ continue; }
                {
                    Dir = dir; break;
                }
                
            }
            MoveCheck();
        }
        private void Move()
        {
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
    }
}
