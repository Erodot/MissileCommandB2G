using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeybindScript : MonoBehaviour
{
    public Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    public Text left, right, shoot;

    private GameObject currentKey;

    // Start is called before the first frame update
    void Start()
    {
        keys.Add("Left", KeyCode.LeftArrow);
        keys.Add("Right", KeyCode.RightArrow);
        keys.Add("Shoot", KeyCode.Mouse0);

        left.text = keys["Left"].ToString();
        right.text = keys["Right"].ToString();
        shoot.text = keys["Shoot"].ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        if(currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                currentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;
    }
}
