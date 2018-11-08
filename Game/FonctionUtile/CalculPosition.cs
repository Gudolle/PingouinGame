using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTest.FonctionUtile
{
    public class CalculPosition
    {
        public static Vector2 Calcul(Vector2 PositionRelatif)
        {
            Vector2 Position = new Vector2();

            Vector2 PositionRelatifPlayer = ListObject.player.PositionRelatif;
            Vector2 PositionPhysiquePlayer = ListObject.player.Position;

            float DifferenceX = PositionRelatif.X - PositionRelatifPlayer.X;
            float DifferenceY = PositionRelatif.Y - PositionRelatifPlayer.Y;

            Position.X = PositionPhysiquePlayer.X + DifferenceX;
            Position.Y = PositionPhysiquePlayer.Y + DifferenceY;

            return Position;
        }
    }
}
