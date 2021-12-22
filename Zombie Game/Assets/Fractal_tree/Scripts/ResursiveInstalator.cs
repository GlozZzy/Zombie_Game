using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResursiveInstalator : MonoBehaviour
{
    public int recurse;
    private int split = 2;

    [Range(10f, 70f)]
    public float angle;

    [Range(0.6f, 0.8f)]
    public float heigh;

    public int greenRange;
    public Material greenMaterial;

    // Start is called before the first frame update
    void Start()
    {
        recurse -= 1;
        for (int i = 0; i < split; i++)
        {
            if (recurse < greenRange)
            {
                gameObject.transform.GetChild(0).GetComponent<Renderer>().material = greenMaterial;
            }
            if (recurse > 0)
            {
                
                var copy = Instantiate(gameObject);
                copy.transform.SetParent(transform.parent);
                copy.transform.position += copy.transform.parent.transform.position + copy.transform.up * copy.transform.localScale.y;

                copy.transform.rotation *= Quaternion.Euler(Random.Range(15, angle) * ((i * 2) - 1), 90, 0);
                //copy.transform.rotation *= Quaternion.Euler(30 * ((i * 2) - 1), 0, 0);
                copy.transform.localScale *= Random.Range(heigh - 0.15f, heigh + 0.15f);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x * 3, transform.localScale.y, transform.localScale.z);
            }
        }
    }
}
