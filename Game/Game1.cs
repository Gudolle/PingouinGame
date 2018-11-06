using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TiledSharp;

namespace GameTest
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public int WIDTH = 960;
		public int HEIGHT = 672;

		public int Direct;
		public Random rand = new Random();

		Player player;

		ListObject listObject = new ListObject();
		List<Mob> Listmob;

		World world;
		SpriteFont font;
		Vector2 fontOrigin = new Vector2(0,0);

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			graphics.PreferredBackBufferWidth = WIDTH;
			graphics.PreferredBackBufferHeight = HEIGHT;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			this.IsMouseVisible = true;
			// TODO: Add your initialization logic here
			world = new World();
			player = new Player(32, 32,0,0);
			player.name = "SkarDeht";

			Listmob = listObject.InitiliasationMob();
			Window.Title = "Game";
			base.Initialize();

		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			world.map = new TmxMap("Content/map1.tmx");
			world.tileset = Content.Load<Texture2D>(world.map.Tilesets[0].Name.ToString());
			world.init();

			player.Texture = Content.Load<Texture2D>("player");
			player.FenetreText = Content.Load<Texture2D>("text");
			player.Position = new Vector2(WIDTH / 2, HEIGHT / 2);

			foreach (Mob elem in Listmob)
			{
				elem.Texture = Content.Load<Texture2D>("mob");
				if(elem.Type == 1)
					elem.Position = new Vector2(rand.Next(0, WIDTH), rand.Next(0, HEIGHT));
			}

			font = Content.Load<SpriteFont>("File");
			//TODO: use this.Content to load your game content here 
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
#if !__IOS__ && !__TVOS__
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
#endif

			// TODO: Add your update logic here

			base.Update(gameTime);
			graphics.PreferredBackBufferWidth = WIDTH;
			graphics.PreferredBackBufferHeight = HEIGHT;

			player.UpdateFrame(gameTime);
			if(!player.Affichage)
				player.Move(Keyboard.GetState(), HEIGHT, WIDTH, Listmob, world);
			player.Action(Keyboard.GetState(), Listmob);


			foreach (Mob elem in Listmob)
			{
				elem.UpdateFrame(gameTime);
				if(elem.Type == 1)
					elem.Move(rand.Next(0, 5), HEIGHT, WIDTH, Listmob, player, world);
			}
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.LimeGreen);

			//TODO: Add your drawing code here
			spriteBatch.Begin();
			world.DrawMap(spriteBatch);
			foreach (Mob elem in Listmob)
			{
				if (elem.PV > 0 || elem.Type == 2)
				{
					elem.DrawAnimation(spriteBatch);
					if (elem.Type == 1)
						elem.DrawHP(spriteBatch, font, fontOrigin);
					else
						elem.DrawName(spriteBatch, font, fontOrigin);
				}
			}
			player.DrawId(spriteBatch, font, fontOrigin);
			player.DrawAnimation(spriteBatch);
			player.DrawName(spriteBatch, font, fontOrigin);

			if (player.idSbire != -1 && player.Affichage)
				player.DrawTextPnj(spriteBatch, font, fontOrigin, HEIGHT, WIDTH);


			spriteBatch.End();




			base.Draw(gameTime);
		}
	}
}
