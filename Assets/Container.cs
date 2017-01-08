using System;
using System.Collections.Generic;

public class Container
{
    List<Bloc> m_blocs = new List<Bloc>();
    int m_width;
    int m_depth;
    int m_height;
    int m_decalage;

    public Container(int width=50, int m_depth=50, int height=0, int decalage=0)
    {
        m_height = height;
        m_decalage = decalage;
    }

    public bool AddBloc(Bloc bloc, bool heigth=false)
    {
        List<Point> potentialPoints = new List<Point>();
        if (m_blocs.Count == 0)
        {
            bloc.setPosition(0, 0, 0);
            m_blocs.Add(bloc);
            return true;
        }
        foreach (Bloc bl in m_blocs)
        {
            //Si plaçable sur le coin en bas à droite
            if (isPlacable(bl.getBottomRightPoint(), bloc, heigth))
                potentialPoints.Add(bl.getBottomRightPoint());
            //si plaçable sur le coin en haut à droite
            if (isPlacable(bl.getUpperLeftPoint(), bloc, heigth))
                potentialPoints.Add(bl.getUpperLeftPoint());
            //si placable sur le point en bas, au fond à gauche
            if (isPlacable(bl.getBackLeftPoint(), bloc, heigth))
                potentialPoints.Add(bl.getBackLeftPoint());
        }
        if (potentialPoints.Count == 0)
            return false;
        m_blocs.Add(PositionHelper.Meilleurposition(potentialPoints, bloc, m_blocs));
        return true;
    }

    public bool isPlacable(Point p, Bloc bloc, bool height=false)
    {
        foreach (Bloc b in m_blocs)
        {
            if (((((b.X + b.Largeur > p.X) && (b.X + b.Largeur < p.X + bloc.Largeur))
                || ((b.X >= p.X) && (b.X < p.X + bloc.Largeur))
                || ((b.X < p.X) && (b.X + b.Largeur >= p.X + bloc.Largeur)))
                && ((p.Y >= b.Y) && (p.Y < b.Y + b.Hauteur)
                    || ((p.Y + bloc.Hauteur > b.Y) && (p.Y + bloc.Hauteur <= b.Y + b.Hauteur))
                    || ((p.Y <= b.Y) && (p.Y + bloc.Hauteur >= b.Y + b.Hauteur)))
                    && ((p.Z >= b.Z) && (p.Z < b.Z + b.Profondeur)
                        || ((p.Z + bloc.Profondeur > b.Z) && (p.Z + bloc.Profondeur <= b.Z + b.Profondeur))
                        || ((p.Z <= b.Z) && (p.Z + bloc.Profondeur >= b.Z + b.Profondeur))))
                || (p.X + bloc.Largeur > m_width)
                || (p.Z + bloc.Profondeur > m_depth)
                || (height == true && p.Y + bloc.Hauteur > m_height))
            {
                return false;
            }
        }
        return true;
    }

    public int Width
    {
        get { return m_width; }
    }

    public int depth
    {
        get { return m_depth; }
    }

    public List<Bloc> Blocs
    {
        get { return m_blocs; }
        set { m_blocs = value; }
    }
}


