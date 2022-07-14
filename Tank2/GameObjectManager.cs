using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Tank2.Properties;

namespace Tank2
{
    internal class GameObjectManager
    {
        private static List<NotMovething> wallList = new List<NotMovething>();
        private static List<NotMovething> steelList = new List<NotMovething>();
        private static List<EnemyTank> enemyTankList = new List<EnemyTank>();
        private static List<Bullet> bulletList = new List<Bullet>();
        private static List<Explosion> expList = new List<Explosion>();
        private static NotMovething boss;
        private static MyTank myTank;
        private static int enemyBornSpeed = 60;
        private static int enemyBornCount = 60;
        private static Point[] points = new Point[3];

        public static void Update()
        {
            foreach (NotMovething wall in wallList)
            {
                wall.Update();
            }
            foreach (NotMovething steel in steelList)
            {
                steel.Update();
            }
            for (int i = 0; i < enemyTankList.Count; i++)
            {
                enemyTankList[i].Update();
            }
            /*foreach (Bullet bullet in bulletList)
            {
                bullet.Update();
            }*/
            for (int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].Update();
            }
            foreach (Explosion exp in expList)
            {
                exp.Update();
            }
            CheckAndDestroyBullet();
            CheckAndDestroyExplosion();
            boss.Update();
            myTank.Update();
            EnemyBorn();

        }

        public static void Start() {
            
            points[0].X = 0;
            points[0].Y = 0;
            points[1].X = 7*30;
            points[1].Y = 0;
            points[2].X = 14 * 30;
            points[2].Y = 0;
        }
        public static void CreateBullet(int x,int y,Direction dir,Tag tag) {
            Bullet bullet = new Bullet(x, y, 5, dir, tag);
            bulletList.Add(bullet);
        }
        private static void CheckAndDestroyBullet() { 
            List<Bullet> needToDestroy=new List<Bullet>();
            foreach (Bullet bullet in bulletList) {
                if (bullet.IsDestroy) {
                    needToDestroy.Add(bullet);
                }
            }
            foreach (Bullet bullet in needToDestroy) { 
                bulletList.Remove(bullet);
            }
        }
        private static void CheckAndDestroyExplosion()
        {
            List<Explosion> needToDestroy = new List<Explosion>();
            foreach (Explosion exp in expList)
            {
                if (exp.IsDestroy)
                {
                    needToDestroy.Add(exp);
                }
            }
            foreach (Explosion exp in needToDestroy)
            {
                expList.Remove(exp);
            }
        }

        private static void EnemyBorn() { 
            enemyBornCount++;
            if (enemyBornCount < enemyBornSpeed) return;
            //生成随机位置
            Random rd = new Random();
            int index=rd.Next(0,3);
            Point position=points[index];
            //生成随机敌人
            int enemyType=rd.Next(1,5);
            switch (enemyType) { 
                case 1:
                    CreateEnemyTank(position.X, position.Y,1);
                    break;
                case 2:
                    CreateEnemyTank(position.X, position.Y,2);
                    break;
                case 3:
                    CreateEnemyTank(position.X, position.Y,3);
                    break;
                case 4:
                    CreateEnemyTank(position.X, position.Y,4);
                    break;

            }

            enemyBornCount = 0;
        }

        private static void CreateEnemyTank(int posx,int posy,int type) {

            switch (type) {
                case 1:
                CreateEnemyTank1(posx, posy);
                    break;
                case 2:
                    CreateEnemyTank2(posx, posy);
                    break;
                case 3:
                    CreateEnemyTank3(posx, posy);
                    break;
                case 4:
                    CreateEnemyTank4(posx, posy);
                    break;
            }

            void CreateEnemyTank1(int x, int y)
            {
                EnemyTank tank = new EnemyTank(x, y, 2, Resources.GrayDown, Resources.GrayUp, Resources.GrayLeft, Resources.GrayRight);
                enemyTankList.Add(tank);
            }
             void CreateEnemyTank2(int x, int y)
            {
                EnemyTank tank = new EnemyTank(x, y, 2, Resources.GreenDown, Resources.GreenUp, Resources.GreenLeft, Resources.GreenRight);
                enemyTankList.Add(tank);
            }
            void CreateEnemyTank3(int x, int y)
            {
                EnemyTank tank = new EnemyTank(x, y, 1, Resources.SlowDown, Resources.SlowUp, Resources.SlowLeft, Resources.SlowRight);
                enemyTankList.Add(tank);
            }
            void CreateEnemyTank4(int x, int y)
            {
                EnemyTank tank = new EnemyTank(x, y, 4, Resources.QuickDown, Resources.QuickUp, Resources.QuickLeft, Resources.QuickRight);
                enemyTankList.Add(tank);
            }
        }
        public static void DestroyEnemyTank(EnemyTank enemy) { 
            enemyTankList.Remove(enemy);
        }
        public static void CreateMyTank() {
            int x = 5 * 30;
            int y = 14 * 30;
            myTank = new MyTank(x, y, 2);

        }

