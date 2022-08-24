using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_interactManager : MonoBehaviour
{
    private void OnMouseDrag()
    {
        //transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        GetComponent<Rigidbody2D>().MovePosition(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z)));
    }
}

