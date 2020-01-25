using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Button DPadUp;
    [SerializeField] Button DPadDown;
    [SerializeField] Button DPadLeft;
    [SerializeField] Button DPadRight;

    public static float timeToMove = 1;

    private int rotation = 0; //0 - forward, 1 - backward, 2 - left, 3 - right
    private bool canMove = false;
    private float time = 0;

    public AnimationCurve positionYCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f * timeToMove, 1), new Keyframe(timeToMove, 0));
    public AnimationCurve rotationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f * timeToMove, 180), new Keyframe(timeToMove, 360));

    // Start is called before the first frame update
    private void Start()
    {
        ClientUIController.Instance.UpArrow.onClick.AddListener(MoveForward);
        ClientUIController.Instance.DownArrow.onClick.AddListener(MoveBackward);
        ClientUIController.Instance.LeftArrow.onClick.AddListener(MoveLeft);
        ClientUIController.Instance.RightArrow.onClick.AddListener(MoveRight);
    }

    private void Update()
    {
        //Debug.Log("Rot = " + transform.localRotation.eulerAngles);
        if (canMove) 
        {
            this.transform.position += 1 * transform.forward * Time.deltaTime;
            this.transform.position = new Vector3(transform.position.x, positionYCurve.Evaluate(time), transform.position.z);
            //this.transform.rotation = Quaternion.Euler(rotationCurve.Evaluate(time), transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z); // demands fix
            time += Time.deltaTime;
            if (time >= timeToMove && transform.position.y == 0) 
            {
                canMove = false;
            }
        }
        
    }

    private void Move()
    {
        time = 0;
        canMove = true;
    }

    private void MoveForward()
    {
        if (!canMove)
        {
            if (rotation != 0)
            {
                rotation = 0;
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            //Debug.Log("PlayerMovement moveForward");
            //animator.SetTrigger("move");
            Move();
        }
    }

    private void MoveBackward()
    {
        if (!canMove)
        {
            if (rotation != 1)
            {
                rotation = 1;
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            //Debug.Log("PlayerMovement moveBackward");
            //animator.SetTrigger("move");
            Move();
        }
    }

    private void MoveLeft()
    {
        if (!canMove)
        {
            if (rotation != 2)
            {
                rotation = 2;
                this.transform.rotation = Quaternion.Euler(0, 270, 0);
            }
            //Debug.Log("PlayerMovement moveLeft");
            //animator.SetTrigger("move");
            Move();
        }
    }

    private void MoveRight()
    {
        if (!canMove)
        {
            if (rotation != 3)
            {
                rotation = 3;
                this.transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            //Debug.Log("PlayerMovement moveRight");
            //animator.SetTrigger("move");
            Move();
        }
    }
}


