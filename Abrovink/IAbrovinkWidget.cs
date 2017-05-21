using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abrovink
{
    public delegate void WidgetClosing();

    public interface IAbrovinkWidget
    {
        event WidgetClosing isClosing;
    }
}
