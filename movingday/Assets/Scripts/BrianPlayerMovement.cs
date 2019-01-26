using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BrianPlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 6f;

    [SerializeField]
    private float grabRadius = 1f;

    [SerializeField]
    private Transform visuals = null;

    private Rigidbody rb;
    private Vector3 moveDir = new Vector3(0, 0, 1f);
    private bool grabbing = false;
    private Rigidbody grabbedObject = null;
    private bool moving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        visuals.transform.rotation = Quaternion.Slerp(visuals.transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * 20f);

        if (!grabbing && Input.GetKey(KeyCode.Return))
        {
            AttemptGrab();
        }
        else if (grabbing)
        {
            UpdateGrabbing();
        }
        else
        {
            grabbing = false;
        }
    }

    void FixedUpdate()
    {
        float horz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        moving = Mathf.Abs(horz) > 0f || Mathf.Abs(vert) > 0f;
        if (moving)
        {
            moveDir.x = horz;
            moveDir.y = 0f;
            moveDir.z = vert;

            rb.AddForce(moveDir * speed * Time.fixedDeltaTime);
        }
    }

    private void AttemptGrab()
    {
        // Find the closest dynamic object.
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, grabRadius);
        Rigidbody closestObject = null;
        float minDist = grabRadius * grabRadius;
        foreach (Collider collider in hitColliders)
        {
            // Ignore ourself.
            if (collider.transform.IsChildOf(transform))
            {
                continue;
            }
            // Ignore the already closest object.
            if (closestObject != null && collider.transform.IsChildOf(closestObject.transform))
            {
                continue;
            }

            Rigidbody objectRB = collider.gameObject.GetComponentInParent<Rigidbody>();
            if (objectRB != null)
            {
                Vector3 diff = objectRB.transform.position - transform.position;
                float dist = diff.x * diff.x + diff.y * diff.y + diff.z * diff.z;
                if (dist < minDist)
                {
                    closestObject = objectRB;
                    minDist = dist;
                }
            }
        }
        grabbedObject = closestObject;
        grabbing = grabbedObject != null;
    }

    private void UpdateGrabbing()
    {
        if (moving && grabbedObject != null)
        {
            // Find the closest point on this object to the player.
            RaycastHit hit;
            Vector3 dir = grabbedObject.transform.position - transform.position;
            dir.Normalize();
            if (Physics.SphereCast(transform.position, 0.5f, dir, out hit))
            {
                Debug.Log("Grabbing");
                // Apply a force in the movement direction.
                grabbedObject.AddForceAtPosition(moveDir * speed * 0.1f * Time.deltaTime, hit.point);
            }
        }
    }
}
