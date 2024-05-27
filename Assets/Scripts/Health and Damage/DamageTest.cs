using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTest : MonoBehaviour
{
    public AttributesManager player;
    public AttributesManager enemy;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            player.DealDamege(enemy.gameObject);
            Debug.Log("");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            enemy.DealDamege(player.gameObject);
            Debug.Log("");
        }
    }
}
