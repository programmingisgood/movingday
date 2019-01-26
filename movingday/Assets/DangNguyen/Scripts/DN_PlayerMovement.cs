using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DN_PlayerMovement : MonoBehaviour
{
    public float speed;
    private bool InPlace;
    public bool Pull;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
       
       
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }
    private void Update()
    {
      if(Input.GetKeyDown(KeyCode.E))
        {
            Pull = !Pull;
        }
      if(Pull && InPlace)
        {
            speed = 200;
        }
      if(Pull == false)
        {
            speed = 800;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag =="Furniture")
        {
            InPlace = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Furniture")
        {
            InPlace = false;
        }
    }
}
