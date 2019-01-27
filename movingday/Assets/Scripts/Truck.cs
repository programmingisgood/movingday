using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Truck : MonoBehaviour
{
    [System.Serializable]
    private class UnloadOrder
    {
        public ItemsList unloadItems = null;
    }

    [SerializeField]
    private MovingDayPlayerManager playerManager = null;

    [SerializeField]
    private List<UnloadOrder> unloadItemsOrder = null;

    [SerializeField]
    private Transform spawnPoint = null;

    [SerializeField]
    private Transform itemExitPoint = null;

    [SerializeField]
    private Transform itemFinishMovementPoint = null;

    [SerializeField]
    private Transform backDoor = null;

    void Start()
    {
        StartCoroutine(UnloadItems());
    }

    private IEnumerator UnloadItems()
    {
        yield return StartCoroutine(OpenDoor());
        for (int l = 0; l < unloadItemsOrder.Count; l++)
        {
            UnloadOrder unloadOrder = unloadItemsOrder[l];
            for (int i = 0; i < unloadOrder.unloadItems.GetItemPrefabs().Count; i++)
            {
                if (FindObjectOfType<DN_MenuMannager>() != null)
                {
                    if (FindObjectOfType<DN_MenuMannager>().Timer > 0)
                    {
                        // Spawn next item.
                        GameObject nextItem = Instantiate(unloadOrder.unloadItems.GetItemPrefabs()[i], spawnPoint.position, spawnPoint.rotation);
                        nextItem.GetComponent<MovingItem>().ExitTruck(itemExitPoint.position, itemFinishMovementPoint.position);

                        int numPlayers = playerManager.GetNumPlayers();
                        float maxPauseTime = 6f;
                        float minPauseTime = 2f;
                        float reducePausePerPlayer = 1f;
                        float pauseTime = maxPauseTime - (reducePausePerPlayer * numPlayers);
                        pauseTime = Mathf.Max(minPauseTime, pauseTime);
                        yield return new WaitForSeconds(pauseTime);
                    }
                }
            }
        }
    }

    private IEnumerator OpenDoor()
    {
        // Slight pause before opening door.
        yield return new WaitForSeconds(0.5f);

        Tween rotateTween = backDoor.DORotate(new Vector3(90f, 0, 0), 1f).SetEase(Ease.OutBack).SetDelay(1f);
        yield return rotateTween.WaitForCompletion();

        Tween slideTween = backDoor.DOLocalMoveZ(-5, 0.5f).SetEase(Ease.InCubic);
    }
}
