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
		public int Direct;
		public int NbDeplacement;

		public int id { get; set;}

		public int Type { get; set;}
		public string Text { get; set;}
		Random rand = new Random();

		public Mob(int frameWidth, int frameHeight,int minX, int minY, int orientation, int idini)
			: base(frameWidth, frameHeight, minX, minY)
		{
			this.direction = (Direction)orientation;
			frameIndexWidth = framesIndex.Marche_2;
			frameIndexHeight = Direction.RIGHT;
			NbDeplacement = 32;
			Direct = 0;
			id = idini;

		}

		public void Move(int nb, int height, int width, List<Mob> mob, Player player, World world)
		{
			if (NbDeplacement == 32)
			{
				Direct = nb;
				NbDeplacement = 0;
			}
			
			if (Direct == 0)
			{
				direction = Direction.TOP;
				int Possible = Cool(height, width, mob, world);

				if (CoolMob(player.Position))
				{
					movement = false;
					NbDeplacement = 32;
					if (rand.Next(0, 5) == 2)
					{
						player.PV -= 10;
						player.Position.Y -= 2;
					}
				}
				else {
					if (Possible == 0)
					{

						Position.Y -= 1;
						movement = true;
						NbDeplacement++;
					}
					else if (Possible == 1)
					{
						Position.Y = height - 32;
						NbDeplacement = 32;
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
				int Possible = Cool(height, width, mob, world);
				if (CoolMob(player.Position))
				{
					movement = false;
					NbDeplacement = 32;
					if (rand.Next(0, 5) == 2)
					{
						player.PV -= 10;
						player.Position.X -= 2;
					}
				}
				else {
					if (Possible == 0)
					{

						Position.X -= 1;
						movement = true;
						NbDeplacement++;
					}
					else if (Possible == 1)
					{
						Position.X = width - 32;
						NbDeplacement = 32;
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
				int Possible = Cool(height, width, mob, world);
				if (CoolMob(player.Position))
				{
					movement = false;
					NbDeplacement++;
					if (rand.Next(0, 5) == 2)
					{
						player.PV -= 10;
						player.Position.Y += 2;
					}
				}
				else {
					if (Possible == 0)
					{

						Position.Y += 1;
						movement = true;
						NbDeplacement++;
					}
					else if (Possible == 1)
					{
						Position.Y = 0;
						NbDeplacement = 32;
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
				int Possible = Cool(height, width, mob, world);
				if (CoolMob(player.Position))
				{
					movement = false;
					NbDeplacement = 32;
					if (rand.Next(0, 5) == 2)
					{
						player.PV -= 10;
						player.Position.X -= 2;
					}
				}
				else {
					if (Possible == 0)
					{

						Position.X += 1;
						movement = true;
						NbDeplacement++;
					}
					else if (Possible == 1)
					{
						Position.X = 0;
						NbDeplacement = 32;
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
		public void DrawHP(SpriteBatch spriteBatch, SpriteFont font, Vector2 fontOrigin)
		{
			spriteBatch.DrawString(
				font,"PV : " + PV,
				new Vector2(Position.X, Position.Y - 20),
				Color.White, 0, fontOrigin, 1, SpriteEffects.None, 0);
		}
	}
}
