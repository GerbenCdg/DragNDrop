using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DragNDrop.BlockViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContainerBlockView : BlockViews.BlockView
    {

        public ContainerBlockView()
        {
            InitializeComponent();
        }

        public Frame GetFrame()
        {
            return XamlFrame;
        }

        public void AddChild(SimpleBlockView sbv)
        {
            XamlContainer.Children.Add(sbv);
          /*  VerticalOptions = "Start"
        HorizontalOptions = "FillAndExpand"*/
            sbv.ParentContainer = this;
        }

        public void RemoveChild(SimpleBlockView sbv)
        {
            XamlContainer.Children.Remove(sbv);
            sbv.ParentContainer = null;
         //   sbv.Parent = al;
        }

        public override BlockViews.BlockView GetCopy()
        {
            return new ContainerBlockView();
        }
    }
}