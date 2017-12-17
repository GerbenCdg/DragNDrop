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
    public partial class ContainerBlockView : BlockView
    {

        private IList<BlockView> Children { get; } =  new List<BlockView>();
    
        public ContainerBlockView()
        {
            InitializeComponent();
        }

        public Frame GetFrame()
        {
            return XamlFrame;
        }

        public void AddChild(SimpleBlockView child)
        {
            Children.Add(child);
            Container.Children.Add(child);
        }


        public override BlockView GetCopy()
        {
            return new ContainerBlockView();
        }
    }
}