using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    private GameObject mycamera;

    // Start is called before the first frame update
    void Start()
    {
        mycamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if (mycamera.transform.position.z > this.transform.position.z)
        {
            Destroy(this.gameObject);
        }
    }
}
