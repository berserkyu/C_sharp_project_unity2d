using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBattle : MonoBehaviour
{
    [SerializeField] private int maxHp;
    [SerializeField] Transform health;
    private Vector3 initScale;
    private HealthSystem hpSys;
   
    // Start is called before the first frame update
    void Start()
    {
        hpSys = new HealthSystem(maxHp);
        initScale = health.localScale;
    }
    public void damage(int val)
    {
        hpSys.Damage(val);
        health.localScale = new Vector3(hpSys.GetHealthPercent()*initScale.x,initScale.y ,initScale.z);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
