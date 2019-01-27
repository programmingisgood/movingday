using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DN_DontDestroy : MonoBehaviour
{
    public static DN_DontDestroy MusicInstance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (MusicInstance == null)
        {
            MusicInstance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
         
    }
    
}
