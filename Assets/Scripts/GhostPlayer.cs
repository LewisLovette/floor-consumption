using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlayer : MonoBehaviour
{

    private GameObject playerPos;

    private int layerIgnorTo = 9;
    private int layerIgnorFrom = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            //Turn off collisions between object and player layer.
            Physics.IgnoreLayerCollision(layerIgnorTo, layerIgnorFrom, true);
            print("Are collisions between " + layerIgnorTo + " and " + layerIgnorFrom + " being ignored?   " + Physics.GetIgnoreLayerCollision(layerIgnorTo, layerIgnorFrom));


        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            //Turn on collisions again
            Physics.IgnoreLayerCollision(layerIgnorTo, layerIgnorFrom, false);
            print("Are collisions between " + layerIgnorTo + " and " + layerIgnorFrom + " being ignored?   " + Physics.GetIgnoreLayerCollision(layerIgnorTo, layerIgnorFrom));


        }
    }

}
