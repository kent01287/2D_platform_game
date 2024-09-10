using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    
    
    public Vector2 minPosition;
    public Vector2 maxPosition;
    // Start is called before the first frame update
    void Start()
    {
        GameController.CamShake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();//尋找Tag，以後GameController.CamShake可任意使用
    }
    void LateUpdate() 
    {
        if(target != null)//Player死了才不會有問題
        {
            if(transform.position != target.position)
            {
                Vector3 targetPos = target.position;//去找Player位置
                targetPos.x = Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);//設定鏡頭限制
                targetPos.y = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);//設定鏡頭限制
                transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);//Lerp線性插植，我也不知道是啥
            }
        }
    }
    public void SetCamPosLimit(Vector2 minPos, Vector2 maxPos)
    {
        minPosition = minPos;
        maxPosition = maxPos;
    }
}
