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
        public ImageBlockView(ImageSource source)
        {
            InitializeComponent();
            ImageSource = source;
        }

        private ImageSource _imageSource;

        public ImageSource ImageSource
        {
            get => _imageSource;
            private set
            {
                _imageSource = value;
                XamlImage.Source = value;
            }
        }    

        public override BlockView GetCopy()
        {
            return new ImageBlockView(ImageSource);
        }
    }
}