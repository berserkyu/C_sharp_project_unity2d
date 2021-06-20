using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBehaviour : MonoBehaviour
{
    private HealthSystem hpSys;
    private int maxHp = 300;
    // Start is called before the first frame update
    void Start()
    {
        hpSys = new HealthSystem(maxHp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
