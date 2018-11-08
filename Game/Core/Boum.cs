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
    class Boum : GameObject
    {
        public static Texture2D TextureBoum;
        public int indexFrameWidth = 0;
        public int indexFrameHeight = 0;
        public Boum()
        {
            Texture = TextureBoum;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Position = CalculPosition.Calcul(PositionRelatif);
            spriteBatch.Draw(TextureBoum, Position, Source, Color.White);
        }

        //Update !
        public new void UpdateFrame(GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (time > 0.01f)
            {
                indexFrameWidth++;
                if (indexFrameWidth == 8) {
                    indexFrameHeight++;
                    indexFrameWidth = 0;
                    if (indexFrameHeight == 8)
                        ListObject.RemoveBoum.Add(this);
                }
                time = 0f;

            }

            Source = new Rectangle(
                indexFrameWidth * 100 + 100 * startX * 3,
                indexFrameHeight * 100 + 100 * startX * 4,
                100,
                100);
        }
    }
}
