using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyed : MonoBehaviour
{
    [SerializeField]
    private GameObject createDestroyed;


    public void DestroyCreate()
    {
        Instantiate(createDestroyed, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
