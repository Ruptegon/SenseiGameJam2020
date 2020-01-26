using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    private float lifeTime = 30f;
    private float speed = 1f;
    [SerializeField] int direction = 0; // 0 - forward, 1 - backwards, 2 - left, 3 - right

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
            Destroy(gameObject);

        switch (direction) 
        {
            case 0:
                transform.position += transform.forward * speed * Time.deltaTime;
                break;
            case 1:
                transform.position += transform.forward * -1 * speed * Time.deltaTime;
                break;
            case 2:
                transform.position += new Vector3(transform.forward.z, transform.forward.y, transform.forward.x) * speed * Time.deltaTime;
                break;
            case 3:
                transform.position += new Vector3(transform.forward.z, transform.forward.y, transform.forward.x) * -1 * speed * Time.deltaTime;
                break;
        }
        
    }
}
