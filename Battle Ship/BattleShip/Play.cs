using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Media;
namespace BattleShip
{
    public partial class Play : Form
    {
        public Play()
        {
            InitializeComponent();
        }
        BattleShip.Program.Newgame g = new BattleShip.Program.Newgame();
        BattleShip.Program.AI AI0 = new BattleShip.Program.AI();
        BattleShip.Program.AI AI1 = new BattleShip.Program.AI();
        BattleShip.Program.AI AI2 = new BattleShip.Program.AI();
        BattleShip.Program.AI AI3 = new BattleShip.Program.AI();
        BattleShip.Program.AI AI4 = new BattleShip.Program.AI();
        List<KeyValuePair<int, int>> allcells=new List<KeyValuePair<int, int>>();
        SoundPlayer hit = new SoundPlayer(@"D:\Projects\Projects\Battle Ship\BattleShip\BattleShip\Resources\shot0Sound.wav");
        SoundPlayer miss = new SoundPlayer(@"D:\Projects\Projects\Battle Ship\BattleShip\BattleShip\Resources\splash0Sound.wav");
        SoundPlayer victory = new SoundPlayer(@"D:\Projects\Projects\Battle Ship\BattleShip\BattleShip\Resources\victorySound.wav");
        public void link_buttons()
        {
            g.computerbtnsmp.Clear();
            g.humanbtnsmp.Clear();
            g.humancellkey.Clear();
            g.computercellkey.Clear();
            foreach (Control c in Controls)
            {
                if (c.Name[0] != c.Name[1] && (c.Name.Length == 2 || c.Name.Length == 3))
                {
                    string n = c.Name;
                    int x, y;
                    x = n[0] - 'A';
                    y = n[1] - '0' - 1;
                    if (n.Length == 3)
                        y = 9;
                    g.humanbtnsmp.Add(n, new KeyValuePair<int, int>(x, y));
                    g.humancellkey.Add(new KeyValuePair<int, int>(x, y), n);
                }
                else if(c.Name[0]==c.Name[1]&&(c.Name.Length==3||c.Name.Length==4))
                {
                    string n = c.Name;
                    int x = n[0] - 'A';
                    int y = n[2] - '0';
                    if (n.Length == 4)
                        y = 10;
                    g.computercellkey.Add(new KeyValuePair<int, int>(x, 10-y), n);
                    g.computerbtnsmp.Add(n, new KeyValuePair<int, int>(x,10- y));
                }
            }
            allcells.Clear();
            for(int i=0; i<10; i++)
            {
                for(int j=0; j<10; j++)
                {
                    g.humangrid[i, j] = -1;
                    g.computergrid[i, j] = -1;
                    allcells.Add(new KeyValuePair<int, int>(i, j));
                }
            }
            AI0.first_time = true;
            AI0.nxt.Clear();
            AI1.first_time = true;
            AI1.nxt.Clear();
            AI2.first_time = true;
            AI2.nxt.Clear();
            AI3.first_time = true;
            AI3.nxt.Clear();
            AI4.first_time = true;
            AI4.nxt.Clear();

        }
        public void computerdeploy()
        {
            while(true)
            {
                Random xr = new Random();
                Random yr = new Random();
                Random dr = new Random();
                int x = xr.Next(0, 10);
                int y = yr.Next(0, 10);
                int d = dr.Next(0, 2);
                if (x>-1&&x<10&&y>-1&&y<10)
                {
                    if(g.computergrid[x,y]==-1)
                    {
                        if(d==1)
                        {
                            int cntr = 0,cntl=0;
                            for(int i=y; i<10&&cntr<g.computer.ship0.length; i++)
                            {
                                if (g.computergrid[x, i]!=-1)
                                    break;
                                cntr++;
                            }
                            for (int i = y; i >-1 && cntl < g.computer.ship0.length; i--)
                            {
                                if (g.computergrid[x,i]!=-1)
                                    break;
                                cntl++;
                            }
                            if(cntr==g.computer.ship0.length)
                            {
                                for(int i=y; i<y+g.computer.ship0.length; i++)
                                {
                                    g.computergrid[x, i] = 0;
                                    KeyValuePair<int, int> p = new KeyValuePair<int, int>(x,i);
                                    g.computer.ship0.pos.Add(g.computercellkey[p], p);
                                }
                                //richTextBox1.Text += "ship0 deployed successfully\n";
                                break;
                            }
                            else if(cntl==g.computer.ship0.length)
                            {
                                for (int i = y; i > y - g.computer.ship0.length; i--)
                                {
                                    g.computergrid[x, i] = 0;
                                    KeyValuePair<int, int> p = new KeyValuePair<int, int>(x, i);
                                    g.computer.ship0.pos.Add(g.computercellkey[p], p);
                                }
                                //richTextBox1.Text += "ship0 deployed successfully\n";
                                break;
                            }
                        }
                        else
                        {
                            int cntu = 0, cntd = 0;
                            for (int i = x; i < 10 && cntd < g.computer.ship0.length; i++)
                            {
                                if (g.computergrid[i, y] != -1)
                                    break;
                                cntd++;
                            }
                            for (int i = x; i > -1 && cntu < g.computer.ship0.length; i--)
                            {
                                if (g.computergrid[i,y] != -1)
                                    break;
                                cntu++;
                            }
                            if (cntd == g.computer.ship0.length)
                            {
                                for (int i = x; i < x + g.computer.ship0.length; i++)
                                {
                                    g.computergrid[i, y] = 0;
                                    KeyValuePair<int, int> p = new KeyValuePair<int, int>(i, y);
                                    g.computer.ship0.pos.Add(g.computercellkey[p], p);
                                }
                                //richTextBox1.Text += "ship0 deployed successfully\n";
                                break;
                            }
                            else if (cntu == g.computer.ship0.length)
                            {
                                for (int i = x; i > x - g.computer.ship0.length; i--)
                                {
                                    g.computergrid[i, y] = 0;
                                    KeyValuePair<int, int> p = new KeyValuePair<int, int>(i, y);
                                    g.computer.ship0.pos.Add(g.computercellkey[p], p);
                                }
                                //richTextBox1.Text += "ship0 deployed successfully\n";
                                break;
                            }
                        }
                    }
                }
            }
            while (true)
            {
                Random xr = new Random();
                Random yr = new Random();
                Random dr = new Random();
                int x = xr.Next(0, 10);
                int y = yr.Next(0, 10);
                int d = dr.Next(0, 2);
                if (x > -1 && x < 10 && y > -1 && y < 10)
                {
                    if (g.computergrid[x, y] == -1)
                    {
                        if (d == 1)
                        {
                            int cntr = 0, cntl = 0;
                            for (int i = y; i < 10 && cntr < g.computer.ship1.length; i++)
                            {
                                if (g.computergrid[x, i] != -1)
                                    break;
                                cntr++;
                            }
                            for (int i = y; i > -1 && cntl < g.computer.ship1.length; i--)
                            {
                                if (g.computergrid[x, i] != -1)
                                    break;
                                cntl++;
                            }
                            if (cntr == g.computer.ship1.length)
                            {
                                for (int i = y; i < y + g.computer.ship1.length; i++)
                                {
                                    g.computergrid[x, i] = 1;
                                    KeyValuePair<int, int> p = new KeyValuePair<int, int>(x, i);
                                    g.computer.ship1.pos.Add(g.computercellkey[p], p);
                                }
                                //richTextBox1.Text += "ship1 deployed successfully\n";
                                break;
                            }
                            else if (cntl == g.computer.ship1.length)
                            {
                                for (int i = y; i > y - g.computer.ship1.length; i--)
                                {
                                    g.computergrid[x, i] = 1;
                                    KeyValuePair<int, int> p = new KeyValuePair<int, int>(x, i);
                                    g.computer.ship1.pos.Add(g.computercellkey[p], p);
                                }
                                //richTextBox1.Text += "ship1 deployed successfully\n";
                                break;
                            }
                        }
                        else
                        {
                            int cntu = 0, cntd = 0;
                            for (int i = x; i < 10 && cntd < g.computer.ship1.length; i++)
                            {
                                if (g.computergrid[i, y] != -1)
                                    break;
                                cntd++;
                            }
                            for (int i = x; i > -1 && cntu < g.computer.ship1.length; i--)
                            {
                                if (g.computergrid[i, y] != -1)
                                    break;
                                cntu++;
                            }
                            if (cntd == g.computer.ship1.length)
                            {
                                for (int i = x; i < x + g.computer.ship1.length; i++)
                                {
                                    g.computergrid[i, y] = 1;
                                    KeyValuePair<int, int> p = new KeyValuePair<int, int>(i, y);
                                    g.computer.ship1.pos.Add(g.computercellkey[p], p);
                                }
                                //richTextBox1.Text += "ship1 deployed successfully\n";
                                break;
                            }
                            else if (cntu == g.computer.ship1.length)
                            {
                                for (int i = x; i > x - g.computer.ship1.length; i--)
                                {
                                    g.computergrid[i, y] = 1;
                                    KeyValuePair<int, int> p = new KeyValuePair<int, int>(i, y);
                                    g.computer.ship1.pos.Add(g.computercellkey[p], p);
                                }
                                //richTextBox1.Text += "ship1 deployed successfully\n";
                                break;
                            }
                        }
                    }
                }
            }
            while (true)
            {
                Random xr = new Random();
                Random yr = new Random();
                Random dr = new Random();
                int x = xr.Next(0, 10);
                int y = yr.Next(0, 10);
                int d = dr.Next(0, 2);
                if (x > -1 && x < 10 && y > -1 && y < 10)
                {
                    if (g.computergrid[x, y] == -1)
                    {
                        if (d == 1)
                        {
                            int cntr = 0, cntl = 0;
                            for (int i = y; i < 10 && cntr < g.computer.ship2.length; i++)
                            {
                                if (g.computergrid[x, i] != -1)
                                    break;
                                cntr++;
                            }
                            for (int i = y; i > -1 && cntl < g.computer.ship2.length; i--)
                            {
                                if (g.computergrid[x, i] != -1)
                                    break;
                                cntl++;
                            }
                            if (cntr == g.computer.ship2.length)
                            {
                                for (int i = y; i < y + g.computer.ship2.length; i++)
                                {
                                    g.computergrid[x, i] = 2;
                                    KeyValuePair<int, int> p = new KeyValuePair<int, int>(x, i);
                                    g.computer.ship2.pos.Add(g.computercellkey[p], p);
                                }
                                //richTextBox1.Text += "ship2 deployed successfully\n";
                                break;
                            }
                            else if (cntl == g.computer.ship2.length)
                            {
                                for (int i = y; i > y - g.computer.ship2.length; i--)
                                {
                                    g.computergrid[x, i] = 2;
                                    KeyValuePair<int, int> p = new KeyValuePair<int, int>(x, i);
                                    g.computer.ship2.pos.Add(g.computercellkey[p], p);
                                }
                                //richTextBox1.Text += "ship2 deployed successfully\n";
                                break;
                            }
                        }
                        else
                        {
                            int cntu = 0, cntd = 0;
                            for (int i = x; i < 10 && cntd < g.computer.ship2.length; i++)
                            {
                                if (g.computergrid[i, y] != -1)
                                    break;
                                cntd++;
                            }
                            for (int i = x; i > -1 && cntu < g.computer.ship2.length; i--)
                            {
                                if (g.computergrid[i, y] != -1)
                                    break;
                                cntu++;
                            }
                            if (cntd == g.computer.ship2.length)
                            {
                                for (int i = x; i < x + g.computer.ship2.length; i++)
                                {
                                    g.computergrid[i, y] = 2;
                                    KeyValuePair<int, int> p = new KeyValuePair<int, int>(i, y);
                                    g.computer.ship2.pos.Add(g.computercellkey[p], p);
                                }
                               // richTextBox1.Text += "ship2 deployed successfully\n";
                                break;
                            }
                            else if (cntu == g.computer.ship2.length)
                            {
                                for (int i = x; i > x - g.computer.ship2.length; i--)
                                {
                                    g.computergrid[i, y] = 2;
                                    KeyValuePair<int, int> p = new KeyValuePair<int, int>(i, y);
                                    g.computer.ship2.pos.Add(g.computercellkey[p], p);
                                }
                               // richTextBox1.Text += "ship2 deployed successfully\n";
                                break;
                            }
                        }
                    }
                }
            }
            while (true)
            {
                Random xr = new Random();
                Random yr = new Random();
                Random dr = new Random();
                int x = xr.Next(0, 10);
                int y = yr.Next(0, 10);
                int d = dr.Next(0, 2);
                if (x > -1 && x < 10 && y > -1 && y < 10)
                {
                    if (g.computergrid[x, y] == -1)
                    {
                        if (d == 1)
                        {
                            int cntr = 0, cntl = 0;
                            for (int i = y; i < 10 && cntr < g.computer.ship3.length; i++)
                            {
                                if (g.computergrid[x, i] != -1)
                                    break;
                                cntr++;
                            }
                            for (int i = y; i > -1 && cntl < g.computer.ship3.length; i--)
                            {
                                if (g.computergrid[x, i] != -1)
                                    break;
                                cntl++;
                            }
                            if (cntr == g.computer.ship3.length)
                            {
                                for (int i = y; i < y + g.computer.ship3.length; i++)
                                {
                                    g.computergrid[x, i] = 3;
                                    KeyValuePair<int, int> p = new KeyValuePair<int, int>(x, i);
                                    g.computer.ship3.pos.Add(g.computercellkey[p], p);
                                }
                               // richTextBox1.Text += "ship3 deployed successfully\n";
                                break;
                            }
                            else if (cntl == g.computer.ship3.length)
                            {
                                for (int i = y; i > y - g.computer.ship3.length; i--)
                                {
                                    g.computergrid[x, i] = 3;
                                    KeyValuePair<int, int> p = new KeyValuePair<int, int>(x, i);
                                    g.computer.ship3.pos.Add(g.computercellkey[p], p);
                                }
                                //richTextBox1.Text += "ship3 deployed successfully\n";
                                break;
                            }
                        }
                        else
                        {
                            int cntu = 0, cntd = 0;
                            for (int i = x; i < 10 && cntd < g.computer.ship3.length; i++)
                            {
                                if (g.computergrid[i, y] != -1)
                                    break;
                                cntd++;
                            }
                            for (int i = x; i > -1 && cntu < g.computer.ship3.length; i--)
                            {
                                if (g.computergrid[i, y] != -1)
                                    break;
                                cntu++;
                            }
                            if (cntd == g.computer.ship3.length)
                            {
                                for (int i = x; i < x + g.computer.ship3.length; i++)
                                {
                                    g.computergrid[i, y] = 3;
                                    KeyValuePair<int, int> p = new KeyValuePair<int, int>(i, y);
                                    g.computer.ship3.pos.Add(g.computercellkey[p], p);
                                }
                                //richTextBox1.Text += "ship3 deployed successfully\n";
                                break;
                            }
                            else if (cntu == g.computer.ship3.length)
                            {
                                for (int i = x; i > x - g.computer.ship3.length; i--)
                                {
                                    g.computergrid[i, y] = 3;
                                    KeyValuePair<int, int> p = new KeyValuePair<int, int>(i, y);
                                    g.computer.ship3.pos.Add(g.computercellkey[p], p);
                                }
                                //richTextBox1.Text += "ship3 deployed successfully\n";
                                break;
                            }
                        }
                    }
                }
            }
            while (true)
            {
                Random xr = new Random();
                Random yr = new Random();
                Random dr = new Random();
                int x = xr.Next(0, 10);
                int y = yr.Next(0, 10);
                int d = dr.Next(0, 2);
                if (x > -1 && x < 10 && y > -1 && y < 10)
                {
                    if (g.computergrid[x, y] == -1)
                    {
                        if (d == 1)
                        {
                            int cntr = 0, cntl = 0;
                            for (int i = y; i < 10 && cntr < g.computer.ship4.length; i++)
                            {
                                if (g.computergrid[x, i] != -1)
                                    break;
                                cntr++;
                            }
                            for (int i = y; i > -1 && cntl < g.computer.ship4.length; i--)
                            {
                                if (g.computergrid[x, i] != -1)
                                    break;
                                cntl++;
                            }
                            if (cntr == g.computer.ship4.length)
                            {
                                for (int i = y; i < y + g.computer.ship4.length; i++)
                                {
                                    g.computergrid[x, i] = 4;
                                    KeyValuePair<int, int> p = new KeyValuePair<int, int>(x, i);
                                    g.computer.ship4.pos.Add(g.computercellkey[p], p);
                                }
                                //richTextBox1.Text += "ship4 deployed successfully\n";
                                break;
                            }
                            else if (cntl == g.computer.ship4.length)
                            {
                                for (int i = y; i > y - g.computer.ship4.length; i--)
                                {
                                    g.computergrid[x, i] = 4;
                                    KeyValuePair<int, int> p = new KeyValuePair<int, int>(x, i);
                                    g.computer.ship4.pos.Add(g.computercellkey[p], p);
                                }
                                //richTextBox1.Text += "ship4 deployed successfully\n";
                                break;
                            }
                        }
                        else
                        {
                            int cntu = 0, cntd = 0;
                            for (int i = x; i < 10 && cntd < g.computer.ship4.length; i++)
                            {
                                if (g.computergrid[i, y] != -1)
                                    break;
                                cntd++;
                            }
                            for (int i = x; i > -1 && cntu < g.computer.ship4.length; i--)
                            {
                                if (g.computergrid[i, y] != -1)
                                    break;
                                cntu++;
                            }
                            if (cntd == g.computer.ship4.length)
                            {
                                for (int i = x; i < x + g.computer.ship4.length; i++)
                                {
                                    g.computergrid[i, y] = 4;
                                    KeyValuePair<int, int> p = new KeyValuePair<int, int>(i, y);
                                    g.computer.ship4.pos.Add(g.computercellkey[p], p);
                                }
                                //richTextBox1.Text += "ship4 deployed successfully\n";
                                break;
                            }
                            else if (cntu == g.computer.ship4.length)
                            {
                                for (int i = x; i > x - g.computer.ship4.length; i--)
                                {
                                    g.computergrid[i, y] = 4;
                                    KeyValuePair<int, int> p = new KeyValuePair<int, int>(i, y);
                                    g.computer.ship4.pos.Add(g.computercellkey[p], p);
                                }
                                //richTextBox1.Text += "ship4 deployed successfully\n";
                                break;
                            }
                        }
                    }
                }
            }
        }
        public void humanundeploy()
        {
            if(g.ship_to_undeploy==0)
            {
                foreach(Control c in Controls)
                {
                    if(g.human.ship0.pos.ContainsKey(c.Name))
                    {
                        c.Text = "";
                        g.humangrid[g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value] = -1;
                    }
                }
                deployShip0Button.Enabled = true;
                deleteShip0Button.Enabled = false;
                g.human.ship0.pos.Clear();
            }
            else if(g.ship_to_undeploy==1)
            {
                foreach (Control c in Controls)
                {
                    if (g.human.ship1.pos.ContainsKey(c.Name))
                    {
                        c.Text = "";
                        g.humangrid[g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value] = -1;
                    }
                }
                deployShip1Button.Enabled = true;
                deleteShip1Button.Enabled = false;
                g.human.ship1.pos.Clear();
            }
            else if (g.ship_to_undeploy == 2)
            {
                foreach (Control c in Controls)
                {
                    if (g.human.ship2.pos.ContainsKey(c.Name))
                    {
                        c.Text = "";
                        g.humangrid[g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value] = -1;
                    }
                }
                deployShip2Button.Enabled = true;
                deleteShip2Button.Enabled = false;
                g.human.ship2.pos.Clear();
            }
            else if (g.ship_to_undeploy == 3)
            {
                foreach (Control c in Controls)
                {
                    if (g.human.ship3.pos.ContainsKey(c.Name))
                    {
                        c.Text = "";
                        g.humangrid[g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value] = -1;
                    }
                }
                deployShip3Button.Enabled = true;
                deleteShip3Button.Enabled = false;
                g.human.ship3.pos.Clear();
            }
            else if (g.ship_to_undeploy == 4)
            {
                foreach (Control c in Controls)
                {
                    if (g.human.ship4.pos.ContainsKey(c.Name))
                    {
                        c.Text = "";
                        g.humangrid[g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value] = -1;
                    }
                }
                deployShip4Button.Enabled = true;
                deleteShip4Button.Enabled = false;
                g.human.ship4.pos.Clear();

            }
        }
        