        public static void CreateExplosion(int x,int y) {
            expList.Add(new Explosion(x, y));

        }
        public static void CreateMap()
        {

            CreateWall(1, 1, 5,Resources.wall,wallList);
            CreateWall(3, 1, 5, Resources.wall, wallList);
            CreateWall(5, 1, 5, Resources.wall, wallList);
            CreateWall(7, 1, 4, Resources.wall, wallList);
            CreateWall(7, 5, 1, Resources.steel, steelList);
            CreateWall(9, 1, 5, Resources.wall, wallList);
            CreateWall(11, 1, 5, Resources.wall, wallList);
            CreateWall(13, 1, 5, Resources.wall, wallList);

            CreateWall(0, 7, 1, Resources.wall, wallList);
            CreateWall(1, 7, 1, Resources.wall, wallList);
            CreateWall(2, 7, 1, Resources.wall, wallList);
            CreateWall(3, 7, 1, Resources.wall, wallList);
            CreateWall(5, 7, 1, Resources.wall, wallList);
            CreateWall(7, 7, 1, Resources.wall, wallList);
            CreateWall(9, 7, 1, Resources.wall, wallList);   
            CreateWall(11, 7, 1, Resources.wall, wallList);
            CreateWall(12, 7, 1, Resources.wall, wallList);
            CreateWall(13, 7, 1, Resources.wall, wallList);
            CreateWall(14, 7, 1, Resources.wall, wallList);


            CreateWall(1, 8, 1, Resources.steel, steelList);
            CreateWall(13, 8, 1, Resources.steel, steelList);


            CreateWall(0, 14, 1, Resources.wall, wallList);
            CreateWall(1, 13, 1, Resources.wall, wallList);
            CreateWall(1, 14, 1, Resources.wall, wallList);
            CreateWall(2, 12, 1, Resources.wall, wallList);
            CreateWall(2, 13, 1, Resources.wall, wallList);
            CreateWall(3, 11, 1, Resources.wall, wallList);
            CreateWall(3, 12, 1, Resources.wall, wallList);
            CreateWall(4, 10, 1, Resources.wall, wallList);
            CreateWall(4, 11, 1, Resources.wall, wallList);
            CreateWall(5, 9, 1, Resources.wall, wallList);
            CreateWall(5, 10, 1, Resources.wall, wallList);
            CreateWall(6, 8, 1, Resources.wall, wallList);
            CreateWall(6, 9, 1, Resources.wall, wallList);
            CreateWall(7, 7, 3, Resources.wall, wallList);
            CreateWall(8, 8, 1, Resources.wall, wallList);
            CreateWall(8, 9, 1, Resources.wall, wallList);
            CreateWall(9, 9, 1, Resources.wall, wallList);
            CreateWall(9, 10, 1, Resources.wall, wallList);
            CreateWall(10, 10, 1, Resources.wall, wallList);
            CreateWall(10, 11, 1, Resources.wall, wallList);
            CreateWall(11, 11, 1, Resources.wall, wallList);
            CreateWall(11,12, 1, Resources.wall, wallList);
            CreateWall(12, 12, 1, Resources.wall, wallList);
            CreateWall(12, 13, 1, Resources.wall, wallList);
            CreateWall(13, 13, 1, Resources.wall, wallList);
            CreateWall(13, 14, 1, Resources.wall, wallList);
            CreateWall(14, 14, 1, Resources.wall, wallList);
            CreateWall(14, 15, 1, Resources.wall, wallList);
            CreateWall(15, 15, 1, Resources.wall, wallList);

            CreateWall(6, 13, 3, Resources.wall, wallList);
            CreateWall(7, 13, 1, Resources.wall, wallList);
            CreateBoss();
            CreateWall(8, 13, 3, Resources.wall, wallList);


        }

        private static void CreateWall(int x, int y, int count,Bitmap type,List<NotMovething> wallList)
        {
            int xPosition = x * 30;
            int yPosition = y * 30;
            for (int i = yPosition; i < yPosition + count * 30; i += 15)
            {
                //绘制坐标
                //一张wall 15px，四张够长长宽30*30的正方形wall
                //i xPosition,i xPosition+15
                //i++
                NotMovething wall1 = new NotMovething(xPosition, i, type);
                NotMovething wall2 = new NotMovething(xPosition + 15, i, type);

                wallList.Add(wall1);
                wallList.Add(wall2);
            }
        }

        public static void DestroyWall(NotMovething wall) { 
            wallList.Remove(wall);
        }
        public static void CreateBoss()
        {
            int xPosition = 7 * 30;
            int yPosition = 14 * 30;
            boss=new NotMovething(xPosition , yPosition, Resources.Boss);

        }

        public static void KeyUp(KeyEventArgs e)
        {
            myTank.KeyUp(e);
        }
        public static void KeyDown(KeyEventArgs e)
        {
            myTank.KeyDown(e);
        }

        public static NotMovething IsColliedWall(Rectangle rt)
        {
            foreach (NotMovething wall in wallList) {
                if (wall.GetRectangle().IntersectsWith(rt)) {
                    return wall;
                }
            }
            return null;
        }
        public static NotMovething IsColliedSteel(Rectangle rt)
        {
            foreach (NotMovething steel in steelList)
            {
                if (steel.GetRectangle().IntersectsWith(rt))
                {
                    return steel;
                }
            }
            return null;
        }
        public static EnemyTank IsColliedEnemyTank(Rectangle rt)
        {
            foreach (EnemyTank enemy in enemyTankList)
            {
                if (enemy.GetRectangle().IntersectsWith(rt))
                {
                    return enemy;
                }
            }
            return null;
        }

        public static bool IsColliedBoss(Rectangle rt)
        {
            return boss.GetRectangle().IntersectsWith(rt);
        }
    }
}
