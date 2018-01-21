using Xamarin.Forms.Xaml;

namespace DragNDrop.BlockViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextBlockView : BlockViews.SimpleBlockView
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

        public override BlockViews.BlockView GetCopy()
        {
            return new TextBlockView(LabelText);
        }
    }
}