using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DN_ObjectAttraction : MonoBehaviour
{
    public float AttractorSpeed;
    private bool MoveToward;
    private GameObject Player;
    private Transform PlayerRot;
    public float turnSpeed;
    public bool LeftPull;
    public bool RightPull;
    public bool UpPull;
    public bool DownPull;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        if(PlayerRot == null)
        {
            PlayerRot = GameObject.FindGameObjectWithTag("Player").transform;
        }
        // PlayerScripts = Player.GetComponent<DN_PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        if (MoveToward && RightPull && FindObjectOfType<DN_PlayerMovement>().Pull)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, AttractorSpeed * Time.deltaTime);
            Vector3 dir = PlayerRot.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        }
        if (MoveToward && FindObjectOfType<DN_PlayerMovement>().Pull && LeftPull)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, AttractorSpeed * Time.deltaTime);
            Vector3 dir = PlayerRot.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(-dir);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        }
        if (MoveToward && FindObjectOfType<DN_PlayerMovement>().Pull && UpPull)
        {
          
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, AttractorSpeed * Time.deltaTime);
                // Vector3 dir = PlayerRot.position - transform.position;
                //Quaternion lookRotation = Quaternion.LookRotation(Vector3.up);
                //Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
                //transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
         
          
                // Vector3 dir = PlayerRot.position - transform.position;
                //Quaternion lookRotation = Quaternion.LookRotation(Vector3.down);
                //Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
                //transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
           

        }
        if (MoveToward && FindObjectOfType<DN_PlayerMovement>().Pull && DownPull)
        {
            
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, AttractorSpeed * Time.deltaTime);
                // Vector3 dir = PlayerRot.position - transform.position;
                //Quaternion lookRotation = Quaternion.LookRotation(Vector3.up);
                //Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
                //transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
          
        }
        if (FindObjectOfType<DN_PlayerMovement>().Pull == false)
        {
            DownPull = false;
            UpPull = false;
            RightPull = false;
            LeftPull = false;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           
                MoveToward = false;
            
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           
                MoveToward = false;
            
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
          
            MoveToward = true;
        }
    }

}
