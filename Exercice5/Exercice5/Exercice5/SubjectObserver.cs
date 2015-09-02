using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercice5
{
    class SubjectObserver
    {
        public SubjectObserver()
        {
            observers = new List<Observer>();
        }

        public void AjouterObservateur(Observer observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        public void RemoveObserver(Observer observer)
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }        
        }

        protected void NotifyAllObservers()
        {
            foreach (Observer o in observers)
            {
                o.Notify(this);
            }
        }

        protected List<Observer> observers;
    }
}
