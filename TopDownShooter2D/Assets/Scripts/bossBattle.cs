using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBattle : MonoBehaviour
{
    private HealthSystem hpSys;
    [SerializeField] private Transform healthTrans;
    [SerializeField] private int maxHp = 300;
    // Start is called before the first frame update
    void Start()
    {
        hpSys = new HealthSystem(maxHp);
    }

    public void damage(int dmg)
    {
        hpSys.Damage(dmg);
    }

    // Update is called once per frame
    void Update()
    {
        healthTrans.localScale = new Vector3(hpSys.GetHealthPercent(), 1, 1);
        if (hpSys.GetHealth() == 0)
        {
            transform.GetChild(0)?.gameObject.GetComponent<bossAnimation>()?.die();
        }
    }
}
