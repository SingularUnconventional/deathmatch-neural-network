using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour 
{ 
    public List<GameObject> pooledObjects = new List<GameObject>(); 
    public GameObject poolPrefabs; 
    public int poolingCount;
    public int Startpooling;
    public float pooling;

    float t = 0;

    void Start()
    {
        for (int i = 0; i < Startpooling / poolingCount; i++) CreatePoolObjects();
        for (int i = 0; i < Startpooling; i++) GetPooledObject().SetActive(true);
    }

    void Update()
    {
        if (t >= pooling)
        {
            GetPooledObject().SetActive(true);
            t = 0;
        }
        t += Time.deltaTime;
    }



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

    //public List<GameObject> GetAllDeactivePooledObjects() 
    //{
    //    List<GameObject> deactivePooledObjects = new List<GameObject>(); 

    //    for (int i = 0; i < pooledObjects.Count; i++) 
    //    { 
    //        if (!pooledObjects[i].activeSelf) deactivePooledObjects.Add(pooledObjects[i]); 
    //    } 
    //    return deactivePooledObjects; 
    //} 

    //public List<GameObject> GetAllActivePooledObjects() 
    //{ 
    //    List<GameObject> activePooledObjects = new List<GameObject>(); 

    //    for (int i = 0; i < pooledObjects.Count; i++) 
    //    { 
    //        if (pooledObjects[i].activeSelf) activePooledObjects.Add(pooledObjects[i]); 
    //    } 
    //    return activePooledObjects; 
    //} 
}
