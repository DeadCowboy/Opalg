using System;
using System.Diagnostics;
using System.Drawing;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Opalg.Models;

namespace Opalg.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }



    public void ShowSolution<SolutionType>(SolutionType solution) where SolutionType : ISolution
    {
        int numBoxes = solution.Boxes.Count;
        (int rows, int cols) = NextProductNumber(solution.Boxes.Count);

        for (int row = 0; row < rows; row++)
        {
            SolutionGrid.RowDefinitions.Add(new RowDefinition());
        }

        for (int col = 0; col < cols; col++)
        {
            SolutionGrid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; r < cols; c++)
            {
                DisplayGrid dGrid = new DisplayGrid();
                int boxIndex = c + r * cols;
                if (boxIndex >= solution.Boxes.Count)
                {
                    break;
                }
                Box toBeShown = solution.Boxes[boxIndex];
                dGrid.ShowBox(toBeShown);
            }
        }
    }

    (int, int) NextProductNumber(int n)
    {
        while (true)
        {
            for (int i = 2; i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    return (i, n / i); // n has integer factors i and n/i
                }
            }
            n++;
        }
    }
}