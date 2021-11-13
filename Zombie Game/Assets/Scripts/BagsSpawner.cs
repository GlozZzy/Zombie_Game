using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagsSpawner : MonoBehaviour
{
    public float Period;
    public GameObject HealthBag;
    public GameObject AmmoBag;
    
    private float TimeUntilNextSpawn;
    private float TypeofBag;

    // Start is called before the first frame update
    void Start()
    { 
        TypeofBag = Random.Range(0, 3);
        if (TypeofBag < 1)
            Instantiate(HealthBag, transform.position, transform.rotation);
        else
            Instantiate(AmmoBag, transform.position, transform.rotation);
        TimeUntilNextSpawn = Random.Range(10, Period + 10);
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] intersecting = Physics.OverlapSphere(transform.position, 0.0001f);
        if (intersecting.Length > 0) return;
        else
        {
            TimeUntilNextSpawn -= Time.deltaTime;
            if (TimeUntilNextSpawn <= 0.0f)
            {
                TypeofBag = Random.Range(0, 3);
                if (TypeofBag < 1)
                    Instantiate(HealthBag, transform.position, transform.rotation);
                else
                    Instantiate(AmmoBag, transform.position, transform.rotation);
                TimeUntilNextSpawn = Random.Range(10, Period + 10);
            }
        }  
    }
}
