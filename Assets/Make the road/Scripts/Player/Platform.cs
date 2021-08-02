using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    Rigidbody rb; //Platform rigidbody

    float time;
    [Header("Time before turning on gravity after spawn")]
    public float timeMax;

    float time2;
    [Header("Time for the block to fall to the ground, after which gravity is turned off")]
    public float timeMax2; //Timers

    int direction; //Player walk direction
    float x, z; //values to offset

    [Header("Default power = 20")]
    public int vibrationPower = 20;

    [Header("How far must the player go before the platform to fall")]
    public float distance = 1.5f;

    void Start() //We get all the values ​​when the object appears
    {
        rb = GetComponent<Rigidbody>(); //Get platform rigidbody

        direction = PlayerPrefs.GetInt("Direction"); //Get direction value 

        //Set to default
        time = 0;
        time2 = 0;

        //If vibration on, vibrate
        if (PlayerPrefs.GetInt("Vibration") == 1)
        {
         Vibration.Vibrate(vibrationPower);
        }
    }

    void FixedUpdate()
    {       
        time = time + Time.fixedDeltaTime; //Calculate timer time
        if (time > timeMax) //When time2 is more then timeMax2
        {
            time2 = time2 + Time.fixedDeltaTime; //Calculate timer2 time

            //Gravity turn on
            rb.isKinematic = false;
            rb.useGravity = true;

            //If time2 > timeMax2 turn off gravity
            if (time2 > timeMax2)
            {
                rb.isKinematic = true;
                rb.useGravity = false;

                this.enabled = false; //Disable this script
            }
        }
        
        if (direction == 0) //If direction == forward 
        {
            z = transform.position.z + distance; //Calculate player position after which block go fall

            if (PlayerPrefs.GetFloat("PlayerZ") > z) //If player achieved this distance
            {
                time2 = time2 + Time.fixedDeltaTime; //Calculate timer

                //Gravity turn on
                rb.isKinematic = false; 
                rb.useGravity = true;

                if (time2 > timeMax2) 
                {
                    //Gravity turn off
                    rb.isKinematic = true;
                    rb.useGravity = false;

                    this.enabled = false; //Disable this script
                }
            }
        }
        else
        {
            x = transform.position.x - distance; //Calculate player position after which block go fall
            if (PlayerPrefs.GetFloat("PlayerX") < x) //If player achieved this distance
            {

                time2 = time2 + Time.fixedDeltaTime;  //Calculate timer

                //Gravity turn on
                rb.isKinematic = false;
                rb.useGravity = true;

                if (time2 > timeMax2)
                {
                    //Gravity turn off
                    rb.isKinematic = true;
                    rb.useGravity = false;

                    this.enabled = false; //Disable this script
                }
            }
        }
        
    }
}