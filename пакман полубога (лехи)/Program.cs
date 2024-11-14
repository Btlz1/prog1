using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading; // пространство имён

namespace Pacman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            char[,] map = ReadMap("Map.txt");

            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false); // изначально выводим w, которая заменится на следующий ReadKey
            // Снизу в параллельном потоке запускаем 
            Task.Run(() =>
            {
                while (true)
                {
                    pressedKey = Console.ReadKey();
                }
            });

            int pacmanX = 1;
            int pacmanY = 1;
            int score = 0;

            while (true)
            {
                Console.Clear();
                HandleInput(pressedKey, ref pacmanX, ref pacmanY, map, ref score);

                Console.ForegroundColor = ConsoleColor.DarkBlue;
                DrawMap(map);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(pacmanX, pacmanY);
                Console.Write('@');

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(15, 0);
                Console.Write($"Score: {score}"); // возвращаем знак из ConsoleKeyInfo

                Thread.Sleep(100);
            }

        }



        private static char[,] ReadMap(string path)
        {
            string[] file = File.ReadAllLines(path);

            char[,] map = new char[GetMaxLengthOfLines(file), file.Length];

            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                    map[x, y] = file[y][x]; // из файла беру строчку y и достаю значение x

            return map;
        }
        private static int GetMaxLengthOfLines(string[] lines)
        {
            if (lines.Length == 0)
                return 0;

            int maxLength = lines[0].Length;

            foreach (var line in lines) // var - чтобы не указывать тип | пока строка из массива строк, мы что-то делаем с этой строкой
                if (line.Length > maxLength)
                    maxLength = line.Length;
            return maxLength;


        }
        private static void DrawMap(char[,] map)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    Console.Write(map[x, y]);
                }
                Console.Write("\n");
            }
        }
        private static void HandleInput(ConsoleKeyInfo pressedKey, ref int pacmanX, ref int pacmanY, char[,] map, ref int score)
        {
            int[] direction = GetDirection(pressedKey);

            int nextPacmanPositionX = pacmanX + direction[0];
            int nextPacmanPositionY = pacmanY + direction[1];
            char nextCell = map[nextPacmanPositionX, nextPacmanPositionY];

            if (nextCell == ' ' || nextCell == '.')
            {
                pacmanX = nextPacmanPositionX;
                pacmanY = nextPacmanPositionY;

                if (nextCell == '.')
                {
                    score++;
                    map[nextPacmanPositionX, nextPacmanPositionY] = ' ';
                }
            }
        }
        private static int[] GetDirection(ConsoleKeyInfo pressedKey)
        {
            int[] direction = { 0, 0 };

            if (pressedKey.Key == ConsoleKey.UpArrow)
            {
                direction[1] = -1;
            }
            else if (pressedKey.Key == ConsoleKey.DownArrow)
            {
                direction[1] = 1;
            }
            else if (pressedKey.Key == ConsoleKey.LeftArrow)
            {
                direction[0] = -1;
            }
            else if (pressedKey.Key == ConsoleKey.RightArrow)
            {
                direction[0] = 1;
            }
            return direction;
        }


    }

}