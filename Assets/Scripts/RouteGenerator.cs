using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteGenerator : MonoBehaviour
{
    public GameObject[] routePrefabs;
    private List<GameObject> activeRoutes = new List<GameObject>();
    public float spawnPos = 0;
    public float routeLength = 100;

    [SerializeField] private Transform player;
    private int startRoutes = 5;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < startRoutes; i++)
        {
            if (i == 0)
                SpawnRoute(2);
            SpawnRoute(Random.Range(0, routePrefabs.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z - 70 > spawnPos - (startRoutes * routeLength))
        {
            SpawnRoute(Random.Range(0, routePrefabs.Length));
            DeleteRoute();
        }
            
    }

    private void SpawnRoute (int routeIndex)
    {
        GameObject newRoute = Instantiate(routePrefabs[routeIndex], transform.forward * spawnPos, transform.rotation);
        activeRoutes.Add(newRoute);
        spawnPos += routeLength;
    }

    private void DeleteRoute ()
    {
        Destroy(activeRoutes[0]);
        activeRoutes.RemoveAt(0);
    }
}
