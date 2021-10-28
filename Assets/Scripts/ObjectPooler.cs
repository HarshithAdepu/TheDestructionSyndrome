using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler objectPoolerInstance;
    [System.Serializable]
    public class PoolClass
    {
        public string name;
        public GameObject prefab;
        public int maxCount;
    }
    public List<PoolClass> poolList;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    private void Awake()
    {
        if(objectPoolerInstance == null)
            objectPoolerInstance = this;
    }
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach(PoolClass pool in poolList)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>();
            for(int i=0;i<pool.maxCount;i++)
            {
                GameObject temp = Instantiate(pool.prefab);
                temp.SetActive(false);
                objectQueue.Enqueue(temp);
            }
            poolDictionary.Add(pool.name, objectQueue);
        }
    }

    void Update()
    {
        
    }

    public GameObject SpawnObject(string name, Vector3 position, Quaternion rotation)
    {
        if(!poolDictionary.ContainsKey(name))
        {
            Debug.Log("Pool \""+name+"\" not found");
            return null;
        }
        GameObject spawnedObject = poolDictionary[name].Dequeue();
        spawnedObject.SetActive(true);
        spawnedObject.transform.position = position;
        spawnedObject.transform.rotation = rotation;
        poolDictionary[name].Enqueue(spawnedObject);
        return spawnedObject;
    }
}
