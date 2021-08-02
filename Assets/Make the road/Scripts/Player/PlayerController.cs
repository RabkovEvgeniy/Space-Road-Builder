using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Game state")] public bool isStarted;
    [Header("Player Speed")] public float speed; // Player speed
    Animation anim; // Player animator
    int direction; // Player movement direction
    int deads; // Value of deads
    Rigidbody rb; // Player rigidbody
    [Header("Maximum player lives")] public int maxLives = 1; //Maximum player lives
    bool gameLose;

    void Start() //Setting default values
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();

        PlayerPrefs.SetInt("Direction", 0); //Set to default
        PlayerPrefs.SetInt("Deads", 0); //Set to default
        direction = 0; //Set to default

        gameLose = false; //Set to default

        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation; //Freeze some coordinates so that the player does not move
    }

    void FixedUpdate()
    {
        deads = PlayerPrefs.GetInt("Deads"); //Get deads value
        if (deads >= maxLives) //If the number of deaths is more than one
        {
            if (PlayerPrefs.GetInt("LevelCompeted") != 1) //Level not passed
            {
                PlayerPrefs.SetInt("Dead", 1); //Dead true
                PlayerPrefs.SetInt("IsStart", 0); //Stop game

                Anim_Dizzy(); //Play dead animation

                gameLose = true; //Game over true
            }
        }


        PlayerPrefs.SetFloat("PlayerX", transform.position.x); //Save the coordinates of the player
        PlayerPrefs.SetFloat("PlayerZ", transform.position.z);

        speed = PlayerPrefs.GetFloat("Speed"); //Getting speed value

        if (PlayerPrefs.GetInt("IsStart") == 1) //If the game is started, turn on the play button
        {
            isStarted = true;
        }
        else //If the game has not started, and the explanation of how to play is not included, turn off the button
        {
            isStarted = false;
        }
       if (direction != PlayerPrefs.GetInt("Direction")) //If the direction of movement has changed, change the player's angle and frozen coordinates
        {
                direction = PlayerPrefs.GetInt("Direction"); //Get direction value 
                if (direction == 0) //If direction == forward
                {
                    gameObject.transform.eulerAngles = Vector3.zero; //Set to default
                    rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation; //Freeze position x and rottation
                }
                else
                {
                    gameObject.transform.eulerAngles = new Vector3(0, -90, 0); //Set left angles
                    rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation; //Freeze position z and rottation
            }
            }

       if (isStarted == true) //If the game is running, set the movement and turn on the animation
       {
                transform.Translate(Vector3.forward * speed * Time.deltaTime); //Add movement
                Anim_Run(); //Play running animation
       }
       else
       {
                if (gameLose == false) //If the game is off, there are less than one deaths and the level is not completed, then turn on the Idle animation
                {
                    Anim_Idle(); //Play animation idle
                }
                else
                {
                    Anim_Dizzy(); //Play dead animation 
                }
            }
        }

        public void Anim_Idle()
        {
            anim.Play("Idle"); //You can rename animations if you animation have another names
    }
        public void Anim_Run()
        {
            anim.Play("Run"); //You can rename animations if you animation have another names
        }
        public void Anim_Dizzy()
        {
            anim.Play("Dizzy"); //You can rename animations if you animation have another names
    }
} 
