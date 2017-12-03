using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DragNDrop
{
    static class GhostFactory
    {
        public static GhostView BlueGhostInstance(BlockView bv)
        {
            return new GhostView(bv)
            {
                OverlayColor = Color.LightSkyBlue,
                OverlayOpacity = 0.4f
            };
        }

        public static GhostView YellowBorderGhostInstance(BlockView bv)
        {
            return new GhostView(bv)
            {
                BorderColor = Color.LightGoldenrodYellow
            };
        }

        public static GhostView RedGhostInstance(BlockView bv)
        {
            return new GhostView(bv)
            {
                OverlayColor = Color.IndianRed,
                OverlayOpacity = 0.4f
            };
        }
    }
}
