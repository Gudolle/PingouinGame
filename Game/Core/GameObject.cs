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
        public Vector2 PositionRelatif = new Vector2();
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
		public int startX { get; }
		private int startY { get; }


        public Mob Monstre { get; set; }


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


        internal void InitialisePosition(Vector2 _Position)
        {
            Position = _Position;
            PositionRelatif = _Position;
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
		public int Cool(int height, int width, World world)
		{
			//Par rapport a un mob
			foreach (Mob elem in ListObject.MesMob)
			{
				switch (direction)
				{
					case Direction.TOP:
						if ((PositionRelatif.X <= elem.PositionRelatif.X && PositionRelatif.X + 32 >= elem.PositionRelatif.X) || (PositionRelatif.X <= elem.PositionRelatif.X + 32 && PositionRelatif.X + 32 >= elem.PositionRelatif.X + 32))
						{
							if (PositionRelatif.Y > elem.PositionRelatif.Y)
							{
								if (PositionRelatif.Y - 32 < elem.PositionRelatif.Y)
								{
									if (elem.PV > 0 || elem.Type == 2)
									{
										idSbire = elem.id;
                                        Monstre = elem.Type == 2 ? null : elem;
										return 2;
									}
								}
							}
						}
						idSbire = -1;
						break;
					case Direction.LEFT:
						if((PositionRelatif.Y <= elem.PositionRelatif.Y && PositionRelatif.Y + 32 >= elem.PositionRelatif.Y) || (PositionRelatif.Y <= elem.PositionRelatif.Y+32 && PositionRelatif.Y +32 >= elem.PositionRelatif.Y + 32))
						{
							if (PositionRelatif.X > elem.PositionRelatif.X)
							{
								if (PositionRelatif.X - 32 < elem.PositionRelatif.X)
								{
									if (elem.PV > 0 || elem.Type == 2)
									{
										idSbire = elem.id;
                                        Monstre = elem.Type == 2 ? null : elem; ;
                                        return 2;
									}
								}
							}
						}
						idSbire = -1;
						break;
					case Direction.RIGHT:
						if ((PositionRelatif.Y <= elem.PositionRelatif.Y && PositionRelatif.Y + 32 >= elem.PositionRelatif.Y) || (PositionRelatif.Y <= elem.PositionRelatif.Y + 32 && PositionRelatif.Y + 32 >= elem.PositionRelatif.Y + 32))
						{
							if (PositionRelatif.X + 32> elem.PositionRelatif.X)
							{
								if (PositionRelatif.X < elem.PositionRelatif.X)
								{
									if (elem.PV > 0 || elem.Type == 2)
									{
										idSbire = elem.id;
                                        Monstre = elem.Type == 2 ? null : elem; ;
                                        return 2;
									}
								}
							}
						}
						idSbire = -1;
						break;
					case Direction.BOTTOM:
						if ((PositionRelatif.X <= elem.PositionRelatif.X && PositionRelatif.X + 32 >= elem.PositionRelatif.X) || (PositionRelatif.X <= elem.PositionRelatif.X + 32 && PositionRelatif.X + 32 >= elem.PositionRelatif.X + 32))
						{
							if (PositionRelatif.Y + 32 > elem.PositionRelatif.Y)
							{
								if (PositionRelatif.Y < elem.PositionRelatif.Y)
								{
									if (elem.PV > 0 || elem.Type == 2)
									{
										idSbire = elem.id;
                                        Monstre = elem.Type == 2 ? null : elem; ;
                                        return 2;
									}
								}
							}
						}
						idSbire = -1;
						break;
				}
			}

			if ((PositionRelatif.X <= 0) && (direction == Direction.LEFT))
				return 1;
			if ((PositionRelatif.X >= (world.map.Width*32) - 32) && (direction == Direction.RIGHT))
				return 1;
			if ((PositionRelatif.Y <= 0) && (direction == Direction.TOP))
				return 1;
			if ((PositionRelatif.Y >= (world.map.Height*32) - 32) && (direction == Direction.BOTTOM))
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
