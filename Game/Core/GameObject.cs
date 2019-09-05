using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using TiledSharp;
using GameTest.FonctionUtile;

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

        public ChangeWorld Change { get; set; }


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
        public enum Collision
        {
            Rien = 0,
            Bordure = 1,
            Monstre = 2,
            Bloque = 3,
            Portail = 4
        }
        public void DrawAnimation(SpriteBatch spriteBatch)
		{
            Position = CalculPosition.Calcul(PositionRelatif);
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
				new Vector2(Position.X-8, Position.Y - 20),
				Color.White, 0, fontOrigin, 1, SpriteEffects.None, 0);
		}


        public void InitialisePosition(Vector2 _Position)
        {
            Position = _Position;
            PositionRelatif = _Position;
        }
        public void InitialisePosition(Vector2 _Position, Vector2 positionEcran)
        {
            Position = positionEcran;
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


        public Collision GetElement(TmxObject item)
        {
            switch (item.Type)
            {
                case "Bloque":
                    return Collision.Bloque;
                case "Portail":
                    Change = new ChangeWorld()
                    {
                        ID = int.Parse(item.Properties["Monde"]),
                        X = int.Parse(item.Properties["X"]),
                        Y = int.Parse(item.Properties["Y"])
                    };

                    return Collision.Portail;
                default:
                    return Collision.Bloque;
            }
        }
        public Collision Cool(int height, int width, int vitesse = 1)
        {
            if (world.map.ObjectGroups.Count > 0)
            {
                foreach (TmxObject item in world.map.ObjectGroups[0].Objects.ToList())
                {
                    Vector2 BloqueA = new Vector2((float)item.X, (float)item.Y);
                    Vector2 BloqueB = new Vector2((float)(item.X + item.Width), (float)(item.Y + item.Height));
                    Vector2 PositionFutur = PositionRelatif;
                    
                    switch (direction)
                    {
                        case Direction.TOP:
                            PositionFutur.Y -= vitesse;
                            if (PositionFutur.X + 32 > ((int)BloqueA.X + 1) && PositionFutur.X < BloqueB.X && PositionFutur.Y + 16 > BloqueA.Y && PositionFutur.Y + 16 < BloqueB.Y)
                            {
                                
                                return GetElement(item);
                            }
                            break;
                        case Direction.BOTTOM:

                            PositionFutur.Y += vitesse;
                            if (PositionFutur.X + 32 > ((int)BloqueA.X + 1 ) && PositionFutur.X  < BloqueB.X && PositionFutur.Y + 32 > BloqueA.Y && PositionFutur.Y + 32 < BloqueB.Y)
                            {
                                return GetElement(item);
                            }
                            break;
                        case Direction.LEFT:
                            PositionFutur.X -= vitesse;
                            if (PositionFutur.X > BloqueA.X && PositionFutur.X < BloqueB.X && PositionFutur.Y + 32 > ((int)BloqueA.Y + 1) && PositionFutur.Y +16  < BloqueB.Y)
                            {
                                return GetElement(item);
                            }
                            break;
                        case Direction.RIGHT:
                            PositionFutur.X += vitesse;
                            if (PositionFutur.X + 32 > BloqueA.X && PositionFutur.X + 32 < BloqueB.X && PositionFutur.Y + 32> ((int)BloqueA.Y + 1) && PositionFutur.Y + 16 < BloqueB.Y)
                            {
                                return GetElement(item);
                            }
                            break;
                    }
                }
            }
            //Par rapport a un mob
            foreach (Mob elem in ListObject.MesMob.Where(x => x.world.nomFiles == ListObject.player.world.nomFiles).ToList())
            {
                Vector2 BloqueA = new Vector2((float)elem.PositionRelatif.X, (float)elem.PositionRelatif.Y);
                Vector2 BloqueB = new Vector2((float)(elem.PositionRelatif.X + 32), (float)(elem.PositionRelatif.Y + 32));
                Vector2 PositionFutur = PositionRelatif;

                switch (direction)
                {
                    case Direction.TOP:
                        PositionFutur.Y -= vitesse;
                        if (PositionFutur.X + 32 > ((int)BloqueA.X + 1) && PositionFutur.X < BloqueB.X && PositionFutur.Y > BloqueA.Y && PositionFutur.Y < BloqueB.Y)
                        {
                            idSbire = elem.id;
                            Monstre = elem;
                            return Collision.Monstre;
                        }
                        break;
                    case Direction.BOTTOM:

                        PositionFutur.Y += vitesse;
                        if (PositionFutur.X + 32 > ((int)BloqueA.X + 1) && PositionFutur.X < BloqueB.X && PositionFutur.Y + 32 > BloqueA.Y && PositionFutur.Y + 32 < BloqueB.Y)
                        {
                            idSbire = elem.id;
                            Monstre = elem;
                            return Collision.Monstre;
                        }
                        break;
                    case Direction.LEFT:
                        PositionFutur.X -= vitesse;
                        if (PositionFutur.X > BloqueA.X && PositionFutur.X < BloqueB.X && PositionFutur.Y + 32 > ((int)BloqueA.Y + 1) && PositionFutur.Y < BloqueB.Y)
                        {
                            idSbire = elem.id;
                            Monstre = elem;
                            return Collision.Monstre;
                        }
                        break;
                    case Direction.RIGHT:
                        PositionFutur.X += vitesse;
                        if (PositionFutur.X + 32 > BloqueA.X && PositionFutur.X + 32 < BloqueB.X && PositionFutur.Y + 32 > ((int)BloqueA.Y + 1) && PositionFutur.Y  < BloqueB.Y)
                        {
                            idSbire = elem.id;
                            Monstre = elem;
                            return Collision.Monstre;
                        }
                        break;
                }
                idSbire = -1;
            }

            if ((PositionRelatif.X <= 0) && (direction == Direction.LEFT))
                return Collision.Bordure;
            if ((PositionRelatif.X >= (world.map.Width * 32) - 32) && (direction == Direction.RIGHT))
                return Collision.Bordure;
            if ((PositionRelatif.Y <= 0) && (direction == Direction.TOP))
                return Collision.Bordure;
            if ((PositionRelatif.Y >= (world.map.Height * 32) - 32) && (direction == Direction.BOTTOM))
                return Collision.Bordure;
            else
                return Collision.Rien;
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
