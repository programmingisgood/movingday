using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class BrianPlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 6f;

    [SerializeField]
    private Transform visuals = null;

    [SerializeField]
    private SpringJoint joint = null;

    [SerializeField]
    private new Transform camera = null;

    private Rigidbody rb;
    private Vector3 moveDir = new Vector3(0, 0, 1f);
    public bool grabbing = false;
    private Rigidbody grabbedObject = null;
    private bool moving = false;
    public GameObject ControlPFI;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        DoCameraZoomIn();
    }

    private void DoCameraZoomIn()
    {
        Vector3 startPos = camera.localPosition;

        camera.localPosition += new Vector3(0, 30f, 0);

        camera.DOLocalMove(startPos, 8f).SetEase(Ease.InOutSine);
    }
    public GameObject Camera;

    void Update()
    {
        // Prevent the moveDir from ever being zeroed out which will cause a warning.
        moveDir = moveDir.magnitude < 0.00001f ? new Vector3(0, 0, 1f) : moveDir;
        visuals.transform.rotation = Quaternion.Slerp(visuals.transform.rotation, Quaternion.LookRotation(grabbing ? -moveDir : moveDir), Time.deltaTime * 20f);

        if (!grabbing && Input.GetKey(KeyCode.Return))
        {
            AttemptGrab();
        }
        else if (grabbing && !Input.GetKey(KeyCode.Return))
        {
            SetGrabbedObject(null);
        }

        if(FindObjectOfType<DN_MenuMannager>() != null)
        {
            if (Input.GetAxis("Horizontal") != 0 && FindObjectOfType<DN_MenuMannager>().FirstPrompt || Input.GetAxis("Vertical") != 0 && FindObjectOfType<DN_MenuMannager>().FirstPrompt)
            {
                ControlPFI.SetActive(false);
                FindObjectOfType<DN_MenuMannager>().FirstPrompt = false;
            }
            if (FindObjectOfType<DN_MenuMannager>().Timer <= 0)
            {
                Camera.SetActive(false);
            }
        }
        if(FindObjectOfType<DN_MenuMannager>() == null)
        {
            ControlPFI.SetActive(false);
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
        grabbedObject = null;
        grabbing = false;

        Vector3 moveDirNorm = moveDir.normalized;
        RaycastHit[] hits = Physics.SphereCastAll(transform.position - moveDirNorm * 1f, 1f, moveDirNorm, 4f).OrderBy(h => h.distance).ToArray();
        foreach (RaycastHit hit in hits)
        {
            MovingItem movingItem = hit.collider.gameObject.GetComponentInParent<MovingItem>();
            if (movingItem != null)
            {
                SetGrabbedObject(movingItem.GetComponent<Rigidbody>());
                break;
            }
        }
    }

    private void SetGrabbedObject(Rigidbody setGrabbedObject)
    {
        if (setGrabbedObject == null)
        {
            grabbing = false;
            joint.connectedBody = null;
        }

        if (grabbedObject != null)
        {
            grabbedObject.gameObject.GetComponent<QuickOutline.Outline>().enabled = false;
            MovingItem movingItem = grabbedObject.GetComponentInParent<MovingItem>();
            if (movingItem != null)
            {
                movingItem.SetGrabbed(false);
            }
            grabbedObject = null;
        }

        grabbedObject = setGrabbedObject;
        grabbing = grabbedObject != null;

        if (grabbedObject != null)
        {
            joint.connectedBody = grabbedObject;

            // Outline the grabbed object.
            QuickOutline.Outline outline = grabbedObject.gameObject.GetComponent<QuickOutline.Outline>();
            if (outline == null)
            {
                outline = grabbedObject.gameObject.AddComponent<QuickOutline.Outline>();
            }
            outline.OutlineColor = new Color(0.9f, 0.45f, 0f);
            outline.OutlineWidth = 10f;
            outline.enabled = true;
        }
    }
}
