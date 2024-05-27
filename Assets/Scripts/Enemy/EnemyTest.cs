using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public AttributesManager player;
    public AttributesManager enemy;
    public float offsetY = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vector3 = new Vector3(transform.position.x,transform.position.y+1,transform.position.z);
        if (Input.GetKeyDown(KeyCode.K))
        {
            DamagePopUpGenerater.current.CreatePopUp(vector3, Random.Range(0, 1000).ToString());
        }
        // vị trí trừ máu
       
    }
    private void OnTriggerEnter(Collider other)
    {
    }
}
