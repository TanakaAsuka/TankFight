using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank2
{
    internal class GameFramework
    {
        public static Graphics g;
        public static void Start() {
            GameObjectManager.Start();
            GameObjectManager.CreateMap();
            GameObjectManager.CreateMyTank();
            GameObjectManager.CreateBoss();
        
        }
        public static void Update() {
            //FPS120
            GameObjectManager.Update();
        }
        
    }
}
