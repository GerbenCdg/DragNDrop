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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private static Size RectSize { get; } = new Size(130, 90);

        private DragNDropHandler DragDropHandler { get; }


        public MainPage()
        {
            InitializeComponent();
            InitializeCardViews();
            InitializeListView();
            DragDropHandler = new DragNDropHandler(al);
            SubscribeToDragNDropEvents();

            al.ChildAdded += (s, e) =>
            {
                DragDropHandler.AddRecognizerOnChild((BlockViews.BlockView)e.Element);
            };
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            Application.Current.MainPage.SizeChanged += (s, e) =>
            {
                // TODO minus width of listView ! (check if tablet or phone)
                double offsetX = Application.Current.MainPage.Width * 0.2;

                DragDropHandler.AvailableMainPageWidth = Application.Current.MainPage.Width - offsetX;
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

       //     AbsoluteLayout.SetLayoutBounds(Img, new Rectangle(new Point(300, 600), RectSize));

        //    AbsoluteLayout.SetLayoutBounds(textBlockView1, new Rectangle(new Point(150, 100), new Size(150, 50)));
            AbsoluteLayout.SetLayoutBounds(textBlockView2, new Rectangle(new Point(250, 100), new Size(150, 50)));
            AbsoluteLayout.SetLayoutBounds(textBlockView3, new Rectangle(new Point(350, 100), new Size(150, 50)));

            // TODO in DragNDropHandler if user drags a SimpleBlockView, check if the copy(or original ?) is removed correctly after the drag 
            // TODO keep the old dimensions of block before starting drag?
        }

        private void InitializeListView()
        {
            List<ListViewItem> blockViewList = new List<ListViewItem>
            {
                new ListViewItem(){Name = "Boucle POUR", BlockView = new BlockViews.TextBlockView("Boucle POUR \n Boucle POUR \n Boucle POUR")},
                new ListViewItem(){Name = "Boucle TANT QUE", BlockView = new BlockViews.TextBlockView("Boucle TANT QUE \n Boucle TANT QUE \n Boucle TANT QUE")},
                new ListViewItem(){Name = "Condition SI", BlockView = new BlockViews.TextBlockView("Condition SI \n Condition SI \n Condition SI \n")},
                new ListViewItem(){Name = "Tourner à droite", BlockView = new BlockViews.TextBlockView("Tourner à droite \n Tourner à droite \n Tourner à droite")},
                new ListViewItem(){Name = "Tourner à gauche", BlockView = new BlockViews.TextBlockView("Tourner à gauche \n Tourner à gauche \n Tourner à gauche")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")},
                new ListViewItem(){Name = "Boucle FOR", BlockView = new BlockViews.TextBlockView("Boucle for")}
            };

            ListView.ItemsSource = blockViewList;
            ListView.ItemTapped += ListView_ItemTapped;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Log("Item tapped !");
            BlockViews.BlockView copy = ((ListViewItem)e.Item).BlockView.GetCopy();
            al.Children.Add(copy);
            // TODO adapt coords and size (phone or tablet) 
            AbsoluteLayout.SetLayoutBounds(copy, new Rectangle(new Point(200, 200), RectSize));
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

                IList<BlockViews.ContainerBlockView> coveredBlockViews = GetContainerBVsCoveredByDragged(e);

                if (coveredBlockViews.Count != 0 && e.Block is BlockViews.SimpleBlockView)
                {
                    BlockViews.SimpleBlockView sbv = (BlockViews.SimpleBlockView)e.Block;
                    BlockViews.ContainerBlockView cbv = coveredBlockViews[0];

                    sbv.RemoveFromParentContainerIfSet();
                    cbv.AddChild(sbv);
                }
            };
        }

        
        private IList<BlockViews.ContainerBlockView> GetContainerBVsCoveredByDragged(DragNDropEventArgs e)
        {
            BlockViews.ContainerBlockView cbv;
            double eCenterX;
            double eCenterY;

            IList<BlockViews.ContainerBlockView> covered = new List<BlockViews.ContainerBlockView>();

            foreach (var child in al.Children)
            {
                if ((cbv = child as BlockViews.ContainerBlockView) == null)
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