﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project2
{
    class Player
    {
        Physics playerPhysics;
        Game game;

        public Boolean isFalling;
        private Boolean isCollidingLeft;
        private Boolean isCollidingRight;
        private Boolean isCollidingTop;
        private Boolean isCollidingBottom;

        public Boolean isOnPlatform;
        Vector2 spawnPosition;
        public Vector2 position;
        Texture2D playerTexture;

        float max_x_velocity = 300;
        float max_y_velocity = 600;
        float min_y_velocity = -1000;
        int max_jump_height = 600;

        Vector2 velocity;
        Vector2 slowdown = new Vector2(15, 0);
        Vector2 gravity = new Vector2(0, 20);

        public int Width
        {
            get { return playerTexture.Width; }
        }

        public int Height
        {
            get { return playerTexture.Height; }
        }

        public Player(int X, int Y, Texture2D playerTexture, Game1 g)
        {

            game = g;
            spawnPosition = new Vector2(X, Y);
            position = new Vector2(X, Y);
            this.playerTexture = playerTexture;
            isFalling = true;
            isOnPlatform = false;

        }

        public void setXVelocity(float velocity)
        {

        }
        public void setYVelocity(float velocity)
        {
            this.velocity.Y = velocity;
        }

        public void Update(GameTime gameTime, KeyboardState keyboard)
        {
            // Update:
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!isOnPlatform)
            {
                if (velocity.Y < max_y_velocity)
                {
                    velocity.Y += gravity.Y;
                }
                if (velocity.Y > max_y_velocity)
                {
                    velocity.Y = max_y_velocity;
                }
            }


            //if velocity < 0, add to velocity until it reaches 0
            // if velocity > 0, subtract until it reaches 0
            if (velocity.X < 25 && velocity.X >= 0)
            {
                velocity.X = 0;
            }
            if (velocity.X > 0 )
            {
                
                velocity.X -= slowdown.X;
            }
            else
            {
                velocity.X += slowdown.X;
            }

            if (velocity.X < 25 && velocity.X >= 0)
            {
                velocity.X = 0;
            }


            if (keyboard.IsKeyDown(Keys.Right))
            {
                if (Math.Abs(velocity.X) < max_x_velocity)
                {
                    velocity.X += 50;
                }
            }
            if (keyboard.IsKeyDown(Keys.Left))
            {

                if (Math.Abs(velocity.X) < max_x_velocity)
                {
                    velocity.X -= 50;
                }
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
            }

            if (keyboard.IsKeyDown(Keys.Up))
            {
                if(isOnPlatform) {
                    Console.Write("\nVelocity Y: " + velocity.Y);
                    velocity.Y += -1000;
                    }
 

            }
            UpdatePosition(time);
            StayWithinBounds();
        }

        /* Player stays within the bounds of the game screen */
        public void StayWithinBounds()
        {
            if (position.X <= 0)
                position.X = 0;

            if (position.X >= game.GraphicsDevice.Viewport.Width - playerTexture.Width)
                position.X = game.GraphicsDevice.Viewport.Width - playerTexture.Width;

            if (position.Y <= 0)
                position.Y = 0;

            if (position.Y >= game.GraphicsDevice.Viewport.Height - playerTexture.Height)
                position.Y = game.GraphicsDevice.Viewport.Height - playerTexture.Height;
        }

        public void UpdatePosition(float time)
        {
                  
            position.X += (int)(velocity.X * time);
            position.Y += (int)(velocity.Y * time);
        }

        public void CheckCollisionSide(Rectangle tile)
        {
            Rectangle player = new Rectangle((int)position.X, (int)position.Y, Width, Height);

            if (player.Intersects(tile))
            {
                //check if player is to the left, right, top, or below the box
                int ydiff = (int)(tile.Y - player.Y);
                int xdiff = (int)(tile.X - player.X);
                int min_translation;
                //check where it's intersecting from. in order: bottom, top, left, right
                
                //if -x, +y - topRight
                //if -x, -y - bottomright
                //if +x, +y - topLeft
                //if +x, -y - bottomleft
                //Console.Write("\nIntersecting X:" + xdiff + " Y:" + ydiff);
                if (xdiff >= 0 && ydiff >= 0)
                {
                    //Console.Write("\nIntersect Top Left");
                    if (Math.Abs(player.Left - tile.Left) > Math.Abs(player.Top - tile.Top))
                    {
                        min_translation = player.Right - tile.Left;
                        position.X -= min_translation;
                        //Shift min_translation to left
                    }
                    else
                    {
                        //Shift min_translation up
                        min_translation = player.Bottom - tile.Top;
                        position.Y -= min_translation;
                        isOnPlatform = true;
                    }
                }
                else if (xdiff <= 0 && ydiff >= 0)
                {
                    if (Math.Abs(player.Right - tile.Right) > Math.Abs(player.Top - tile.Top))
                    {
                        //Shift min_translation to right
                        min_translation = player.Left - tile.Right;
                        position.X -= min_translation;
                    }
                    else
                    {
                        //Shift min_translation up
                        min_translation = player.Bottom - tile.Top;
                        position.Y -= min_translation;
                        isOnPlatform = true;
                    }
                }
                else if (xdiff >= 0 && ydiff <= 0)
                {
                    if (Math.Abs(player.Left - tile.Left) > Math.Abs(player.Bottom - tile.Bottom))
                    {
                        min_translation = player.Right - tile.Left;
                        position.X -= min_translation;
                        //Shift min_translation to left
                    }
                    else
                    {
                        min_translation = player.Top - tile.Bottom;
                        position.Y -= min_translation;
                        //Shift min translation down
                    }
                }
                else if (xdiff <= 0 && ydiff <= 0)
                {
                    if (Math.Abs(player.Right - tile.Right) > Math.Abs(player.Bottom - tile.Bottom))
                    {
                        //Shift min_translation to the right
                        min_translation = player.Left - tile.Right;
                        position.X -= min_translation;
                    }
                    else
                    {
                        min_translation = player.Top - tile.Bottom;
                        position.Y -= min_translation;
                        //Shift min translation down
                    }
                }

                ////Shifting up
                //if (collidingBottom(player, tile)) {
                //        min_translation = player.Bottom - tile.Top;
                //        position.Y -= min_translation;
                //        isOnPlatform = true;
                //}

                ////Shifting down
                //else if (collidingTop(player, tile))
                //{
                //    //player's pos is smaller than tile's, 
                //    min_translation = player.Top - tile.Bottom;
                //    position.Y -= min_translation;
                //}

                ////Shifting right
                //else if (collidingLeft(player, tile))
                //{
                //    min_translation = player.Left - tile.Right;
                //    position.X -= min_translation;
                //}

                ////Shifting left
                //else if (collidingRight(player, tile))
                //{
                //    min_translation = player.Right - tile.Left;
                //    position.X -= min_translation;
                //}
            }
        }

        private void resetCollisions()
        {
            isCollidingBottom = false;
            isCollidingLeft = false;
            isCollidingRight = false;
            isCollidingTop = false;
        }
        private Boolean collidingLeft(Rectangle A, Rectangle B)
        {
            if (A.Left <= B.Right && A.Right > B.Right)
            {
                return true;
            }
            return false;
        }

        private Boolean collidingRight(Rectangle A, Rectangle B)
        {
            if (A.Right >= B.Left && A.Left < B.Left)
            {
                return true;
            }
            return false;
        }

        private Boolean collidingTop(Rectangle A, Rectangle B)
        {
            if (A.Top <= B.Bottom && A.Bottom > B.Bottom)
            {
                return true;
            }
            return false;
        }

        private Boolean collidingBottom(Rectangle A, Rectangle B)
        {
            if (A.Bottom >= B.Top && A.Top < B.Top) 
            {
                return true;
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, new Rectangle((int)position.X,
                (int)position.Y,
                playerTexture.Width, playerTexture.Height), Color.White);
            resetCollisions();
        }

    }
}
