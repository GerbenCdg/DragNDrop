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
	public partial class ListViewCell : ViewCell
	{
		public ListViewCell ()
		{
			InitializeComponent ();
		}

        public static readonly BindableProperty NameProperty =
            BindableProperty.Create("Name", typeof(string), typeof(ListViewCell), "");

        public string Name
        {
            get => (string)GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }

        public static readonly BindableProperty BlockViewProperty =
            BindableProperty.Create("BlockView", typeof(BlockViews.BlockView), typeof(ListViewCell));

        public BlockViews.BlockView BlockView
        {
            get => (BlockViews.BlockView)GetValue(BlockViewProperty);
            set => SetValue(BlockViewProperty, value);
        }
    }
}