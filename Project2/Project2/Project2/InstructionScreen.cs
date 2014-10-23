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
    public class InstructionScreen
    {
        private Game1 game;
        private KeyboardState lastState;
        //private Texture2D texture; 
        private SpriteFont font; /* Place holder if you want to have text display instructions */

        public InstructionScreen(Game1 game)
        {
            this.game = game;
            lastState = Keyboard.GetState();
            //texture = game.Content.Load<Texture2D>("");
            font = game.Content.Load<SpriteFont>("SpriteFont1");
        }

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Enter) && lastState.IsKeyUp(Keys.Enter))
            {
                game.ReturnToMenu();
            }

            lastState = keyboardState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            //if (texture != null)
            //    spriteBatch.Draw(texture, new Vector2(0f, 0f), Color.White);

            //Used as a placeholder for now 
            spriteBatch.DrawString(font, "PRESS [ENTER] TO RETURN \n [PLACEHOLDER FOR INSTRUCTIONS]",
            new Vector2(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height),
            Color.GhostWhite); 

            spriteBatch.End();
        }
    }
}
