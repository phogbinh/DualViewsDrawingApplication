using System.ComponentModel;
using System.Windows.Forms;

namespace WindowsFormsCustomComponents
{
    public partial class DoubleBufferedPanel : Panel, IComponent
    {
        public DoubleBufferedPanel()
        {
            DoubleBuffered = true;
        }
    }
}

