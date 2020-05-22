using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coursework
{
    public partial class Form1 : Form
    {
        bool flag = false;
        bool flag1 = true;
        int x = 0, y = 27;
        Random rand = new Random();
        string move = "X";
        string filePath = $"D:/Курсова/Images/wall.jpg";
        Button[,] buttons = new Button[17, 17];

        public Form1()
        {
            InitializeComponent();
            
            StartPosition = FormStartPosition.CenterScreen;
            Height = 746;
            Width = 696;
            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Size = new Size(40, 40);
                    buttons[i, j].Location = new Point(x + 40 * i, y + 40 * j);
                    Controls.Add(buttons[i, j]);
                    buttons[i, j].Text = "";
                    buttons[i, j].Enabled = false;
                    buttons[i, j].Font = new Font("Tahoma", 15, FontStyle.Bold);
                    buttons[i, j].BackColor = System.Drawing.Color.SandyBrown;
                    buttons[i, j].FlatStyle = FlatStyle.Flat;
                }
            }

            clearFieldToolStripMenuItem.Click += clearFieldToolStripMenuItem_Click;
            withFriendToolStripMenuItem.Click += withFriendToolStripMenuItem_Click;
            liteToolStripMenuItem.Click += liteToolStripMenuItem_Click;
            mediumToolStripMenuItem.Click += mediumToolStripMenuItem_Click;
            hardToolStripMenuItem.Click += hardToolStripMenuItem_Click;

            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    if (i == buttons.GetLength(0) - 1 || i == 0 || j == buttons.GetLength(1) - 1 || j == 0)
                    {
                        buttons[i, j].Enabled = false;
                        buttons[i, j].BackgroundImage = Image.FromFile(filePath);
                    }
                }
            }

            void EdgeWalls()
            {
                for (int i = 0; i < buttons.GetLength(0); i++)
                {
                    for (int j = 0; j < buttons.GetLength(1); j++)
                    {
                        if (i == buttons.GetLength(0) - 1 || i == 0 || j == buttons.GetLength(1) - 1 || j == 0)
                        {
                            buttons[i, j].Enabled = false;
                        }
                    }
                }
            }
            
            void WinMessage(string move)
            {
                for (int i = 0; i < buttons.GetLength(0); i++)
                {
                    for (int j = 0; j < buttons.GetLength(1); j++)
                    {
                        buttons[i, j].Enabled = false;
                    }
                }

                MessageBox.Show(move + " выиграл!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            void CheckWin(int i, int j, string move, ref bool flag)
            {
                if (buttons[i, j].Text == move && buttons[i - 1, j].Text == move && buttons[i - 2, j].Text == move
                    && buttons[i + 1, j].Text == move && buttons[i + 2, j].Text == move)
                {
                    WinMessage(move);
                    flag = true;
                }
                if (buttons[i, j].Text == move && buttons[i, j - 1].Text == move && buttons[i, j - 2].Text == move
                    && buttons[i, j + 1].Text == move && buttons[i, j + 2].Text == move)
                {
                    WinMessage(move);
                    flag = true;
                }
                if (buttons[i, j].Text == move && buttons[i - 1, j - 1].Text == move && buttons[i - 2, j - 2].Text == move
                    && buttons[i + 1, j + 1].Text == move && buttons[i + 2, j + 2].Text == move)
                {
                    WinMessage(move);
                    flag = true;
                }
                if (buttons[i, j].Text == move && buttons[i - 1, j + 1].Text == move && buttons[i - 2, j + 2].Text == move
                    && buttons[i + 1, j - 1].Text == move && buttons[i + 2, j - 2].Text == move)
                {
                    WinMessage(move);
                    flag = true;
                }
            }

            void Check()
            {
                for (int i = 0; i < buttons.GetLength(0); i++)
                {
                    for (int j = 0; j < buttons.GetLength(1); j++)
                    {
                        CheckWin(i, j, "X", ref flag);
                        CheckWin(i, j, "O", ref flag);
                    }
                }
            }
            //Event Handler
            void OnClick1(object sender, EventArgs e)
            {
                Button button = (Button)sender;

                if (button.Text == "")
                {
                    button.Text = move;
                    button.Enabled = false;
                }

                if (move == "X")
                {
                    move = "O";
                }
                else
                {
                    move = "X";
                }

                Check();

                if (flag == true)
                {
                    for (int i = 0; i < buttons.GetLength(0); i++)
                    {
                        for (int j = 0; j < buttons.GetLength(1); j++)
                        {
                            buttons[i, j].Click -= new EventHandler(OnClick1);
                        }
                    }
                }

                flag = false;
            }

            void OnClick2(object sender, EventArgs e)
            {
                int randPosition1 = rand.Next(3, 15);
                int randPosition2 = rand.Next(3, 15);

                Button button = (Button)sender;

                if (button.Text == "")
                {
                    button.Text = move;
                    button.Enabled = false;
                }

                if (move == "X")
                {
                    if (buttons[randPosition1, randPosition2].Text == "X")
                    {
                        buttons[randPosition1 + 2, randPosition2 + 2].Text = "O";
                        buttons[randPosition1 + 2, randPosition2 + 2].Enabled = false;
                    }
                    else
                    {
                        buttons[randPosition1, randPosition2].Text = "O";
                        buttons[randPosition1, randPosition2].Enabled = false;
                    }
                }

                Check();

                if (flag == true)
                {
                    for (int i = 0; i < buttons.GetLength(0); i++)
                    {
                        for (int j = 0; j < buttons.GetLength(1); j++)
                        {
                            buttons[i, j].Click -= new EventHandler(OnClick2);
                        }
                    }
                }

                flag = false;
            }

            void OnClick3(object sender, EventArgs e)
            {
                int randPosition1 = rand.Next(8, 13);
                int randPosition2 = rand.Next(8, 13);

                Button button = (Button)sender;

                if (button.Text == "")
                {
                    button.Text = move;
                    button.Enabled = false;
                }

                if (move == "X")
                {
                    for (int i = 0; i < buttons.GetLength(0); i++)
                    {
                        for (int j = 0; j < buttons.GetLength(1); j++)
                        {
                            if (buttons[randPosition1, randPosition2].Text == "X")
                            {
                                buttons[randPosition1 + 2, randPosition2 + 2].Text = "O";
                                buttons[randPosition1 + 2, randPosition2 + 2].Enabled = false;
                            }
                            else
                            {
                                buttons[randPosition1, randPosition2].Text = "O";
                                buttons[randPosition1, randPosition2].Enabled = false;
                            }
                        }
                    }
                }

                Check();

                if (flag == true)
                {
                    for (int i = 0; i < buttons.GetLength(0); i++)
                    {
                        for (int j = 0; j < buttons.GetLength(1); j++)
                        {
                            buttons[i, j].Click -= new EventHandler(OnClick3);
                        }
                    }
                }

                flag = false;
            }

            void OnClick4(object sender, EventArgs e)
            {
                int Tag = 0;

                Button button = (Button)sender;
                for (int i = 0; i < buttons.GetLength(0); ++i)
                {
                    for (int j = 0; j < buttons.GetLength(1); j++)
                    {
                        if (button.Text == "")
                        {
                            button.Text = move;
                            button.Tag = Tag;
                            Tag++;
                            button.Enabled = false;
                        }
                    }
                }

                flag1 = true;

                for (int i = 0; i < buttons.GetLength(0); ++i)
                {
                    for (int j = 0; j < buttons.GetLength(1); j++)
                    {
                        if (buttons[i, j].Text == "X" && button.Text == "X")
                        {
                            if (flag1 == true)
                            {
                                buttons[i + 1, j + 1].Text = "O";
                                buttons[i + 1, j + 1].Enabled = false;
                                flag1 = false;
                            }
                            else if (flag1 == false)
                            {
                                buttons[i - 1, j - 1].Text = "O";
                                buttons[i - 1, j - 1].Enabled = false;
                                flag1 = true;
                            }
                            flag1 = false;
                        }
                    }
                }

                Check();

                if (flag == true)
                {
                    for (int i = 0; i < buttons.GetLength(0); i++)
                    {
                        for (int j = 0; j < buttons.GetLength(1); j++)
                        {
                            buttons[i, j].Click -= new EventHandler(OnClick4);
                        }
                    }
                }

                flag = false;
            }
            //Menu
            void clearFieldToolStripMenuItem_Click(object sender, EventArgs e)
            {
                for (int i = 0; i < buttons.GetLength(0); i++)
                {
                    for (int j = 0; j < buttons.GetLength(1); j++)
                    {
                        buttons[i, j].Enabled = false;
                        buttons[i, j].Text = "";
                        buttons[i, j].Click -= new EventHandler(OnClick1);
                        buttons[i, j].Click -= new EventHandler(OnClick2);
                        buttons[i, j].Click -= new EventHandler(OnClick3);
                    }
                }
            }

            void withFriendToolStripMenuItem_Click(object sender, EventArgs e)
            {
                for (int i = 0; i < buttons.GetLength(0); i++)
                {
                    for (int j = 0; j < buttons.GetLength(1); j++)
                    {
                        buttons[i, j].Enabled = true;
                        buttons[i, j].Text = "";
                        buttons[i, j].Click += new EventHandler(OnClick1);
                    }
                }

                EdgeWalls();
            }

            void liteToolStripMenuItem_Click(object sender, EventArgs e)
            {
                move = "X";

                for (int i = 0; i < buttons.GetLength(0); i++)
                {
                    for (int j = 0; j < buttons.GetLength(1); j++)
                    {
                        buttons[i, j].Enabled = true;
                        buttons[i, j].Text = "";
                        buttons[i, j].Click += new EventHandler(OnClick2);
                    }
                }

                EdgeWalls();
            }

            void mediumToolStripMenuItem_Click(object sender, EventArgs e)
            {
                move = "X";

                for (int i = 0; i < buttons.GetLength(0); i++)
                {
                    for (int j = 0; j < buttons.GetLength(1); j++)
                    {
                        buttons[i, j].Enabled = true;
                        buttons[i, j].Text = "";
                        buttons[i, j].Click += new EventHandler(OnClick3);
                    }
                }

                EdgeWalls();
            }

            void hardToolStripMenuItem_Click(object sender, EventArgs e)
            {
                move = "X";

                for (int i = 0; i < buttons.GetLength(0); i++)
                {
                    for (int j = 0; j < buttons.GetLength(1); j++)
                    {
                        buttons[i, j].Enabled = true;
                        buttons[i, j].Text = "";
                        buttons[i, j].Click += new EventHandler(OnClick4);
                    }
                }

                EdgeWalls();
            }
        }
    }
}
