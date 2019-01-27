using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingItem : MonoBehaviour
{
    public GameObject Prompt;
    public bool BoxPrompt;
    [SerializeField]
    private Rigidbody movingRigidbody = null;

    public void ExitTruck(Vector3 exitPoint, Vector3 finishPoint)
    {
        StartCoroutine(ExitTruckCoroutine(exitPoint, finishPoint));
    }

    private IEnumerator ExitTruckCoroutine(Vector3 exitPoint, Vector3 finishPoint)
    {
        
        movingRigidbody.isKinematic = true;

        yield return transform.DOMove(exitPoint, 1f).SetEase(Ease.InSine).WaitForCompletion();

        transform.DOMove(finishPoint, 1f).SetEase(Ease.OutCubic);

        movingRigidbody.isKinematic = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && BoxPrompt)
        {
            if (FindObjectOfType<BrianPlayerMovement>().grabbing)
            {
                Prompt.SetActive(false);
            }
        }
    }
    //private void OnCollisionStay(Collision collision)
    //{
    //    if (Input.GetKeyDown(KeyCode.Return) && BoxPrompt)
    //    {
    //        Prompt.SetActive(false);
    //    }
    //}

}
