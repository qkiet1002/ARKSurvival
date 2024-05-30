using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerAttack : NetworkBehaviour
{
    public GameObject weapon;
    public GameObject gun;
    private bool isCanSung;
    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            enabled = false;
            return;
        }


    }
    // Start is called before the first frame update
    private Animator amin;
    void Start()
    {
        amin = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCanSung)
        {
            amin.SetBool("isGun", true);
        }
        else
        {
            amin.SetBool("isGun", false);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                amin.SetTrigger("slash");
            }
        }

       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            weapon.SetActive(true);
            gun.SetActive(false);
            isCanSung = false;
        }
        else
        {
            weapon.SetActive(false );
            gun.SetActive(true);
            isCanSung = true;
        }
    }
}
