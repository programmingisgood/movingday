using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DN_Triggers : MonoBehaviour
{
    public bool RightTrigger;
    public bool LeftTrigger;
    public bool UpTrigger;
    public bool DownTrigger;
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
        if (RightTrigger)
        {
            FurnitureScripts.RightPull = true;
            FurnitureScripts.LeftPull = false;
                FurnitureScripts.UpPull = false;
                FurnitureScripts.DownPull = false;
        }
        if (LeftTrigger)
        {
            FurnitureScripts.RightPull = false;
            FurnitureScripts.LeftPull = true;
                FurnitureScripts.UpPull = false;
                FurnitureScripts.DownPull = false;
            }
        if(UpTrigger)
            {
                FurnitureScripts.RightPull = false;
                FurnitureScripts.LeftPull = false;
                FurnitureScripts.UpPull = true;
                FurnitureScripts.DownPull = false;
            }
        if(DownTrigger)
            {
                FurnitureScripts.RightPull = false;
                FurnitureScripts.LeftPull = false;
                FurnitureScripts.UpPull = false;
                FurnitureScripts.DownPull = true;
            }
    }
}
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (RightTrigger)
            {
                FurnitureScripts.RightPull = true;
                FurnitureScripts.LeftPull = false;
                FurnitureScripts.UpPull = false;
                FurnitureScripts.DownPull = false;
            }
            if (LeftTrigger)
            {
                FurnitureScripts.RightPull = false;
                FurnitureScripts.LeftPull = true;
                FurnitureScripts.UpPull = false;
                FurnitureScripts.DownPull = false;
            }
            if (UpTrigger)
            {
                FurnitureScripts.RightPull = false;
                FurnitureScripts.LeftPull = false;
                FurnitureScripts.UpPull = true;
                FurnitureScripts.DownPull = false;
            }
            if (DownTrigger)
            {
                FurnitureScripts.RightPull = false;
                FurnitureScripts.LeftPull = false;
                FurnitureScripts.UpPull = false;
                FurnitureScripts.DownPull = true;
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
