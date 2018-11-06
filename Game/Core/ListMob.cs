using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameTest
{
	class ListObject
	{
		public List<Mob> InitiliasationMob()
		{
			int id = 0;
			int idfor;
			List<Mob> mobList = new List<Mob>();

			idfor = id + 10;
			for (; id < idfor; id++)
			{
				mobList.Add(new Mob(32, 32, 0, 0, 0, id));
				mobList[id].name = "Yann";
				mobList[id].PV = 100;
				mobList[id].Type = 1;
			}

			//PNJ Test
			mobList.Add(new Mob(32, 32, 1, 0, 0, id));
			mobList[id].name = "PNJ test";
			mobList[id].Text = "Text de pnj en test";
			mobList[id].Type = 2;
			mobList[id].Position = new Vector2(300, 300);

			id++;
			mobList.Add(new Mob(32, 32, 1, 0, 0, id));
			mobList[id].name = "PNJ test v2";
			mobList[id].Text = "Long text de la mort qui tue";
			mobList[id].Type = 2;
			mobList[id].Position = new Vector2(450, 450);

			return mobList;
		}
	}
}