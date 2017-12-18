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
        private static Size RectSize { get; } = new Size(130, 90);

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
            AbsoluteLayout.SetLayoutBounds(testCardView, new Rectangle(new Point(50, 100), RectSize));
            AbsoluteLayout.SetLayoutBounds(testCardView2, new Rectangle(new Point(200, 100), RectSize));
            AbsoluteLayout.SetLayoutBounds(testCardView3, new Rectangle(new Point(50, 200), RectSize));
            AbsoluteLayout.SetLayoutBounds(testCardView4, new Rectangle(new Point(200, 200), RectSize));
            AbsoluteLayout.SetLayoutBounds(testCardView5, new Rectangle(new Point(50, 300), RectSize));
            AbsoluteLayout.SetLayoutBounds(testCardView6, new Rectangle(new Point(200, 300), RectSize));
            AbsoluteLayout.SetLayoutBounds(testCardView7, new Rectangle(new Point(50, 400), RectSize));
            AbsoluteLayout.SetLayoutBounds(testCardView8, new Rectangle(new Point(200, 400), RectSize));

            AbsoluteLayout.SetLayoutBounds(textBlockView1, new Rectangle(new Point(150,100), new Size(150, 50)));
            AbsoluteLayout.SetLayoutBounds(textBlockView2, new Rectangle(new Point(250, 100), new Size(150, 50)));
            AbsoluteLayout.SetLayoutBounds(textBlockView3, new Rectangle(new Point(350, 100), new Size(150, 50)));

            // TODO in DragNDropHandler if user drags a SimpleBlockView, check if the copy(or original ?) is removed correctly after the drag 
            // TODO keep the old dimensions of block before starting drag?
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
              
                IList<ContainerBlockView> coveredBlockViews = getContainerBVsCoveredByDragged(e);

                if (coveredBlockViews.Count != 0 && e.Block is SimpleBlockView)
                {
                    SimpleBlockView sbv = (SimpleBlockView)e.Block;
                    ContainerBlockView cbv = coveredBlockViews[0];

                    sbv.RemoveFromParentContainerIfSet();
                    cbv.AddChild(sbv);                    
                }
            };
        }

        private IList<ContainerBlockView> getContainerBVsCoveredByDragged(DragNDropEventArgs e)
        {
            ContainerBlockView cbv;
            double eCenterX;
            double eCenterY;

            IList<ContainerBlockView> covered = new List<ContainerBlockView>();

            foreach (var child in al.Children)
            {
                if ((cbv = child as ContainerBlockView) == null)
                    break;

                if (!cbv.Equals(e.Block))
                {
                    eCenterX = e.Position.X + e.Block.Width / 2;
                    eCenterY = e.Position.Y + e.Block.Height / 2;

                    if (eCenterX > cbv.X && eCenterX < cbv.X + cbv.Width
                        && eCenterY > cbv.Y && eCenterY < cbv.Y + cbv.Height)
                    {
                        // Center of e.Block is above bv.
                        covered.Add(cbv);
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