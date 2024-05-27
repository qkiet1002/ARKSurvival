using System.Collections;
using UnityEngine;
using TMPro;

public class SimpleGun : MonoBehaviour
{
    [Header("Gun Settings")]
    public int damage = 10;
    public float timeBetweenShooting = 0.5f;
    public int magazineSize = 30;
    public float reloadTime = 2f;
    public bool allowButtonHold = true;

    private int bulletsLeft;
    private bool readyToShoot = true;
    private bool reloading;

    [Header("References")]
    public Camera fpsCam;
    public Transform attackPoint;
    public GameObject bulletPrefab;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI outOfAmmoText;

    private void Start()
    {
        InitializeGun();
    }

    private void InitializeGun()
    {
        bulletsLeft = magazineSize;
        outOfAmmoText.gameObject.SetActive(false);
    }

    private void Update()
    {
        HandleInput();
        ammoText.SetText(bulletsLeft + "/" + magazineSize);

        if (bulletsLeft == 0 && !reloading)
        {
            outOfAmmoText.gameObject.SetActive(true);
        }
    }

    private void HandleInput()
    {
        if (allowButtonHold ? Input.GetKey(KeyCode.Mouse1) : Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (readyToShoot && !reloading && bulletsLeft > 0)
            {
                Shoot();
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        // Instantiate bullet
        GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, fpsCam.transform.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.damage = damage;

        bulletsLeft--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsLeft == 0 && !reloading)
        {
            outOfAmmoText.gameObject.SetActive(true);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        outOfAmmoText.gameObject.SetActive(false);
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
