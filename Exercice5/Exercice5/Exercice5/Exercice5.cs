using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Exercice5
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Exercice5 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public const int SCREENWIDTH = 1280;
        public const int SCREENHEIGHT = 796;
        float asteroidSpeed = 5;
        Vector2 asteroidMovement;
        Texture2D spacefield;
        Objet2D asteroid;
        Ship playerShip;
        //float sunRotation = 0;


        public Exercice5()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO : ajouter la logique d’initialisation ici
            InitGraphicsMode(SCREENWIDTH, SCREENHEIGHT, false);
            base.Initialize();
        }

        private bool InitGraphicsMode(int width, int height, bool fullScreen)
        {
            // If we aren't using a full screen mode, the height and width of the window can
            // be set to anything equal to or smaller than the actual screen size.
            if (fullScreen == false)
            {
                if ((width <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                    && (height <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height))
                {
                    graphics.PreferredBackBufferWidth = width;
                    graphics.PreferredBackBufferHeight = height;
                    graphics.IsFullScreen = fullScreen;
                    graphics.ApplyChanges();
                    return true;
                }
            }
            else
            {
                // If we are using full screen mode, we should check to make sure that the display
                // adapter can handle the video mode we are trying to set.  To do this, we will
                // iterate thorugh the display modes supported by the adapter and check them against
                // the mode we want to set.
                foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
                {
                    // Check the width and height of each mode against the passed values
                    //if ((dm.Width == width) && (dm.Height == height))
                    //{
                    // The mode is supported, so set the buffer formats, apply changes and return
                    graphics.PreferredBackBufferWidth = width;
                    graphics.PreferredBackBufferHeight = height;
                    graphics.IsFullScreen = fullScreen;
                    graphics.ApplyChanges();
                    return true;
                    //}
                }
            }
            return false;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spacefield = Content.Load<Texture2D>("Background\\stars");
            //sun = new Objet2D(Content.Load<Texture2D>("Background\\sun"), new Vector2(SCREENWIDTH / 2, SCREENHEIGHT / 2));
            Ship.Create(Content.Load<Texture2D>("Sprites\\PlayerShip"), new Vector2(SCREENWIDTH / 4, SCREENHEIGHT / 2));
            playerShip = Ship.GetInstance();
            asteroid = new Asteroid(Content.Load<Texture2D>("Sprites\\bigAsteroid"), new Vector2(500, 100), AsteroidSize.BIG);

            Random r = new Random();
            double startAngle = r.NextDouble() * 2 * Math.PI;
            asteroidMovement.X = (float)Math.Cos(startAngle) * asteroidSpeed;
            asteroidMovement.Y = (float)Math.Sin(startAngle) * asteroidSpeed;      
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            GamePadState padOneState = GamePad.GetState(PlayerIndex.One);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape) || padOneState.Buttons.Back == ButtonState.Pressed)
                this.Exit();

            playerShip.RotationAngle += padOneState.ThumbSticks.Right.X / 16.0f;
            playerShip.MoveShip(padOneState.ThumbSticks.Left.Y);
            asteroid.MoveAsteroid(asteroidMovement.X, asteroidMovement.Y);

            //playerShip.CheckCollisionSphere(sun);
            //sunRotation += (float)(Math.PI / 500);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin();
            spriteBatch.Draw(spacefield, Vector2.Zero, Color.White);
            spriteBatch.Draw(asteroid.Image, asteroid.Position, Color.White);
            //spriteBatch.Draw(sun.Image, sun.Position, null, Color.White, sunRotation, sun.Offset, 1.0f, SpriteEffects.None, 0f);

            if (playerShip.Alive)
            {
                spriteBatch.Draw(playerShip.Image, playerShip.Position, null, Color.White, playerShip.RotationAngle, playerShip.Offset, 1.0f, SpriteEffects.None, 0f);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
