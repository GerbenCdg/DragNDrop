using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DragNDrop
{
    class DragNDropHandler
    {
        public double AvailableWidth { private get; set; } = 340;
        public double AvailableHeight { private get; set; } = 540;

        private double LastX { get; set; } = -1;
        private double LastY { get; set; } = -1;

        private PanGestureRecognizer PanRecognizer { get; } = new PanGestureRecognizer();
        private BlockView Dragged { get; set; }
        private BlockView DraggedCopy { get; set; }

        public EventHandler<DragNDropEventArgs> DragStarted;
        public EventHandler<DragNDropEventArgs> DragUpdated;
        public EventHandler<DragNDropEventArgs> DragEnded;

        private AbsoluteLayout AbsoluteLayout { get; }

        public DragNDropHandler(AbsoluteLayout al)
        {
            AbsoluteLayout = al;
            SetupPanGestureRecognizers();
        }

        private void SetupPanGestureRecognizers()
        {
            PanRecognizer.PanUpdated += PanGesture_PanUpdated;

            foreach (var view in AbsoluteLayout.Children)
            {
                ((ContentView)view).Content.GestureRecognizers.Add(PanRecognizer);
            }

            Device.StartTimer(TimeSpan.FromMilliseconds(150), () =>
            {
                if (DraggedCopy != null)
                {
                    if (DraggedCopy.X == LastX && DraggedCopy.Y == LastY)
                    {
                        CompletePan();
                    }
                    else
                    {
                        LastX = DraggedCopy.X;
                        LastY = DraggedCopy.Y;
                    }
                }
                return true; // repeat the timer
            });
        }

        private void PanGesture_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            BlockView currentDrag = ((Frame)sender).Parent as BlockView;

            if (Dragged != currentDrag && DraggedCopy != null)
                return;

            if (e.StatusType != GestureStatus.Started && DraggedCopy == null)
                return;

            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    Dragged = currentDrag;
                    StartPan();
                    DragStarted?.Invoke(this, new DragNDropEventArgs(Dragged, new Point(Dragged.X, Dragged.Y)));
                    break;

                case GestureStatus.Running:
                    RunPan(e);
                    DragUpdated?.Invoke(this, new DragNDropEventArgs(Dragged, new Point(DraggedCopy.X, DraggedCopy.Y)));
                    break;

                case GestureStatus.Completed:
                    CompletePan();
                    DragEnded.Invoke(this, new DragNDropEventArgs(Dragged, new Point(Dragged.X, Dragged.Y)));
                    break;

                case GestureStatus.Canceled:
                    break;
            }
        }

        private void StartPan()
        {
            DraggedCopy = GhostFactory.YellowBorderGhostInstance(Dragged);

            AbsoluteLayout.Children.Add(DraggedCopy);
            Dragged.IsVisible = false;

            Point lastPoint = new Point(Dragged.X, Dragged.Y);
            AbsoluteLayout.SetLayoutBounds(DraggedCopy, new Rectangle(lastPoint, MainPage.RectSize));
        }

        private void RunPan(PanUpdatedEventArgs e)
        {
            // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
            double x =
                Math.Max(0, Math.Min(Dragged.X + e.TotalX, AvailableWidth));
            double y =
                Math.Max(0, Math.Min(Dragged.Y + e.TotalY, AvailableHeight));

            AbsoluteLayout.SetLayoutBounds(DraggedCopy, new Rectangle(new Point(x, y), MainPage.RectSize));
        }

        private void CompletePan()
        {
            AbsoluteLayout.SetLayoutBounds(Dragged, new Rectangle(new Point(DraggedCopy.X, DraggedCopy.Y), MainPage.RectSize));
            AbsoluteLayout.Children.Remove(Dragged);
            AbsoluteLayout.Children.Add(Dragged);
            Dragged.BlockGuid = Guid.NewGuid();
            Dragged.IsVisible = true;

            AbsoluteLayout.Children.Remove(DraggedCopy);
            DraggedCopy = null;
        }

    }
}
