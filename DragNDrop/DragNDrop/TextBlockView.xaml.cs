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
    public partial class TextBlockView : SimpleBlockView
    {
        private string _labelText;
        public string LabelText
        {
            get => _labelText;
            set
            {
                _labelText = value;
                XamlLabel.Text = value;
            }
        }

        public TextBlockView(string labelText)
        {
            InitializeComponent();
            LabelText = labelText;
        }

        public override BlockView GetCopy()
        {
            return new TextBlockView(LabelText);
        }
    }
}