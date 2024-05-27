using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // public vThirdPersonController thirdPersonController;
    public GameObject weapon;
    public void EnableWeaponCollider(int isEnable)
    {
        var col = weapon.GetComponentInChildren<MeshCollider>();
        if (col != null)
        {
            if (isEnable == 1)
            {
                col.enabled = true;
            }
            else
            {
                col.enabled = false;
            }
        }
    }

/*    public void EnableMovement(bool enable)
    {
        if (enable == false)
        {
            thirdPersonController.lockMovement = true;
            thirdPersonController.lockRotation = true;
        }
        else
        {
            thirdPersonController.lockMovement = false;
            thirdPersonController.lockRotation = false;
        }
    }*/
}
