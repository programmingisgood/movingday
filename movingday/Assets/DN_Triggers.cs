using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DN_Triggers : MonoBehaviour
{
    public bool RIghtTrigger;
    public bool LeftTrigger;
    public GameObject Furniture;
    private DN_ObjectAttraction FurnitureScripts;
    // Start is called before the first frame update
    void Start()
    {
        FurnitureScripts = Furniture.GetComponent<DN_ObjectAttraction>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        if(RIghtTrigger)
    //        {
    //            FurnitureScripts.RightPull = true;
    //            FurnitureScripts.LeftPull = false;
    //        }
    //        if (LeftTrigger)
    //        {
    //            FurnitureScripts.RightPull = false;
    //            FurnitureScripts.LeftPull = true;
    //        }
    //    }
private void OnTriggerEnter(Collider other)
{
    if (other.tag == "Player")
    {
        if (RIghtTrigger)
        {
            FurnitureScripts.RightPull = true;
            FurnitureScripts.LeftPull = false;
        }
        if (LeftTrigger)
        {
            FurnitureScripts.RightPull = false;
            FurnitureScripts.LeftPull = true;
        }
    }
}


    

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        if (RIghtTrigger)
    //        {
    //            FurnitureScripts.RightPull = false;
    //            FurnitureScripts.LeftPull = false;
    //        }
    //        if (LeftTrigger)
    //        {
    //            FurnitureScripts.RightPull = false;
    //            FurnitureScripts.LeftPull = false;
    //        }
    //    }
    //}
}
