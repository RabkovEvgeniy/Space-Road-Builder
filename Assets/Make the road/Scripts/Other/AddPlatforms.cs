using UnityEngine;

public class AddPlatforms : MonoBehaviour
{
    [Header("The number of platforms added to the player")]
    public int addNumber; //How many platforms will add to the player after the collision

    int nowPlatforms, maxPlatfroms;  //the maximum number of platforms available now
    bool Check = false; //If the method worked, then check = true

    [Header("If you need to add sound when player get plaforms, set it true")]
    public bool haveAudio;
    AudioSource audioSource;

    void Start()
    {
        maxPlatfroms = GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformSpawner>().maximumBlocks; //Get the maximum number of platforms
        if (haveAudio) //If audio turned on
        {
            audioSource = GameObject.FindGameObjectWithTag("Audio_1").GetComponent<AudioSource>(); //Get audio source
        }
    } 

    void OnCollisionEnter(Collision collision) //In case of a collision with AddPlatform, add platforms for the number of AddNumber, if there are more than 50 platforms, do not add anything
    {       
        if (collision.gameObject.tag == "Player") //If you collision an object with the "Player" tag
        {
            if (Check == false) //If this is the first collision
            { 
                nowPlatforms = PlayerPrefs.GetInt("PlatformsNumber"); //Get number of platforms available now
                nowPlatforms = nowPlatforms + addNumber; //Add platforms to general platforms number

                if (nowPlatforms > maxPlatfroms) //If platforms number more then maximum platforms number
                {
                    nowPlatforms = maxPlatfroms; //Set general platforms number == maximum platforms 
                }

                PlayerPrefs.SetInt("PlatformsNumber", nowPlatforms); //Set platforms number to player prefs

                if (haveAudio) //If audio turned on
                {
                    audioSource.Play(); //Play sound
                }

                Check = true; //The collision has already happened
                gameObject.SetActive(false); //Turn off adders gameobject
            }
        }
    }
}

