using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public LayerMask whatIsEnemy;
    public LayerMask whatIsRig;
    public GameObject bulletHoleGraphicEnemy;
    public GameObject bulletHoleGraphicRig;

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & whatIsEnemy) != 0)
        {
           // if (other.CompareTag("Enemy"))
           // {
              //  other.GetComponent<Shootin>().TakeDamage(damage);
            //}
            GameObject hole = Instantiate(bulletHoleGraphicEnemy, transform.position, Quaternion.Euler(0, 180, 0));
            Destroy(hole, 3f);
            Destroy(gameObject);
        }
        else if (((1 << other.gameObject.layer) & whatIsRig) != 0)
        {
            GameObject hole = Instantiate(bulletHoleGraphicRig, transform.position, Quaternion.Euler(0, 180, 0));
            Destroy(hole, 3f);
            Destroy(gameObject);
        }
    }
}
