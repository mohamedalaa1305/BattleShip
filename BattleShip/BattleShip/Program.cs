using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BattleShip
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new main_menu());
        }
        public class Ship
        {
            public int health = new int();
            public int length = new int();
            public string name;
            public Dictionary<string, KeyValuePair<int, int>> pos = new Dictionary<string, KeyValuePair<int, int>>();
        }
        public class Player
        {
            public Ship ship0 = new Ship();
            public Ship ship1 = new Ship();
            public Ship ship2 = new Ship();
            public Ship ship3 = new Ship();
            public Ship ship4 = new Ship();
            public Player()
            {
                ship0.health = 2; ship0.length = 2; ship0.name = "Patrol Boat";
                ship1.health = 3; ship1.length = 3; ship1.name = "Submarine";
                ship2.health = 3; ship2.length = 3; ship2.name = "Destroyer";
                ship3.health = 4; ship3.length = 4; ship3.name = "Battleship";
                ship4.health = 5; ship4.length = 5; ship4.name = "Aircraft Carrier";
            }
        }
        public class Newgame
        {
            public int[,] humangrid = new int[10, 10];
            public int[,] computergrid = new int[10, 10];
            public Player human = new Player();
            public Player computer = new Player();
            public int ship_to_deploy=new int();
            public int ship_to_undeploy = new int();
            public bool ship_direction = new bool();
            public int computer_last_hit,lastx,lasty;
            public Dictionary<string, KeyValuePair<int, int>> humanbtnsmp = new Dictionary<string, KeyValuePair<int, int> >();
            public Dictionary<string, KeyValuePair<int, int>> computerbtnsmp = new Dictionary<string, KeyValuePair<int, int>>();
            public Dictionary<KeyValuePair<int, int>, string> humancellkey = new Dictionary<KeyValuePair<int, int>, string>();
            public Dictionary<KeyValuePair<int, int>, string> computercellkey = new Dictionary<KeyValuePair<int, int>, string>();
            public Newgame()
            {
                computer_last_hit = -1;
                lastx = -1;
                lasty = -1;
               ship_direction = true;
                ship_to_deploy = -1;
                ship_to_undeploy = -1;
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        humangrid[i, j] = -1;
                        computergrid[i, j] = -1;
                    }
                }
            }
        }
        public class AI
        {
            public bool first_time = new bool();
            public int x = new int();
            public int y = new int();
            public List<KeyValuePair<int,int>>nxt=new List<KeyValuePair<int,int>>();
            public AI()
            {
                nxt.Clear();
                first_time = true;
            }
        }
    }
}
