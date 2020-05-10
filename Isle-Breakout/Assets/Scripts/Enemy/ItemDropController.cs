using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropController : MonoBehaviour
{
    public List<GameObject> dropItems;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<EnemyHealthController>().IsDead())
        {
            StartCoroutine(DropItems());
        }
    }

    IEnumerator DropItems()
    {
        enabled = false;
        foreach (var item in dropItems)
        {
            var dropChance = item.GetComponent<ItemSettings>().getDropChance();
            var chance = Random.Range(1, 100);
            if (chance < dropChance)
            {
                Instantiate(item, transform.position + new Vector3(Random.Range(-2,2),0,Random.Range(-2,2)), transform.rotation);
            }
        }
        yield return 0;
    }
}
