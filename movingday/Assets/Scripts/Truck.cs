using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    [System.Serializable]
    private class UnloadOrder
    {
        public ItemsList unloadItems = null;
        public float pauseBetweenItems = 1f;
    }

    [SerializeField]
    private List<UnloadOrder> unloadItemsOrder = null;

    [SerializeField]
    private Transform spawnPoint = null;

    void Start()
    {
        StartCoroutine(UnloadItems());
    }

    private IEnumerator UnloadItems()
    {
        for (int l = 0; l < unloadItemsOrder.Count; l++)
        {
            UnloadOrder unloadOrder = unloadItemsOrder[l];
            for (int i = 0; i < unloadOrder.unloadItems.GetItemPrefabs().Count; i++)
            {
                // Spawn next item.
                GameObject nextItem = Instantiate(unloadOrder.unloadItems.GetItemPrefabs()[i], spawnPoint.position, spawnPoint.rotation);
                nextItem.GetComponent<MovingItem>().Push(spawnPoint.forward * 1700f);

                yield return new WaitForSeconds(unloadOrder.pauseBetweenItems);
            }
        }
    }
}
