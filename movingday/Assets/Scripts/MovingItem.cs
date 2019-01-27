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

    private bool grabbed = false;
    private float alignSpinSpeed = 600f;
    private float alignMoveSpeed = 500f;

    public void ExitTruck(Vector3 exitPoint, Vector3 finishPoint)
    {
        StartCoroutine(ExitTruckCoroutine(exitPoint, finishPoint));
    }

    public void SetGrabbed(bool setGrabbed)
    {
        grabbed = setGrabbed;
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

        if (!grabbed)
        {
            RotateAlignToGrid();
            PositionAlignToGrid();
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

    private void RotateAlignToGrid()
    {
        // Check if we are not roughly aligned to the grid, rotationally.
        if (transform.eulerAngles.y % 90f > 2f)
        {
            float spinDir = 1f;
            // Slowly rotate to be aligned with the closest 90 angle.
            if (transform.eulerAngles.y % 90f < 45f)
            {
                spinDir = -1;
            }
            movingRigidbody.AddTorque(transform.up * alignSpinSpeed * spinDir * Time.deltaTime);
        }
    }

    private void PositionAlignToGrid()
    {
        if (Mathf.Abs(transform.position.x % 2f) > 0.01f)
        {
            float moveDir = 1f;
            if (Mathf.Abs(transform.position.x % 2f) < 1f)
            {
                moveDir = -1;
            }
            movingRigidbody.AddForce(new Vector3(moveDir * alignMoveSpeed * Time.deltaTime, 0f, 0f));
        }

        if (Mathf.Abs(transform.position.z % 2f) > 0.01f)
        {
            float moveDir = 1f;
            if (Mathf.Abs(transform.position.z % 2f) < 1f)
            {
                moveDir = -1;
            }
            movingRigidbody.AddForce(new Vector3(0f, 0f, moveDir * alignMoveSpeed * Time.deltaTime));
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
