using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DragNDrop
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GhostView : BlockViews.BlockView
    {
        private BlockViews.BlockView _blockview;

        public BlockViews.BlockView BlockView
        {
            get => _blockview;
            private set
            {
                _blockview = value;
                XamlGrid.Children.Insert(0, value);
            }
        }

        private double _overlayOpacity;

        internal double OverlayOpacity
        {
            get => _overlayOpacity;
            set
            {
                if (value > 1 || value < 0)
                {
                    throw new ArgumentException("Value should be between 0 and 1");
                }
                _overlayOpacity = value;
                XamlOverlay.Opacity = value;
            }
        }

        private Color _overlayColor;

        internal Color OverlayColor
        {
            private get => _overlayColor;
            set
            {
                _overlayColor = value;
                XamlOverlay.Color = _overlayColor;
            }
        }

        private Color _borderColor;

        internal Color BorderColor
        {
            private get => _borderColor;
            set
            {
                _borderColor = value;
                XamlBorder.BackgroundColor = _borderColor;

                if (BlockView is BlockViews.ContainerBlockView container)
                {
                    container.GetFrame().HasShadow = false;
                    container.GetFrame().Margin = 7;
                }
            }
        }

        private int _borderWidth;

        internal int BorderWidth
        {
            private get => _borderWidth;
            set
            {
                _borderWidth = value;
                XamlBorder.Margin = _borderWidth;
            }
        }

        private double _borderOpacity;

        internal double BorderOpacity
        {
            get => _borderOpacity;
            set
            {
                if (value > 1 || value < 0)
                {
                    throw new ArgumentException("Value should be between 0 and 1");
                }
                _borderOpacity = value;
            }
        }

        public GhostView(BlockViews.BlockView b)
        {
            InitializeComponent();

            BlockView = b.GetCopy();
        }
    }
}