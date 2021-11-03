using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    static SpawnManager instance;


    [Serializable]
    class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    [SerializeField]
    Pool[] pools;
    Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

    
    void Awake() 
    {
        instance = this;
        InitPools();
    } 
    private void Start()
    { 
        SpawnFromPool("Slime", new Vector3(0, 0, 0));
        StartCoroutine("SpawnRandomEnemy");
    }
    IEnumerator SpawnRandomEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 3f));
            SpawnFromPool("Slime", new Vector3(Random.Range(-75f, 75f), 3f, Random.Range(-75f, 75f)));
        }
    }
    void InitPools()
    {
        foreach (Pool pool in pools)
        {
            poolDictionary.Add(pool.tag, new Queue<GameObject>());
            for (int i = 0; i < pool.size; i++)
            {
                CreateNewObject(pool.tag, pool.prefab);
            }
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position)
    {
        if(!instance.poolDictionary.ContainsKey(tag))
            throw new Exception($"Pool with tag {tag} doesn't exist");
        Queue<GameObject> poolQueue = poolDictionary[tag];
        if(poolQueue.Count <= 0)
        {
            Pool pool =  Array.Find(pools, x => x.tag == tag);
            CreateNewObject(tag, pool.prefab);
        }
        GameObject spawnTarget = poolQueue.Dequeue();
        spawnTarget.transform.position = position;
        spawnTarget.transform.rotation = Quaternion.identity;
        spawnTarget.SetActive(true);
        spawnTarget.transform.SetParent(null);
        return spawnTarget;
    }

    GameObject CreateNewObject(string tag, GameObject prefabs)
    {
        GameObject newObj = Instantiate(prefabs, transform);
        newObj.name = tag;
        newObj.SetActive(false);
        instance.poolDictionary[tag].Enqueue(newObj);
        return newObj;
    }

    public static void ReturnToPool(GameObject obj)
    {
        if (!instance.poolDictionary.ContainsKey(obj.name))
            throw new Exception($"Pool with tag {obj.name} doesn't exist");
        obj.SetActive(false);
        obj.transform.SetParent(instance.transform);
        instance.poolDictionary[obj.name].Enqueue(obj);
    }
}
