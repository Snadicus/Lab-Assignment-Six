using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class CreateScene : MonoBehaviour
{
    // Player Input
    // How large the forest is
    public int forestSize;
    // How many trees are in the forest
    public int forestDensity;
    [Range(3,10)] public int pyramidSize;
    public bool dayNight;

    // Calculating object
    private float pyramidWidth;


    // Parent objects
    private GameObject treeParent;
    private GameObject stoneParent;

    // Oject to hold Celestial body gameObject
    private GameObject celestialBody;
    // Infor to move celestial body
    private float angle = 0f;
    private float orbitRadius = 30f;
    private float orbitSpeed = 0.5f;



    void Start()
    {
        InstantiateParent();
        CreateGround();
        CreatePrymaid();
        CreateForest();
        CreateCelestial();
    }

    private void Update()
    {
        MoveCelestialBody();
    }

    
    void InstantiateParent()
    {
        // Create empty game objects to house trees and stones
        treeParent = new GameObject("Trees");
        stoneParent = new GameObject("Stones");

        // Get width of pyramid so trees do not spawn in it
        pyramidWidth = pyramidSize * 1.1f + 1;
    }

    void CreateGround()
    {
        // Create ground, name it, change color and scale
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground";
        Renderer rend = ground.GetComponent<Renderer>();

        rend.material.color = Color.darkSalmon;
        ground.transform.localScale = new Vector3(5f, 1, 5f);
    }

    void CreateForest()
    {
        // Create trees based on forestDensity
        for (int i = 0; i < forestDensity; i++) 
        {
            // Create tree, name it, change color, position, and scale, assign parent object
            GameObject tree = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            tree.transform.parent = treeParent.transform;
            Renderer rend = tree.GetComponent<Renderer>();

            rend.material.color = Color.green;
            tree.name = "Tree " + i;

            tree.transform.localScale = new Vector3(UnityEngine.Random.Range(0.5f, 2f), UnityEngine.Random.Range(0.5f, 2f), UnityEngine.Random.Range(0.5f, 2f));
            tree.transform.position = new Vector3(UnityEngine.Random.Range(-pyramidWidth, -forestSize), 1f * tree.transform.localScale.y, UnityEngine.Random.Range(-pyramidWidth, -forestSize));
        }
    }

    void CreatePrymaid()
    {
        // i changes the y axis
        // i starts at zero and climbs until it reaches pyramidSize
        for (int i = 0; i < pyramidSize; i++)
        {
            // j changes the z axis
            // j is equal to the pyramid size minus i to place the correct amount of blocks
            for (int j = pyramidSize - i; j > 0; j--)
            {
                // k changes the x axis
                // k is equal to the pyramid size minus i to place the correct amount of blocks
                for (int k = pyramidSize - i; k > 0; k--)
                {
                    // instantiating object based on i, k, and j, assign player transform
                    GameObject stone = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    stone.transform.parent = stoneParent.transform;

                    stone.transform.position = new Vector3(1.1f * k + (0.5f * i), 1.1f * (i+0.5f), 1.1f * j + (0.5f * i));
                    stone.name = "Stone " + i + j + k;
                }
            }
        }
    }

    void CreateCelestial()
    {
        celestialBody = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        celestialBody.name = "Celestial Body";
        celestialBody.transform.position = new Vector3(0f, 10f, 0f);
        Renderer rend = celestialBody.GetComponent<Renderer>();
        Light lightComp = celestialBody.AddComponent<Light>();

        if (dayNight == true)
        {
            // Daylight Settings
            rend.material.color = Color.yellow;
            lightComp.type = LightType.Directional;
            lightComp.color = Color.white;
            lightComp.intensity = 1.2f;

        } else
        {
            // Nighttime Settngs
            rend.material.color = Color.grey;
            lightComp.type = LightType.Directional;
            lightComp.color = new Color(0.4f, 0.4f, 0.6f);
            lightComp.intensity = 0.3f;
        }
    }

    // Moving the Celestial Body in a circle around the ground
    void MoveCelestialBody()
    {
        angle += orbitSpeed * Time.deltaTime;

        float x = Mathf.Cos(angle) * orbitRadius;
        float y = Mathf.Sin(angle) * orbitRadius;

        celestialBody.transform.position = new Vector3(x, y, 0f);
    }

}
