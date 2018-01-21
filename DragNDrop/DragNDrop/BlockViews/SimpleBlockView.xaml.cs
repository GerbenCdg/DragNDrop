using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DragNDrop.BlockViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimpleBlockView : BlockViews.BlockView
    {
        public BlockViews.ContainerBlockView ParentContainer { private get; set; }

        public SimpleBlockView()
        {
            InitializeComponent();
        }

        public void RemoveFromParentContainerIfSet()
        {
            ParentContainer?.RemoveChild(this);
        }

        public Point GetParentPoint()
        {
            if (ParentContainer == null)
                return new Point(-1,-1);
            else return new Point(ParentContainer.X, ParentContainer.Y);
        }

    }
}