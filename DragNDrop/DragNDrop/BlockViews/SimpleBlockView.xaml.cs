using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DragNDrop
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimpleBlockView : BlockView
    {
        public ContainerBlockView ParentContainer { private get; set; }

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