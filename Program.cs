using System;
using System.Threading;

namespace FlappyBirdConsole
{
    class Program
    {
        const int Width = 40;
        const int Height = 20;

        const int BirdX = 10;
        const char BirdChar = '@';
        const char PipeChar = '#';
        const char EmptyChar = ' ';
        const char GroundChar = '=';

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            double birdYPos = Height / 2.0;
            int birdY = (int)birdYPos;
            double velocity = 0.0;
            double gravity = 0.10;      // daha yavaş düşüş
            double jumpStrength = -1.0; // daha yumuşak zıplama

            int score = 0;

            int pipeX = Width - 1;
            Random rnd = new Random();
            int gapSize = 6;
            int gapY = rnd.Next(3, Height - gapSize - 3);

            bool gameOver = false;

            DateTime lastFrame = DateTime.Now;

            ShowIntro();

            while (!gameOver)
            {
                DateTime now = DateTime.Now;
                double delta = (now - lastFrame).TotalMilliseconds / 16.0;
                if (delta < 1)
                {
                    Thread.Sleep(1);
                    continue;
                }
                lastFrame = now;

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        velocity = jumpStrength;
                    }
                    else if (key.Key == ConsoleKey.Escape)
                    {
                        return;
                    }
                }

                // basit fizik: yerçekimi + zıplama
                velocity += gravity;
                birdYPos += velocity;

                // üst sınıra çarpınca yukarıdan çıkmasın
                if (birdYPos < 0)
                {
                    birdYPos = 0;
                    velocity = 0;
                }

                // ekranda göstermek için en yakın satıra yuvarla
                birdY = (int)Math.Round(birdYPos);

                pipeX--;

                if (pipeX < 0)
                {
                    pipeX = Width - 1;
                    gapY = rnd.Next(3, Height - gapSize - 3);
                }

                if (birdY >= Height - 1)
                {
                    gameOver = true;
                }
                else if (pipeX == BirdX)
                {
                    if (birdY < gapY || birdY > gapY + gapSize)
                    {
                        gameOver = true;
                    }
                    else
                    {
                        score++;
                    }
                }

                DrawFrame(birdY, pipeX, gapY, gapSize, score);

                Thread.Sleep(30);
            }

            ShowGameOver(score);
        }

        static void ShowIntro()
        {
            Console.Clear();
            Console.WriteLine("=== Flappy Bird - C# Console ===");
            Console.WriteLine();
            Console.WriteLine("Kontroller:");
            Console.WriteLine("- Space: Zıpla");
            Console.WriteLine("- ESC: Çık");
            Console.WriteLine();
            Console.WriteLine("Oyun boyunca kuş (@) sabit sütunda durur,");
            Console.WriteLine("borular soldan sağa doğru kayar. Boşluğu kullanarak");
            Console.WriteLine("borulardaki boşluktan geçmeye çalış.");
            Console.WriteLine();
            Console.WriteLine("Devam etmek için bir tuşa bas...");
            Console.ReadKey(true);
        }

        static void DrawFrame(int birdY, int pipeX, int gapY, int gapSize, int score)
        {
            Console.SetCursorPosition(0, 0);

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    char ch = EmptyChar;

                    if (y == Height - 1)
                    {
                        ch = GroundChar;
                    }

                    if (x == pipeX)
                    {
                        if (y < gapY || y > gapY + gapSize)
                        {
                            if (y < Height - 1)
                                ch = PipeChar;
                        }
                    }

                    if (x == BirdX && y == birdY)
                    {
                        ch = BirdChar;
                    }

                    // Renk seçimi
                    if (ch == BirdChar)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (ch == PipeChar)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (ch == GroundChar)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.Write(ch);
                }
                Console.ResetColor();
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Score: {score}");
            Console.WriteLine("Press ESC to quit, SPACE to jump");
            Console.ResetColor();
        }

        static void ShowGameOver(int score)
        {
            Console.SetCursorPosition(0, Height + 1);
            Console.WriteLine();
            Console.WriteLine("Game Over!");
            Console.WriteLine($"Final Score: {score}");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(true);
        }
    }
}
