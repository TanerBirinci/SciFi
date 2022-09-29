using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    [SerializeField]
    private AudioClip coinPickup;

    
    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag=="Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.GetComponent<Player>();
                
                if (player!=null)
                {
                    player.havaCoin = true;
                    AudioSource.PlayClipAtPoint(coinPickup,transform.position,1f);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
