using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace Deadline24.Core.Visualization
{
    public class World
    {
        private readonly Point _worldSize;

        private readonly int _pointSize;

        private readonly IList<Item> _items = new List<Item>();

        public World(int width, int height, int pointSize)
        {
            _pointSize = pointSize;
            _worldSize = new Point(width, height);
        }

        public void AddItem(int x, int y, int itemType)
        {
            _items.Add(new Item(x, y, itemType));
        }

        public Bitmap GenerateBitmap()
        {
            var bitmapWidth = _worldSize.X * _pointSize;
            var bitmapHeight = _worldSize.Y * _pointSize;

            var image = new Bitmap(bitmapWidth, bitmapHeight, PixelFormat.Format24bppRgb);
            using (var graphics = Graphics.FromImage(image))
            {
                graphics.FillRectangle(Brushes.White, 0, 0, bitmapWidth, bitmapHeight);

                foreach (var items in _items.GroupBy(i => i.ItemType))
                {
                    var brush = GetBrush(items.Key);
                    var rects = items.Select(GetItemRect).ToArray();
                    if (items.Key % 2 == 0)
                    {
                        graphics.FillRectangles(brush, rects);
                    }
                    else
                    {
                        graphics.FillRectangles(brush, rects);
                    }
                }
            }

            return image;
        }

        private Brush GetBrush(int itemsType)
        {
            switch (itemsType)
            {
                case 0: return Brushes.Blue;
                case 1: return Brushes.Red;
                case 2: return Brushes.DarkGreen;
                case 3: return Brushes.Aqua;
                case 4: return Brushes.Fuchsia;
                case 5: return Brushes.Yellow;
                case 6: return Brushes.Sienna;
                case 7: return Brushes.Orange;
                case 8: return Brushes.Purple;
                case 9: return Brushes.MediumSpringGreen;
                default: return Brushes.Black;
            }
        }

        private Rectangle GetItemRect(Item item)
        {
            return new Rectangle(item.X * _pointSize, item.Y * _pointSize, _pointSize, _pointSize);
        }

        private struct Item
        {
            public Item(int x, int y, int itemType)
            {
                X = x;
                Y = y;
                ItemType = itemType;
            }

            public int X { get; }

            public int Y { get; }

            public int ItemType { get; }
        }
    }
}
