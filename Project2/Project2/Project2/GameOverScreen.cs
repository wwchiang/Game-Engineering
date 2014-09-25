﻿using System;
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

namespace Project2
{
    public class GameOverScreen
    {
        private Game1 game;
        private KeyboardState lastState;
        private Texture2D texture; /* Place holder if you want to have a picture for the start game screen */
        private SpriteFont font; /* Place holder if you want to have text display instructions */

        public GameOverScreen(Game1 game)
        {
            this.game = game;
            lastState = Keyboard.GetState();
            //texture = game.Content.Load<Texture2D>("");
            //font = game.Content.Load<SpriteFont>("");
        }

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Enter) && lastState.IsKeyUp(Keys.Enter))
            {
                game.StartGame();
            }
            else if (keyboardState.IsKeyDown(Keys.Escape) && lastState.IsKeyUp(Keys.Escape))
            {
                game.Exit();
            }

            lastState = keyboardState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
                spriteBatch.Draw(texture, new Vector2(0f, 0f), Color.White);

            // Used if you want to draw the instructions on 
            /* spriteBatch.DrawString(font, "PRESS [ENTER] TO START \n PRESS [ESC] TO EXIT",
                new Vector2(game.GraphicsDevice.Viewport.Width / 3 - 7, game.GraphicsDevice.Viewport.Height - 63),
                Color.GhostWhite); */
        }
    }
}
