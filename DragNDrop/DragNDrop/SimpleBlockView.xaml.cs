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
        public ContainerBlockView ParentContainer { get; }

        public SimpleBlockView(ContainerBlockView parent)
        {
            InitializeComponent();
            ParentContainer = parent;
        }

    }
}