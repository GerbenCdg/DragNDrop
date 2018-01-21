using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DragNDrop.BlockViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BlockView : ContentView
    {
        public override bool Equals(Object obj)
        {
            if (!(obj is BlockView)) return false;
            BlockView other = (BlockView)obj;
            return other.BlockGuid == BlockGuid;
        }

        public virtual BlockView GetCopy()
        {
            throw new NotImplementedException("This method should be overriden in extending classes");
        }

        public Guid BlockGuid { private get; set; } = Guid.NewGuid();

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
