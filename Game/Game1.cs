using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TiledSharp;
using GameTest.Core;
using System.Linq;


namespace GameTest
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

		public int WIDTH = 960;
		public int HEIGHT = 672;

		public int Direct;
		public Random rand = new Random();


        public SpriteFont font;
        public Vector2 fontOrigin = new Vector2(0,0);

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

            ListObject.HEIGHT = HEIGHT;
            ListObject.WIDTH = WIDTH;

			this.IsMouseVisible = true;
			// TODO: Add your initialization logic here



            ListObject.InitialiseMonde();

			Window.Title = "PingouinGame";
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

			
            foreach(World item in ListObject.MesMondes)
            {

                item.map = new TmxMap(String.Format("Content/Map/{0}", item.nomFiles));
                item.tileset = Content.Load<Texture2D>(String.Format("tiles/{0}", item.map.Tilesets[0].Name.ToString()));
                item.init();

                //PremierMonde.map = new TmxMap("Content/Map/map1.tmx");
                //PremierMonde.tileset = Content.Load<Texture2D>(String.Format("tiles/{0}", world.map.Tilesets[0].Name.ToString()));
                //PremierMonde.init();
            }

            ListObject.player = new Player(32, 32, 0, 0);
            ListObject.player.name = "Pingouin";
            ListObject.player.PV = 150;
            ListObject.player.world = ListObject.MesMondes[0];

            Shot.TextureShotLeft = Content.Load<Texture2D>("shoot/shootleft");
            Shot.TextureShotRight = Content.Load<Texture2D>("shoot/shootright");
            Shot.TextureShotTop = Content.Load<Texture2D>("shoot/shoottop");
            Shot.TextureShotBottom = Content.Load<Texture2D>("shoot/shootbottom");

            Boum.TextureBoum = Content.Load<Texture2D>("Boum");


            ListObject.player.Texture = Content.Load<Texture2D>("player/player");
            ListObject.player.FenetreText = Content.Load<Texture2D>("text");
            ListObject.player.InitialisePosition(new Vector2(WIDTH / 2, HEIGHT / 2));


            Mob.TextureMonstre = Content.Load<Texture2D>("mob/mob");
            
			font = Content.Load<SpriteFont>("Font/File");
			//TODO: use this.Content to load your game content here 
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
            // TODO: Add your update logic here

			base.Update(gameTime);

			graphics.PreferredBackBufferWidth = WIDTH;
			graphics.PreferredBackBufferHeight = HEIGHT;


            if(ListObject.MesMob.Count == 0)
                ListObject.AppartionMob();

            ListObject.player.UpdateFrame(gameTime);
			if(!ListObject.player.Affichage)
                ListObject.player.Move(Keyboard.GetState(), HEIGHT, WIDTH);

            ListObject.player.Action(Keyboard.GetState());

            ListObject.RemoveEntity();

            foreach (Shot item in ListObject.MesTires)
            {
                item.Collision(HEIGHT, WIDTH, ListObject.player.world);
                item.Move();
            }
            foreach (Boum item in ListObject.MesBoum)
            {
                item.UpdateFrame(gameTime);
            }
            foreach (Mob elem in ListObject.MesMob)
			{
                elem.IsDead();
				elem.UpdateFrame(gameTime);
                elem.CalculPositionParRapportAuJoueur();
                //elem.Tape(gameTime);
                if (elem.Type == 1)
					elem.Move(rand.Next(0, 5), HEIGHT, WIDTH, ListObject.player.world);
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
            ListObject.player.world.DrawMapFirstCalc(spriteBatch);
            ListObject.player.world.DrawMapSecondCalc(spriteBatch);
            foreach (Mob elem in ListObject.MesMob.Where(x => x.world.nomFiles == ListObject.player.world.nomFiles).ToList())
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
            
            foreach (Shot item in ListObject.MesTires)
            {
                item.DrawShoot(spriteBatch);
            }
            foreach (Boum item in ListObject.MesBoum)
            {
                item.Draw(spriteBatch);
            }

            ListObject.player.DrawId(spriteBatch, font, fontOrigin);
            ListObject.player.drawPVplayer(spriteBatch, font, fontOrigin);
            ListObject.player.DrawAnimation(spriteBatch);
            ListObject.player.DrawName(spriteBatch, font, fontOrigin);

			if (ListObject.player.idSbire != -1 && ListObject.player.Affichage)
                ListObject.player.DrawTextPnj(spriteBatch, font, fontOrigin, HEIGHT, WIDTH);


			spriteBatch.End();




			base.Draw(gameTime);
		}
	}
}
