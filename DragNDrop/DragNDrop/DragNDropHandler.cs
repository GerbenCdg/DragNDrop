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
        public double AvailableMainPageWidth { private get; set; } = 340;
        public double AvailableMainPageHeight { private get; set; } = 540;

        // these Width and Height take in account the size of the dragged BlockView.
        private double AvailableWidth { get; set; }
        private double AvailableHeight { get; set; }

        private double LastX { get; set; } = -1;
        private double LastY { get; set; } = -1;

        private PanGestureRecognizer PanRecognizer { get; } = new PanGestureRecognizer();
        private Point lastPoint { get; set; }
        private BlockView Dragged { get; set; }
        private BlockView DraggedCopy { get; set; }

        public EventHandler<DragNDropEventArgs> DragStarted;
        public EventHandler<DragNDropEventArgs> DragUpdated;
        public EventHandler<DragNDropEventArgs> DragEnded;

        private Size DraggedSize { get; set; }
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
                    DragStarted?.Invoke(this, new DragNDropEventArgs(Dragged, new Point(lastPoint.X, lastPoint.Y)));
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
            DraggedCopy = GhostFactory.BlueGhostInstance(Dragged);
            DraggedSize = new Size(Dragged.Width, Dragged.Height);
            AvailableWidth = AvailableMainPageWidth - Dragged.Width;
            AvailableHeight = AvailableMainPageHeight - Dragged.Height;

            AbsoluteLayout.Children.Add(DraggedCopy);
            Dragged.IsVisible = false;

            lastPoint = GetScreenCoordinates(Dragged);

            AbsoluteLayout.SetLayoutBounds(DraggedCopy, new Rectangle(lastPoint, DraggedSize));
            MainPage.Log($"Real X : {Dragged.X}, Real Y : {Dragged.Y}");
        }

        private void RunPan(PanUpdatedEventArgs e)
        {
            // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
            double x =
                Math.Max(0, Math.Min(lastPoint.X + e.TotalX, AvailableWidth));
            double y =
                Math.Max(0, Math.Min(lastPoint.Y + e.TotalY, AvailableHeight));

            AbsoluteLayout.SetLayoutBounds(DraggedCopy, new Rectangle(new Point(x, y), DraggedSize));
        }

        private void CompletePan()
        {
            AbsoluteLayout.SetLayoutBounds(Dragged, new Rectangle(new Point(DraggedCopy.X, DraggedCopy.Y), DraggedSize));
            AbsoluteLayout.Children.Remove(Dragged);
            AbsoluteLayout.Children.Add(Dragged);
            Dragged.BlockGuid = Guid.NewGuid();
            Dragged.IsVisible = true;

            AbsoluteLayout.Children.Remove(DraggedCopy);
            DraggedCopy = null;
        }


        private Point GetScreenCoordinates(BlockView bv)
        {

            if (bv is SimpleBlockView)
            {
                SimpleBlockView sbv = (SimpleBlockView) bv;
                Point parentPoint = sbv.getParentPoint();

                if (parentPoint.X != -1)
                {
                    MainPage.Log("ParentPointX:" + parentPoint.X + ", PArentPoint.Y : " + parentPoint.Y);
                    return parentPoint;
                }
            }
             //else,  it's a container
            
            return new Point(bv.X,bv.Y);
        }

     /*   private Point GetScreenCoordinates(VisualElement view)
        {
            // A view's default X- and Y-coordinates are LOCAL with respect to the boundaries of its parent,
            // and NOT with respect to the screen. This method calculates the SCREEN coordinates of a view.
            // The coordinates returned refer to the top left corner of the view.

            // Initialize with the view's "local" coordinates with respect to its parent
            double screenCoordinateX = view.X;
            double screenCoordinateY = view.Y;

            // Get the view's parent (if it has one...)
            if (view.Parent.GetType() != typeof(App))
            {
                VisualElement parent = (VisualElement)view.Parent;


                // Loop through all parents
                while (parent != null)
                {
                    // Add in the coordinates of the parent with respect to ITS parent
                    screenCoordinateX += parent.X;
                    screenCoordinateY += parent.Y;

                    // If the parent of this parent isn't the app itself, get the parent's parent.
                    if (parent.Parent.GetType() == typeof(App))
                        parent = null;
                    else
                        parent = (VisualElement)parent.Parent;
                }
            }

            // Return the final coordinates...which are the global SCREEN coordinates of the view
            return new Point(screenCoordinateX, screenCoordinateY);
        }*/

    }
}
