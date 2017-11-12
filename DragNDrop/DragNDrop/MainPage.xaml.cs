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
        Size rectSize = new Size(200, 180);
        const int screenWidth = 340;
        const int screenHeight = 540;

        // associates dragged with dragCopy
        IDictionary<CardView, CardView> dict = new Dictionary<CardView, CardView>();

        public MainPage()
        {
            InitializeComponent();

            AbsoluteLayout.SetLayoutBounds(testCardView, new Rectangle(new Point(100, 100), rectSize));
            AbsoluteLayout.SetLayoutBounds(testCardView2, new Rectangle(new Point(200, 100), rectSize));
            AbsoluteLayout.SetLayoutBounds(testCardView3, new Rectangle(new Point(100, 200), rectSize));
            AbsoluteLayout.SetLayoutBounds(testCardView4, new Rectangle(new Point(200, 200), rectSize));
            AbsoluteLayout.SetLayoutBounds(testCardView5, new Rectangle(new Point(100, 300), rectSize));
            AbsoluteLayout.SetLayoutBounds(testCardView6, new Rectangle(new Point(200, 300), rectSize));
            AbsoluteLayout.SetLayoutBounds(testCardView7, new Rectangle(new Point(100, 400), rectSize));
            AbsoluteLayout.SetLayoutBounds(testCardView8, new Rectangle(new Point(200, 400), rectSize));
            AbsoluteLayout.SetLayoutBounds(testCardView9, new Rectangle(new Point(100, 500), rectSize));

            PanGestureRecognizer panRecognizer;

            foreach (CardView cardView in al.Children)
            {
                panRecognizer = new PanGestureRecognizer();
                panRecognizer.PanUpdated += PanGesture_PanUpdated;
                cardView.Content.GestureRecognizers.Add(panRecognizer);
            }

            // allows to Complete the pan by performing a pan gesture near the dragged cardView if it's bugged.
            // TODO : check why it makes crash the app
            //    var panGesture2 = new PanGestureRecognizer();
            //    panGesture2.PanUpdated += PanGesture2_PanUpdated;
            //    Content.GestureRecognizers.Add(panGesture2);
        }

        private void PanGesture_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            CardView dragged;
            dragged = ((Frame)sender).Parent as CardView;

            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    StartPan(dragged);
                    break;

                case GestureStatus.Running:
                    RunPan(dragged, e);
                    break;

                case GestureStatus.Completed:
                    CompletePan(dragged);
                    break;

                case GestureStatus.Canceled:
                    Log("Cancelled ! ");
                    break;
            }
        }

        private void StartPan(CardView dragged)
        {
            Log("Started");

            CardView dragCopy = new CardView() { BackgroundColor = Color.IndianRed };
            dict.Add(dragged, dragCopy);

            al.Children.Add(dragCopy);
            dragged.IsVisible = false;

            Point lastPoint = new Point(dragged.X, dragged.Y);
            Log($"X : {lastPoint.X}, Y : {lastPoint.Y}");
            AbsoluteLayout.SetLayoutBounds(dragCopy, new Rectangle(lastPoint, rectSize));
        }

        private void RunPan(CardView dragged, PanUpdatedEventArgs e)
        {
            Log("Running");
            // Translate and ensure we don't pan beyond the wrapped user interface element bounds.

            double x =
                Math.Max(0, Math.Min(dragged.X + e.TotalX, screenWidth));
            double y =
                Math.Max(0, Math.Min(dragged.Y + e.TotalY, screenHeight));

            try
            {
                AbsoluteLayout.SetLayoutBounds(dict[dragged], new Rectangle(new Point(x, y), rectSize));
            }
            catch (KeyNotFoundException exc)
            {
                Log(exc.Message);
            }
            //PrintViews();
        }

        private void CompletePan(CardView dragged)
        {
            Log("Completed");
            PrintViewsInDict();

            try
            {
                CardView dragCopy = dict[dragged];

                al.Children.Remove(dragged);

                var panRecognizer = new PanGestureRecognizer();
                panRecognizer.PanUpdated += PanGesture_PanUpdated;

                dragCopy.BackgroundColor = Color.Transparent;
                dragCopy.Content.GestureRecognizers.Add(panRecognizer);

                dict.Remove(dragged);
            }
            catch (KeyNotFoundException exc)
            {
                Log(exc.Message);
            }
        }


        private void Log(String msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
        }

        private void PrintViews()
        {
            foreach (View v in al.Children)
            {
                Log("Child : " + v.GetType());
            }
        }

        private void PrintViewsInDict()
        {
            foreach (KeyValuePair<CardView, CardView> kv in dict)
            {
                Log($"Key : {kv.Key}, Value : {kv.Value}");
            }
        }
    }
}
