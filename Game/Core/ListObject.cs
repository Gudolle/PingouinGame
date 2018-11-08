using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using GameTest.Core;

namespace GameTest
{
	class ListObject
	{
        public static int multiplicateur = 1;
        public static int HEIGHT;
        public static int WIDTH;
        public static Player player;

		public static void AppartionMob()
		{
			int id = 0;
			int idfor;
            Random rand = new Random();
			idfor = id + (5 + 5*multiplicateur);
			for (; id < idfor; id++)
			{
                MesMob.Add(new Mob(32, 32, 0,0, 0, id));
                MesMob[id].name = "DARKWOLF";
                MesMob[id].PV = 100 * multiplicateur;
                MesMob[id].Type = 1;
                MesMob[id].InitialisePosition(new Vector2(rand.Next(0, WIDTH), rand.Next(0, HEIGHT)));
			}
            multiplicateur++;
            //PNJ Test

            //Mob Monstre1 = new Mob(32, 32, 1, 0, 0, id);
            //Monstre1.name = "PNJ test";
            //Monstre1.Text = "Text de pnj en test";
            //Monstre1.Type = 2;
            //Monstre1.Position = new Vector2(300, 300);

            //MesMob.Add(Monstre1);
        
            
		}

        public static void InitialiseMonde()
        {
            World PremierMonde = new World("map1.tmx");
            MesMondes.Add(PremierMonde);
            World SecondMonde = new World("map2.tmx");
            MesMondes.Add(SecondMonde);

        }

        //Entity
        public static List<Mob> MesMob = new List<Mob>();
        public static List<Shot> MesTires = new List<Shot>();
        public static List<Boum> MesBoum = new List<Boum>();
        public static List<World> MesMondes = new List<World>();

        //RemoveEntity
        public static List<Mob> RemoveMob = new List<Mob>();
        public static List<Shot> RemoveTires = new List<Shot>();
        public static List<Boum> RemoveBoum = new List<Boum>();

        public static void RemoveEntity()
        {
            foreach(Mob item in RemoveMob)
                MesMob.Remove(item);
            RemoveMob = new List<Mob>();

            foreach (Shot item in RemoveTires)
                MesTires.Remove(item);
            RemoveTires = new List<Shot>();

            foreach (Boum item in RemoveBoum)
                MesBoum.Remove(item);
            RemoveBoum = new List<Boum>();
        }
    }
}