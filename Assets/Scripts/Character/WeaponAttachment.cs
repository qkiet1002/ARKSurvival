using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponAttachment : MonoBehaviour
{
    public GameObject weapon; //Prefab của vũ khí
    public Transform weaponAttachPoint;// Điểm gắn kết (gán trong Inspector)

    private GameObject currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        AttachWeapon();
    }

    private void LateUpdate()
    {
        if (currentWeapon == null || currentWeapon.name != weapon.name + "(Clone)")
        {
            AttachWeapon();
        }
    }
    public void AttachWeapon()
    {
        // Xóa vũ khí cũ nếu có
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        if (weapon != null && weaponAttachPoint != null)
        {
            currentWeapon = Instantiate(weapon, weaponAttachPoint.position, weaponAttachPoint.rotation, weaponAttachPoint);
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;
        }
    }



}
