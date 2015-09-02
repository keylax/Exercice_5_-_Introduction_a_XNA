using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Exercice5
{
    enum AsteroidSize { BIG, MEDIUM, SMALL};
    class Asteroid : Objet2D
    {
        private Vector2 movement;
        private const double MAXSPEED = 100.0;
        private const float SLOWFACTOR = 20.0f;
        private AsteroidSize size;
        public Asteroid[] asteroids = new Asteroid[2];
        public bool destroyed = false;

        public Asteroid(Texture2D image, Vector2 position, AsteroidSize size) : base(image, position)
        {
            this.size = size;

            if (size == AsteroidSize.BIG)
            {
                asteroids[0] = new Asteroid(image, position, AsteroidSize.MEDIUM);
                asteroids[1] = new Asteroid(image, position, AsteroidSize.MEDIUM);
            }
            else if (size == AsteroidSize.MEDIUM)
            {
                asteroids[0] = new Asteroid(image, position, AsteroidSize.SMALL);
                asteroids[1] = new Asteroid(image, position, AsteroidSize.SMALL);
            }
        }

        public void CheckCollisionSphere(Objet2D theOther)
        {
            if (sphereCollision.Intersects(theOther.SphereCollision))
            {
                destroyed = true;
                if (theOther.GetType() == this.GetType())
                {

                }
            }
        }


        public void MoveAsteroid(float moveX, float moveY)
        {
            Vector2 tempPos = new Vector2(this.position.X += moveX, this.position.Y += moveY);
            Vector2 tempCenter = tempPos + offset;
            sphereCollision.Center.X = tempCenter.X;
            sphereCollision.Center.Y = tempCenter.Y;

            this.position.X = tempPos.X;
            this.position.Y = tempPos.Y;
            posCenter = position + offset;
            boiteCollision.Min.X = position.X;
            boiteCollision.Min.Y = position.Y;
            boiteCollision.Max.X = position.X + image.Width;
            boiteCollision.Max.Y = position.Y + image.Height;
            sphereCollision.Center.X = posCenter.X;
            sphereCollision.Center.Y = posCenter.Y;

            OtherSide(ref position.X, ref boiteCollision.Min.X, ref boiteCollision.Max.X, ref sphereCollision.Center.X, Exercice5.SCREENWIDTH, image.Width);
            OtherSide(ref position.Y, ref boiteCollision.Min.Y, ref boiteCollision.Max.Y, ref sphereCollision.Center.Y, Exercice5.SCREENHEIGHT, image.Height);

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

        private void MaxThrust()
        {
            //Calcul de la vitesse maximum actuelle (pythagore)
            double totalSpeed = Math.Sqrt((double)(movement.X * movement.X + movement.Y * movement.Y));

            //Si plus grande que la vitesse actuelle, on se fait un ratio
            if (totalSpeed > MAXSPEED)
            {
                float ratio = (float)(MAXSPEED / totalSpeed);
                movement.X *= ratio;
                movement.Y *= ratio;
            }
        }
    }
}
