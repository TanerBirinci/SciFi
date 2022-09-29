using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    private float _speed = 3.5f;
    private float _gravity = 9.81f;
    [SerializeField] private GameObject fireMuzzle;
    [SerializeField] private GameObject hitMarketPrefab;
    private GameObject hitmarkerInstantiate;
    [SerializeField]
    private AudioSource _audioSource;
    private int maxAmmo = 50;
    [SerializeField] private int curentAmmo;
    [SerializeField] private GameObject gun;
    private bool canReolad = true;
    public bool havaCoin = false;
    public bool haveGun = false;

    private UIManager _uıManager;
    private SharkShop _sharkShop;


    void Start()
    {
        _controller = FindObjectOfType<CharacterController>();
        _uıManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _sharkShop = FindObjectOfType<SharkShop>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        
        curentAmmo = maxAmmo;


    }


    void Update()
    {
        ReloadGun();
        HaveCoin();
        if (Input.GetMouseButton(0)&& curentAmmo>0)
        {
            Shoot();
            
        }
        else
        {
            _audioSource.Stop();
            fireMuzzle.SetActive(false);
        }
        
        
        
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }



    private void Shoot()
    {
        if (haveGun==true)
        {
            curentAmmo--;
            _uıManager.UpdateAmmo(curentAmmo);
            if (_audioSource.isPlaying==false)
            {
                _audioSource.Play();
            }
            fireMuzzle.SetActive(true);
            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f, 0));
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin,out hitInfo))
            {
                Debug.Log("Hit: "+ hitInfo.transform.name);
                hitmarkerInstantiate=Instantiate(hitMarketPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(hitmarkerInstantiate,0.5f);

                destroyed crate = hitInfo.transform.GetComponent<destroyed>();
                if (crate!=null)
                {
                    crate.DestroyCreate();
                }
            }
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0,verticalInput);
        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity;

        velocity = transform.transform.TransformDirection(velocity);
        _controller.Move(velocity*Time.deltaTime);
    }

    private void ReloadGun()
    {
        StartCoroutine(ReoladGunRotuine());
    }

    IEnumerator ReoladGunRotuine()
    {
        if (canReolad==true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                canReolad = false;
                yield return new WaitForSeconds(1.5f);
                curentAmmo = 50;
                _uıManager.UpdateAmmo(curentAmmo);
                yield return new WaitForSeconds(4f);
                canReolad = true;
            }
        }
    }

    private void HaveCoin()
    {
        if (havaCoin==true)
        {
            _uıManager.UpdateCoin();
            _sharkShop.UpdateSharkGun();
        }
    }

    public void BuyGun()
    {
        gun.SetActive(true);
        haveGun = true; 
    }
    
}
