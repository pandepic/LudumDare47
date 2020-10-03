using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandaMonogame
{
    public interface IPoolable
    {
        bool IsAlive { get; set; }

        void Reset();
    }
}
