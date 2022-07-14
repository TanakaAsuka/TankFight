using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank2.Properties;

namespace Tank2
{
    enum GameState { 
        Running,
        GameOver
    }
    internal class GameFramework
    {
        public static Graphics g;
        private static GameState gameState=GameState.Running;
        public static void Start() {
            GameObjectManager.Start();
            GameObjectManager.CreateMap();
            GameObjectManager.CreateMyTank();
            GameObjectManager.CreateBoss();
        
        }
        private static void GameOverUpdate() {
            int x = 450/2 - Resources.GameOver.Width / 2;
            int y = 450/2 - Resources.GameOver.Height / 2; ;
            g.DrawImage(Resources.GameOver, x, y);
        }
        public static void Update() {
            //FPS120

            if (gameState == GameState.Running)
            {
                GameObjectManager.Update();
            }
            else if (gameState == GameState.GameOver) {
                GameOverUpdate();
            }
        }

        public static void ChangeToGameOver() { 
            gameState= GameState.GameOver;
        }
        
    }
}
