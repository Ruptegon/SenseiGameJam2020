using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    private float lifeTime = 30f;
    private float speed = 1f;
    int direction = 0; // 0 - forward, 1 - backwards, 2 - left, 3 - right

    private void Start()
    {
        direction = GetComponentInParent<ArrowSpit>().Direction;

        switch (direction)
        {
            case 0:
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 90, transform.rotation.eulerAngles.z);
                break;
            case 1:
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 90, transform.rotation.eulerAngles.z);
                break;
        }
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
            Destroy(gameObject);

        switch (direction) 
        {
            case 0: 
                transform.position += new Vector3(transform.forward.z, transform.forward.y, transform.forward.x) * speed * Time.deltaTime;
                break;
            case 1:
                transform.position += new Vector3(transform.forward.z, transform.forward.y, transform.forward.x) * -1 * speed * Time.deltaTime;
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
