using System;
using System.Collections.Generic;
using System.Linq;

	public class PositionHelper
	{
		private PositionHelper () {}

		public static Bloc Meilleurposition(List<Point> p_listepointpotentiel, Bloc p_blocavenir, List<Bloc> p_listeobjetplace)
		{
			if (p_listepointpotentiel.Count != 0)
			{
				int minY = p_listepointpotentiel[0].Y;
				Point meilleurpoint = p_listepointpotentiel[0];

				foreach (Point p in p_listepointpotentiel)
				{
                    if (p.Y < minY)
                    {
                        minY = p.Y;
                        meilleurpoint = p;
                    }
                    else if (p.Y == minY && p.X < meilleurpoint.X)
                        meilleurpoint = p;
                    else if (p.Y == minY && p.X == meilleurpoint.X && p.Z < meilleurpoint.Z)
                        meilleurpoint = p;
				}
				p_blocavenir.X = meilleurpoint.X;
				p_blocavenir.Y = meilleurpoint.Y;
                p_blocavenir.Z = meilleurpoint.Z;
			}
			return p_blocavenir;
		}

		public static List<Bloc> TriBloc(List<Bloc> p_listeobjetavenir)
		{
			List<Bloc> retour = p_listeobjetavenir.OrderByDescending(b => b.Hauteur).ToList();
			return retour;
		}

        public static List<Bloc> TriBlocCout(List<Bloc> p_listeobjetavenir)
        {
            List<Bloc> retour = p_listeobjetavenir.OrderByDescending(b => b.Cout).ToList();
            return retour;
        }
	}


