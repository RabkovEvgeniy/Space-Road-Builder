using UnityEngine;

public class DeadPlatform : MonoBehaviour
{

    int deads; //Value of deads now
    float time; //Timer
    [Header("The time after which the player will be thrown back forward")]public float timeMax; //Max time for timer

    bool Check = false; //If collision has already happend, Check == true;

    [Header("Force throwing up")] public float thrust;
    [Header("Force throwing backward")] public float thrust1;
    [Header("Force throwing forward")] public float thrust2;

    Rigidbody rb; //Player rigidbody

    [Header("If you need to add sound when player collision with obstacle, set it true")]
    public bool haveAudio;
    AudioSource audioSource;

    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>(); //Getting player rigidbody
        if (haveAudio) //If audio turned on
        {
            audioSource = GameObject.FindGameObjectWithTag("Audio_2").GetComponent<AudioSource>(); //Get audio source
        }
    }

    void OnCollisionEnter(Collision collision) //When collision with obstacle
    {
        if (collision.gameObject.tag == "Player") //If collision gameobject.tag == "Player"
        {
            if (Check == false) //If this is first collision
            {
                    if (PlayerPrefs.GetInt("Direction") == 0) //If direction == forward
                    {
                        rb.AddForce(transform.up * thrust, ForceMode.Impulse); //Throw the player up with thrust speed
                        rb.AddForce(-transform.forward * thrust1, ForceMode.Impulse); //And backward with the speed of thrust2

                        deads = PlayerPrefs.GetInt("Deads"); //Add the number of deads
                        deads = deads + 1;
                        PlayerPrefs.SetInt("Deads", deads); //Set number of deads

                        Check = true; //Set collision happend
                }
                    else //If direction == left
                    {
                        rb.AddForce(transform.up * thrust, ForceMode.Impulse);//Throw the player up with thrust speed
                        rb.AddForce(transform.forward * thrust1, ForceMode.Impulse);//And backward with the speed of thrust2

                        deads = PlayerPrefs.GetInt("Deads"); //Add the number of deads
                        deads = deads + 1;
                        PlayerPrefs.SetInt("Deads", deads); //Set number of deads

                        Check = true; //Set collision happend
                    }

                if (haveAudio) //If audio turned on
                {
                    audioSource.Play(); //Play sound
                }

            }
        }
        
    }

    void FixedUpdate()
    {
        if (Check == true) //If collision happend
        {
            time = time + Time.fixedDeltaTime; //Counting the time

            if (time > timeMax) //If time > time maximum value
            {
                Check = false; //Allow the method to work again
                time = 0; //Set time == 0

                if (PlayerPrefs.GetInt("Direction") == 0) //The depending of direction, add force forward
                {
                    rb.AddForce(transform.forward * thrust2, ForceMode.Impulse); //Add force forward in forward direction
                    
                }
                else
                {
                    rb.AddForce(-transform.forward * thrust2, ForceMode.Impulse); //Add force forward in left direction
                }
            }
        }
    }
}
