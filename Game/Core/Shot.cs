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

        public Shot(Direction _direction)
        {
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
            spriteBatch.Draw(Texture, new Vector2(Position.X, Position.Y),  Color.Blue);
        }
        public void Move()
        {
            switch (direction)
            {
                case Direction.TOP:
                    Position.Y -= Vitesse;
                    break;
                case Direction.LEFT:
                    Position.X -= Vitesse;
                    break;
                case Direction.BOTTOM:
                    Position.Y += Vitesse;
                    break;
                case Direction.RIGHT:
                    Position.X += Vitesse;
                    break;
            }
        }
        public void Collision(int HEIGHT,int WIDTH, World world)
        {
            int col = Cool(HEIGHT, WIDTH, world);
            if(col == 2)
            {
                Boum monBoum = new Boum()
                {
                    Position = Position
                };
                monBoum.Position.X -= 50;
                monBoum.Position.Y -= 50;
                if(Monstre != null)
                    Monstre.PV -= 30;
                ListObject.MesBoum.Add(monBoum);
                ListObject.RemoveTires.Add(this);
            }
            else if(col == 1)
            {
                ListObject.RemoveTires.Add(this);
            }
        }
    }
}
