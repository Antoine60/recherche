using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class packing : MonoBehaviour
{

    class Bloc
    {

        int x;
        int y;
        int largeur;
        int hauteur;

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
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

        public Bloc(int x, int y, int largeur, int hauteur)
        {
            this.x = x;
            this.y = y;
            this.largeur = largeur;
            this.hauteur = hauteur;
        }
    }

    class Point
    {
        int x;
        int y;

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public Point()
        {
            this.x = 0;
            this.y = 0;
        }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    class Program
    {
        public static List<Bloc> blocavenir = new List<Bloc>();
        public static List<Bloc> blocplace = new List<Bloc>();
        public static int max_container;
        public static List<Point> pointpotentiel = new List<Point>();

        public static bool test(Point p, Bloc p_objetavenir, List<Bloc> p_listobjetplace)
        {
            foreach (Bloc b in p_listobjetplace)
            {
                if ((((((b.X + b.Largeur > p.X) && (b.X + b.Largeur < p.X + p_objetavenir.Largeur))
                     || ((b.X >= p.X) && (b.X < p.X + p_objetavenir.Largeur))
                     || ((b.X < p.X) && (b.X + b.Largeur >= p.X + p_objetavenir.Largeur)))
                     )
                     && (((b.Y < p.Y + p_objetavenir.Hauteur) && (b.Y >= p.Y))
                     || ((b.Y + b.Hauteur > p.Y) && (b.Y + b.Hauteur <= p.Y + p_objetavenir.Hauteur))
                     || ((b.Y <= p.Y) && (b.Y + b.Hauteur >= p.Y + p_objetavenir.Hauteur)))) || (p.X + p_objetavenir.Largeur > max_container))
                {
                    return false;
                }
            }
            return true;
        }

        public static Bloc Meilleurposition(List<Point> p_listepointpotentiel, Bloc p_blocavenir, List<Bloc> p_listeobjetplace)
        {
            if (p_listepointpotentiel.Count != 0)
            {
                int min = p_listepointpotentiel[0].Y;
                Point meilleurpoint = p_listepointpotentiel[0];

                foreach (Point p in p_listepointpotentiel)
                {
                    if (p.Y < min)
                    {
                        min = p.Y;
                        meilleurpoint = p;
                    }
                    else if (p.Y == min)
                    {
                        if (p.X < meilleurpoint.X)
                        {
                            min = p.Y;
                            meilleurpoint = p;
                        }
                    }
                }


                p_blocavenir.X = meilleurpoint.X;
                p_blocavenir.Y = meilleurpoint.Y;
            }
            else
            {
                //
            }
            return p_blocavenir;
        }

        public static List<Bloc> TriBloc(List<Bloc> p_listeobjetavenir)
        {
            List<Bloc> retour = p_listeobjetavenir.OrderByDescending(b => b.Hauteur).ToList();
            return retour;
        }

        public static List<Bloc> Binpacking(List<Bloc> blocavenir, List<Bloc> blocplace)
        {
            List<Bloc> listeblocretour = new List<Bloc>();

            //Console.Write("Press any key to continue . . . ");
            //Console.ReadKey(true);

            if (blocplace.Count != 0)
            {

                foreach (Bloc b in blocplace)
                {

                    Point p_bas = new Point(b.X + b.Largeur, b.Y);
                    bool retour = test(p_bas, blocavenir[0], blocplace);

                    if (retour == true)
                    {
                        pointpotentiel.Add(p_bas);
                    }
                    //Console.Write(retour);
                    Point p_haut = new Point(b.X, b.Y + b.Hauteur);
                    retour = test(p_haut, blocavenir[0], blocplace);
                    //Console.Write(retour);
                    if (retour == true)
                    {
                        pointpotentiel.Add(p_haut);
                    }
                }

                blocplace.Add(Meilleurposition(pointpotentiel, blocavenir[0], blocplace));
                blocavenir.Remove(blocavenir[0]);

                pointpotentiel.Clear();
                if (blocavenir.Count != 0)
                {
                    blocplace = Binpacking(blocavenir, blocplace);
                }
            }
            else
            {
                Bloc o = new Bloc(0, 0, blocavenir[0].Largeur, blocavenir[0].Hauteur);
                blocplace.Add(o);
                blocavenir.Remove(o);

                blocplace = Binpacking(blocavenir, blocplace);

            }

            return blocplace;
        }


        /* public static void Main(string[] args)
         {


             // TODO: Implement Functionality Here

             max_container = 50;

             for (int i = 1; i < 15; i++)
             {
                 Program.blocavenir.Add(new Bloc(0, 0, i, i));
             }

             Program.blocavenir = TriBloc(Program.blocavenir);

             foreach(Bloc b in blocavenir){
                 Console.WriteLine(b.X + " - " + b.Y + " - " + b.Largeur + " - " + b.Hauteur);
             }

             blocplace = Program.Binpacking(Program.blocavenir, Program.blocplace);

         }*/
    }
    void Start()
    {
        Debug.Log("start");

        Program.max_container = 50;
        int compteur = 0;
        for (int i = 1; i < 15; i++)
        {
            Program.blocavenir.Add(new Bloc(0, 0, i, i));
        }

        Program.blocavenir = Program.TriBloc(Program.blocavenir);

       /*foreach (Bloc b in Program.blocavenir)
        {
            Debug.Log(b.X + " - " + b.Y + " - " + b.Largeur + " - " + b.Hauteur);
        }
        */
        Program.blocplace = Program.Binpacking(Program.blocavenir, Program.blocplace);

        foreach (Bloc b in Program.blocplace)
        {
            Color whateverColor = new Color(83, 83, 195,1);
            Texture newTexture = new Texture();
            compteur++;
            Debug.Log(b.X + " - " + b.Y + " - " + b.Largeur + " - " + b.Hauteur);
            GameObject nouveaucube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            nouveaucube.transform.position = transform.TransformVector(new Vector3(b.X, b.Y, 0));
            nouveaucube.transform.localScale = new Vector3(b.Largeur, b.Hauteur, 0);
            nouveaucube.name = "Objet " + compteur;
            nouveaucube.GetComponent<Renderer>().material.color = Color.black;
            /*nouveaucube.AddComponent<MeshRenderer>();
            MeshRenderer nouveaucubeRendrer = nouveaucube.GetComponent<MeshRenderer>();
            Material newMaterial = new Material(Shader.Find("Specular"));
            newMaterial.color = whateverColor;
            newMaterial.SetTexture("_MainTex", newTexture);
            nouveaucubeRendrer.material = newMaterial;*/
         
            //rend.enabled = true;

            /*nouveaucube.AddComponent<Renderer>();
            Renderer rend = nouveaucube.GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_SpecColor", Color.red);*/

        }
 
        /*
           foreach (Bloc b in Program.blocavenir)
           {
               Debug.Log("b.X " + b.X + " b.Y " + b.Y);

               /*Debug.Log("placed a cube");
               newcube.transform.position = transform.TransformVector(new Vector3(b.X, b.Y, 0));
               newcube.transform.localScale = new Vector3(b.Largeur, b.Hauteur, 0);
               Instantiate(newcube);
           }*/



    }
    // Update is called once per frame
    void Update()
    {

        //Instantiate(newcube, origine.position, origine.rotation);
        /*if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody instance;
            instance = Instantiate(newcube, origine.position, origine.rotation) as Rigidbody;
        }*/
    }
}



