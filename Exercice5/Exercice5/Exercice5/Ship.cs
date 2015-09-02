using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Exercice5
{
    class Ship : Objet2D
    {
        private Vector2 thrust;
        private const double MAXSPEED = 100.0;
        private const float SLOWFACTOR = 20.0f;
        private bool alive;
        private static Ship spaceShip = null;

        public bool Alive
        {
            get
            {
                return alive;
            }
        }

        private Ship(Texture2D image, Vector2 position) : base(image, position)
        {
            alive = true;
        }

        public static Ship GetInstance()
        {
            if (spaceShip == null)
            {
                throw new Exception("Object does not exist");
            }
            return spaceShip;
        }

        public static void Create(Texture2D image, Vector2 position)
        {
            if (spaceShip != null)
            {
                throw new Exception("Object already created");
            }
            spaceShip = new Ship(image, position);
        } 

        public void CheckCollisionSphere(Objet2D theOther)
        {
            if (sphereCollision.Intersects(theOther.SphereCollision))
            {
                alive = false;
            }
        }

        public void MoveShip(float newThrust)
        {
            //Rappel, le thrust arrière doit être plus lent
            if (newThrust < 0)
                newThrust /= 2;

            //Angle 0 est un vecteur qui pointe vers le haut, et on augmente l'angle dans le sens des aiguilles d'une montre
            thrust.X += (float)(Math.Sin((double)rotationAngle) * newThrust);
            thrust.Y -= (float)(Math.Cos((double)rotationAngle) * newThrust);

            //Dans bien des vieux jeux la vitesse maximum semble être par axe, mais comme on a de la puissance de calcul, on va la faire totale.
            MaxThrust();

            //Il faut aussi déplacer les poly de collision
            MoveAll(thrust.X / SLOWFACTOR, thrust.Y / SLOWFACTOR);

            //Déplacement de l'autre côté.  On se donne un buffer de la taille de notre objet
            OtherSide(ref position.X, ref boiteCollision.Min.X, ref boiteCollision.Max.X, ref sphereCollision.Center.X, Exercice5.SCREENWIDTH, image.Width);
            OtherSide(ref position.Y, ref boiteCollision.Min.Y, ref boiteCollision.Max.Y, ref sphereCollision.Center.Y, Exercice5.SCREENHEIGHT, image.Height);
        }

        private void MoveAll(float moveX, float moveY)
        {
            this.position.X += moveX;
            this.position.Y += moveY;

            this.boiteCollision.Min.X += moveX;
            this.boiteCollision.Min.Y += moveY;
            this.boiteCollision.Max.X += moveX;
            this.boiteCollision.Max.Y += moveY;

            this.sphereCollision.Center.X += moveX;
            this.sphereCollision.Center.Y += moveY;
        }

        private void MaxThrust()
        {
            //Calcul de la vitesse maximum actuelle (pythagore)
            double totalSpeed = Math.Sqrt((double)(thrust.X * thrust.X + thrust.Y * thrust.Y));

            //Si plus grande que la vitesse actuelle, on se fait un ratio
            if (totalSpeed > MAXSPEED)
            {
                float ratio = (float)(MAXSPEED / totalSpeed);
                thrust.X *= ratio;
                thrust.Y *= ratio;
            }
        }

        private void OtherSide(ref float position, ref float minBox, ref float maxBox, ref float sphere, int screenSize, int imageSize)
        {
            if (position > screenSize + imageSize / 2)
            {
                position -= screenSize + imageSize;
                minBox -= screenSize + imageSize;
                maxBox -= screenSize + imageSize;
                sphere -= screenSize + imageSize;
            }
            else if (position < -imageSize / 2)
            {
                position += screenSize + imageSize;
                minBox += screenSize + imageSize;
                maxBox += screenSize + imageSize;
                sphere += screenSize + imageSize;
            }
        }
    }
}
