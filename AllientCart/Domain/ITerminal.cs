using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllientCart.Domain
{
    public interface ITerminal
    {
        void Scan(string item);
        decimal Total();
    }
}
