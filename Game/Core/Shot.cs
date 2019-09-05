using GameTest.FonctionUtile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTest.Core
{
    class Shot : GameObject
    {
        public static Texture2D TextureShotLeft;
        public static Texture2D TextureShotRight;
        public static Texture2D TextureShotTop;
        public static Texture2D TextureShotBottom;
        private int Vitesse = 5;
        private int porte = 250;
        private Vector2 PositionDepart = new Vector2();

        public Shot(Direction _direction, Vector2 _positionRelative)
        {
            PositionDepart = _positionRelative;
            PositionRelatif = _positionRelative;
            PositionRelatif.X += 16;
            PositionRelatif.Y += 16;


            direction = _direction;
            switch (_direction)
            {
                case Direction.TOP:
                    Texture = TextureShotTop;
                    break;
                case Direction.LEFT:
                    Texture = TextureShotLeft;
                    break;
                case Direction.BOTTOM:
                    Texture = TextureShotBottom;
                    break;
                case Direction.RIGHT:
                    Texture = TextureShotRight;
                    break;
            }
        }

        


        public void DrawShoot(SpriteBatch spriteBatch)
        {
            Position = CalculPosition.Calcul(PositionRelatif);
            spriteBatch.Draw(Texture, new Vector2(Position.X, Position.Y),  Color.Blue);
        }
        public void Move()
        {
            if (isPorte())
            {
                switch (direction)
                {
                    case Direction.TOP:
                        PositionRelatif.Y -= Vitesse;
                        break;
                    case Direction.LEFT:
                        PositionRelatif.X -= Vitesse;
                        break;
                    case Direction.BOTTOM:
                        PositionRelatif.Y += Vitesse;
                        break;
                    case Direction.RIGHT:
                        PositionRelatif.X += Vitesse;
                        break;
                }
            }
            else
            {
                ListObject.RemoveTires.Add(this);
            }
        }

        public Boolean isPorte()
        {
            switch (direction)
            {
                case Direction.TOP:
                case Direction.BOTTOM:
                    return Math.Abs(PositionRelatif.Y - PositionDepart.Y) < porte;
                case Direction.LEFT:
                case Direction.RIGHT:
                    return Math.Abs(PositionRelatif.X - PositionDepart.X) < porte;
                default:
                    return false;
            }
        }

        public void isTouche(int HEIGHT,int WIDTH)
        {
            Collision col = Cool(HEIGHT, WIDTH);
            if(col == Collision.Monstre)
            {
                Boum monBoum = new Boum()
                {
                    PositionRelatif = PositionRelatif
                };
                //monBoum.PositionRelatif.X -= 50;
                monBoum.PositionRelatif.Y -= 50;
                if(Monstre != null)
                    Monstre.PV -= 30;
                ListObject.MesBoum.Add(monBoum);
                ListObject.RemoveTires.Add(this);
            }
            else if(col == Collision.Bordure || col == Collision.Bloque)
            {
                ListObject.RemoveTires.Add(this);
            }
        }
    }
}
