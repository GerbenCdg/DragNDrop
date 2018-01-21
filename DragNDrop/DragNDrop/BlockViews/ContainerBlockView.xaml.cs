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
            xamlContainer.Children.Add(sbv);
          /*  VerticalOptions = "Start"
        HorizontalOptions = "FillAndExpand"*/
            sbv.ParentContainer = this;
        }

        public void RemoveChild(SimpleBlockView sbv)
        {
            xamlContainer.Children.Remove(sbv);
            sbv.ParentContainer = null;
         //   sbv.Parent = al;
        }

        public override BlockView GetCopy()
        {
            return new ContainerBlockView();
        }
    }
}