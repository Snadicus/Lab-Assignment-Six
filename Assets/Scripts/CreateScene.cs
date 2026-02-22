using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class CreateScene : MonoBehaviour
{

    public int forestSize;
    public int forestProximity;
    public int pyramidSize;
    public bool dayNight;
    public GameObject[] trees;
    public GameObject[] stones;
    public GameObject celestialBody;
    private float angle = 0f;
    private float orbitRadius = 30f;
    private float orbitSpeed = 0.5f;

    void Start()
    {
        InstantiateVariable();
        CreateGround();
        CreatePrymaid();
        CreateForest();
        CreateCelestial();
    }

    private void Update()
    {
        MoveCelestialBody();
    }

    void InstantiateVariable()
    {
        if (pyramidSize < 3)
        {
            pyramidSize = 3;
            Debug.Log("Pyramid size is set to 3.");
        }
        else if (pyramidSize > 10)
        {
            pyramidSize = 10;
            Debug.Log("Pyramid size is set to 10.");
        }
    }

    void CreateGround()
    {
        //
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Renderer rend = ground.GetComponent<Renderer>();

        rend.material.color = Color.darkSalmon;
        ground.transform.localScale = new Vector3(5f, 1, 5f);
    }

    void CreateForest()
    {
        for (int i = 0; i < forestSize; i++) 
        { 
            GameObject tree = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            Renderer rend = tree.GetComponent<Renderer>();

            rend.material.color = Color.green;            
            tree.transform.localScale = new Vector3(UnityEngine.Random.Range(0.5f, 3f), UnityEngine.Random.Range(0.5f, 3f), UnityEngine.Random.Range(0.5f, 3f));
            tree.transform.position = new Vector3(UnityEngine.Random.Range(0f, 5f), 1f * tree.transform.localScale.y, UnityEngine.Random.Range(0f, 5f));
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
                    // instantiating object based on i, k, and j
                    GameObject stone = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    stone.transform.position = new Vector3(1.1f * k + (0.5f * i), 1.1f * (i+0.5f), 1.1f * j + (0.5f * i));
                }
            }
        }
    }

    void CreateCelestial()
    {
        celestialBody = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        celestialBody.transform.position = new Vector3(0f, 10f, 0f);
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
