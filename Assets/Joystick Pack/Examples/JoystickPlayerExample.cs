using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    float horizontalMove = 0f;
    float roadWidth = 3f;

    public Joystick joy;
    //public Rigidbody rb;
    public Transform tb;
    public Transform showBody;

    public ParticleSystem leftSmoke;
    public ParticleSystem rightSmoke;

    public bool leftPuff = false;
    public bool rightPuff = false;

    public float collisionX;


    private void Start()
    {
        tb = this.GetComponent<Transform>();
    }




    private void Update()
    {

        if(joy.Horizontal==0)
        {
            Vector3 targetRotation = new Vector3(0, joy.Horizontal * 8, joy.Horizontal * 16);
            showBody.localRotation = Quaternion.RotateTowards(showBody.localRotation, Quaternion.Euler(targetRotation), speed / 2);
            rightPuff = false;
            leftPuff = false;
            if (horizontalMove > 0)
            {
                horizontalMove = Mathf.Clamp(horizontalMove - Time.deltaTime * speed * 2,0,Mathf.Infinity);
            }
            if(horizontalMove < 0)
            {
                horizontalMove = Mathf.Clamp(horizontalMove + Time.deltaTime * speed * 2, -Mathf.Infinity , 0);
            }
        }
        else
        {
            horizontalMove = joy.Horizontal * speed;
            if(joy.Horizontal>0.2)
            {
                if(!leftPuff)
                {
                    leftSmoke.Play();
                    rightPuff = false;
                    leftPuff = true;
                }
            }
            if (joy.Horizontal < -0.2)
            {
                if (!rightPuff)
                {
                    rightSmoke.Play();
                    leftPuff = false;
                    rightPuff= true;
                }
            }
            Vector3 targetRotation = new Vector3(0, joy.Horizontal*8, joy.Horizontal*16);
            showBody.localRotation = Quaternion.RotateTowards(showBody.localRotation, Quaternion.Euler(targetRotation), speed/2); //Quaternion.Euler(targetRotation);
            
            /*
            if (tb.position.x > roadWidth)
            {
                //horizontalMove = 0;
                tb.position = new Vector3(roadWidth, tb.position.y, tb.position.z);
            }
            if (tb.position.x < -roadWidth)
            {
                //horizontalMove = 0;
                tb.position = new Vector3(-roadWidth, tb.position.y, tb.position.z);
            }
            */
            
            /*
            if(rb.position.x>2.8f)
            {
                horizontalMove = 0;
                rb.position = new Vector3(2.79f, rb.position.y, rb.position.z);
            }
            if (rb.position.x<-2.8f)
            {
                horizontalMove = 0;
                rb.position = new Vector3(-2.79f, rb.position.y, rb.position.z);
            }
            */

        }
        //rb.AddForce(new Vector3(horizontalMove*speed,0,0),ForceMode.Acceleration);
        //rb.velocity += new Vector3(horizontalMove, 0,0);
        Vector3 targetPos = new Vector3(tb.position.x + (horizontalMove * speed * Time.deltaTime), 0, 0);
        if(targetPos.x>-roadWidth && targetPos.x<roadWidth)
        {
            tb.Translate(new Vector3(horizontalMove * speed * Time.deltaTime, 0, 0), Space.World);
        }



    }

}