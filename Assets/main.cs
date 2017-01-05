using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEditor;

public class main : MonoBehaviour
{
    public static List<Bloc> solution2d()
    {
        const int BlocNb = 100, maxSize = 40, minSize = 1;
        List<Bloc> blocsToPlace = new List<Bloc>();
        int width, height, depth;
        Debug.Log("start");
        System.Random rand = new System.Random();

        //Program.max_container = 50;
        for (int i = 1; i <= BlocNb / 2; i++)
        {
            blocsToPlace.Add(new Bloc(10, 10, 0));
        }

        for (int i = 1; i <= BlocNb / 2; i++)
        {
            blocsToPlace.Add(new Bloc(20, 20, 0));
        }

        Container container = new Container();
        PositionHelper.TriBloc(blocsToPlace);
        foreach (Bloc bl in blocsToPlace)
        {
            container.AddBloc(bl);
        }
        return blocsToPlace;
    }

    public static List<Bloc> solution3d()
    {
        const int BlocNb = 500, maxSize = 40, minSize = 1;
        List<Bloc> blocsToPlace = new List<Bloc>();
        int width, height, depth;
        Debug.Log("start");
        System.Random rand = new System.Random();

        for (int i = 1; i <= BlocNb / 2; i++)
        {
            blocsToPlace.Add(new Bloc(10, 10, 10));
        }

        for (int i = 1; i <= BlocNb / 2; i++)
        {
            blocsToPlace.Add(new Bloc(15, 15, 15));
        }

        Container container = new Container();
        PositionHelper.TriBloc(blocsToPlace);
        foreach (Bloc bl in blocsToPlace)
        {
            container.AddBloc(bl);
        }
        return blocsToPlace;
    }

    public static List<Bloc> solution3dRandom()
    {
        const int BlocNb = 100, maxSize = 40, minSize = 1;
        List<Bloc> blocsToPlace = new List<Bloc>();
        int width, height, depth;
        Debug.Log("start");
        System.Random rand = new System.Random();

        //Program.max_container = 50;
        for (int i = 1; i < BlocNb; i++)
        {
            width = rand.Next(minSize, maxSize);
            height = rand.Next(minSize, maxSize);
            //EN 3D :   décommenter la ligne pour avoir le résultat en 2d ou en 3d
            depth = rand.Next(minSize, maxSize);
            //EN 2D :s
            //depth = 0;
            blocsToPlace.Add(new Bloc(width, height, depth));
        }

        Container container = new Container();
        //PositionHelper.TriBloc(blocsToPlace);
        foreach (Bloc bl in blocsToPlace)
        {
            container.AddBloc(bl);
        }
        return blocsToPlace;
    }

    public static List<Bloc> solution3dMultipleContainers()
    {
        int cpt = 0; int decalage = 0;
        const int BlocNb = 100, maxSize = 40, minSize = 1;
        List<Bloc> blocsToPlace = new List<Bloc>();
        int width, height, depth;
        Debug.Log("start");
        System.Random rand = new System.Random();

        //Program.max_container = 50;
        for (int i = 0; i < BlocNb; i++)
        {
            width = rand.Next(minSize, maxSize);
            height = rand.Next(minSize, maxSize);
            //EN 3D :   décommenter la ligne pour avoir le résultat en 2d ou en 3d
            depth = rand.Next(minSize, maxSize);
            //EN 2D :s
            //depth = 0;
            blocsToPlace.Add(new Bloc(width, height, depth));
        }

        while (cpt < BlocNb)
        {
            Container container = new Container(100, decalage);
            //PositionHelper.TriBloc(blocsToPlace);
            bool flag = true;
            while (flag)
            {
                flag = false;
                foreach (Bloc bl in blocsToPlace)
                {
                    if (container.AddBloc(bl, true))
                    {
                        flag = true;
                        cpt++;
                    }
                }
            }
            decalage += container.Width + 50;
        }
        return blocsToPlace;
    }

    void Start()
    {
        //affichage 4 blocs de choix de solution à exec



        //List<Bloc> blocsToPlace = solution2d();
        List<Bloc> blocsToPlace = solution3dMultipleContainers();

        int compteur = 0;

        foreach (Bloc b in blocsToPlace)
        {

            Texture newTexture = new Texture();
            compteur++;
            Debug.Log(b.X + " - " + b.Y + " - " + b.Z + " - " + b.Largeur + " - " + b.Hauteur + " - " + b.Profondeur);
            GameObject nouveaucube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //nouveaucube.transform.position = transform.TransformVector(new Vector3(b.X, b.Y, /*0*/ b.Z));
            float fx = b.X + (float)b.Largeur / 2;
            float fy = b.Y + (float)b.Hauteur / 2;
            float fz = b.Z + (float)b.Profondeur / 2;

            float fl = b.Largeur;
            float fh = b.Hauteur;
            float fp = b.Profondeur;
            Debug.Log(" fx : " + fx + " fy : " + " fz : " + fz);
            nouveaucube.transform.position = new Vector3(fx, fy, fz);
            nouveaucube.transform.localScale = new Vector3(fl, fh, /*0*/ fp);
            nouveaucube.name = "Objet " + compteur;
            Color newColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 1.0f);
            Shader shader1 = Shader.Find("Sprites/Default");
            nouveaucube.GetComponent<Renderer>().material.shader = shader1;
            nouveaucube.GetComponent<Renderer>().material.color = newColor;
            AssetDatabase.Refresh();
        
        }


    }
    // Update is called once per frame
    void Update()
    {

    }
}