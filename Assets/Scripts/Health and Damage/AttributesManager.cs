using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesManager : MonoBehaviour
{
    public int healt;
    public int attack;
    public float critDamage = 1.5f;
    public float critChanece = 0.5f;

    public void TakeDamage(int amout)
    {
        healt -= amout;
        Vector3 rand = new Vector3(Random.Range(0, 0.25f),
            Random.Range(0, 0.25f),
            Random.Range(0, 0.25f));
        DamagePopUpGenerater.current.CreatePopUp(transform.position+rand, amout.ToString());
    }

    public void DealDamege(GameObject target)
    {
        float totalDamage = attack;
        var atm = target.GetComponent<AttributesManager>();
        atm.TakeDamage(attack);
        if (atm != null)
        {

            if (Random.Range(0f, 1f) < critChanece)
            {
                totalDamage *= critDamage;
            }
            atm.TakeDamage((int)totalDamage);
        }
        /* return totalDamage;*/
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
