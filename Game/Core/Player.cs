using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using GameTest.Core;

namespace GameTest
{
    public class Player : GameObject
    {
        public int aff = 0;
        public int tape = 0;
        public Boolean SpacePush = false; 

        public Boolean PositionBlockY {get;set; }
        public Boolean PositionBlockX { get; set; }


        public World CurrentWorld { get; set; }

		public Player(int frameWidth, int frameHeight,int minX, int minY)
			: base(frameWidth, frameHeight, minX, minY)
		{
			PV = 1000;
			direction = Direction.RIGHT;
			frameIndexWidth = framesIndex.Marche_2;
			frameIndexHeight = Direction.RIGHT;
		}
		public void Move(KeyboardState state, int height, int width)
		{
			if (state.IsKeyDown(Keys.Z) || state.IsKeyDown(Keys.W))
			{
				direction = Direction.TOP;
				int Possible = Cool(height, width, world);

				if (Possible == 0)
				{
					movement = true;
                    if (!PositionBlockY)
                    {
                        if (state.IsKeyDown(Keys.LeftShift))
                        {
                            Position.Y -= 3;
                            PositionRelatif.Y -= 3;
                        }
                        else
                        {
                            PositionRelatif.Y -= 1;
                            Position.Y -= 1;
                        }
                    }
                    else
                    {
                        PositionRelatif.Y -= 1;
                    }
				}
				else if (Possible == 1)
				{
					//Position.Y = height - 32;
				}

			}
			if (state.IsKeyDown(Keys.Q) || state.IsKeyDown(Keys.A))
			{
				direction = Direction.LEFT;
				int Possible = Cool(height, width, world);
				if (Possible == 0)
				{
					movement = true;
                    if (!PositionBlockX)
                    {
                        if (state.IsKeyDown(Keys.LeftShift))
                        {
                            Position.X -= 3;
                            PositionRelatif.X -= 3;
                        }
                        else
                        {
                            PositionRelatif.X -= 1;
                            Position.X -= 1;
                        }
                    }
                    else
                    {
                        PositionRelatif.X -= 1;
                    }

				}
				else if (Possible == 1)
				{
					//Position.X = width - 32;
				}
			}
			if (state.IsKeyDown(Keys.S))
			{
				direction = Direction.BOTTOM;
				int Possible = Cool(height, width, world);

				if (Possible == 0)
				{
					movement = true;
                    if (!PositionBlockY)
                    {
                        if (state.IsKeyDown(Keys.LeftShift))
                        {
                            Position.Y += 3;
                            PositionRelatif.Y += 3;
                        }
                        else
                        {
                            PositionRelatif.Y += 1;
                            Position.Y += 1;
                        }
                    }
                    else
                    {
                        PositionRelatif.Y += 1;
                    }
                }
				//else if(Possible == 1)
					//Position.Y = 0;
			}
			if (state.IsKeyDown(Keys.D))
			{
				direction = Direction.RIGHT;
				int Possible = Cool(height, width, world);

				if (Possible == 0)
				{
					movement = true;
                    if (!PositionBlockX)
                    {
                        if (state.IsKeyDown(Keys.LeftShift))
                        {
                            Position.X += 3;
                            PositionRelatif.X += 3;
                        }
                        else
                        {
                            PositionRelatif.X += 1;
                            Position.X += 1;
                        }
                    }
                    else
                    {
                        PositionRelatif.X += 1;
                    }
                }
				else if(Possible == 1) { 
					Position.X = 0;
                    PositionRelatif = Position;
                    world = ListObject.MesMondes[1];
                }
            }

			if(!state.IsKeyDown(Keys.D) && !state.IsKeyDown(Keys.Z) && !state.IsKeyDown(Keys.Q) && !state.IsKeyDown(Keys.S))
			{
				movement = false;
			}
		}


        public void Action(KeyboardState state)
		{
            #region ActionPnj
            if (state.IsKeyDown(Keys.Enter) && idSbire != -1)
			{
				if (Monstre.Type == 2 && aff == 0 && !Affichage)
				{
					Affichage = true;
					TextSelec = Monstre.Text;
					aff++;
				}
				else if(aff == 0){
					TextSelec = "";
					Affichage = false;
					aff++;
				}
			}
			else
			{
				aff = 0;
			}
            #endregion

            #region TapeMob
            if (state.IsKeyDown(Keys.F) && idSbire != -1)
			{
				if (Monstre.Type == 1 && tape == 0)
				{
					switch (direction)
					{
						case Direction.TOP:
                            Monstre.Position.Y -= 2;
                            Monstre.direction = Direction.BOTTOM;
							break;
						case Direction.BOTTOM:
                            Monstre.Position.Y += 2;
                            Monstre.direction = Direction.TOP;
							break;
						case Direction.LEFT:
                            Monstre.Position.X -= 2;
                            Monstre.direction = Direction.RIGHT;
							break;
						case Direction.RIGHT:
                            Monstre.Position.X += 2;
                            Monstre.direction = Direction.LEFT;
							break;	

					}
                    Monstre.PV -= 10;
                    Monstre.Direct = 4;
                    Monstre.NbDeplacement = 0;
					tape++;
				}
			}
			else
			{
				tape = 0;
			}
            #endregion

            #region tire
            
            if (state.IsKeyDown(Keys.Space) )
            {
                if (!SpacePush)
                {
                    SpacePush = true;
                    Tire();
                }
            }
            else
            {
                SpacePush = false;
            }
            #endregion

        }



        #region draw
        public void drawPVplayer(SpriteBatch spriteBatch, SpriteFont font, Vector2 fontOrigin)
        {
            spriteBatch.DrawString(
                font, "PV : " + PV,
                new Vector2(ListObject.WIDTH / 2, 40),
                Color.Black, 0, fontOrigin, 1, SpriteEffects.None, 0);
        }
        public void DrawTextPnj(SpriteBatch spriteBatch, SpriteFont font, Vector2 fontOrigin, int Height, int width)
        {
            spriteBatch.Draw(FenetreText, new Vector2((width / 2 - +FenetreText.Width / 2), (Height / 2 - FenetreText.Height / 2)),
                             new Rectangle(0, 0, 371, 151), Color.White);
            spriteBatch.DrawString(font, TextSelec, new Vector2((width / 2 - +FenetreText.Width / 2) + 15, (Height / 2 - FenetreText.Height / 2) + 15), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
        }
        #endregion





        public void Tire()
        {
            Shot MonTire = new Shot(direction);
            MonTire.Position.Y = Position.Y + 16;
            MonTire.Position.X = Position.X + 16;
            ListObject.MesTires.Add(MonTire);
        }

	}
}
