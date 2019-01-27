using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using InControl;

public class BrianPlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 6f;

    [SerializeField]
    private Transform visuals = null;

    [SerializeField]
    private SpringJoint joint = null;

    [SerializeField]
    private Transform rightArm = null;

    [SerializeField]
    private Transform leftArm = null;

    private Quaternion startingRightRotation;
    private Quaternion startingLeftRotation;

    private Rigidbody rb;
    private Vector3 moveDir = new Vector3(0, 0, 1f);
    public bool grabbing = false;
    private Rigidbody grabbedObject = null;
    private bool moving = false;
    public GameObject ControlPFI;
    private InputDevice myInputDevice = null;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        startingRightRotation = rightArm.rotation;
        startingLeftRotation = leftArm.rotation;
    }

    public void SetInputDevice(InputDevice setInputDevice)
    {
        myInputDevice = setInputDevice;
    }

    public bool GetActionPressed()
    {
        if (myInputDevice != null)
        {
            return myInputDevice.GetControl(InputControlType.Action1);
        }
        return Input.GetKey(KeyCode.Return);
    }

    public Vector2 GetInputDirection()
    {
        if (myInputDevice != null)
        {
            return myInputDevice.Direction;
        }

        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void Update()
    {
        // Prevent the moveDir from ever being zeroed out which will cause a warning.
        moveDir = moveDir.magnitude < 0.00001f ? new Vector3(0, 0, 1f) : moveDir;
        visuals.transform.rotation = Quaternion.Slerp(visuals.transform.rotation, Quaternion.LookRotation(grabbing ? -moveDir : moveDir), Time.deltaTime * 20f);

        if (!grabbing && GetActionPressed())
        {
            AttemptGrab();
        }
        else if (grabbing && !GetActionPressed())
        {
            SetGrabbedObject(null);
        }

        if (grabbing)
        {
            CheckGrabbingJointBreak();
        }

        if(DN_MenuMannager.Restart)
        {
            ControlPFI.SetActive(false);
            FindObjectOfType<DN_MenuMannager>().FirstPrompt = false;
        }

        if(FindObjectOfType<DN_MenuMannager>() != null)
        {
            Vector2 inputDir = GetInputDirection();
            if (inputDir.x != 0 && FindObjectOfType<DN_MenuMannager>().FirstPrompt || inputDir.y != 0 && FindObjectOfType<DN_MenuMannager>().FirstPrompt)
            {
                ControlPFI.SetActive(false);
                FindObjectOfType<DN_MenuMannager>().FirstPrompt = false;
            }
            //if (FindObjectOfType<DN_MenuMannager>().Timer <= 0)
            //{
            //    Camera.main.gameObject.SetActive(false);
            //}
            //if (FindObjectOfType<DN_MenuMannager>().Timer > 0)
            //{
            //    Camera.main.gameObject.SetActive(true);
            //}
        }
        if(FindObjectOfType<DN_MenuMannager>() == null)
        {
            ControlPFI.SetActive(false);
        }


    }

    void FixedUpdate()
    {
        Vector2 inputDir = GetInputDirection();
        float horz = inputDir.x;
        float vert = inputDir.y;

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
            AnimateArmsIn();
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
            AnimateArmsOut();
        }
    }

    // Check if we should break the grabbing joint if the object is too far away.
    private void CheckGrabbingJointBreak()
    {
        if (grabbedObject != null)
        {
            float dist = (grabbedObject.transform.TransformPoint(joint.connectedAnchor) - transform.position).magnitude;
            if (dist > 4f)
            {
                SetGrabbedObject(null);
            }
        }
    }

    private void AnimateArmsIn()
    {
        DOTween.Kill(rightArm);
        rightArm.DOLocalRotate(startingRightRotation.eulerAngles, 1f);

        DOTween.Kill(leftArm);
        leftArm.DOLocalRotate(startingLeftRotation.eulerAngles, 1f);
    }

    private void AnimateArmsOut()
    {
        DOTween.Kill(rightArm);
        Vector3 rotateGoal = startingRightRotation.eulerAngles + new Vector3(50f, 0, 0);
        rightArm.DOLocalRotate(rotateGoal, 1f);

        DOTween.Kill(leftArm);
        rotateGoal = startingLeftRotation.eulerAngles - new Vector3(50f, 0, 0);
        leftArm.DOLocalRotate(rotateGoal, 1f);
    }
}
