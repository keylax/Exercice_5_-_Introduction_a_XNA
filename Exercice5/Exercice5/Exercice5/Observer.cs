using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercice5
{
    interface Observer
    {
        void Notify(SubjectObserver suject);
    }
}
