using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XState
{
    public interface Observer<T>
    {
        void Next(T value);

        void Error(Exception err);

        void Complete();
    }
}
