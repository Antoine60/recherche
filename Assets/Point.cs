using System;

	public class Point
	{
		int m_x;
		int m_y;
        int m_z;

		public Point()
		{
			this.m_x = 0;
			this.m_y = 0;
            this.m_z = 0;
		}

		public Point(int x, int y, int z = 0)
		{
			this.m_x = x;
			this.m_y = y;
            this.m_z = z;
		}

		public Point(Point pt)
		{
			m_x = pt.X;
			m_y = pt.Y;
            m_z = pt.Z;
		}

		public int X
		{
			get { return m_x; }
			set { m_x = value; }
		}

		public int Y 
		{
			get { return m_y; }
			set { m_y = value; }
		}

        public int Z
        {
            get { return m_z; }
            set { m_z = value; }
        }
	}


