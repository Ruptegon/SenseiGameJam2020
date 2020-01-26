using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    private float lifeTime = 30f;
    private float speed = 1f;

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
            Destroy(gameObject);

        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
