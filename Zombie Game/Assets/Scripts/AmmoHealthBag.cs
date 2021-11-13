using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoHealthBag : MonoBehaviour
{
    Player player;
    public AudioSource pickUpSound;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, 30 * Time.deltaTime);
    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            if (gameObject.name == "Health Variant(Clone)")
            {
                player.health += 20;
                if (player.health > 100) player.health = 100;
            }
            else
                player.ammo += 150;
            pickUpSound.Play();
            Destroy(gameObject, pickUpSound.clip.length);
        }
    }
}
