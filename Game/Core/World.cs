using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using TiledSharp;


namespace GameTest
{
	public class World : GameObject
	{
		public TmxMap map { get; set; }
        public Texture2D tileset { get; set; }

        public string nomFiles { get; set; }

        private int tileWidth { get; set; }
        private int tileHeight { get; set; }
        private int tilesetTilesWide { get; set; }
        private int tilesetTilesHigh { get; set; }




		public World(string _file)
		{
            nomFiles = _file;

		}
		public void init()
		{
			tileWidth = map.Tilesets[0].TileWidth;
			tileHeight = map.Tilesets[0].TileHeight;

			tilesetTilesWide = tileset.Width / tileWidth;
			tilesetTilesHigh = tileset.Height / tileHeight;
		}
		public void DrawMapFirstCalc(SpriteBatch spriteBatch)
		{
            Vector2 PositionMap = CalculPlayer();

			for (var i = 0; i < map.Layers[0].Tiles.Count; i++)
			{
				int gid = map.Layers[0].Tiles[i].Gid;

				// Empty tile, do nothing
				if (gid != 0)
				{
					int tileFrame = gid - 1;
					int column = tileFrame % tilesetTilesWide;
					int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

					float x = (i % map.Width) * map.TileWidth + PositionMap.X;
                    float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight + PositionMap.Y;

					Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);

					spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
				}
			}
		}
        public void DrawMapSecondCalc(SpriteBatch spriteBatch)
        {
            if (map.Layers.Count > 1)
            {
                for (var i = 0; i < map.Layers[1].Tiles.Count; i++)
                {
                    int gid = map.Layers[1].Tiles[i].Gid;

                    // Empty tile, do nothing
                    if (gid != 0)
                    {
                        int tileFrame = gid - 1;
                        int column = tileFrame % tilesetTilesWide;
                        int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                        float x = (i % map.Width) * map.TileWidth;
                        float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;

                        Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);

                        spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                    }
                }
            }
        }

        private Vector2 CalculPlayer()
        {
            Vector2 PlayerPosition = ListObject.player.PositionRelatif;
            Vector2 PositionMapRelatif = new Vector2();

            int mapY = (map.Height * 32) - ListObject.HEIGHT / 2;
            if (PlayerPosition.Y > ListObject.HEIGHT / 2)
            {
                if (mapY < PlayerPosition.Y)
                {
                    PositionMapRelatif.Y = ListObject.HEIGHT / 2 - mapY;
                    ListObject.player.PositionBlockY = false;

                }
                else
                {
                    PositionMapRelatif.Y = ListObject.HEIGHT / 2 - PlayerPosition.Y;
                    ListObject.player.PositionBlockY = true;
                }
            }
            else
                ListObject.player.PositionBlockY = false;


            int mapX = (map.Width * 32) - (ListObject.WIDTH / 2); 
            if (PlayerPosition.X > ListObject.WIDTH / 2)
            {
                if (mapX < PlayerPosition.X)
                {
                    PositionMapRelatif.X = ListObject.WIDTH / 2 - mapX;
                    ListObject.player.PositionBlockX = false;
                }
                else
                {
                    PositionMapRelatif.X = ListObject.WIDTH / 2 - PlayerPosition.X;
                    ListObject.player.PositionBlockX = true;
                }
            }
            else
                ListObject.player.PositionBlockX = false;
           

            return PositionMapRelatif;
        }
	}
}