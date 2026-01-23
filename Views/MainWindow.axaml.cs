using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Remote.Protocol.Designer;
using Opalg.Models;

namespace Opalg.Views;

public partial class MainWindow : Window
{
    ISolution? currentSolution;
    public MainWindow()
    {
        InitializeComponent();
    }

    public void Init(object sender, RoutedEventArgs args)
    {
        this._init();
    }

    private void _init()
    {
        GeometricSolution initSolution = new GeometricSolution(3, 1, 5, 8);
        this.currentSolution = initSolution;
        ShowSolution(initSolution);
        if (initSolution.Boxes == null) throw new Exception("Solution init did not work");
    }

    public void Optimize(object sender, RoutedEventArgs args)           // TODO: Make Generic
    {
        if (this.currentSolution == null) throw new Exception("Problem not inizialized bevor optimization");
        INeighbourhood<GeometricSolution> neighbourhood = new GeoNeighbourhood();
        NaiveLocalSearch<GeometricSolution> optimizer = new();
        GeometricSolution bestSolution = optimizer.Optimize((GeometricSolution)this.currentSolution, neighbourhood);
        this._reset();
        this.ShowSolution(bestSolution);
    }

    public void Reset(object sender, RoutedEventArgs args)
    {
        this._reset();
    }
    private void _reset()
    {
        SolutionGrid.RowDefinitions.Clear();
        SolutionGrid.ColumnDefinitions.Clear();
        SolutionGrid.Children.Clear();
    }


    public void ShowSolution<SolutionType>(SolutionType solution) where SolutionType : ISolution  // TODO: Does this need to be generic? Just always show the boxes
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

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                DisplayGrid dGrid = new DisplayGrid();
                int boxIndex = x + y * cols;
                if (boxIndex >= solution.Boxes.Count)
                {
                    break;
                }
                Box toBeShown = solution.Boxes[boxIndex];
                dGrid.ShowBox(toBeShown);
                dGrid.identifyer.Content = boxIndex;
                Grid.SetRow(dGrid, y);
                Grid.SetColumn(dGrid, x);
                SolutionGrid.Children.Add(dGrid);
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