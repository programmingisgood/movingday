using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingItem : MonoBehaviour
{
    public GameObject Prompt;
    public bool BoxPrompt;
    private bool Protected;
    [SerializeField]
    private Rigidbody movingRigidbody = null;
    private bool StopGivingScore;
    
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
        if(FindObjectOfType<DN_MenuMannager>().Timer <=0 && !Protected)
        {
            Destroy(gameObject.GetComponent<MovingItem>());
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "CountingBox")
        {
            Protected = true;
            if (FindObjectOfType<DN_MenuMannager>() != null)
            {

                if (FindObjectOfType<DN_MenuMannager>().AfterMathTimer <= 0 && !StopGivingScore)
                {
                    FindObjectOfType<DN_MenuMannager>().Score += 1;
                    StopGivingScore = true;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CountingBox")
        {
            Protected = false;
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
