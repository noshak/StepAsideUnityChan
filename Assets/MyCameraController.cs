using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour
{
    private GameObject unitychan; //Unityちゃんのオブジェクト

    private float difference; //Unityちゃんとカメラの距離

    // Start is called before the first frame update
    void Start()
    {
        this.unitychan = GameObject.Find("unitychan"); //Unityちゃんのオブジェクトを取得

        //Unityちゃんとカメラの位置の差(座標Z)を求める
        this.difference = unitychan.transform.position.z - this.transform.position.z; 
    }

    // Update is called once per frame
    void Update()
    {
        //Unityちゃんの位置に合わせてカメラの位置を移動
        this.transform.position = new Vector3(
            0, this.transform.position.y, this.unitychan.transform.position.z - difference); 
    }
}
