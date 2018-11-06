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
		public TmxMap map;
		public Texture2D tileset;

		int tileWidth;
		int tileHeight;
		int tilesetTilesWide;
		int tilesetTilesHigh;


		public World()
		{
			

		}
		public void init()
		{
			tileWidth = map.Tilesets[0].TileWidth;
			tileHeight = map.Tilesets[0].TileHeight;

			tilesetTilesWide = tileset.Width / tileWidth;
			tilesetTilesHigh = tileset.Height / tileHeight;
		}
		public void DrawMap(SpriteBatch spriteBatch)
		{
			for (var i = 0; i < map.Layers[0].Tiles.Count; i++)
			{
				int gid = map.Layers[0].Tiles[i].Gid;

				// Empty tile, do nothing
				if (gid == 0)
				{

				}
				else {
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
}