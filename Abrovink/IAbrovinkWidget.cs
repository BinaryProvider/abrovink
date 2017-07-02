using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abrovink
{
    public enum WidgetType { EyeDropper, Ruler }

    public delegate void WidgetClosing();

    public interface IAbrovinkWidget
    {
        WidgetType Type
        {
            get;
        }

        event WidgetClosing isClosing;
    }
}
