using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DragNDrop.BlockViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageBlockView : SimpleBlockView
    {
        public ImageBlockView(string source)
        {
            InitializeComponent();
            ImageSource = source;
        }


        public static readonly BindableProperty ImageSourceProperty =
            BindableProperty.Create("ImageSource", typeof(string), typeof(ImageBlockView), "");

        public string ImageSource
        {
            get => (string)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public override BlockView GetCopy()
        {
            return new ImageBlockView(ImageSource);
        }
    }
}