using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Threading;

namespace конс_прил_практика
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            char[,] map = ReadMap("map.txt");
            Console.ForegroundColor = ConsoleColor.Red;
            DrawMap(map);
            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);
           
             int pacmanX = 1;
             int pacmanY = 1;
             int score = 0;
            
             while (true)
             {
                Console.Clear();
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                HandleInput(pressedKey, ref pacmanX, ref pacmanY, map, ref score);

                Console.ForegroundColor = ConsoleColor.DarkBlue;
                DrawMap(map);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(pacmanX, pacmanY);
                Console.Write('@');

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(10, 0);
                Console.Write($"Score: {score}"); // возвращаем знак из ConsoleKeyInfo

                Thread.Sleep(100);
             }
            

        }
        private static char[,] ReadMap(string path)
        {
            string[] file = File.ReadAllLines(path);

            int maxLength = GetMaxLenghtOfLine(file);

            char[,] map = new char[maxLength, file.Length];

            for (int y = 0; y < file.Length; y++)
            {
                for (int x = 0; x < maxLength; x++)
                {
                    if (x < file[y].Length)
                    {
                        map[x, y] = file[y][x];
                    }
                    else
                    {
                        map[x, y] = ' ';
                    }
                }
            }

            return map;
        }
        private static void DrawMap (char [,] map)
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
        private static int GetMaxLenghtOfLine(string[] lines) 
        {
            int maxLenght = lines[0].Length;
            foreach (var line in lines) 
                if (line.Length > maxLenght)
                    maxLenght = line.Length;   
            return maxLenght;
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