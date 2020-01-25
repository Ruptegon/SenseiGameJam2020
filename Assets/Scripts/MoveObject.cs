using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{

  //  public GameObject movableObject;


    // Start is called before the first frame update
    void Start()
    {
        //   iTween.MoveAdd(this.gameObject, transform.TransformDirection(Vector3.forward), 0.1f);
        iTween.MoveAdd(this.gameObject, iTween.Hash("amount", transform.TransformDirection(Vector3.forward) * 100,  "speed", 5));
    }

    // Update is called once per frame
    void Update()
    {
     //   iTween.MoveUpdate(this.gameObject, transform.TransformDirection(Vector3.forward), 0.1f);

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 40.0f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            Debug.Log("Was hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1, Color.white);
            Debug.Log("Hit was not");
        }
    }
}
