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
        public static Size RectSize { get; } = new Size(200, 180);

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
                DragDropHandler.AvailableWidth = Application.Current.MainPage.Width - CardWidth;
                DragDropHandler.AvailableHeight = Application.Current.MainPage.Height - CardHeight;
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

          //  GhostView gh = GhostFactory.YellowBorderGhostInstance(testCardView4);
          //  al.Children.Add(gh);
          //  AbsoluteLayout.SetLayoutBounds(gh, new Rectangle(new Point(100, 100), RectSize));

        }

        private void SubscribeToDragNDropEvents()
        {
            DragDropHandler.DragStarted += (s, e) =>
            {
                Log("Drag started !");
            };

            DragDropHandler.DragUpdated += (s, e) =>
            {
                Log($"Drag updated ! newX : {e.Position.X}, newY : {e.Position.Y}");
            };

            DragDropHandler.DragEnded += (s, e) =>
            {
                Log("Drag ended !");
            };

        }


        public static void Log(String msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
        }

    }
}
