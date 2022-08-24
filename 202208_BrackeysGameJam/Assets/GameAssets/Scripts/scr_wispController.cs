using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_wispController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //mouse follow
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
    }

}
