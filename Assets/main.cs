using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class main : MonoBehaviour
{
    void Start()
    {
		const int BlocNb = 15, maxSize = 20, minSize = 5;
		List<Bloc> blocsToPlace = new List<Bloc>();
		int width, height;
        Debug.Log("start");
		System.Random rand = new System.Random();

        //Program.max_container = 50;
        int compteur = 0;
		for (int i = 1; i < BlocNb; i++)
        {
			width = rand.Next (minSize, maxSize);
			height = rand.Next (minSize, maxSize);
			blocsToPlace.Add(new Bloc(0, 0, width, height));
        }

		Container container = new Container ();
		PositionHelper.TriBloc (blocsToPlace);
		foreach (Bloc bl in blocsToPlace) {
			container.AddBloc (bl);
		}

        //Program.blocavenir = Program.TriBloc(Program.blocavenir);

        //Program.blocplace = Program.Binpacking(Program.blocavenir, Program.blocplace);

		foreach (Bloc b in blocsToPlace)
        {
  
            Texture newTexture = new Texture();
            compteur++;
            Debug.Log(b.X + " - " + b.Y + " - " + b.Largeur + " - " + b.Hauteur);
            GameObject nouveaucube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            nouveaucube.transform.position = transform.TransformVector(new Vector3(b.X, b.Y, 0));
            nouveaucube.transform.localScale = new Vector3(b.Largeur, b.Hauteur, 0);
            nouveaucube.name = "Objet " + compteur;
            Color newColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 1.0f);
            Shader shader1 = Shader.Find("Sprites/Default");
            nouveaucube.GetComponent<Renderer>().material.shader = shader1;
            nouveaucube.GetComponent<Renderer>().material.color = newColor;

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



