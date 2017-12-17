using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DragNDrop
{
    public partial class MainPage : ContentPage
    {
        private static readonly int CardWidth = 200;
        private static readonly int CardHeight = 180;
        private static Size RectSize { get; } = new Size(200, 180);

        private DragNDropHandler DragDropHandler { get; }

        public MainPage()
        {
            InitializeComponent();
            InitializeCardViews();
            DragDropHandler = new DragNDropHandler(al);
            SubscribeToDragNDropEvents();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Application.Current.MainPage.SizeChanged += (s, e) =>
            {
                DragDropHandler.AvailableMainPageWidth = Application.Current.MainPage.Width;
                DragDropHandler.AvailableMainPageHeight = Application.Current.MainPage.Height;
            };
        }

        private void InitializeCardViews()
        {
            AbsoluteLayout.SetLayoutBounds(testCardView, new Rectangle(new Point(100, 100), RectSize));
            AbsoluteLayout.SetLayoutBounds(testCardView2, new Rectangle(new Point(200, 100), RectSize));
            AbsoluteLayout.SetLayoutBounds(testCardView3, new Rectangle(new Point(100, 200), RectSize));
            AbsoluteLayout.SetLayoutBounds(testCardView4, new Rectangle(new Point(200, 200), RectSize));
            AbsoluteLayout.SetLayoutBounds(testCardView5, new Rectangle(new Point(100, 300), RectSize));
            AbsoluteLayout.SetLayoutBounds(testCardView6, new Rectangle(new Point(200, 300), RectSize));
            AbsoluteLayout.SetLayoutBounds(testCardView7, new Rectangle(new Point(100, 400), RectSize));
            AbsoluteLayout.SetLayoutBounds(testCardView8, new Rectangle(new Point(200, 400), RectSize));

            SimpleBlockView textBlockView1 = new TextBlockView(testCardView2, "Im a textBlockView !"); 
            AbsoluteLayout.SetLayoutBounds(textBlockView1, new Rectangle(new Point(150,100), new Size(150, 50)));
            // TODO in DragNDropHandler if user drags a SimpleBlockView, check if the copy(or original ?) is removed correctly after the drag 
        }

        private void SubscribeToDragNDropEvents()
        {
            DragDropHandler.DragStarted += (s, e) => { Log("Drag started !"); };

            DragDropHandler.DragUpdated += (s, e) =>
            {
                Log($"Drag updated ! newX : {e.Position.X}, newY : {e.Position.Y}");
            };

            DragDropHandler.DragEnded += (s, e) =>
            {
                Log("Drag ended !");
              
                IList<BlockView> coveredBlockViews = getBlockViewsCoveredByDragged(e);

                if (coveredBlockViews.Count != 0 && e.Block is SimpleBlockView)
                {
                    al.Children.Remove(e.Block);
                    (coveredBlockViews[0] as ContainerBlockView).AddChild((SimpleBlockView) e.Block);
                }
            };
        }

        private IList<BlockView> getBlockViewsCoveredByDragged(DragNDropEventArgs e)
        {
            BlockView bv;
            double eCenterX;
            double eCenterY;

            IList<BlockView> covered = new List<BlockView>();

            foreach (var child in al.Children)
            {
                bv = child as BlockView;
                if (!bv.Equals(e.Block))
                {
                    eCenterX = e.Position.X + e.Block.Width / 2;
                    eCenterY = e.Position.Y + e.Block.Height / 2;

                    if (eCenterX > bv.X && eCenterX < bv.X + bv.Width
                        && eCenterY > bv.Y && eCenterY < bv.Y + bv.Height)
                    {
                        // Center of e.Block is above bv.
                        covered.Add(bv);
                    }
                }
            }
            return covered;
        }

        public static void Log(String msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
        }
    }
}