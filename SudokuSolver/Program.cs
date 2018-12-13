// Author Asko Tuomiaro

using System;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            // Note: This is a level 3 sudoku.
            Sudoku sudoku = new Sudoku();
            
            sudoku.SetValue(0, 0, 9);
            sudoku.SetValue(0, 5, 6);

            sudoku.SetValue(1, 0, 5);
            sudoku.SetValue(1, 2, 1);
            sudoku.SetValue(1, 3, 8);
            sudoku.SetValue(1, 5, 3);
            sudoku.SetValue(1, 7, 9);

            sudoku.SetValue(2, 2, 8);
            sudoku.SetValue(2, 3, 5);

            sudoku.SetValue(3, 0, 1);
            sudoku.SetValue(3, 2, 5);
            sudoku.SetValue(3, 4, 7);
            sudoku.SetValue(3, 6, 2);
            sudoku.SetValue(3, 8, 9);

            sudoku.SetValue(4, 0, 7);
            sudoku.SetValue(4, 2, 8);
            sudoku.SetValue(4, 4, 4);
            sudoku.SetValue(4, 6, 6);
            sudoku.SetValue(4, 8, 5);
            
            sudoku.SetValue(5, 0, 4);
            sudoku.SetValue(5, 2, 2);
            sudoku.SetValue(5, 4, 5);
            sudoku.SetValue(5, 6, 8);
            sudoku.SetValue(5, 8, 7);

            sudoku.SetValue(6, 5, 2);
            sudoku.SetValue(6, 6, 3);
            
            sudoku.SetValue(7, 1, 7);
            sudoku.SetValue(7, 3, 3);
            sudoku.SetValue(7, 5, 6);
            sudoku.SetValue(7, 6, 1);
            sudoku.SetValue(7, 8, 2);

            sudoku.SetValue(8, 3, 1);
            sudoku.SetValue(8, 8, 4);

            Console.WriteLine(sudoku);

            sudoku.Solve();
            Console.WriteLine(sudoku);
        }
    }
}
