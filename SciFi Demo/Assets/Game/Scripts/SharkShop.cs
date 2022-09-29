using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{
    private Player _player;
    public bool haveCoinn = false;
    [SerializeField]
    private AudioClip gunPickUp;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag=="Player"&& haveCoinn==true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _player.BuyGun();
                AudioSource.PlayClipAtPoint(gunPickUp, transform.position, 1f);
            }
        }
    }

    public void UpdateSharkGun()
    {
        haveCoinn = true;
    }
}
