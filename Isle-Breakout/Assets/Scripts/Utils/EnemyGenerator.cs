using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public List<GameObject> camps;
    public List<GameObject> enemies;
    public int threshold;
 
    public List<GameObject> campAreas;
    public List<GameObject> enemyAreas;

    public int campsToSpawn;
    public int enemiesPerAreaMin;
    public int enemiesPerAreaMax;

    void Start()
    {
        for (int i = 0; i < campsToSpawn; i++)
        {
            threshold = Random.Range(0, camps.Count - 1);
            var campPlace = Random.Range(0, campAreas.Count - 1);
            var camp = Instantiate(camps[threshold], campAreas[campPlace].transform.position, Quaternion.identity);
            if (camp != null)
            {
                campAreas.RemoveAt(campPlace);
            }
        }

        bool canSpawn = true;

        for (int i = 0; i < enemyAreas.Count; i++)
        {
            threshold = Random.Range(0, enemies.Count - 1);
            List<GameObject> enemiesSpawned = new List<GameObject>();
            var enemyCount = Random.Range(enemiesPerAreaMin, enemiesPerAreaMax);
            for (int j = 0; j < enemyCount; j++)
            {
                var pos = new Vector3(Random.Range(enemyAreas[i].GetComponent<BoxCollider>().bounds.min.x, enemyAreas[i].GetComponent<BoxCollider>().bounds.max.x), enemyAreas[i].transform.position.y,
                        Random.Range(enemyAreas[i].GetComponent<BoxCollider>().bounds.min.z, enemyAreas[i].GetComponent<BoxCollider>().bounds.max.z));
                if (enemiesSpawned.Count == 0)
                {
                    GameObject en = null;
                    enemiesSpawned.Add(en = Instantiate(enemies[threshold], pos, Quaternion.identity));
                }
                else
                {
                    for (int x = 0; x < enemiesSpawned.Count; x++)
                    {
                        if (Vector3.Distance(enemiesSpawned[x].transform.position, pos) < 3f)
                        {
                            canSpawn = false;
                        }
                    }
                    if (!canSpawn)
                    {
                        j--;
                    }
                    else
                    {
                        enemiesSpawned.Add(Instantiate(enemies[threshold], pos, Quaternion.identity));
                    }
                }
            }
        }
        //StartCoroutine(StartSpawning());
    }

    IEnumerator StartSpawning()
    {
        for (int i = 0; i < campsToSpawn; i++)
        {
            threshold = Random.Range(0, camps.Count - 1);
            var campPlace = Random.Range(0, campAreas.Count - 1);
            var camp = Instantiate(camps[threshold], campAreas[campPlace].transform.position, Quaternion.identity);
            if(camp != null)
            {
                campAreas.RemoveAt(campPlace);
            }
        }

        bool canSpawn = true;

        for (int i = 0; i < enemyAreas.Count; i++)
        {
            threshold = Random.Range(0, enemies.Count - 1);
            List<GameObject> enemiesSpawned = new List<GameObject>();
            var enemyCount = Random.Range(enemiesPerAreaMin, enemiesPerAreaMax);
            for (int j = 0; j < enemyCount; j++)
            {
                var pos = new Vector3(Random.Range(enemyAreas[i].GetComponent<BoxCollider>().bounds.min.x, enemyAreas[i].GetComponent<BoxCollider>().bounds.max.x), enemyAreas[i].transform.position.y,
                        Random.Range(enemyAreas[i].GetComponent<BoxCollider>().bounds.min.z, enemyAreas[i].GetComponent<BoxCollider>().bounds.max.z));
                if (enemiesSpawned.Count == 0)
                {
                    GameObject en = null;
                    enemiesSpawned.Add(en = Instantiate(enemies[threshold], pos, Quaternion.identity));
                }
                else
                {
                    for(int x = 0; x < enemiesSpawned.Count; x++)
                    {
                        if(Vector3.Distance(enemiesSpawned[x].transform.position, pos) < 3f)
                        {
                            canSpawn = false;
                        } 
                    }
                    if (!canSpawn)
                    {
                        j--;
                    }
                    else
                    {
                        enemiesSpawned.Add(Instantiate(enemies[threshold], pos, Quaternion.identity));
                    }
                }
            }
        }
        yield return 0;
    }
}
