using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingItem : MonoBehaviour
{
    [SerializeField]
    private Rigidbody movingRigidbody = null;

    public void Push(Vector3 force)
    {
        movingRigidbody.AddForce(force);
    }
}
