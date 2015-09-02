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
        private bool powerShoot = false;




        public void Notify(SubjectObserver subject)
        {
            if (subject is Objet2D)
            {
                if (subject is Ship)
                {
                    analyseShipStatus(subject);
                }
                else if (subject is Projectile)
                {
                    analyseBonus(subject);
                }

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

        public void analyseShipStatus(SubjectObserver subject)
        {

        }

        public void analyseBonus(SubjectObserver subject)
        {
            
        }
    }
}
