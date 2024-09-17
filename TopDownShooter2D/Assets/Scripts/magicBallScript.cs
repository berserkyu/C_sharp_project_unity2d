using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicBallScript : MonoBehaviour
{
    [SerializeField] private GameObject damageDealer;
    private GameObject new_damageDealer;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sortingLayerName = "Non_background_layer1";
        GetComponent<SpriteRenderer>().sortingOrder = 3;
    }
    void spawnDamageDealer()
    {
        new_damageDealer = Instantiate(damageDealer, transform.position, transform.rotation);
        GetComponent<Animator>()?.Play("dissolves");
    }
    void dissolved()
    {
        Destroy(new_damageDealer);
        Destroy(gameObject);
    }

}
