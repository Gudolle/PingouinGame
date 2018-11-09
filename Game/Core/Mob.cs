using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using System.Collections.Generic;

namespace GameTest
{
	public class Mob : GameObject
	{
        public static Texture2D TextureMonstre;
		public int Direct;
		public int NbDeplacement;
        private float timeTape = 0;


        public Mob()
        {
            Texture = TextureMonstre;
        }

		public int id { get; set;}

		public int Type { get; set;}
		public string Text { get; set;}
		Random rand = new Random();

		public Mob(int frameWidth, int frameHeight,int minX, int minY, int orientation, int idini)
			: base(frameWidth, frameHeight, minX, minY)
        {
            Texture = TextureMonstre;
            this.direction = (Direction)orientation;
			frameIndexWidth = framesIndex.Marche_2;
			frameIndexHeight = Direction.RIGHT;
			NbDeplacement = 32;
			Direct = 0;
			id = idini;

		}
        public void Tape(GameTime gameTime)
        {
            if (!movement)
            {
                timeTape += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timeTape > 1f)
                {
                    ListObject.player.PV -= 10;
                    timeTape = 0;
                }
            }
            
        }
		public void Move(int nb, int height, int width)
		{
			if (NbDeplacement == 32)
			{
				Direct = nb;
				NbDeplacement = 0;
			}

            Collision Possible = Cool(height, width);
            if (Direct == 0)
            {
                direction = Direction.TOP;

                if (CoolMob(ListObject.player.Position))
                {
                    movement = false;
                    NbDeplacement = 32;
                    if (rand.Next(0, 5) == 2)
                    {
                        ListObject.player.PV -= 10;
                        ListObject.player.Position.Y -= 2;
                    }
                }
                else
                {
                    if (Possible == Collision.Rien)
                    {

                        PositionRelatif.Y -= 1;
                        movement = true;
                        NbDeplacement++;
                    }
                    else
                    {
                        NbDeplacement = 32;
                    }
                }
            }
            if (Direct == 1)
            {
                direction = Direction.LEFT;
                if (CoolMob(ListObject.player.Position))
                {
                    movement = false;
                    NbDeplacement = 32;
                    if (rand.Next(0, 5) == 2)
                    {
                        ListObject.player.PV -= 10;
                        ListObject.player.Position.X -= 2;
                    }
                }
                else
                {
                    if (Possible == Collision.Rien)
                    {

                        PositionRelatif.X -= 1;
                        movement = true;
                        NbDeplacement++;
                    }
                    else
                    {
                        NbDeplacement = 32;
                    }
                }
            }
            if (Direct == 2)
            {
                direction = Direction.BOTTOM;
                if (CoolMob(ListObject.player.Position))
                {
                    movement = false;
                    NbDeplacement++;
                    if (rand.Next(0, 5) == 2)
                    {
                        ListObject.player.PV -= 10;
                        ListObject.player.Position.Y += 2;
                    }
                }
                else
                {
                    if (Possible == Collision.Rien)
                    {

                        PositionRelatif.Y += 1;
                        movement = true;
                        NbDeplacement++;
                    }
                    else
                    {
                        NbDeplacement = 32;
                    }
                }

            }
            if (Direct == 3)
            {
                direction = Direction.RIGHT;
                if (CoolMob(ListObject.player.Position))
                {
                    movement = false;
                    NbDeplacement = 32;
                    if (rand.Next(0, 5) == 2)
                    {
                        ListObject.player.PV -= 10;
                        ListObject.player.Position.X -= 2;
                    }
                }
                else
                {
                    if (Possible == Collision.Rien)
                    {

                        PositionRelatif.X += 1;
                        movement = true;
                        NbDeplacement++;
                    }
                    else
                    {
                        NbDeplacement = 32;
                    }
                }
            }
            if (Direct >= 4)
            {
                movement = false;
                NbDeplacement++;
            }
        }


        private void DeplacementAgressif()
        {
            movement = false;
            float distanceX = ListObject.player.Position.X - Position.X;
            float distanceY = ListObject.player.Position.Y - Position.Y;
            int vitesse = ListObject.multiplicateur / 2;


            if (distanceX <= distanceY)
            {
                if (distanceY > 32)
                {

                    direction = Direction.BOTTOM;
                }
                else
                {
                    direction = Direction.TOP;
                }
            }
            if (distanceY < distanceX)
            {
                if (distanceX > 0)
                {
                    direction = Direction.RIGHT;
                }
                else
                {
                    direction = Direction.LEFT;
                }

            }

            if (distanceY >= 32)
            {
                movement = true;
                Position.Y += vitesse;
            }
            if (distanceY <= -32)
            {
                movement = true;
                Position.Y -= vitesse;
            }
            if (distanceX >= 32)
            {
                movement = true;
                Position.X += vitesse;
            }
            if (distanceX <= -32)
            {
                movement = true;
                Position.X -= vitesse;
            }
        }


		public void DrawHP(SpriteBatch spriteBatch, SpriteFont font, Vector2 fontOrigin)
		{

            Color Affichage;
            float pourcentage = (float)PV / (100 * ((float)ListObject.multiplicateur -1));


            if (pourcentage == 1)
                Affichage = Color.Green;
            else if (pourcentage > 0.5)
                Affichage = Color.White;
            else if (pourcentage > 0.3)
                Affichage = Color.Orange;
            else
                Affichage = Color.Red;



			spriteBatch.DrawString(
				font,"PV : " + PV,
				new Vector2(Position.X, Position.Y - 20),
                Affichage, 0, fontOrigin, 1, SpriteEffects.None, 0);
		}
        public void IsDead()
        {
            if(PV <= 0 && Type == 1)
            {
                ListObject.RemoveMob.Add(this);
            }
        }
	}
}
