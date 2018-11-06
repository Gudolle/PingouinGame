using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameTest
{
	public class GameObject
	{
		public Vector2 Position;
		public Texture2D Texture;
		public Texture2D FenetreText;

		public Rectangle Source;
		public float time;
		public float frameTime = 0.1f;
		public framesIndex frameIndexWidth;
		public Direction frameIndexHeight;
		public Direction direction;

		public int frameWidth { get; }
		public int frameHeight { get; }
		private int startX { get; }
		private int startY { get; }
		public int HEIGHT { get; set; }
		public int WIDTH { get; set; }

		public int idSbire { get; set; }
		public string TextSelec { get; set;}
		public bool Affichage { get; set;}

		public string name { get; set; }
		public bool movement { get; set; }
		public int PV { get; set; }

		public World world;

		public GameObject()
		{
		}
		public GameObject(int fWidth, int fHeight, int minX, int minY)
		{
			this.movement = false;

			idSbire = -1;

			frameWidth = fWidth;
			frameHeight = fHeight;

			TextSelec = "";

			startX = minX;
			startY = minY;
		}

		//Enumeration
		public enum Direction
		{
			LEFT = 1,
			RIGHT = 2,
			TOP = 3,
			BOTTOM = 0
		}

		public enum framesIndex
		{

			Marche_1 = 0,
			Marche_2 = 1,
			Marche_3 = 2,
		}

		//Draw all
		public void DrawAnimation(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, Position, Source, Color.White);
		}
		public void DrawId(SpriteBatch spriteBatch, SpriteFont font,Vector2 fontOrigin)
		{
			spriteBatch.DrawString(
				font, "Id : " + idSbire,
				new Vector2(40, 40),
				Color.White, 0, fontOrigin, 1, SpriteEffects.None, 0);
		}
		public void DrawName(SpriteBatch spriteBatch, SpriteFont font, Vector2 fontOrigin)
		{
			spriteBatch.DrawString(
				font, name,
				new Vector2(Position.X, Position.Y - 20),
				Color.White, 0, fontOrigin, 1, SpriteEffects.None, 0);
		}
		public void DrawTextPnj(SpriteBatch spriteBatch, SpriteFont font, Vector2 fontOrigin,int Height, int width)
		{
			spriteBatch.Draw(FenetreText, new Vector2((width/2 - +FenetreText.Width/2),(Height / 2 - FenetreText.Height / 2)), 
			                 new Rectangle(0,0,371,151), Color.White);
			spriteBatch.DrawString(font, TextSelec, new Vector2((width / 2 - +FenetreText.Width / 2)+15, (Height / 2 - FenetreText.Height / 2)+15), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
		}

		//Collision !
		public bool CoolMob(Vector2 PosPlayer)
		{
			switch (direction)
			{
				case Direction.TOP:
					if ((Position.X < PosPlayer.X && Position.X + 32 > PosPlayer.X) || (Position.X < PosPlayer.X + 32 && Position.X + 32 > PosPlayer.X + 32))
					{
						if (Position.Y > PosPlayer.Y)
						{
							if (Position.Y - 32 < PosPlayer.Y)
							{
								return true;
							}
						}
					}
					idSbire = -1;
					break;
				case Direction.LEFT:
					if ((Position.Y < PosPlayer.Y && Position.Y + 32 >= PosPlayer.Y) || (Position.Y < PosPlayer.Y + 32 && Position.Y + 32 > PosPlayer.Y + 32))
					{
						if (Position.X > PosPlayer.X)
						{
							if (Position.X - 32 < PosPlayer.X)
							{
								return true;
							}
						}
					}
					idSbire = -1;
					break;
				case Direction.RIGHT:
					if ((Position.Y < PosPlayer.Y && Position.Y + 32 > PosPlayer.Y) || (Position.Y < PosPlayer.Y + 32 && Position.Y + 32 > PosPlayer.Y + 32))
					{
						if (Position.X + 32 > PosPlayer.X)
						{
							if (Position.X < PosPlayer.X)
							{
								return true;
							}
						}
					}
					idSbire = -1;
					break;
				case Direction.BOTTOM:
					if ((Position.X < PosPlayer.X && Position.X + 32 > PosPlayer.X) || (Position.X < PosPlayer.X + 32 && Position.X + 32 > PosPlayer.X + 32))
					{
						if (Position.Y + 32 > PosPlayer.Y)
						{
							if (Position.Y < PosPlayer.Y)
							{
								return true;
							}
						}
					}
					idSbire = -1;
					break;
			}
			return false;
		}
		public int Cool(int height, int width, List<Mob> spriteMob, World world)
		{
			//Par rapport a l'environnement
			/*for (int x = 0; x < world.tailleY; x++)
			{
				for (int y = 0; y < world.tailleX; y++)
				{

					int PosX = world.TailleTile * x;
					int PosY = world.TailleTile * y;

					if (world.map[y, x, 2] == 1)
					{
						switch (direction)
						{
							case Direction.TOP:
								if ((Position.X < PosX && Position.X + 32 > PosX) || (Position.X < PosX + 32 && Position.X + 32 > PosX + 32))
								{
									if (Position.Y > PosY)
									{
										if (Position.Y - 32 < PosY)
											return 2;


									}
								}
								idSbire = -1;
								break;
							case Direction.LEFT:
								if ((Position.Y < PosY && Position.Y + 32 >= PosY) || (Position.Y < PosY + 32 && Position.Y + 32 > PosY + 32))
								{
									if (Position.X > PosX)
									{
										if (Position.X - 32 < PosX)
											return 2;
									}
								}
								idSbire = -1;
								break;
							case Direction.RIGHT:
								if ((Position.Y < PosY && Position.Y + 32 > PosY) || (Position.Y < PosY + 32 && Position.Y + 32 > PosY + 32))
								{
									if (Position.X + 32 > PosX)
									{
										if (Position.X < PosX)
											return 2;

									}
								}
								idSbire = -1;
								break;
							case Direction.BOTTOM:
								if ((Position.X < PosX && Position.X + 32 > PosX) || (Position.X < PosX + 32 && Position.X + 32 > PosX + 32))
								{
									if (Position.Y + 32 > PosY)
									{
										if (Position.Y < PosY)
											return 2;
									}
								}
								idSbire = -1;
								break;
						}
					}
				}
			}*/
			//Par rapport a un mob
			foreach (Mob elem in spriteMob)
			{
				switch (direction)
				{
					case Direction.TOP:
						if ((Position.X <= elem.Position.X && Position.X + 32 >= elem.Position.X) || (Position.X <= elem.Position.X + 32 && Position.X + 32 >= elem.Position.X + 32))
						{
							if (Position.Y > elem.Position.Y)
							{
								if (Position.Y - 32 < elem.Position.Y)
								{
									if (elem.PV > 0 || elem.Type == 2)
									{
										idSbire = elem.id;
										return 2;
									}
								}
							}
						}
						idSbire = -1;
						break;
					case Direction.LEFT:
						if((Position.Y <= elem.Position.Y && Position.Y + 32 >= elem.Position.Y) || (Position.Y <= elem.Position.Y+32 && Position.Y +32 >= elem.Position.Y + 32))
						{
							if (Position.X > elem.Position.X)
							{
								if (Position.X - 32 < elem.Position.X)
								{
									if (elem.PV > 0 || elem.Type == 2)
									{
										idSbire = elem.id;
										return 2;
									}
								}
							}
						}
						idSbire = -1;
						break;
					case Direction.RIGHT:
						if ((Position.Y <= elem.Position.Y && Position.Y + 32 >= elem.Position.Y) || (Position.Y <= elem.Position.Y + 32 && Position.Y + 32 >= elem.Position.Y + 32))
						{
							if (Position.X + 32> elem.Position.X)
							{
								if (Position.X < elem.Position.X)
								{
									if (elem.PV > 0 || elem.Type == 2)
									{
										idSbire = elem.id;
										return 2;
									}
								}
							}
						}
						idSbire = -1;
						break;
					case Direction.BOTTOM:
						if ((Position.X <= elem.Position.X && Position.X + 32 >= elem.Position.X) || (Position.X <= elem.Position.X + 32 && Position.X + 32 >= elem.Position.X + 32))
						{
							if (Position.Y + 32 > elem.Position.Y)
							{
								if (Position.Y < elem.Position.Y)
								{
									if (elem.PV > 0 || elem.Type == 2)
									{
										idSbire = elem.id;
										return 2;
									}
								}
							}
						}
						idSbire = -1;
						break;
				}
			}

			if ((Position.X <= 0) && (direction == Direction.LEFT))
				return 1;
			if ((Position.X >= width - 32) && (direction == Direction.RIGHT))
				return 1;
			if ((Position.Y <= 0) && (direction == Direction.TOP))
				return 1;
			if ((Position.Y >=( height - 32)) && (direction == Direction.BOTTOM))
				return 1;
			else
				return 0;
		}


		//Update !
		public void UpdateFrame(GameTime gameTime)
		{
			time += (float)gameTime.ElapsedGameTime.TotalSeconds;

			while (time > frameTime)
			{
				if (movement)
				{
					if (frameIndexWidth == framesIndex.Marche_1)
						frameIndexWidth = framesIndex.Marche_2;
					else if (frameIndexWidth == framesIndex.Marche_2)
						frameIndexWidth = framesIndex.Marche_3;
					else
						frameIndexWidth = framesIndex.Marche_1;
				}
				else
					frameIndexWidth = framesIndex.Marche_2;
				
				switch (direction)
				{
					case Direction.TOP:
						frameIndexHeight = Direction.TOP;
						break;
					case Direction.LEFT:
						frameIndexHeight = Direction.LEFT;
						break;
					case Direction.BOTTOM:
						frameIndexHeight = Direction.BOTTOM;
						break;
					case Direction.RIGHT:
						frameIndexHeight = Direction.RIGHT;
						break;
				}

				time = 0f;
			}

			Source = new Rectangle(
				(int)frameIndexWidth * frameWidth + 32 * startX * 3,
				(int)frameIndexHeight * frameHeight + 32 * startX * 4,
				frameWidth,
				frameHeight);
		}

	}
}
