using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameTest
{
	public class Player : GameObject
	{
		public int aff = 0;
		public int tape = 0;

		public Player(int frameWidth, int frameHeight,int minX, int minY)
			: base(frameWidth, frameHeight, minX, minY)
		{
			PV = 1000;
			direction = Direction.RIGHT;
			frameIndexWidth = framesIndex.Marche_2;
			frameIndexHeight = Direction.RIGHT;
		}


		public void Move(KeyboardState state, int height, int width, List<Mob> mob, World world)
		{

			if (state.IsKeyDown(Keys.Z) || state.IsKeyDown(Keys.W))
			{
				direction = Direction.TOP;
				int Possible = Cool(height, width, mob, world);

				if (Possible == 0)
				{
					movement = true;
					if (state.IsKeyDown(Keys.LeftShift))
						Position.Y -= 3;
					else
						Position.Y -= 1;
				}
				else if (Possible == 1)
				{
					Position.Y = height - 32;
				}

			}
			if (state.IsKeyDown(Keys.Q) || state.IsKeyDown(Keys.A))
			{
				direction = Direction.LEFT;
				int Possible = Cool(height, width, mob, world);
				if (Possible == 0)
				{
					movement = true;
					if (state.IsKeyDown(Keys.LeftShift))
						Position.X -= 3;
					else
						Position.X -= 1;

				}
				else if (Possible == 1)
				{
					Position.X = width - 32;
				}
			}
			if (state.IsKeyDown(Keys.S))
			{
				direction = Direction.BOTTOM;
				int Possible = Cool(height, width, mob, world);

				if (Possible == 0)
				{
					movement = true;
					if (state.IsKeyDown(Keys.LeftShift))
						Position.Y += 3;
					else
						Position.Y += 1;
				}
				else if(Possible == 1)
					Position.Y = 0;
			}
			if (state.IsKeyDown(Keys.D))
			{
				direction = Direction.RIGHT;
				int Possible = Cool(height, width, mob, world);

				if (Possible == 0)
				{
					movement = true;
					if (state.IsKeyDown(Keys.LeftShift))
						Position.X += 3;
					else
						Position.X += 1;
				}
				else if(Possible == 1)
					Position.X = 0;
			}

			if(!state.IsKeyDown(Keys.D) && !state.IsKeyDown(Keys.Z) && !state.IsKeyDown(Keys.Q) && !state.IsKeyDown(Keys.S))
			{
				movement = false;
			}
		}
		public void Action(KeyboardState state, List<Mob> mob)
		{
			if (state.IsKeyDown(Keys.Enter) && idSbire != -1)
			{
				if (mob[idSbire].Type == 2 && aff == 0 && !Affichage)
				{
					Affichage = true;
					TextSelec = mob[idSbire].Text;
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

			if (state.IsKeyDown(Keys.F) && idSbire != -1)
			{
				if (mob[idSbire].Type == 1 && tape == 0)
				{
					switch (direction)
					{
						case Direction.TOP:
							mob[idSbire].Position.Y -= 2;
							mob[idSbire].direction = Direction.BOTTOM;
							break;
						case Direction.BOTTOM:
							mob[idSbire].Position.Y += 2;
							mob[idSbire].direction = Direction.TOP;
							break;
						case Direction.LEFT:
							mob[idSbire].Position.X -= 2;
							mob[idSbire].direction = Direction.RIGHT;
							break;
						case Direction.RIGHT:
							mob[idSbire].Position.X += 2;
							mob[idSbire].direction = Direction.LEFT;
							break;	

					}
					mob[idSbire].PV -= 10;
					mob[idSbire].Direct = 4;
					mob[idSbire].NbDeplacement = 0;
					tape++;
				}
			}
			else
			{
				tape = 0;
			}

		}

	}
}
