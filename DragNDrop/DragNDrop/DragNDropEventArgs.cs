using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DragNDrop
{
    class DragNDropEventArgs : EventArgs
    {
        public BlockViews.BlockView Block { get; }
        public Point Position { get; }

        public DragNDropEventArgs(BlockViews.BlockView block, Point position)
        {
            Block = block;
            Position = position;
        }
    }
}
