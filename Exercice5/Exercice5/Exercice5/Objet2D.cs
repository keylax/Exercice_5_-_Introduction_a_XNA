using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Exercice5
{
    class Objet2D: SubjectObserver
    {
        protected Texture2D image;
        protected Vector2 position;
        protected Vector2 posCenter;
        protected Vector2 offset;

        protected BoundingBox boiteCollision;
        protected BoundingSphere sphereCollision;
        protected float rotationAngle;

        public Texture2D Image
        {
            get
            {
                return image;
            }
        }
        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }
        public Vector2 Offset
        {
            get
            {
                return offset;
            }
        }
        public BoundingBox BoiteCollision
        {
            get
            {
                return boiteCollision;
            }
        }
        public BoundingSphere SphereCollision
        {
            get
            {
                return sphereCollision;
            }
        }
        public float RotationAngle
        {
            get
            {
                return rotationAngle;
            }
            set
            {
                rotationAngle = value;
            }
        }


        public Objet2D(Texture2D image, Vector2 position)
        {
            this.image = image;
            this.position = position;
            this.offset = new Vector2(image.Width / 2, image.Height / 2);
            this.posCenter = new Vector2(position.X + offset.X, position.Y + offset.Y);
            this.boiteCollision = new BoundingBox(new Vector3(position.X - offset.X, position.Y - offset.Y, 0), new Vector3(position.X + offset.X, position.Y + +offset.Y, 0));
            this.sphereCollision = new BoundingSphere(new Vector3(position.X, position.Y, 0), offset.X);
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
        }   
     

    }
}
