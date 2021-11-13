using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    public GameObject fastEnemy;
    public GameObject usualEnemy;
    public GameObject bigEnemy;
    private float TimeUntilNextSpawn = 0;
    private int Typeofenemy;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TimeUntilNextSpawn -= Time.deltaTime;
        if (TimeUntilNextSpawn <= 0.0f)
        {
            Typeofenemy = Random.Range(1, 9);
            if (Typeofenemy < 5)
                Instantiate(usualEnemy, transform.position, transform.rotation);
            else if (Typeofenemy >= 5 && Typeofenemy <= 7)
                Instantiate(fastEnemy, transform.position, transform.rotation);
            else
                Instantiate(bigEnemy, transform.position, transform.rotation);
            TimeUntilNextSpawn = Typeofenemy;
        }
    }
}
