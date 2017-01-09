using System;

	public class Bloc
	{
		Point m_p = new Point(0,0);
		int largeur;
		int hauteur;
        int profondeur;
        bool placed = false;
        int cout;

        public int Cout
        {
            get { return cout; }
            set { cout = value; }
        }

        
        public Bloc(int largeur, int hauteur, int profondeur = 0)
        {
            m_p.X = 0;
            m_p.Y = 0;
            m_p.Z = 0;
            this.largeur = largeur;
            this.hauteur = hauteur;
            this.profondeur = profondeur;
        }

        public Bloc(int largeur, int hauteur, int cout=0, int profondeur = 0)
        {
            m_p.X = 0;
            m_p.Y = 0;
            m_p.Z = 0;
            this.largeur = largeur;
            this.hauteur = hauteur;
            this.profondeur = profondeur;
            this.cout = cout;
        }


		public Bloc(int x, int y, int z,int largeur, int hauteur, int profondeur = 0)
		{
			m_p.X = x;
			m_p.Y = y;
            m_p.Z = z;
			this.largeur = largeur;
			this.hauteur = hauteur;
            this.profondeur = profondeur;
		}
			
		public Bloc(Bloc bloc)
		{
			m_p.X = bloc.X;
			m_p.Y = bloc.Y;
            m_p.Z = bloc.Z; 
			this.largeur = bloc.Largeur;
			this.hauteur = bloc.Hauteur;
            this.profondeur = bloc.profondeur;
		}

	public void setPosition(int x, int y, int z = 0)
	{
		m_p.X = x;
		m_p.Y = y;
        m_p.Z = z;
	}

	public Point getBottomRightPoint()
	{
        return new Point(m_p.X + largeur, m_p.Y, m_p.Z);
	}

	public Point getUpperLeftPoint()
	{
        return new Point(m_p.X, m_p.Y + hauteur, m_p.Z);
	}

    public Point getBackLeftPoint()
    {
        return new Point(m_p.X, m_p.Y, m_p.Z + profondeur);
    }
		public int X
		{
			get { return m_p.X; }
			set { m_p.X = value; }
		}

		public int Y
		{
			get { return m_p.Y; }
			set { m_p.Y = value; }
		}

        public int Z
        {
            get { return m_p.Z; }
            set { m_p.Z = value; }
        }

		public int Largeur
		{
			get { return largeur; }
			set { largeur = value; }
		}

		public int Hauteur
		{
			get { return hauteur; }
			set { hauteur = value; }
		}

        public int Profondeur
        {
            get { return profondeur; }
            set { profondeur = value; }
        }

        public bool Placed
        {
            get { return placed; }
            set { placed = value; }
        }
	}


