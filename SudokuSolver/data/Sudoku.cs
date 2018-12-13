// Author Asko Tuomiaro

namespace SudokuSolver
{
    /// <summary>
    /// This class represents the main sudoku data structure. It allows the user to set
    /// individual tile values as well as solve the sudoku. 
    /// </summary>
    class Sudoku
    {
        private const int StandardSize = 9;
        private Grid grid = new Grid();

        public void SetValue(int plotIndex, int tileIndex, int value)
        {
            this.grid.Plots[plotIndex].Tiles[tileIndex].Value = value;
            this.grid.Plots[plotIndex].ConfirmDigit(value);
        }

        public void Solve() 
        {
            bool inferenceSucceeded = true;
            bool eliminationSucceeded = true;

            while (inferenceSucceeded || eliminationSucceeded)
            {
                inferenceSucceeded = false;
                eliminationSucceeded = false;

                for (int i = 0; i < StandardSize; i++)
                {
                    for (int j = 0; j < StandardSize; j++)
                    {
                        Tile currentTile = this.grid.Plots[i].Tiles[j];
                        
                        if (currentTile.IsValueSet())
                        {
                            continue;
                        }

                        RowCheck(i, j);
                        ColumnCheck(i, j);
                        PlotCheck(i, j);


                        if (currentTile.TryToInferValue())
                        {
                            inferenceSucceeded = true;
                            this.grid.Plots[i].ConfirmDigit(currentTile.Value);
                        }
                    }
                }

                for (int i = 0; i < StandardSize; i++)
                {
                    if (PlotEliminationCheck(i))
                    {
                        eliminationSucceeded = true;
                    }
                }
            }
        }

        public override string ToString()
        {
            string result = "";

            for (int i = 0; i < StandardSize; i += 3)
            {
                result += "\n";
                for (int j = i; j < i + 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        result += this.grid.Plots[j].Tiles[k].ToString();
                    }
                    result += "  ";
                }
                result += "\n";

                for (int j = i; j < i + 3; j++)
                {
                    for (int k = 3; k < 6; k++)
                    {
                        result += this.grid.Plots[j].Tiles[k].ToString();
                    }
                    result += "  ";
                }
                result += "\n";

                for (int j = i; j < i + 3; j++)
                {
                    for (int k = 6; k < 9; k++)
                    {
                        result += this.grid.Plots[j].Tiles[k].ToString();
                    }
                    result += "  ";
                }
                result += "\n";
            }

            return result;
        }



        /// <summary> 
        /// This method goes through the row the current tile is on, and checks off all the
        /// known values on that row for that tile.
        /// </summary>
        /// <param name="plotIndex">Index of the plot.</param>
        /// <param name="tileIndex">Index of the tile.</param>
        private void RowCheck(int plotIndex, int tileIndex) 
        {
            Tile currentTile = this.grid.Plots[plotIndex].Tiles[tileIndex];
            if (currentTile.IsValueSet())
            {
                return;
            }

            int i = 0;
            int p = 0;

            // i represents the index of the wanted plot.
            if (plotIndex < 3) 
            {
                i = 0;
            }
            else if (plotIndex > 5) 
            {
                i = 6;
            }
            else 
            {
                i = 3;
            }

            // p repsents the index of the wanted tile.
            if (tileIndex < 3) 
            {
                p = 0;
            }
            else if(tileIndex > 5) 
            {
                p = 6;
            }
            else 
            {
                p = 3;
            }

            for (int j = i; j < i + 3; j++)
            {
                for (int k = p; k < p + 3; k++)
                {
                    Tile tileOfinterest = this.grid.Plots[j].Tiles[k];
                    if (tileOfinterest.IsValueSet())
                    {
                        currentTile.CrossOffDigit(tileOfinterest.Value);
                    }
                }
            }
        }

        /// <summary> 
        /// This method goes through the column the current tile is on, and checks off all the
        /// known values on that column for that tile.
        /// </summary>
        /// <param name="plotIndex">Index of the plot.</param>
        /// <param name="tileIndex">Index of the tile.</param>
        private void ColumnCheck(int plotIndex, int tileIndex)
        {
            Tile currentTile = this.grid.Plots[plotIndex].Tiles[tileIndex];
            if (currentTile.IsValueSet())
            {
                return;
            }

            int i = plotIndex % 3;
            int p = tileIndex % 3;

            for (int j = i; j < StandardSize; j += 3)
            {
                for (int k = p; k < StandardSize; k += 3)
                {
                    Tile tileOfinterest = this.grid.Plots[j].Tiles[k];
                    if (tileOfinterest.IsValueSet())
                    {
                        currentTile.CrossOffDigit(tileOfinterest.Value);
                    }
                }
            }
        }

        /// <summary> 
        /// This method checks off all the known values in a plot for a given tile.
        /// </summary>
        /// <param name="plotIndex">Index of the plot.</param>
        /// <param name="tileIndex">index of the tile.</param>
        private void PlotCheck(int plotIndex, int tileIndex)
        {
            TilePlot currentPlot = this.grid.Plots[plotIndex];
            Tile currentTile = currentPlot.Tiles[tileIndex];

            if (currentTile.IsValueSet())
            {
                return;
            }

            for (int i = 0; i < StandardSize; i++)
            {
                Tile tileOfinterest = currentPlot.Tiles[i];
                if (tileOfinterest.IsValueSet())
                {
                    currentTile.CrossOffDigit(tileOfinterest.Value);
                }
            }
        }

        /// <summary> 
        /// This method aims to discover the value of a tile by a process of elimination.
        /// The idea is to look at the plot, and see, if there is only a single tile, where
        /// a given value can be placed. 
        /// </summary>
        /// <param name="plotIndex">index of the plot.</param>
        /// <returns></returns>
        private bool PlotEliminationCheck(int plotIndex)
        {
            TilePlot currentPlot = this.grid.Plots[plotIndex];

            if (currentPlot.IsFull())
            {
                return false;
            }

            bool returnable = false;
            for (int i = 1; i <= StandardSize; i++)
            {
                if (currentPlot.IsDigitConfirmed(i))
                {
                    continue;
                }

                Tile correctNumberTile = null;
                int possibilities = 0;

                for (int j = 0; j < StandardSize; j++)
                {
                    Tile currentTile = currentPlot.Tiles[j];
                    if (currentTile.IsValueSet())
                    {
                        continue;
                    }
                    if (currentTile.IsDigitPossible(i))
                    {
                        correctNumberTile = currentTile;
                        possibilities++;
                    }
                }

                if (possibilities == 1)
                {
                    correctNumberTile.Value = i;
                    returnable = true;
                }
            }
            return returnable;
        }
    }
}