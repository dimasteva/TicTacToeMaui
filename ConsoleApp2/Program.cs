using System.ComponentModel;
using System.Data;
using System.Numerics;

namespace ConsoleApp2
{
    class TicTacToe
    {
        public enum FieldStatus
        {
            Empty,
            X,
            O
        }
        public struct FieldCoordinate
        {
            public int x;
            public int y;
        }

        public FieldStatus[][] field = new FieldStatus[3][];
        public TicTacToe()
        {
            for (int i = 0; i < field.Length; i++)
            {
                field[i] = new FieldStatus[3];
                for (int j = 0; j < field[i].Length; j++)
                {
                    field[i][j] = FieldStatus.Empty;
                }
            }
        }
        public void PrintField()
        {
            Console.WriteLine();
            for (int i = 0; i < field.Length; i++)
            {
                for (int j = 0; j < field[i].Length; j++)
                {
                    Console.Write(field[i][j]);
                    if (j != 2)
                    {
                        Console.Write(" | ");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("---------------------");
            }
            Console.WriteLine();
        }

        public int? CheckField()
        {
            int? won = null;
            for (int i = 0; i < 3; i++)
            {
                if (field[i][0] != FieldStatus.Empty && field[i][0] == field[i][1] && field[i][1] == field[i][2])
                {
                    won = field[i][0] == FieldStatus.X ? 1 : 2;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (field[0][i] != FieldStatus.Empty && field[0][i] == field[1][i] && field[1][i] == field[2][i])
                {
                    won = field[0][i] == FieldStatus.X ? 1 : 2;
                }
            }

            if (field[0][0] != FieldStatus.Empty && field[0][0] == field[1][1] && field[1][1] == field[2][2])
            {
                won = field[0][0] == FieldStatus.X ? 1 : 2;
            }
            if (field[0][2] != FieldStatus.Empty && field[0][2] == field[1][1] && field[1][1] == field[2][0])
            {
                won = field[0][2] == FieldStatus.X ? 1 : 2;
            }

            return won;
        }

        public int getColumnandRow(bool done)
        {
            int x = -1;
            while (x < 1 || x > 3)
            {
                Console.WriteLine("Enter " + (done? "row" : "column") + "(1-3):");
                try
                {
                    x = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occured: " + ex.Message);
                }
                if (x < 1 || x > 3)
                {
                    Console.WriteLine((done ? "Row" : "Column ") + "is out of bounds");
                }
            }
            return x;
        }

        private void PlaceMark(int row, int column, bool move)
        {
            field[row][column] = move ? FieldStatus.X : FieldStatus.O;
        }

        public void GameFlow()
        {
            PrintField();
            int duration = 1;
            bool move = true; //X plays
            bool done = true; //For input
            while(duration <= 9)
            {
                FieldCoordinate coordinates = new FieldCoordinate();
                Console.WriteLine("The " + (move ? "X" : "O") + " player's move");
                while (true)
                {
                    coordinates.x = getColumnandRow(done);
                    done = !done;
                    coordinates.y = getColumnandRow(done);
                    done = !done;
                    if (field[coordinates.x - 1][coordinates.y - 1] == FieldStatus.Empty)
                        break;
                }
                PlaceMark(coordinates.x - 1, coordinates.y - 1, move);
                int? winner = CheckField();
                if (winner == 1)
                {
                    Console.WriteLine();
                    Console.WriteLine("The X player won");
                    Console.WriteLine();
                    PrintField();
                    return;
                }
                else if (winner == 2)
                {
                    Console.WriteLine();
                    Console.WriteLine("The O player won");
                    PrintField();
                    return;
                }
                move = !move;
                duration++;
                PrintField();
            }
            PrintField();
            Console.WriteLine("Draw");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TicTacToe Game = new TicTacToe();
            Game.GameFlow();
        }
    }
}