using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StockUsernameTest : MonoBehaviour
{
    #region Singleton
    public static StockUsernameTest instance;

    void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this);
        }

        usernameInput.characterLimit = 7;
    }
    #endregion

    public string usernameToShow;
    public Text usernameToGet;
    public InputField usernameInput;

    public void UsernameToStock()
    {
        usernameToShow = usernameToGet.text;
    }
}
