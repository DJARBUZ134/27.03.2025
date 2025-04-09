using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _27._03._2025
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        int x0 = 250;
        int y0 = 150;
        int xt = 250;
        int yt = 250;
        Graphics g;
        bool needApple = true;
        int x = 0;
        int y = 0;
        int X = 0;
        int Y = 0;
        int l = 1;
        int k = 1;
        int j = 1;
        bool needBonus = true;
        int score = 1;
        SolidBrush sb;

       
        Queue<Point> Tail = new Queue<Point>();

        private void DrawApple(int R, int G, int B)
        {
            sb = new SolidBrush(Color.FromArgb(R, G, B));
            if (needApple)
            {
                Random rnd = new Random();
                x = rnd.Next(20) * 10;
                y = rnd.Next(20) * 10;
                needApple = false;
            }
            g.FillRectangle(sb, x, y, 10, 10);
        }

        private void BonusSize(int X, int Y)
        {

            sb = new SolidBrush(Color.Black);
            if (score % 10 == 0)
            {
                Random rnd = new Random();
                x = rnd.Next(20) * 10;
                y = rnd.Next(20) * 10;
                needBonus = false;
            }
            g.FillRectangle(sb, x, y, 10, 10);
        }
        private void Form1_Keydown(object sender, KeyEventArgs e)
        {
            Random rand = new Random();
            g = pictureBox1.CreateGraphics();
            sb = new SolidBrush(Color.Red);
           // g.Clear(Color.AliceBlue);
            DrawApple(l, k, j);
            int speed = (e.Shift) ? 3 : 1;
            // Добавление новой головы в очередь
            Point newHead = new Point(x0, y0);
            if (Tail.Count == 0)
            {
                Point PP = new Point(x0, y0);
                Tail.Enqueue(PP);
            }
                switch (e.KeyCode)
                {
                    case Keys.A: DrawSquare(-1, 0,speed); break;
                    case Keys.W: DrawSquare(0 , -1, speed); break;
                    case Keys.S: DrawSquare(0 , 1, speed); break;
                    case Keys.D: DrawSquare(1 , 0, speed); break;
                }
            // Проверка на выход за границы игрового поля
            if (x0 < 0 || x0 >= pictureBox1.Width || y0 < 0 || y0 >= pictureBox1.Height) // исправлена ошибка с границами
            {
                GameOver(); // Конец игры
                return;
            }

            // Проверка на столкновение со своим телом
            
            foreach (Point segment in Tail)
            {

                if (segment.X == xt && segment.Y == yt)
                {
                    GameOver(); // Конец игры
                    return;
                }
            }

            foreach (Point l in Tail)
            {
                richTextBox1.Text = l.X+" - "+l.Y;
            }

            if (x0 == x && y0 == y)
            {
                needApple = true;
                l = rand.Next(255);
                k = rand.Next(255);
                j = rand.Next(255);
                Point PP = new Point();
                Tail.Enqueue(PP);
                DrawApple(l, k, j);
                label1.Text = $"СЧЁТ: {score++}";
            }

        }

        public void DrawSquare(int n, int m, int speed = 1)
        {
            sb = new SolidBrush(Color.Red);
            x0 += 10 * n * speed;
            y0 += 10 * m * speed;
            g.FillRectangle(sb, x0, y0, 10, 10);
            Point PP = new Point(x0,y0);
            Tail.Enqueue(PP);
            g.FillRectangle(new SolidBrush(Color.White), Tail.Peek().X, Tail.Peek().Y, 10, 10);
            Tail.Dequeue();
        }
        private void GameOver()
        {
            MessageBox.Show("Game Over!");
            Application.Exit(); // Или можно перезапустить игру
        }

        //Проблема 1: при наезде на саму себя змейка рвётся, ломается, короче типичный Омск
        //Проблема 4: При ускоренном перемещении змейка рвётся, ломается, короче типичный Омск
        //Проблема 5: В одном из углов должен быть квадрат с рандомным цветом, и цвет квадрата(вашего) менается, когда вы на него наступаете.
        //Проблема 6: в другом углу квадратик для смены размера вашего квадрата(на определенное кол-во шагов, например 50)
        //Проблема 7: Наличие возможности выхода за поле (РЕШЕНО)
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
