using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProjectileType
{
    public string projectileName;
    public GameObject projectilePrefab;
}

public class SubPoolData
{
    public Queue<GameObject> subPool;
    public GameObject prefab;

    public SubPoolData(GameObject p)
    {
        prefab = p;
        subPool = new Queue<GameObject>();
    }
}

public class Player_ProjectilePool : MonoBehaviour
{
    public ProjectileType[] projectileTypes;

    private Dictionary<string, SubPoolData> pool;
    private int subPoolCount;

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        pool = new Dictionary<string, SubPoolData>();

        for(int i = 0; i < projectileTypes.Length; i++)
        {
            pool[projectileTypes[i].projectileName] = new SubPoolData(projectileTypes[i].projectilePrefab);
        }
    }

    public GameObject GetObject(string obName, Transform spawn)
    {
        if (!pool.ContainsKey(obName))
        {
            //Debug.Log("No Key");
            return null;
        }

        Queue<GameObject> sub = pool[obName].subPool;

        if(sub.Count > 0 && !sub.Peek().activeSelf)
        {
            GameObject existingObject = sub.Dequeue();
            existingObject.transform.position = spawn.position;
            existingObject.transform.rotation = spawn.rotation;
            existingObject.SetActive(true);
            //Debug.Log("Used");
            sub.Enqueue(existingObject);
            return existingObject;
        }
        else
        {
            GameObject newObject = Instantiate(pool[obName].prefab, spawn.position, spawn.rotation);
            //Debug.Log("Not Used");
            sub.Enqueue(newObject);
            return newObject;
        }
    }

    private void OnDisable()
    {
        
    }
}
