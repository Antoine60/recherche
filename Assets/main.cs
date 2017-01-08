using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEditor;
using UnityEngine.SceneManagement;

public class main : MonoBehaviour
{
    public Button StartText;
    public Button ExitText;
    public Canvas StartMenu;
    public Canvas PremierLevel;
    public Canvas Previous; 


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
        blocsToPlace = PositionHelper.TriBloc(blocsToPlace);
        foreach (Bloc bl in blocsToPlace)
        {
            container.AddBloc(bl);
        }
        return blocsToPlace;
    }

    public static List<Bloc> solution3d()
    {
        const int BlocNb = 50;
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
        blocsToPlace = PositionHelper.TriBloc(blocsToPlace);
        foreach (Bloc bl in blocsToPlace)
        {
            container.AddBloc(bl);
        }
        return blocsToPlace;
    }

    public static List<Bloc> solution3dRandom()
    {
        const int BlocNb = 50, maxSize = 40, minSize = 1;
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
        blocsToPlace = PositionHelper.TriBloc(blocsToPlace);
        foreach (Bloc bl in blocsToPlace)
        {
            container.AddBloc(bl);
        }
        return blocsToPlace;
    }

    public static List<Bloc> solution3dMultipleContainers()
    {
        int cpt = 0; int decalage = 0;
        const int BlocNb = 50, maxSize = 40, minSize = 1;
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
            blocsToPlace = PositionHelper.TriBloc(blocsToPlace);
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

    private IEnumerator coroutine;
    private object blocavenir;

    private IEnumerator exec_demo(float waitTime, List<Bloc> blocsToPlace)
    {
        int cpt = 0;

        int total = blocsToPlace.Count();

        while (cpt < total)
        {

            Bloc b = blocsToPlace[cpt];

            Canvas PremierLevel = GameObject.Find("PremierLevel").GetComponent<Canvas>();
            GameObject nouveaucube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            float fx = b.X + (float)b.Largeur / 2;
            float fy = b.Y + (float)b.Hauteur / 2;
            float fz = b.Z + (float)b.Profondeur / 2;
            float fl = b.Largeur;
            float fh = b.Hauteur;
            float fp = b.Profondeur;
            nouveaucube.transform.position = new Vector3(fx, fy, fz);
            nouveaucube.transform.localScale = new Vector3(fl, fh, fp);
            nouveaucube.transform.parent = PremierLevel.transform;

            Color newColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 1.0f);
            Shader shader1 = Shader.Find("Sprites/Default");
            nouveaucube.GetComponent<Renderer>().material.shader = shader1;
            nouveaucube.GetComponent<Renderer>().material.color = newColor;
            //Rigidbody gameObjectsRigidBody = nouveaucube.AddComponent<Rigidbody>(); // Add the rigidbody.
            //gameObjectsRigidBody.mass = 9.81f; // Set the GO's mass to 5 via the Rigidbody.

            cpt++;

            yield return new WaitForSeconds(waitTime);


        }
        Previous.enabled = true;


    }
    public void previous()
    {

        Vector3 vector = new Vector3(0, 0, -200);
        Camera.main.transform.position = vector;
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
        PremierLevel.enabled = false;
        foreach (Transform child in PremierLevel.transform)
        {

              GameObject.Destroy(child.gameObject);

        }
        StartMenu.enabled = true;
        Previous.enabled = false;

    }
    void Start()
    {
        PremierLevel = GameObject.Find("PremierLevel").GetComponent<Canvas>();
        Previous.enabled = false;
        PremierLevel.enabled = false;

        ExitText = ExitText.GetComponent<Button>();
        StartText = StartText.GetComponent<Button>();
    }
    public void demo1()
    {
        print("Starting " + Time.time);
        StartMenu.enabled = false;
        PremierLevel.enabled = true;
        List<Bloc> blocsToPlace  = solution2d();
        coroutine = exec_demo(0.05f, blocsToPlace);
        StartCoroutine(coroutine);
    }
    public void demo2()
    {
        Vector3 vector = new Vector3(200, 100, -30);
        Camera.main.transform.position = vector;
        Camera.main.transform.rotation = Quaternion.Euler(0, -50, 0);
        print("Starting " + Time.time);
        StartMenu.enabled = false;
        PremierLevel.enabled = true;
        List<Bloc> blocsToPlace = solution3d();
        coroutine = exec_demo(0.05f, blocsToPlace);
        StartCoroutine(coroutine);
    }
    public void demo3()
    {
        print("Starting " + Time.time);
        StartMenu.enabled = false;
        PremierLevel.enabled = true;
        List<Bloc> blocsToPlace = solution3dMultipleContainers();
        coroutine = exec_demo(0.05f, blocsToPlace);
        StartCoroutine(coroutine);
    }
  

    public void ExitGame()
    {
        Application.Quit();
    }
}