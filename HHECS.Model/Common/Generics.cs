using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Common
{
    public class Generics<X, Y>
    {
        public Generics(X x, Y y)
        {
            this.x = x;
            this.y = y;
        }

        public X x { set; get; }
        public Y y { set; get; }
    }


}
