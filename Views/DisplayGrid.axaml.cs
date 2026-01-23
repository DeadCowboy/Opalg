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

    public Label identifyer;

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
        for (int y = 0; y < rows; y++)
        {
            // Add Row Label at start of Row
            Label label = new Label
            {
                Content = rows - y - 1,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                FontSize = size / 2,
            };
            Grid.SetRow(label, y);
            Grid.SetColumn(label, 0);
            SquareGrid.Children.Add(label);

            // Fill Grid cells with colored borders
            for (int x = 1; x < cols + 1; x++)
            {
                Border border = new Border
                {
                    Width = size,
                    Height = size,
                    Background = new SolidColorBrush(colors[x - 1, y]),
                };

                Grid.SetRow(border, rows - y - 1); // Invert in grid display to put 0.0 at bottom left
                Grid.SetColumn(border, x);
                SquareGrid.Children.Add(border);
            }
        }

        // Add Column Label at bottom
        for (int x = 0; x < cols + 1; x++)
        {
            Label label = new Label
            {
                Content = x == 0 ? "" : x - 1,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                FontSize = size / 2,
            };
            if (x == 0)
            {
                this.identifyer = label;
                this.identifyer.Foreground = new SolidColorBrush(Color.FromRgb(0x78, 0x06, 0x06));
            }
            Grid.SetRow(label, rows + 1);
            Grid.SetColumn(label, x);
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