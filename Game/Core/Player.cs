using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using GameTest.Core;
using System.Linq;
using TiledSharp;

namespace GameTest
{
    public class Player : GameObject
    {
        public int aff = 0;
        public int tape = 0;
        public Boolean SpacePush = false;

        public Boolean PositionBlockY { get; set; }
        public Boolean PositionBlockX { get; set; }


        public World CurrentWorld { get; set; }

        public Player(int frameWidth, int frameHeight, int minX, int minY)
            : base(frameWidth, frameHeight, minX, minY)
        {
            PV = 1000;
            direction = Direction.RIGHT;
            frameIndexWidth = framesIndex.Marche_2;
            frameIndexHeight = Direction.RIGHT;
        }
        public void Move(KeyboardState state, int height, int width)
        {
            int Vitesse = 1;
            if (state.IsKeyDown(Keys.LeftShift))
                Vitesse = 3;


            if (state.IsKeyDown(Keys.Z) || state.IsKeyDown(Keys.W))
            {
                direction = Direction.TOP;
                Collision Possible = Cool(height, width, Vitesse);
                if (Possible == Collision.Rien)
                {
                    movement = true;
                    if (!PositionBlockY)
                    {
                        PositionRelatif.Y -= Vitesse;
                        Position.Y -= Vitesse;
                    }
                    else
                    {
                        PositionRelatif.Y -= Vitesse;
                    }
                }

            }
            if (state.IsKeyDown(Keys.Q) || state.IsKeyDown(Keys.A))
            {
                direction = Direction.LEFT;
                Collision Possible = Cool(height, width, Vitesse);
                if (Possible == Collision.Rien)
                {
                    movement = true;
                    if (!PositionBlockX)
                    {
                        PositionRelatif.X -= Vitesse;
                        Position.X -= Vitesse;
                    }
                    else
                    {
                        PositionRelatif.X -= Vitesse;
                    }

                }
            }
            if (state.IsKeyDown(Keys.S))
            {
                direction = Direction.BOTTOM;
                Collision Possible = Cool(height, width, Vitesse);
                if (Possible == Collision.Rien)
                {
                    movement = true;
                    if (!PositionBlockY)
                    {
                        Position.Y += Vitesse;
                        PositionRelatif.Y += Vitesse;
                    }
                    else
                    {
                        PositionRelatif.Y += Vitesse;
                    }
                }
            }
            if (state.IsKeyDown(Keys.D))
            {
                direction = Direction.RIGHT;

                Collision Possible = Cool(height, width, Vitesse);
                if (Possible == Collision.Rien)
                {
                    movement = true;
                    if (!PositionBlockX)
                    {
                        Position.X += Vitesse;
                        PositionRelatif.X += Vitesse;
                    }
                    else
                    {
                        PositionRelatif.X += Vitesse;
                    }
                }
                else if (Possible == Collision.Bordure)
                {
                    Position.X = 0;
                    PositionRelatif = Position;
                    world = ListObject.MesMondes[1];
                }
            }

            if (!state.IsKeyDown(Keys.D) && !state.IsKeyDown(Keys.Z) && !state.IsKeyDown(Keys.Q) && !state.IsKeyDown(Keys.S))
            {
                movement = false;
            }
        }


        public void Action(KeyboardState state)
        {
            #region ActionPnj
            if (state.IsKeyDown(Keys.Enter) && idSbire != -1)
            {
                if (Monstre.Type == 2 && aff == 0 && !Affichage)
                {
                    Affichage = true;
                    TextSelec = Monstre.Text;
                    aff++;
                }
                else if (aff == 0)
                {
                    TextSelec = "";
                    Affichage = false;
                    aff++;
                }
            }
            else
            {
                aff = 0;
            }
            #endregion

            #region TapeMob
            if (state.IsKeyDown(Keys.F) && idSbire != -1)
            {
                if (Monstre.Type == 1 && tape == 0)
                {
                    switch (direction)
                    {
                        case Direction.TOP:
                            Monstre.Position.Y -= 2;
                            Monstre.direction = Direction.BOTTOM;
                            break;
                        case Direction.BOTTOM:
                            Monstre.Position.Y += 2;
                            Monstre.direction = Direction.TOP;
                            break;
                        case Direction.LEFT:
                            Monstre.Position.X -= 2;
                            Monstre.direction = Direction.RIGHT;
                            break;
                        case Direction.RIGHT:
                            Monstre.Position.X += 2;
                            Monstre.direction = Direction.LEFT;
                            break;

                    }
                    Monstre.PV -= 10;
                    Monstre.Direct = 4;
                    Monstre.NbDeplacement = 0;
                    tape++;
                }
            }
            else
            {
                tape = 0;
            }
            #endregion

            #region tire

            if (state.IsKeyDown(Keys.Space))
            {
                if (!SpacePush)
                {
                    SpacePush = true;
                    Tire();
                }
            }
            else
            {
                SpacePush = false;
            }
            #endregion

        }



        #region draw
        public void drawPVplayer(SpriteBatch spriteBatch, SpriteFont font, Vector2 fontOrigin)
        {
            spriteBatch.DrawString(
                font, "PV : " + PV,
                new Vector2(ListObject.WIDTH / 2, 40),
                Color.Black, 0, fontOrigin, 1, SpriteEffects.None, 0);
        }
        public void DrawTextPnj(SpriteBatch spriteBatch, SpriteFont font, Vector2 fontOrigin, int Height, int width)
        {
            spriteBatch.Draw(FenetreText, new Vector2((width / 2 - +FenetreText.Width / 2), (Height / 2 - FenetreText.Height / 2)),
                             new Rectangle(0, 0, 371, 151), Color.White);
            spriteBatch.DrawString(font, TextSelec, new Vector2((width / 2 - +FenetreText.Width / 2) + 15, (Height / 2 - FenetreText.Height / 2) + 15), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
        }
        #endregion





        public void Tire()
        {
            Shot MonTire = new Shot(direction, PositionRelatif);
            MonTire.Position.Y = Position.Y + 16;
            MonTire.Position.X = Position.X + 16;
            MonTire.world = world;
            ListObject.MesTires.Add(MonTire);
        }

    }
}
