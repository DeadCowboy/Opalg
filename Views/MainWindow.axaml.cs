using System;
using System.Diagnostics;
using System.Drawing;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Opalg.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var rng = new Random();
        int gridSize = 10;
        var colors = new Avalonia.Media.Color[gridSize, gridSize];

        for (int r = 0; r < gridSize; r++)
        {
            for (int c = 0; c < gridSize; c++)
            {
                colors[r, c] = Avalonia.Media.Color.FromRgb(
                    (byte)rng.Next(256),
                    (byte)rng.Next(256),
                    (byte)rng.Next(256)
                );
            }
        }
        ColorGrid.SetColors(colors, 30);

        // DisplayGrid secondGrid = new DisplayGrid(colors, 80);
        // MainStack.Children.Add(secondGrid);
    }
}