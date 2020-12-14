using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BonusEffect
{
    GameManager gameManager;
    public GameObject shieldFX;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Effect();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Effect()
    {
        GameObject fx = Instantiate(shieldFX, GameObject.Find("Field").transform.position, Quaternion.identity);
        gameManager.isShieldActivated = true;
        base.Effect();
    }
}
