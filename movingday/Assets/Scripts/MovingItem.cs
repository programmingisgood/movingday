using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingItem : MonoBehaviour
{
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
}