        // function to deploy human player ships
        public void humandeploy(Button b,int evnt)
        {
            int x = g.humanbtnsmp[b.Name].Key;
            int y = g.humanbtnsmp[b.Name].Value;
            Dictionary<string, bool> ymen = new Dictionary<string, bool>();
            Dictionary<string, bool> shmal = new Dictionary<string, bool>();
            Dictionary<string, bool> fo2 = new Dictionary<string, bool>();
            Dictionary<string, bool> t7t = new Dictionary<string, bool>();
            bool done = false;
            ymen.Clear();shmal.Clear();fo2.Clear();t7t.Clear();
            if (g.ship_to_deploy==0)
            {
                if (g.ship_direction==true)
                {
                    int cntr = 0;
                    int cntl = 0;
                    for(int i=y; i<10&&cntr<g.human.ship0.length; i++)
                    {
                        if (g.humangrid[x, i]!=-1)
                            break;
                        cntr++;
                        KeyValuePair<int, int> p = new KeyValuePair<int, int>(x,i);
                        string s = g.humancellkey[p];
                        ymen.Add(s, true);
                    }
                    for(int i=y; i>-1&&cntl<g.human.ship0.length; i--)
                    {
                        if (g.humangrid[x, i]!=-1)
                            break;
                        cntl++;
                        KeyValuePair<int, int> p = new KeyValuePair<int, int>(x, i);
                        string s = g.humancellkey[p];
                        shmal.Add(s, true);
                    }
                    if(cntr==g.human.ship0.length)
                    {
                        foreach(Control c in Controls)
                        {
                            if(ymen.ContainsKey(c.Name))
                            {
                                if(evnt==1)
                                {
                                    c.Text = "D";
                                    c.ForeColor = Color.Yellow;
                                }
                                else if (evnt==2)
                                {
                                    c.Text = "";
                                }
                                else if(evnt==3)
                                {
                                    done = true;
                                    c.ForeColor = Color.Yellow;
                                    c.Text = "D";
                                    g.humangrid[g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value] = 0;
                                    g.human.ship0.pos.Add(c.Name, new KeyValuePair<int, int>(g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value));
                                    deleteShip0Button.Enabled = true;
                                    deployShip0Button.Enabled = false;
                                    //c.Enabled = false;
                                }
                            }
                        }
                    }
                    else if(cntl==g.human.ship0.length)
                    {
                        foreach (Control c in Controls)
                        {
                            if (shmal.ContainsKey(c.Name))
                            {
                                if (evnt == 1)
                                {
                                    c.Text = "D";
                                    c.ForeColor = Color.Yellow;
                                }
                                else if (evnt == 2)
                                {
                                    c.Text = "";
                                }
                                else if (evnt == 3)
                                {
                                    done = true;
                                    c.ForeColor = Color.Yellow;
                                    c.Text = "D";
                                    //c.Enabled = false;
                                    g.human.ship0.pos.Add(c.Name, new KeyValuePair<int, int>(g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value));
                                    g.humangrid[g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value] = 0;
                                    deleteShip0Button.Enabled = true;
                                    deployShip0Button.Enabled = false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    int cntu = 0;
                    int cntd = 0;
                    for (int i = x; i < 10 && cntd < g.human.ship0.length; i++)
                    {
                        if (g.humangrid[i, y]!=-1)
                            break;
                        cntd++;
                        KeyValuePair<int, int> p = new KeyValuePair<int, int>(i, y);
                        string s = g.humancellkey[p];
                        t7t.Add(s, true);
                    }
                    for (int i = x; i > -1 && cntu < g.human.ship0.length; i--)
                    {
                        if (g.humangrid[i, y]!=-1)
                            break;
                        cntu++;
                        KeyValuePair<int, int> p = new KeyValuePair<int, int>(i, y);
                        string s = g.humancellkey[p];
                        fo2.Add(s, true);
                    }
                    if (cntd == g.human.ship0.length)
                    {
                        foreach (Control c in Controls)
                        {
                            if (t7t.ContainsKey(c.Name))
                            {
                                if (evnt == 1)
                                {
                                    c.Text = "D";
                                    c.ForeColor = Color.Yellow;
                                }
                                else if (evnt == 2)
                                {
                                    c.Text = "";
                                }
                                else if (evnt == 3)
                                {
                                    done = true;
                                    c.ForeColor = Color.Yellow;
                                    c.Text = "D";
                                    g.humangrid[g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value] = 0;
                                    g.human.ship0.pos.Add(c.Name, new KeyValuePair<int, int>(g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value));
                                    deleteShip0Button.Enabled = true;
                                    deployShip0Button.Enabled = false;
                                    //c.Enabled = false;
                                }
                            }
                        }
                    }
                    else if (cntu == g.human.ship0.length)
                    {
                        foreach (Control c in Controls)
                        {
                            if (fo2.ContainsKey(c.Name))
                            {
                                if (evnt == 1)
                                {
                                    c.Text = "D";
                                    c.ForeColor = Color.Yellow;
                                }
                                else if (evnt == 2)
                                {
                                    c.Text = "";
                                }
                                else if (evnt == 3)
                                {
                                    done = true;
                                    c.ForeColor = Color.Yellow;
                                    c.Text = "D";
                                    //c.Enabled = false;
                                    g.human.ship0.pos.Add(c.Name, new KeyValuePair<int, int>(g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value));
                                    g.humangrid[g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value] = 0;
                                    deleteShip0Button.Enabled = true;
                                    deployShip0Button.Enabled = false;
                                }
                            }
                        }
                    }
                }
            }
            if (g.ship_to_deploy == 1)
            {
                if (g.ship_direction == true)
                {
                    int cntr = 0;
                    int cntl = 0;
                    for (int i = y; i < 10 && cntr < g.human.ship1.length; i++)
                    {
                        if (g.humangrid[x, i] !=-1)
                            break;
                        cntr++;
                        KeyValuePair<int, int> p = new KeyValuePair<int, int>(x, i);
                        string s = g.humancellkey[p];
                        ymen.Add(s, true);
                    }
                    for (int i = y; i > -1 && cntl < g.human.ship1.length; i--)
                    {
                        if (g.humangrid[x, i] !=-1)
                            break;
                        cntl++;
                        KeyValuePair<int, int> p = new KeyValuePair<int, int>(x, i);
                        string s = g.humancellkey[p];
                        shmal.Add(s, true);
                    }
                    if (cntr == g.human.ship1.length)
                    {
                        foreach (Control c in Controls)
                        {
                            if (ymen.ContainsKey(c.Name))
                            {
                                if (evnt == 1)
                                {
                                    c.Text = "S";
                                    c.ForeColor = Color.Red;
                                }
                                else if (evnt == 2)
                                {
                                    c.Text = "";
                                }
                                else if (evnt == 3)
                                {
                                    done = true;
                                    c.ForeColor = Color.Red;
                                    c.Text = "S";
                                    g.humangrid[g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value] =1;
                                    g.human.ship1.pos.Add(c.Name, new KeyValuePair<int, int>(g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value));
                                    deleteShip1Button.Enabled = true;
                                    deployShip1Button.Enabled = false;
                                    //c.Enabled = false;
                                }
                            }
                        }
                    }
                    else if (cntl == g.human.ship1.length)
                    {
                        foreach (Control c in Controls)
                        {
                            if (shmal.ContainsKey(c.Name))
                            {
                                if (evnt == 1)
                                {
                                    c.Text = "S";
                                    c.ForeColor = Color.Red;
                                }
                                else if (evnt == 2)
                                {
                                    c.Text = "";
                                }
                                else if (evnt == 3)
                                {
                                    done = true;
                                    c.ForeColor = Color.Red;
                                    c.Text = "S";
                                    //c.Enabled = false;
                                    g.human.ship1.pos.Add(c.Name, new KeyValuePair<int, int>(g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value));
                                    g.humangrid[g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value] = 1;
                                    deleteShip1Button.Enabled = true;
                                    deployShip1Button.Enabled = false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    int cntu = 0;
                    int cntd = 0;
                    for (int i = x; i < 10 && cntd < g.human.ship1.length; i++)
                    {
                        if (g.humangrid[i, y] !=-1)
                            break;
                        cntd++;
                        KeyValuePair<int, int> p = new KeyValuePair<int, int>(i, y);
                        string s = g.humancellkey[p];
                        t7t.Add(s, true);
                    }
                    for (int i = x; i > -1 && cntu < g.human.ship1.length; i--)
                    {
                        if (g.humangrid[i, y] !=-1)
                            break;
                        cntu++;
                        KeyValuePair<int, int> p = new KeyValuePair<int, int>(i, y);
                        string s = g.humancellkey[p];
                        fo2.Add(s, true);
                    }
                    if (cntd == g.human.ship1.length)
                    {
                        foreach (Control c in Controls)
                        {
                            if (t7t.ContainsKey(c.Name))
                            {
                                if (evnt == 1)
                                {
                                    c.Text = "S";
                                    c.ForeColor = Color.Red;
                                }
                                else if (evnt == 2)
                                {
                                    c.Text = "";
                                }
                                else if (evnt == 3)
                                {
                                    done = true;
                                    c.ForeColor = Color.Red;
                                    c.Text = "S";
                                    g.humangrid[g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value] = 1;
                                    g.human.ship1.pos.Add(c.Name, new KeyValuePair<int, int>(g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value));
                                    deleteShip1Button.Enabled = true;
                                    deployShip1Button.Enabled = false;
                                    //c.Enabled = false;
                                }
                            }
                        }
                    }
                    else if (cntu == g.human.ship1.length)
                    {
                        foreach (Control c in Controls)
                        {
                            if (fo2.ContainsKey(c.Name))
                            {
                                if (evnt == 1)
                                {
                                    c.Text = "S";
                                    c.ForeColor = Color.Red;
                                }
                                else if (evnt == 2)
                                {
                                    c.Text = "";
                                }
                                else if (evnt == 3)
                                {
                                    done = true;
                                    c.ForeColor = Color.Red;
                                    c.Text = "S";
                                    //c.Enabled = false;
                                    g.human.ship1.pos.Add(c.Name, new KeyValuePair<int, int>(g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value));
                                    g.humangrid[g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value] = 1;
                                    deleteShip1Button.Enabled = true;
                                    deployShip1Button.Enabled = false;
                                }
                            }
                        }
                    }
                }
            }
            if (g.ship_to_deploy == 2)
            {
                if (g.ship_direction == true)
                {
                    int cntr = 0;
                    int cntl = 0;
                    for (int i = y; i < 10 && cntr < g.human.ship2.length; i++)
                    {
                        if (g.humangrid[x, i] !=-1)
                            break;
                        cntr++;
                        KeyValuePair<int, int> p = new KeyValuePair<int, int>(x, i);
                        string s = g.humancellkey[p];
                        ymen.Add(s, true);
                    }
                    for (int i = y; i > -1 && cntl < g.human.ship2.length; i--)
                    {
                        if (g.humangrid[x, i] !=-1)
                            break;
                        cntl++;
                        KeyValuePair<int, int> p = new KeyValuePair<int, int>(x, i);
                        string s = g.humancellkey[p];
                        shmal.Add(s, true);
                    }
                    if (cntr == g.human.ship2.length)
                    {
                        foreach (Control c in Controls)
                        {
                            if (ymen.ContainsKey(c.Name))
                            {
                                if (evnt == 1)
                                {
                                    c.Text = "U";
                                    c.ForeColor = Color.Lime;
                                }
                                else if (evnt == 2)
                                {
                                    c.Text = "";
                                }
                                else if (evnt == 3)
                                {
                                    done = true;
                                    c.ForeColor = Color.Lime;
                                    c.Text = "U";
                                    g.humangrid[g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value] = 2;
                                    g.human.ship2.pos.Add(c.Name, new KeyValuePair<int, int>(g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value));
                                    deleteShip2Button.Enabled = true;
                                    deployShip2Button.Enabled = false;
                                    //c.Enabled = false;
                                }
                            }
                        }
                    }
                    else if (cntl == g.human.ship2.length)
                    {
                        foreach (Control c in Controls)
                        {
                            if (shmal.ContainsKey(c.Name))
                            {
                                if (evnt == 1)
                                {
                                    c.Text = "U";
                                    c.ForeColor = Color.Lime;
                                }
                                else if (evnt == 2)
                                {
                                    c.Text = "";
                                }
                                else if (evnt == 3)
                                {
                                    done = true;
                                    c.ForeColor = Color.Lime;
                                    c.Text = "U";
                                    //c.Enabled = false;
                                    g.human.ship2.pos.Add(c.Name, new KeyValuePair<int, int>(g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value));
                                    g.humangrid[g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value] = 2;
                                    deleteShip2Button.Enabled = true;
                                    deployShip2Button.Enabled = false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    int cntu = 0;
                    int cntd = 0;
                    for (int i = x; i < 10 && cntd < g.human.ship2.length; i++)
                    {
                        if (g.humangrid[i, y] != -1)
                            break;
                        cntd++;
                        KeyValuePair<int, int> p = new KeyValuePair<int, int>(i, y);
                        string s = g.humancellkey[p];
                        t7t.Add(s, true);
                    }
                    for (int i = x; i > -1 && cntu < g.human.ship2.length; i--)
                    {
                        if (g.humangrid[i, y] !=-1)
                            break;
                        cntu++;
                        KeyValuePair<int, int> p = new KeyValuePair<int, int>(i, y);
                        string s = g.humancellkey[p];
                        fo2.Add(s, true);
                    }
                    if (cntd == g.human.ship2.length)
                    {
                        foreach (Control c in Controls)
                        {
                            if (t7t.ContainsKey(c.Name))
                            {
                                if (evnt == 1)
                                {
                                    c.Text = "U";
                                    c.ForeColor = Color.Lime;
                                }
                                else if (evnt == 2)
                                {
                                    c.Text = "";
                                }
                                else if (evnt == 3)
                                {
                                    done = true;
                                    c.ForeColor = Color.Lime;
                                    c.Text = "U";
                                    g.humangrid[g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value] = 2;
                                    g.human.ship2.pos.Add(c.Name, new KeyValuePair<int, int>(g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value));
                                    deleteShip2Button.Enabled = true;
                                    deployShip2Button.Enabled = false;
                                    //c.Enabled = false;
                                }
                            }
                        }
                    }
                    else if (cntu == g.human.ship2.length)
                    {
                        foreach (Control c in Controls)
                        {
                            if (fo2.ContainsKey(c.Name))
                            {
                                if (evnt == 1)
                                {
                                    c.Text = "U";
                                    c.ForeColor = Color.Lime;
                                }
                                else if (evnt == 2)
                                {
                                    c.Text = "";
                                }
                                else if (evnt == 3)
                                {
                                    done = true;
                                    c.ForeColor = Color.Lime;
                                    c.Text = "U";
                                    //c.Enabled = false;
                                    g.human.ship2.pos.Add(c.Name, new KeyValuePair<int, int>(g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value));
                                    g.humangrid[g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value] = 2;
                                    deleteShip2Button.Enabled = true;
                                    deployShip2Button.Enabled = false;
                                }
                            }
                        }
                    }
                }
            }
            if (g.ship_to_deploy == 3)
            {
                if (g.ship_direction == true)
                {
                    int cntr = 0;
                    int cntl = 0;
                    for (int i = y; i < 10 && cntr < g.human.ship3.length; i++)
                    {
                        if (g.humangrid[x, i] !=-1)
                            break;
                        cntr++;
                        KeyValuePair<int, int> p = new KeyValuePair<int, int>(x, i);
                        string s = g.humancellkey[p];
                        ymen.Add(s, true);
                    }
                    for (int i = y; i > -1 && cntl < g.human.ship3.length; i--)
                    {
                        if (g.humangrid[x, i] !=-1)
                            break;
                        cntl++;
                        KeyValuePair<int, int> p = new KeyValuePair<int, int>(x, i);
                        string s = g.humancellkey[p];
                        shmal.Add(s, true);
                    }
                    if (cntr == g.human.ship3.length)
                    {
                        foreach (Control c in Controls)
                        {
                            if (ymen.ContainsKey(c.Name))
                            {
                                if (evnt == 1)
                                {
                                    c.Text = "B";
                                    c.ForeColor = Color.Blue;
                                }
                                else if (evnt == 2)
                                {
                                    c.Text = "";
                                }
                                else if (evnt == 3)
                                {
                                    done = true;
                                    c.ForeColor = Color.Blue;
                                    c.Text = "B";
                                    g.humangrid[g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value] = 3;
                                    g.human.ship3.pos.Add(c.Name, new KeyValuePair<int, int>(g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value));
                                    deleteShip3Button.Enabled = true;
                                    deployShip3Button.Enabled = false;
                                    //c.Enabled = false;
                                }
                            }
                        }
                    }
                    else if (cntl == g.human.ship3.length)
                    {
                        foreach (Control c in Controls)
                        {
                            if (shmal.ContainsKey(c.Name))
                            {
                                if (evnt == 1)
                                {
                                    c.Text = "B";
                                    c.ForeColor = Color.Blue;
                                }
                                else if (evnt == 2)
                                {
                                    c.Text = "";
                                }
                                else if (evnt == 3)
                                {
                                    done = true;
                                    c.ForeColor = Color.Blue;
                                    c.Text = "B";
                                    //c.Enabled = false;
                                    g.human.ship3.pos.Add(c.Name, new KeyValuePair<int, int>(g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value));
                                    g.humangrid[g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value] =3;
                                    deleteShip3Button.Enabled = true;
                                    deployShip3Button.Enabled = false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    int cntu = 0;
                    int cntd = 0;
                    for (int i = x; i < 10 && cntd < g.human.ship3.length; i++)
                    {
                        if (g.humangrid[i, y] !=-1)
                            break;
                        cntd++;
                        KeyValuePair<int, int> p = new KeyValuePair<int, int>(i, y);
                        string s = g.humancellkey[p];
                        t7t.Add(s, true);
                    }
                    for (int i = x; i > -1 && cntu < g.human.ship3.length; i--)
                    {
                        if (g.humangrid[i, y] !=-1)
                            break;
                        cntu++;
                        KeyValuePair<int, int> p = new KeyValuePair<int, int>(i, y);
                        string s = g.humancellkey[p];
                        fo2.Add(s, true);
                    }
                    if (cntd == g.human.ship3.length)
                    {
                        foreach (Control c in Controls)
                        {
                            if (t7t.ContainsKey(c.Name))
                            {
                                if (evnt == 1)
                                {
                                    c.Text = "B";
                                    c.ForeColor = Color.Blue;
                                }
                                else if (evnt == 2)
                                {
                                    c.Text = "";
                                }
                                else if (evnt == 3)
                                {
                                    done = true;
                                    c.ForeColor = Color.Blue;
                                    c.Text = "B";
                                    g.humangrid[g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value] = 3;
                                    g.human.ship3.pos.Add(c.Name, new KeyValuePair<int, int>(g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value));
                                    deleteShip3Button.Enabled = true;
                                    deployShip3Button.Enabled = false;
                                    //c.Enabled = false;
                                }
                            }
                        }
                    }
                    else if (cntu == g.human.ship3.length)
                    {
                        foreach (Control c in Controls)
                        {
                            if (fo2.ContainsKey(c.Name))
                            {
                                if (evnt == 1)
                                {
                                    c.Text = "B";
                                    c.ForeColor = Color.Blue;
                                }
                                else if (evnt == 2)
                                {
                                    c.Text = "";
                                }
                                else if (evnt == 3)
                                {
                                    done = true;
                                    c.ForeColor = Color.Blue;
                                    c.Text = "B";
                                    //c.Enabled = false;
                                    g.human.ship3.pos.Add(c.Name, new KeyValuePair<int, int>(g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value));
                                    g.humangrid[g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value] =3;
                                    deleteShip3Button.Enabled = true;
                                    deployShip3Button.Enabled = false;
                                }
                            }
                        }
                    }
                }
            }
            if (g.ship_to_deploy == 4)
            {
                if (g.ship_direction == true)
                {
                    int cntr = 0;
                    int cntl = 0;
                    for (int i = y; i < 10 && cntr < g.human.ship4.length; i++)
                    {
                        if (g.humangrid[x, i] !=-1)
                            break;
                        cntr++;
                        KeyValuePair<int, int> p = new KeyValuePair<int, int>(x, i);
                        string s = g.humancellkey[p];
                        ymen.Add(s, true);
                    }
                    for (int i = y; i > -1 && cntl < g.human.ship4.length; i--)
                    {
                        if (g.humangrid[x, i] !=-1)
                            break;
                        cntl++;
                        KeyValuePair<int, int> p = new KeyValuePair<int, int>(x, i);
                        string s = g.humancellkey[p];
                        shmal.Add(s, true);
                    }
                    if (cntr == g.human.ship4.length)
                    {
                        foreach (Control c in Controls)
                        {
                            if (ymen.ContainsKey(c.Name))
                            {
                                if (evnt == 1)
                                {
                                    c.Text = "R";
                                    c.ForeColor = Color.Purple;
                                }
                                else if (evnt == 2)
                                {
                                    c.Text = "";
                                }
                                else if (evnt == 3)
                                {
                                    done = true;
                                    c.ForeColor = Color.Purple;
                                    c.Text = "R";
                                    g.humangrid[g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value] = 4;
                                    g.human.ship4.pos.Add(c.Name, new KeyValuePair<int, int>(g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value));
                                    deleteShip4Button.Enabled = true;
                                    deployShip4Button.Enabled = false;
                                    //c.Enabled = false;
                                }
                            }
                        }
                    }
                    else if (cntl == g.human.ship4.length)
                    {
                        foreach (Control c in Controls)
                        {
                            if (shmal.ContainsKey(c.Name))
                            {
                                if (evnt == 1)
                                {
                                    c.Text = "R";
                                    c.ForeColor = Color.Purple;
                                }
                                else if (evnt == 2)
                                {
                                    c.Text = "";
                                }
                                else if (evnt == 3)
                                {
                                    done = true;
                                    c.ForeColor = Color.Purple;
                                    c.Text = "R";
                                    //c.Enabled = false;
                                    g.human.ship4.pos.Add(c.Name, new KeyValuePair<int, int>(g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value));
                                    g.humangrid[g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value] = 4;
                                    deleteShip4Button.Enabled = true;
                                    deployShip4Button.Enabled = false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    int cntu = 0;
                    int cntd = 0;
                    for (int i = x; i < 10 && cntd < g.human.ship4.length; i++)
                    {
                        if (g.humangrid[i, y] !=-1)
                            break;
                        cntd++;
                        KeyValuePair<int, int> p = new KeyValuePair<int, int>(i, y);
                        string s = g.humancellkey[p];
                        t7t.Add(s, true);
                    }
                    for (int i = x; i > -1 && cntu < g.human.ship4.length; i--)
                    {
                        if (g.humangrid[i, y] !=-1)
                            break;
                        cntu++;
                        KeyValuePair<int, int> p = new KeyValuePair<int, int>(i, y);
                        string s = g.humancellkey[p];
                        fo2.Add(s, true);
                    }
                    if (cntd == g.human.ship4.length)
                    {
                        foreach (Control c in Controls)
                        {
                            if (t7t.ContainsKey(c.Name))
                            {
                                if (evnt == 1)
                                {
                                    c.Text = "R";
                                    c.ForeColor = Color.Purple;
                                }
                                else if (evnt == 2)
                                {
                                    c.Text = "";
                                }
                                else if (evnt == 3)
                                {
                                    done = true;
                                    c.ForeColor = Color.Purple;
                                    c.Text = "R";
                                    g.humangrid[g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value] =4;
                                    g.human.ship4.pos.Add(c.Name, new KeyValuePair<int, int>(g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value));
                                    deleteShip4Button.Enabled = true;
                                    deployShip4Button.Enabled = false;
                                    //c.Enabled = false;
                                }
                            }
                        }
                    }
                    else if (cntu == g.human.ship4.length)
                    {
                        foreach (Control c in Controls)
                        {
                            if (fo2.ContainsKey(c.Name))
                            {
                                if (evnt == 1)
                                {
                                    c.Text = "R";
                                    c.ForeColor = Color.Purple;
                                }
                                else if (evnt == 2)
                                {
                                    c.Text = "";
                                }
                                else if (evnt == 3)
                                {
                                    done = true;
                                    c.ForeColor = Color.Purple;
                                    c.Text = "R";
                                    //c.Enabled = false;
                                    g.human.ship4.pos.Add(c.Name, new KeyValuePair<int, int>(g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value));
                                    g.humangrid[g.humanbtnsmp[c.Name].Key, g.humanbtnsmp[c.Name].Value] = 4;
                                    deleteShip4Button.Enabled = true;
                                    deployShip4Button.Enabled = false;
                                }
                            }
                        }
                    }
                }
            }
            if(evnt==3 && done)
            {
                g.ship_to_deploy = -1;
                shipRotateButton.Enabled = false;
            }
        }
        public void enabledeploying()
        {
            foreach (Control c in Controls)
            {
                GroupBox gbx = new GroupBox();
                Button b = new Button();
                if ((c.GetType() == gbx.GetType()) || (b.GetType() == c.GetType()) && (c.Name.Length == 3 || c.Name.Length == 2) && c.Name[0] != c.Name[1] && c.Name[0] >= 'A' && c.Name[0] <= 'J')
                {
                    c.Enabled = true;
                }
            }
        }
        public void disabledeploying()
        {
            foreach (Control c in Controls)
            {
                GroupBox gbx = new GroupBox();
                if (c.GetType() == gbx.GetType())
                {
                    c.Enabled = false;
                }
            }
        }
        private void mainmenubtn_Click(object sender, EventArgs e)
        {
            DialogResult ok = MessageBox.Show("any progress in the game will be lost", "Are you sure ?", MessageBoxButtons.YesNo);
            if(ok==DialogResult.Yes)
            {
                this.Hide();
                main_menu f = new main_menu();
                f.Show();
            }
        }
        private void shipRotateButton_Click(object sender, EventArgs e)
        {
            g.ship_direction = !g.ship_direction;
        }
        private void doneButton_Click(object sender, EventArgs e)
        {
            if (deleteShip0Button.Enabled && deleteShip1Button.Enabled && deleteShip2Button.Enabled && deleteShip3Button.Enabled && deleteShip4Button.Enabled)
            {
                DialogResult d = MessageBox.Show("this cannot be changed during the game", "Ready to start ?", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (d == DialogResult.Yes)
                {
                    disabledeploying();
                    startgamebtn.Enabled = true;
                }
            }
            else
                MessageBox.Show("you must deploy all ships ", "!!",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
        }
        public void newgamebtn_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            g = new BattleShip.Program.Newgame();
            link_buttons();
            enabledeploying();
            newgamebtn.Enabled = false;
            doneButton.Enabled = true;
        }
        private void deployShip0Button_Click(object sender, EventArgs e)
        {
            g.ship_to_deploy = 0;
            shipRotateButton.Enabled = true;
        }
        private void deploy_hover(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if(g.humangrid[g.humanbtnsmp[b.Name].Key, g.humanbtnsmp[b.Name].Value] == -1)
            humandeploy(b,1);
        }
        private void deploy_leave(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (g.humangrid[g.humanbtnsmp[b.Name].Key,g.humanbtnsmp[b.Name].Value]==-1) 
            humandeploy(b,2);
        }
        public void deploy_Click(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            humandeploy(b,3);
        }

        private void deleteShip0Button_Click(object sender, EventArgs e)
        {
            g.ship_to_undeploy = 0;
            humanundeploy();
        }

        private void deleteShip1Button_Click(object sender, EventArgs e)
        {
            g.ship_to_undeploy = 1;
            humanundeploy();
        }

        private void deleteShip2Button_Click(object sender, EventArgs e)
        {
            g.ship_to_undeploy = 2;
            humanundeploy();
        }

        private void deleteShip3Button_Click(object sender, EventArgs e)
        {
            g.ship_to_undeploy = 3;
            humanundeploy();
        }

        private void deleteShip4Button_Click(object sender, EventArgs e)
        {
            g.ship_to_undeploy = 4;
            humanundeploy();
        }

        private void deployShip1Button_Click(object sender, EventArgs e)
        {
            g.ship_to_deploy = 1;
            shipRotateButton.Enabled = true;
        }

        
        private void deployShip2Button_Click(object sender, EventArgs e)
        {
            g.ship_to_deploy = 2;
            shipRotateButton.Enabled = true;
        }

        private void deployShip3Button_Click(object sender, EventArgs e)
        {
            g.ship_to_deploy = 3;
            shipRotateButton.Enabled = true;
        }

        private void deployShip4Button_Click(object sender, EventArgs e)
        {
            g.ship_to_deploy = 4;
            shipRotateButton.Enabled = true;
        }
        public void enablecomputergrid()
        {
            Button b = new Button();
            foreach(Control c in Controls)
            {
                if (c.GetType() == b.GetType()&&(c.Name.Length==3||c.Name.Length==4)&&(c.Name[0]==c.Name[1]))
                    c.Enabled = true;
            }
        }
        private void startgamebtn_Click(object sender, EventArgs e)
        {
            //richTextBox1.Text = g.human.ship0.name;
            //Random r = new Random();
            //richTextBox1.Text = r.Next(0, 2).ToString();
            computerdeploy();
            enablecomputergrid();
            startgamebtn.Enabled = false;
        }
        public int Check_For_Winner()
        {
            if (g.human.ship0.health == 0 && g.human.ship1.health == 0 && g.human.ship2.health == 0 && g.human.ship3.health == 0 && g.human.ship4.health == 0)
                return -1;
            if (g.computer.ship0.health == 0 && g.computer.ship1.health == 0 && g.computer.ship2.health == 0 && g.computer.ship3.health == 0 && g.computer.ship4.health == 0)
                return 1;
            return 0;
        }
        public void Human_Fire(Button b)
        {   
            if (b.Text == "O" || b.Text == "X")
                return;
            //b.Enabled = false;
            int x, y;
            x = g.computerbtnsmp[b.Name].Key;
            y = g.computerbtnsmp[b.Name].Value;
            if(g.computergrid[x,y]==0)
            {
                hit.Play();
                g.computer.ship0.health--;
                b.Text = "O";
                b.ForeColor = Color.Green;
                if (g.computer.ship0.health == 0)
                    richTextBox1.Text += "you destroyed opponents Patrol Boat\n\n";
                else
                    richTextBox1.Text += "you hit opponents Patrol Boat\n\n";
            }
            else if (g.computergrid[x, y] == 1)
            {
                hit.Play();
                g.computer.ship1.health--;
                b.Text = "O";
                b.ForeColor = Color.Green;
                if (g.computer.ship1.health == 0)
                    richTextBox1.Text += "you destroyed opponents Submarine\n\n";
                else
                    richTextBox1.Text += "you hit opponents Submarine\n\n";
            }
            else if (g.computergrid[x, y] == 2)
            {
                hit.Play();
                g.computer.ship2.health--;
                b.Text = "O";
                b.ForeColor = Color.Green;
                if (g.computer.ship2.health == 0)
                    richTextBox1.Text += "you destroyed opponents Destroyer\n\n";
                else
                    richTextBox1.Text += "you hit opponents Destroyer\n\n";
            }
            else if (g.computergrid[x, y] == 3)
            {
                hit.Play();
                g.computer.ship3.health--;
                b.Text = "O";
                b.ForeColor = Color.Green;
                if (g.computer.ship3.health == 0)
                    richTextBox1.Text += "you destroyed opponents Battleship\n\n";
                else
                    richTextBox1.Text += "you hit opponents Battleship\n\n";
            }
            else if (g.computergrid[x, y] == 4)
            {
                hit.Play();
                g.computer.ship4.health--;
                b.Text = "O";
                b.ForeColor = Color.Green;
                if (g.computer.ship4.health == 0)
                    richTextBox1.Text += "you destroyed opponents Aircraft Carrier\n\n";
                else
                    richTextBox1.Text += "you hit opponents Aircraft Carrier\n\n";
            }
            else
            {
                miss.Play();
                b.Text = "X";
                b.ForeColor = Color.Red;
                richTextBox1.Text += "you hit the water\n\n";
            }
        }
        public void Set_Button_text(string name, string text)
        {
            foreach(Control c in Controls)
            {
                if (c.Name == name)
                {
                    c.Text = text;
                    if (text == "X")
                        c.ForeColor = Color.Red;
                    else
                        c.ForeColor = Color.Green;
                    break;
                }
            }
        }
        public List<KeyValuePair<int,int>>  get_the_list(int x, int y)
        {
            List<KeyValuePair<int, int>> cells=new List<KeyValuePair<int, int>>();
            //right
            for(int i=y+1; i<10; i++)
            {
                if (g.humangrid[x, i] == -2)
                    break;
                cells.Add(new KeyValuePair<int, int>(x, i));
                //richTextBox1.Text += x.ToString() + " " + i.ToString() + "\n";
                if (g.humangrid[x, i] != g.computer_last_hit)
                    break;
            }
            //left
            for(int i=y-1; i>-1; i--)
            {
                if (g.humangrid[x, i] == -2)
                    break;
                cells.Add(new KeyValuePair<int, int>(x, i));
                //richTextBox1.Text += x.ToString() + " " + i.ToString() + "\n";
                if (g.humangrid[x, i] != g.computer_last_hit)
                    break;
            }
            //down
            for(int i=x+1; i<10; i++)
            {
                if (g.humangrid[i, y] == -2)
                    break;
                cells.Add(new KeyValuePair<int, int>(i, y));
                //richTextBox1.Text += i.ToString() + " " + y.ToString() + "\n";
                if (g.humangrid[i, y] != g.computer_last_hit)
                    break;
            }
            //up
            for(int i=x-1; i>-1; i--)
            {
                if (g.humangrid[i, y] == -2)
                    break;
                cells.Add(new KeyValuePair<int, int>(i, y));
                //richTextBox1.Text += i.ToString() + " " + y.ToString() + "\n";
                if (g.humangrid[i, y] != g.computer_last_hit)
                    break;
            }
            return cells;
        }
        private void WaitNSeconds(int seconds)
        {
            if (seconds < 1) return;
            DateTime _desired = DateTime.Now.AddSeconds(seconds);
            while (DateTime.Now < _desired)
            {
                System.Threading.Thread.Sleep(1000);
                System.Windows.Forms.Application.DoEvents();
            }
        }
        public void enable(bool ok)
        {
            foreach (Control c in Controls)
            {
                if (c.Text == "" && ok)
                    c.Enabled = true;
                else if (c.Text == "" && !ok)
                    c.Enabled = false;
            }
        }
        public void Computer_Fire()
        {
            WaitNSeconds(3);
            enable(true);
            if (g.computer_last_hit==-1)
            {
                while(true)
                {
                    Random xr = new Random();
                    Random yr = new Random();
                    Random rcell = new Random();
                    int cell = rcell.Next(0, allcells.Count);

                    g.lastx = allcells[cell].Key;
                    g.lasty = allcells[cell].Value;
                    if(g.humangrid[g.lastx,g.lasty]!=-2)
                    {
                        allcells.Remove(new KeyValuePair<int, int>(g.lastx, g.lasty));
                        g.computer_last_hit = g.humangrid[g.lastx, g.lasty];
                        if(g.computer_last_hit==-1)
                        {
                            miss.Play();
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(g.lastx, g.lasty)], "X");
                            richTextBox1.Text += "Your opponent hit the water\n\n";
                        }
                        else
                        {
                            hit.Play();
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(g.lastx, g.lasty)], "O");
                            if(g.computer_last_hit==0)
                            {
                                g.human.ship0.health--;
                                richTextBox1.Text += "Your opponent hit your Patrol Boat\n\n";
                            }
                            if (g.computer_last_hit == 1)
                            {
                                g.human.ship1.health--;
                                richTextBox1.Text += "Your opponent hit your Submarine\n\n";
                            }
                            if (g.computer_last_hit == 2)
                            {
                                g.human.ship2.health--;
                                richTextBox1.Text += "Your opponent hit your Destroyer\n\n";
                            }
                            if (g.computer_last_hit == 3)
                            {
                                g.human.ship3.health--;
                                richTextBox1.Text += "Your opponent hit your Battleship\n\n";
                            }
                            if (g.computer_last_hit == 4)
                            {
                                g.human.ship4.health--;
                                richTextBox1.Text += "Your opponent hit your Aircraft Carrier\n\n";
                            }
                        }
                        g.humangrid[g.lastx, g.lasty] = -2;
                        //richTextBox1.ScrollToCaret();
                        break;
                    }
                }
            }
            else if (g.computer_last_hit == 0)
            {
                if (AI0.first_time == true)
                {
                    AI0.x = g.lastx;
                    AI0.y = g.lasty;
                    AI0.nxt = get_the_list(AI0.x, AI0.y);
                    AI0.first_time = false;
                }
                for (int i = 0; i < AI0.nxt.Count; i++)
                {
                    int x = AI0.nxt[i].Key;
                    int y = AI0.nxt[i].Value;
                    if (g.humangrid[x, y] != -2)
                    {
                        if (g.humangrid[x, y] == 0)
                        {
                            hit.Play();
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "O");
                            g.human.ship0.health--;
                            if (g.human.ship0.health == 0)
                            {
                                richTextBox1.Text += "your opponent Destroyed your Patrol Boat\n\n";
                                if (AI1.first_time == false && g.human.ship1.health != 0)
                                    g.computer_last_hit = 1;
                                else if (AI2.first_time == false && g.human.ship2.health != 0)
                                    g.computer_last_hit = 2;
                                else if (AI3.first_time == false && g.human.ship3.health != 0)
                                    g.computer_last_hit = 3;
                                else if (AI4.first_time == false && g.human.ship4.health != 0)
                                    g.computer_last_hit = 4;
                                else
                                    g.computer_last_hit = -1;
                            }
                            else
                            {
                                richTextBox1.Text += "your opponenet hit your Patrol Boat\n\n";
                            }
                        }
                        else if (g.humangrid[x, y] == 1)
                        {
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "O");
                            g.human.ship1.health--;
                            richTextBox1.Text += "your opponent hit your Submarine\n\n";
                            g.computer_last_hit = 1;
                            AI1.first_time = false;
                            AI1.x = x;
                            AI1.y = y;
                            AI1.nxt = get_the_list(x, y);
                            g.computer_last_hit = 0;
                        }
                        else if (g.humangrid[x, y] == 2)
                        {
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "O");
                            g.human.ship2.health--;
                            richTextBox1.Text += "your opponent hit your Destroyer\n\n";
                            g.computer_last_hit = 2;
                            AI2.first_time = false;
                            AI2.x = x;
                            AI2.y = y;
                            AI2.nxt = get_the_list(x, y);
                            g.computer_last_hit = 0;
                        }
                        else if (g.humangrid[x, y] == 3)
                        {
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "O");
                            g.human.ship3.health--;
                            richTextBox1.Text += "your opponent hit your Battleship\n\n";
                            g.computer_last_hit = 3;
                            AI3.first_time = false;
                            AI3.x = x;
                            AI3.y = y;
                            AI3.nxt = get_the_list(x, y);
                            g.computer_last_hit = 0;
                        }
                        else if (g.humangrid[x, y] == 4)
                        {
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "O");
                            g.human.ship4.health--;
                            richTextBox1.Text += "your opponent hit your Aircraft Carrier\n\n";
                            g.computer_last_hit = 4;
                            AI4.first_time = false;
                            AI4.x = x;
                            AI4.y = y;
                            AI4.nxt = get_the_list(x, y);
                            g.computer_last_hit = 0;
                        }
                        else
                        {
                            miss.Play();
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "X");
                            richTextBox1.Text += "your opponent hit the water\n\n";
                        }
                        //richTextBox1.ScrollToCaret();
                        g.humangrid[x, y] = -2;
                        allcells.Remove(new KeyValuePair<int, int>(x, y));
                        break;
                    }
                }
            }
            else if (g.computer_last_hit == 1)
            {
                if (AI1.first_time == true)
                {
                    AI1.x = g.lastx;
                    AI1.y = g.lasty;
                    AI1.nxt = get_the_list(AI1.x, AI1.y);
                    AI1.first_time = false;
                }
                for (int i = 0; i < AI1.nxt.Count; i++)
                {
                    int x = AI1.nxt[i].Key;
                    int y = AI1.nxt[i].Value;
                    if (g.humangrid[x, y] != -2)
                    {
                        if (g.humangrid[x, y] == 1)
                        {
                            hit.Play();
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "O");
                            g.human.ship1.health--;
                            if (g.human.ship1.health == 0)
                            {
                                richTextBox1.Text += "your opponent Destroyed your Submarine\n\n";
                                if (AI0.first_time == false && g.human.ship0.health != 0)
                                    g.computer_last_hit = 0;
                                else if (AI2.first_time == false && g.human.ship2.health != 0)
                                    g.computer_last_hit = 2;
                                else if (AI3.first_time == false && g.human.ship3.health != 0)
                                    g.computer_last_hit = 3;
                                else if (AI4.first_time == false && g.human.ship4.health != 0)
                                    g.computer_last_hit = 4;
                                else
                                    g.computer_last_hit = -1;
                            }
                            else
                            {
                                richTextBox1.Text += "your opponenet hit your Submarine\n\n";
                            }
                        }
                        else if (g.humangrid[x, y] == 0)
                        {
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "O");
                            g.human.ship0.health--;
                            richTextBox1.Text += "your opponent hit your Patrol Boat\n\n";
                            g.computer_last_hit = 0;
                            AI0.first_time = false;
                            AI0.x = x;
                            AI0.y = y;
                            AI0.nxt = get_the_list(x, y);
                            g.computer_last_hit = 1;
                        }
                        else if (g.humangrid[x, y] == 2)
                        {
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "O");
                            g.human.ship2.health--;
                            richTextBox1.Text += "your opponent hit your Destroyer\n\n";
                            g.computer_last_hit = 2;
                            AI2.first_time = false;
                            AI2.x = x;
                            AI2.y = y;
                            AI2.nxt = get_the_list(x, y);
                            g.computer_last_hit = 1;
                        }
                        else if (g.humangrid[x, y] == 3)
                        {
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "O");
                            g.human.ship3.health--;
                            richTextBox1.Text += "your opponent hit your Battleship\n\n";
                            g.computer_last_hit = 3;
                            AI3.first_time = false;
                            AI3.x = x;
                            AI3.y = y;
                            AI3.nxt = get_the_list(x, y);
                            g.computer_last_hit = 1;
                        }
                        else if (g.humangrid[x, y] == 4)
                        {
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "O");
                            g.human.ship4.health--;
                            richTextBox1.Text += "your opponent hit your Aircraft Carrier\n\n";
                            g.computer_last_hit = 4;
                            AI4.first_time = false;
                            AI4.x = x;
                            AI4.y = y;
                            AI4.nxt = get_the_list(x, y);
                            g.computer_last_hit = 1;
                        }
                        else
                        {
                            miss.Play();
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "X");
                            richTextBox1.Text += "your opponent hit the water\n\n";
                        }
                        //richTextBox1.ScrollToCaret();
                        g.humangrid[x, y] = -2;
                        allcells.Remove(new KeyValuePair<int, int>(x, y));
                        break;
                    }
                }
            }
            else if (g.computer_last_hit == 2)
            {
                if (AI2.first_time == true)
                {
                    AI2.x = g.lastx;
                    AI2.y = g.lasty;
                    AI2.nxt = get_the_list(AI2.x, AI2.y);
                    AI2.first_time = false;
                }
                for (int i = 0; i < AI2.nxt.Count; i++)
                {
                    int x = AI2.nxt[i].Key;
                    int y = AI2.nxt[i].Value;
                    if (g.humangrid[x, y] != -2)
                    {
                        if (g.humangrid[x, y] == 2)
                        {
                            hit.Play();
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "O");
                            g.human.ship2.health--;
                            if (g.human.ship2.health == 0)
                            {
                                richTextBox1.Text += "your opponent Destroyed your Destroyer\n\n";
                                if (AI1.first_time == false && g.human.ship1.health != 0)
                                    g.computer_last_hit = 1;
                                else if (AI0.first_time == false && g.human.ship0.health != 0)
                                    g.computer_last_hit = 0;
                                else if (AI3.first_time == false && g.human.ship3.health != 0)
                                    g.computer_last_hit = 3;
                                else if (AI4.first_time == false && g.human.ship4.health != 0)
                                    g.computer_last_hit = 4;
                                else
                                    g.computer_last_hit = -1;
                            }
                            else
                            {
                                richTextBox1.Text += "your opponenet hit your Destroyer\n\n";
                            }
                        }
                        else if (g.humangrid[x, y] == 1)
                        {
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "O");
                            g.human.ship1.health--;
                            richTextBox1.Text += "your opponent hit your Submarine\n\n";
                            g.computer_last_hit = 1;
                            AI1.first_time = false;
                            AI1.x = x;
                            AI1.y = y;
                            AI1.nxt = get_the_list(x, y);
                            g.computer_last_hit = 2;
                        }
                        else if (g.humangrid[x, y] == 0)
                        {
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "O");
                            g.human.ship0.health--;
                            richTextBox1.Text += "your opponent hit your Patrol Boat\n\n";
                            g.computer_last_hit = 0;
                            AI0.first_time = false;
                            AI0.x = x;
                            AI0.y = y;
                            AI0.nxt = get_the_list(x, y);
                            g.computer_last_hit = 2;
                        }
                        else if (g.humangrid[x, y] == 3)
                        {
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "O");
                            g.human.ship3.health--;
                            richTextBox1.Text += "your opponent hit your Battleship\n\n";
                            g.computer_last_hit = 3;
                            AI3.first_time = false;
                            AI3.x = x;
                            AI3.y = y;
                            AI3.nxt = get_the_list(x, y);
                            g.computer_last_hit = 2;
                        }
                        else if (g.humangrid[x, y] == 4)
                        {
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "O");
                            g.human.ship4.health--;
                            richTextBox1.Text += "your opponent hit your Aircraft Carrier\n\n";
                            g.computer_last_hit = 4;
                            AI4.first_time = false;
                            AI4.x = x;
                            AI4.y = y;
                            AI4.nxt = get_the_list(x, y);
                            g.computer_last_hit = 2;
                        }
                        else
                        {
                            miss.Play();
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "X");
                            richTextBox1.Text += "your opponent hit the water\n\n";
                        }
                        //richTextBox1.ScrollToCaret();
                        g.humangrid[x, y] = -2;
                        allcells.Remove(new KeyValuePair<int, int>(x, y));
                        break;
                    }
                }
            }
            else if (g.computer_last_hit == 3)
            {
                if (AI3.first_time == true)
                {
                    AI3.x = g.lastx;
                    AI3.y = g.lasty;
                    AI3.nxt = get_the_list(AI3.x, AI3.y);
                    AI3.first_time = false;
                }
                for (int i = 0; i < AI3.nxt.Count; i++)
                {
                    int x = AI3.nxt[i].Key;
                    int y = AI3.nxt[i].Value;
                    if (g.humangrid[x, y] != -2)
                    {
                        if (g.humangrid[x, y] == 3)
                        {
                            hit.Play();
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "O");
                            g.human.ship3.health--;
                            if (g.human.ship3.health == 0)
                            {
                                richTextBox1.Text += "your opponent Destroyed your Battleship\n\n";
                                if (AI1.first_time == false && g.human.ship1.health != 0)
                                    g.computer_last_hit = 1;
                                else if (AI2.first_time == false && g.human.ship2.health != 0)
                                    g.computer_last_hit = 2;
                                else if (AI0.first_time == false && g.human.ship0.health != 0)
                                    g.computer_last_hit = 0;
                                else if (AI4.first_time == false && g.human.ship4.health != 0)
                                    g.computer_last_hit = 4;
                                else
                                    g.computer_last_hit = -1;
                            }
                            else
                            {
                                richTextBox1.Text += "your opponenet hit your Battleship\n\n";
                            }
                        }
                        else if (g.humangrid[x, y] == 1)
                        {
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "O");
                            g.human.ship1.health--;
                            richTextBox1.Text += "your opponent hit your Submarine\n\n";
                            g.computer_last_hit = 1;
                            AI1.first_time = false;
                            AI1.x = x;
                            AI1.y = y;
                            AI1.nxt = get_the_list(x, y);
                            g.computer_last_hit = 3;
                        }
                        else if (g.humangrid[x, y] == 2)
                        {
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "O");
                            g.human.ship2.health--;
                            richTextBox1.Text += "your opponent hit your Destroyer\n\n";
                            g.computer_last_hit = 2;
                            AI2.first_time = false;
                            AI2.x = x;
                            AI2.y = y;
                            AI2.nxt = get_the_list(x, y);
                            g.computer_last_hit = 3;
                        }
                        else if (g.humangrid[x, y] == 0)
                        {
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "O");
                            g.human.ship0.health--;
                            richTextBox1.Text += "your opponent hit your Patrol Boat\n\n";
                            g.computer_last_hit = 0;
                            AI0.first_time = false;
                            AI0.x = x;
                            AI0.y = y;
                            AI0.nxt = get_the_list(x, y);
                            g.computer_last_hit = 3;
                        }
                        else if (g.humangrid[x, y] == 4)
                        {
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "O");
                            g.human.ship4.health--;
                            richTextBox1.Text += "your opponent hit your Aircraft Carrier\n\n";
                            g.computer_last_hit = 4;
                            AI4.first_time = false;
                            AI4.x = x;
                            AI4.y = y;
                            AI4.nxt = get_the_list(x, y);
                            g.computer_last_hit = 3;
                        }
                        else
                        {
                            miss.Play();
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "X");
                            richTextBox1.Text += "your opponent hit the water\n\n";
                        }
                        //richTextBox1.ScrollToCaret();
                        g.humangrid[x, y] = -2;
                        allcells.Remove(new KeyValuePair<int, int>(x, y));
                        break;
                    }
                }
            }
            else if (g.computer_last_hit == 4)
            {
                if (AI4.first_time == true)
                {
                    AI4.x = g.lastx;
                    AI4.y = g.lasty;
                    AI4.nxt = get_the_list(AI4.x, AI4.y);
                    AI4.first_time = false;
                }
                for (int i = 0; i < AI4.nxt.Count; i++)
                {
                    int x = AI4.nxt[i].Key;
                    int y = AI4.nxt[i].Value;
                    if (g.humangrid[x, y] != -2)
                    {
                        if (g.humangrid[x, y] == 4)
                        {
                            hit.Play();
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "O");
                            g.human.ship4.health--;
                            if (g.human.ship4.health == 0)
                            {
                                richTextBox1.Text += "your opponent Destroyed your Aircraft Carrier\n\n";
                                if (AI1.first_time == false && g.human.ship1.health != 0)
                                    g.computer_last_hit = 1;
                                else if (AI2.first_time == false && g.human.ship2.health != 0)
                                    g.computer_last_hit = 2;
                                else if (AI3.first_time == false && g.human.ship3.health != 0)
                                    g.computer_last_hit = 3;
                                else if (AI0.first_time == false && g.human.ship0.health != 0)
                                    g.computer_last_hit = 0;
                                else
                                    g.computer_last_hit = -1;
                            }
                            else
                            {
                                richTextBox1.Text += "your opponenet hit yout Aircraft Carrier\n\n";
                            }
                        }
                        else if (g.humangrid[x, y] == 1)
                        {
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "O");
                            g.human.ship1.health--;
                            richTextBox1.Text += "your opponent hit your Submarine\n\n";
                            g.computer_last_hit = 1;
                            AI1.first_time = false;
                            AI1.x = x;
                            AI1.y = y;
                            AI1.nxt = get_the_list(x, y);
                            g.computer_last_hit = 4;
                        }
                        else if (g.humangrid[x, y] == 2)
                        {
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "O");
                            g.human.ship2.health--;
                            richTextBox1.Text += "your opponent hit your Destroyer\n\n";
                            g.computer_last_hit = 2;
                            AI2.first_time = false;
                            AI2.x = x;
                            AI2.y = y;
                            AI2.nxt = get_the_list(x, y);
                            g.computer_last_hit = 4;
                        }
                        else if (g.humangrid[x, y] == 3)
                        {
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "O");
                            g.human.ship3.health--;
                            richTextBox1.Text += "your opponent hit your Battleship\n\n";
                            g.computer_last_hit = 3;
                            AI3.first_time = false;
                            AI3.x = x;
                            AI3.y = y;
                            AI3.nxt = get_the_list(x, y);
                            g.computer_last_hit = 4;
                        }
                        else if (g.humangrid[x, y] == 0)
                        {
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "O");
                            g.human.ship0.health--;
                            richTextBox1.Text += "your opponent hit your Patrol Boat\n\n";
                            g.computer_last_hit = 0;
                            AI0.first_time = false;
                            AI0.x = x;
                            AI0.y = y;
                            AI0.nxt = get_the_list(x, y);
                            g.computer_last_hit = 4;
                        }
                        else
                        {
                            miss.Play();
                            Set_Button_text(g.humancellkey[new KeyValuePair<int, int>(x, y)], "X");
                            richTextBox1.Text += "your opponent hit the water\n\n";
                        }
                        //richTextBox1.ScrollToCaret();
                        g.humangrid[x, y] = -2;
                        allcells.Remove(new KeyValuePair<int, int>(x, y));
                        break;
                    }
                }
            }
            //System.Threading.Thread.Sleep(1500);
        }
        public void End_Game()
        {
            foreach (Control c in Controls)
            {
                c.Enabled = false;
                Button b = new Button();
                if (c.Text == "D" || c.Text == "U" || c.Text == "S" || c.Text == "B" || c.Text == "R" || c.Text == "O" || c.Text == "X")
                {
                    if (c.GetType() == b.GetType())
                        c.Text = "";
                }
            }
            richTextBox1.Enabled = true;
            mainmenubtn.Enabled = true;
            newgamebtn.Enabled = true;
            for(int i=0; i<5; i++)
            {
                g.ship_to_undeploy = i;
                humanundeploy();
            }

        }
        private void Fire_Click(object sender, MouseEventArgs e)
        {
            Button b = (Button) sender;
            if (b.Text=="O"||b.Text=="X")
                return ;
            Human_Fire(b);
            enable(false);
            int Winner=Check_For_Winner();
            if(Winner==1)
            {
                victory.Play();
                richTextBox1.Text = "You Won\n";
                End_Game();
            }
            else if(Winner==-1)
            {
                richTextBox1.Text = "You Lost\n";
                End_Game();
            }
            else
            {
                //System.Threading.Thread.Sleep(2000);
                
                Computer_Fire();
         //       System.Threading.Thread.Sleep(1500);
                Winner = Check_For_Winner();
                if (Winner == 1)
                {
                    victory.Play();
                    richTextBox1.Text = "You Won\n";
                    End_Game();
                }
                else if (Winner == -1)
                {
                    richTextBox1.Text = "You Lost\n";
                    End_Game();
                }
            }
        }
    }
}
