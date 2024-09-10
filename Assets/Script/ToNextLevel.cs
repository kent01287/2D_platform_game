using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToNextLevel : MonoBehaviour
{
    [Header("去哪一關")]
    public int roomID;
    [Header("門編號")]
    public string id = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        
        if(other.gameObject.CompareTag("Player") 
            && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            //紀錄門ID
            GameData.PrevEntranceID = id;
            UnityEngine.SceneManagement.SceneManager.LoadScene(roomID);
        }
    }
    public Vector2 GetFrontPosition()
    {
        return transform.GetChild(0).position;
    }
}
