// Author Asko Tuomiaro

using System.Diagnostics;

namespace SudokuSolver
{
    /// <summary>
    /// This class represents a plot of nine tiles, that makes up a single unit in a sudoku.  
    /// </summary>
    class TilePlot 
    {
        private bool[] confirmedDigits = new bool[10];

        public Tile[] Tiles { get; } = {
            new Tile(), new Tile(), new Tile(),
            new Tile(), new Tile(), new Tile(),
            new Tile(), new Tile(), new Tile(),
        };

        public bool IsDigitConfirmed(int digit)
        {
            return this.confirmedDigits[digit];
        }

        public void ConfirmDigit(int digit)
        {
            Debug.Assert(0 < digit && digit < 10);
            this.confirmedDigits[digit] = true;
        }

        public bool IsFull()
        {
            if (!this.confirmedDigits[0])
            {
                for (int i = 0; i < this.confirmedDigits.Length; i++)
                {
                    if (!this.confirmedDigits[i])
                    {
                        return false;
                    }
                }
                this.confirmedDigits[0] = true;
            }
            return true;
        }
    }
}