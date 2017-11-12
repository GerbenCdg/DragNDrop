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
    public partial class CardView : ContentView
    {

        public CardView()
        {
            InitializeComponent();
        }
        
        public override bool Equals(Object obj)
        {
            if (obj == this) return true;
            if (obj == null || !(obj is CardView)) return false;
            CardView other = obj as CardView;
            return other.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    
    }
}