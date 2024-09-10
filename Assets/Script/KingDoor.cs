using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KingDoor : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogBoxText;
    private bool isPlayerInSign;
    [Header("有鑰匙的對話")]
    public string haveKeyText;
    [Header("沒鑰匙的對話")]
    public string noKeyText;
    [Header("去王關")]
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
        if(Input.GetKeyDown(KeyCode.E) 
            && isPlayerInSign
            && GameData.HaveKey)
        {
            //紀錄門ID
            GameData.PrevEntranceID = id;
            UnityEngine.SceneManagement.SceneManager.LoadScene(roomID);
        }
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player") 
            && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInSign = true;
            dialogBox.SetActive(true);
            if(GameData.HaveKey)
            {
                dialogBoxText.text = haveKeyText;//"前方應該就是放著奧亞單之環的房間了吧(按E進入)";
            }
            else
            {
                dialogBoxText.text = noKeyText;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") 
            && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            
            isPlayerInSign = false;
            dialogBox.SetActive(false);
        }
    }
    public Vector2 GetFrontPosition()
    {
        return transform.GetChild(0).position;
    }
}
