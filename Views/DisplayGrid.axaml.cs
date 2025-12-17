using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Opalg.Models;

namespace Opalg.Views;

public partial class DisplayGrid : UserControl
{
    public DisplayGrid()
    {
        InitializeComponent();
    }

    public void SetColors(Color[,] colors, int size)
    {
        if (colors == null) throw new ArgumentNullException(nameof(colors));

        int rows = colors.GetLength(0);
        int cols = colors.GetLength(1);

        for (int r = 0; r < rows + 1; r++)
        {
            SquareGrid.RowDefinitions.Add(new RowDefinition(new GridLength(size - 1)));
        }

        for (int c = 0; c < cols + 1; c++)
        {
            SquareGrid.ColumnDefinitions.Add(new ColumnDefinition(new GridLength(size - 1)));
        }

        // Add cells
        for (int r = 0; r < rows; r++)
        {
            // Add Row Label at start of Row
            Label label = new Label
            {
                Content = rows - r,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                FontSize = size / 2,
            };
            Grid.SetRow(label, r);
            Grid.SetColumn(label, 0);
            SquareGrid.Children.Add(label);

            // Fill Grid cells with colored borders
            for (int c = 1; c < cols + 1; c++)
            {
                Border border = new Border
                {
                    Width = size,
                    Height = size,
                    Background = new SolidColorBrush(colors[r, c - 1]),
                };

                Grid.SetRow(border, r);
                Grid.SetColumn(border, c);
                SquareGrid.Children.Add(border);
            }
        }

        // Add Column Label at bottom
        for (int c = 0; c < cols + 1; c++)
        {
            Label label = new Label
            {
                Content = c,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                FontSize = size / 2,
            };
            Grid.SetRow(label, rows + 1);
            Grid.SetColumn(label, c);
            SquareGrid.Children.Add(label);
        }

    }

    public void SetColorsFromHex(string[][] hexRows, int size = 40)
    {
        if (hexRows == null) throw new ArgumentNullException(nameof(hexRows));

        int rows = hexRows.Length;
        int cols = rows > 0 ? hexRows[0].Length : 0;
        var arr = new Color[rows, cols];

        for (int r = 0; r < rows; r++)
        {
            if (hexRows[r].Length != cols) throw new ArgumentException("All rows must have same length");
            for (int c = 0; c < cols; c++)
            {
                arr[r, c] = Color.Parse(hexRows[r][c]);
            }
        }
        SetColors(arr, size);
    }

    public void ShowBox(Box box)
    {
        var rng = new Random();
        Color[,] colors = new Color[box.size, box.size];
        foreach (PositionedRect rect in box.rectangles)
        {
            Color rndColor = Color.FromRgb(
                (byte)rng.Next(256),
                (byte)rng.Next(256),
                (byte)rng.Next(256)
            );
            rect.ForEachPosition((x, y) => colors[x, y] = rndColor);
        }
        SetColors(colors, box.size * 10);

    }
}