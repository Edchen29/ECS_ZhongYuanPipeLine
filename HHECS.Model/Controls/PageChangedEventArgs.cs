using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace HHECS.Model.Controls
{
    public class PageChangedEventArgs : RoutedEventArgs
    {
        public int CurrentPageIndex { get; set; }
    }
}
