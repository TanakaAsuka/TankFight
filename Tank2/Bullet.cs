using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank2.Properties;

namespace Tank2
{
    enum Tag { 
        MyTank,
        EnemyTank
    }

    internal class Bullet:Movething
    {
        public Tag Tag { get; set; }
        public bool IsDestroy { get; set; }
        public Bullet(int x, int y, int speed,Direction dir,Tag tag)
        {
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            IsDestroy = false;
            this.BitmapUp = Resources.BulletUp;
            this.BitmapDown = Resources.BulletDown;
            this.BitmapLeft = Resources.BulletLeft;
            this.BitmapRight = Resources.BulletRight;
            this.Dir = dir;
            this.Tag = tag;
            this.X-=Width/2;
            this.Y-=Height/2;

        }


        public override void DrawSelf()
        {

            base.DrawSelf();
        }


        private void MoveCheck()
        {
            #region//检查有没有超出窗体边界
            if (Dir == Direction.Up)
            {
                if (Y +Height/2+3 < 0)
                {
                    IsDestroy=true; return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y  + Height/2-3 > 450)
                {
                    IsDestroy = true; return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X - Speed < 0)
                {
                    IsDestroy = true; return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Speed + Width > 450)
                {
                    IsDestroy = true; return;
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

        private void ChangeDirection() { }
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
