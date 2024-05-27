using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUpGenerater : MonoBehaviour
{
    public static DamagePopUpGenerater current;
    public GameObject prefab;

    private void Awake()
    {
        current = this;
    }

    public void CreatePopUp(Vector3 position, string text)
    {
        var popup = Instantiate(prefab,position, Quaternion.identity);
        TextMeshProUGUI temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        temp.text = text;
        Destroy(popup, 1f);
        Destroy(temp, 1f);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
/*        if(Input.GetKeyDown(KeyCode.I))
        {
            CreatePopUp(Vector3.one,Random.Range(0,1000).ToString());   
        }*/
    }
}
