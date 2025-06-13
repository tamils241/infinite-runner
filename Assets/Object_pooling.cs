using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_pooling : MonoBehaviour
{
    public GameObject[] objectPrefabs;     // Array of different prefabs to pool
    public int poolSizePerPrefab = 5;      // Pool size for each type of prefab

    private List<GameObject> pool;

    void Start()
    {
        pool = new List<GameObject>();

        // Loop through each prefab and instantiate `poolSizePerPrefab` instances
        foreach (GameObject prefab in objectPrefabs)
        {
            for (int i = 0; i < poolSizePerPrefab; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                pool.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(int prefabIndex = 0)
    {
        if (prefabIndex < 0 || prefabIndex >= objectPrefabs.Length)
        {
            Debug.LogWarning("Invalid prefab index.");
            return null;
        }

        // Look for inactive object of the requested type
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy && obj.name.StartsWith(objectPrefabs[prefabIndex].name))
            {
                return obj;
            }
        }

        // If none available, optionally expand the pool
        GameObject newObj = Instantiate(objectPrefabs[prefabIndex]);
        newObj.SetActive(false);
        pool.Add(newObj);
        return newObj;
    }
}

