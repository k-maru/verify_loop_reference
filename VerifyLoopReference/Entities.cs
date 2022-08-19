using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerifyLoopReference
{
    public class A
    {
        public List<B> Bs { get; set; } = new List<B>();

        public B B { get; set; }
    }

    //public class B
    //{
    //    public List<AB> Bs { get; set; } = new List<AB>();
    //}

    public class B
    {
        public A A { get; set; }

        // public B PropB { get; set; } = default!;
    }
}
