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
    public partial class CardView : BlockView
    {

        private string _labelText;
        public string LabelText
        {
            get => _labelText;
            set { _labelText = value;
                XamlLabel.Text = value;
            }
        }

        public CardView(string labelText)
        {
            InitializeComponent();

            LabelText = labelText;
        }

        public Frame GetFrame()
        {
            return XamlFrame;
        }


        public override BlockView GetCopy()
        {
            return new CardView(LabelText);
        }
    }
}