using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletPooling : MonoBehaviour
{
    public List<GameObject> pooledObjects = new List<GameObject>();
    public GameObject poolPrefabs;
    public int poolingCount;

    private void Awake()
    {
        CreatePoolObjects();
    }

    public void CreatePoolObjects()
    {
        for (int i = 0; i < poolingCount; i++)
        {
            GameObject newPoolingObject = Instantiate(poolPrefabs, transform);
            newPoolingObject.SetActive(false);
            pooledObjects.Add(newPoolingObject);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++) if (!pooledObjects[i].activeSelf) return pooledObjects[i];
        CreatePoolObjects();
        return pooledObjects[pooledObjects.Count];
    }
}
