// Author Asko Tuomiaro

namespace SudokuSolver
{
    /// <summary>
    /// This class represents the main grid of a sudoku, that holds the nine individual plots.
    /// </summary>
    class Grid
    {
        public TilePlot[] Plots { get; set; } = {
            new TilePlot(), new TilePlot(), new TilePlot(),
            new TilePlot(), new TilePlot(), new TilePlot(),
            new TilePlot(), new TilePlot(), new TilePlot(), 
        };
    }
}