// Author Asko Tuomiaro

namespace SudokuSolver
{
    /// <summary>
    /// This class represents a single tile in a given sudoku. It should have values between 1-9.
    /// </summary>
    class Tile
    {
        private bool[] possibleDigits = new bool[]
            { 
                false, true, true, true, true, 
                true, true, true, true, true 
            };
        private int value;

        public string Symbol { get; set; } = "X";
        public int Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (value >= 1 && value <= 9)
                {
                    this.value = value;
                    this.possibleDigits[0] = true;
                    this.Symbol = value.ToString();
                }
            }
        }

        public void CrossOffDigit(int digit)
        {
            this.possibleDigits[digit] = false;
        }

        public bool IsDigitPossible(int digit)
        {
            return this.possibleDigits[digit] == true;
        }

        public bool IsValueSet()
        {
            return this.possibleDigits[0];
        }

        public override string ToString()
        {
            return Symbol;
        }

        public bool TryToInferValue()
        {
            if (this.IsValueSet())
            {
                return false;
            }

            int possibleValue = 0;
            int sum = 0;
            for (int i = 0; i < this.possibleDigits.Length; i++)
            {
                if (this.possibleDigits[i])
                {
                    sum++;
                    possibleValue = i;
                }
            }

            if (sum == 1)
            {
                Value = possibleValue;
                return true;
            }
            return false;
        }
    }
}