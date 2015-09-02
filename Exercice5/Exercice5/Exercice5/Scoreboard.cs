using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercice5
{
    class Scoreboard: Observer
    {
        private int lives;
        private int score;
        public void Notify(SubjectObserver subject)
        {
            if (subject is Objet2D)
            {
                //Do something

            }
        }

        public int GetLives()
        {
            return lives;
        }

        public int GetScore()
        {
            return score;
        }
    }
}
