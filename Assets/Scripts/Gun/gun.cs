using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Invector.vCharacterController;
using Unity.Netcode;

public class Gun : NetworkBehaviour
{
    [Header("Gun Settings")]
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    private int bulletsLeft, bulletsShot;
    private bool shooting, readyToShoot, reloading;

    [Header("References")]
    public Camera fpsCam;
    public Transform attackPoint;
    public LayerMask whatIsEnemy;
    public LayerMask whatIsRig;
    public GameObject bulletHoleGraphicEnemy;
    public GameObject bulletHoleGraphicRig;
    public GameObject muzzleFlash;
    public GameObject bulletPrefab;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI outOfAmmoText;

    private vThirdPersonController thirdPersonController;

    private void Start()
    {
        if (IsOwner)
        {
            InitializeGun();
        }
    }

    private void InitializeGun()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        outOfAmmoText.gameObject.SetActive(false);

        // Find the local player object managed by Netcode
        var localPlayer = NetworkManager.Singleton.LocalClient.PlayerObject;
        Debug.Log(localPlayer);

        if (localPlayer != null)
        {
            thirdPersonController = localPlayer.GetComponentInChildren<vThirdPersonController>();
            if (thirdPersonController == null)
            {
                Debug.LogError("vThirdPersonController component not found on the local player.");
            }
        }
        else
        {
            Debug.LogError("Local player object not found.");
        }
    }

    private void Update()
    {
        if (!IsOwner) return;

        HandleInput();
        ammoText.SetText(bulletsLeft + "/" + magazineSize);

        if (bulletsLeft == 0 && !reloading)
        {
            outOfAmmoText.gameObject.SetActive(true);
            //StartCoroutine(HideOutOfAmmoTextAfterDelay(5f));
        }
    }

    private IEnumerator HideOutOfAmmoTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        outOfAmmoText.gameObject.SetActive(false);
    }

    private void HandleInput()
    {
        if (thirdPersonController == null) return;

        // Handle movement input
        Vector3 movementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        thirdPersonController.UpdateMovementState(movementInput);

        // Handle shooting input
        shooting = allowButtonHold ? Input.GetKey(KeyCode.Mouse1) : Input.GetKeyDown(KeyCode.Mouse1);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
            Reload();

        // Shoot if ready and shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, Quaternion.LookRotation(direction));
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.damage = damage;
        bulletScript.whatIsEnemy = whatIsEnemy;
        bulletScript.whatIsRig = whatIsRig;
        bulletScript.bulletHoleGraphicEnemy = bulletHoleGraphicEnemy;
        bulletScript.bulletHoleGraphicRig = bulletHoleGraphicRig;

        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        Destroy(bullet, 10f);

        bulletsLeft--;
        bulletsShot--;

        thirdPersonController.RotateToCameraDirection(fpsCam.transform.forward);

        Invoke("ResetShot", timeBetweenShooting);
        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
        outOfAmmoText.gameObject.SetActive(false);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
