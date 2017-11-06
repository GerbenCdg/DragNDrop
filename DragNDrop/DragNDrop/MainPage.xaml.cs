using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DragNDrop
{
    public partial class MainPage : ContentPage
    {
        Point lastPoint;
        Point lastDrag;
        Size rectSize = new Size(100, 100);
        const int screenWidth = 340;
        const int screenHeight = 540;

        const bool seeBug = false;

        public MainPage()
        {
            InitializeComponent();

            lastPoint = new Point(100, 100);
            lastDrag = new Point(100, 100);
            AbsoluteLayout.SetLayoutBounds(aquaBv, new Rectangle(lastPoint, rectSize));

            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += PanGesture_PanUpdated;
            aquaBv.GestureRecognizers.Add(panGesture);
        }


        private void PanGesture_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    if (!seeBug)
                    {
                        aquaBv.IsVisible = false;
                        aquaBv2.IsVisible = true;
                    }
                    break;

                case GestureStatus.Running:
                    // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
                    lastDrag.X =
                        Math.Max(0, Math.Min(lastPoint.X + e.TotalX, screenWidth));
                    lastDrag.Y =
                        Math.Max(0, Math.Min(lastPoint.Y + e.TotalY, screenHeight));

                    if (!seeBug)
                    {
                        AbsoluteLayout.SetLayoutBounds(aquaBv2, new Rectangle(lastDrag, rectSize));
                    }
                    else
                    {
                        AbsoluteLayout.SetLayoutBounds(aquaBv, new Rectangle(lastDrag, rectSize));
                    }

                    break;

                case GestureStatus.Completed:
                    // Store the translation applied during the pan
                    lastPoint.X = lastDrag.X;
                    lastPoint.Y = lastDrag.Y;

                    if (!seeBug)
                    {
                        AbsoluteLayout.SetLayoutBounds(aquaBv, new Rectangle(lastDrag, rectSize));

                        aquaBv2.IsVisible = false;
                        aquaBv.IsVisible = true;
                    }
                    break;

            }
        }
    }
}
