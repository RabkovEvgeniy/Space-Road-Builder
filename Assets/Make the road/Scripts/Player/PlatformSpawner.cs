using System.Collections.Generic;
using UnityEngine;

using Input = InputWrapper.Input;

public class PlatformSpawner : MonoBehaviour
{
    [HideInInspector]public int platformsNumber;

    [Header("Prefabs for different directions of movement Path: Prefabs/Players")]
    public Platform platform_0;
    public Platform platform_1; //Prefabs for different directions of movement

    [Header("Maximum blocks in backpack")]
    [Header("BACKPACK SETTINGS")]
    public int maximumBlocks;

    [Header("Backpack block prefab. Prefabs/Players/Platform")]
    public GameObject prefab;

    [Header("Block parent on player")]
    public Transform parent;

    [Header("Backpack first block start position")]
    public Vector3 startBlockPosition;
    [Header("Offset between blocks in backpack")]
    public Vector3 offsetBetweenBlocks;
    [Header("Size of block in backpack")]
    public Vector3 backpackBlockSize;

    [Header("Forward first platform offset")]
    [Header("SPAWNER SETTINGS")]
    public Vector3 forwardOffsetFirst;
    [Header("Forward platform offset")]
    public Vector3 forwardOffset;

    [Header("Left first platform offset")]
    public Vector3 leftOffsetFirst;
    [Header("Left platform offset")]
    public Vector3 leftOffset;

    [HideInInspector]public List<GameObject> spawnedBlocks;
    int onPlatformNumber;

    [HideInInspector] public bool down, spawned, first;
    [HideInInspector] public List<Platform> spawnedPlatforms = new List<Platform>(); //List of already spawned platforms

    float time, timeMax;

    int direction;

    PlayerController playerCont;
    Touch touch;

    void Awake() 
    {
        PlayerPrefs.SetInt("PlatformsNumber", platformsNumber); //Set platforms number
        PlayerPrefs.SetFloat("spawnerTime", 0.1f); //Set time between spawn blocks

        //Set to default
        down = false; 
        spawned = false;
        first = true;

        //Instantiate blocks
        InstantiateBlocks();
        //Turn on platforms == platformsNumber
        CheckPlatforms();

        //Get player controller
        playerCont = GetComponent<PlayerController>();
    }
   
    void FixedUpdate()
    {  
     if (Input.touchCount > 0) //When you click on the screen, spawn blocks
     {
            ButtonDown(); //Spawn platforms
     }
     else
     {
            ButtonUp(); //Dont spawn platforms
     }

    if (PlayerPrefs.GetInt("IsStart") == 1) //If game started
    {
            timeMax = PlayerPrefs.GetFloat("spawnerTime"); //Get time between spawn blocks

            direction = PlayerPrefs.GetInt("Direction"); //Get the direction

            if (platformsNumber != PlayerPrefs.GetInt("PlatformsNumber")) //If the number of platforms has changed, run the method
            {
                platformsNumber = PlayerPrefs.GetInt("PlatformsNumber"); //Get this platforms number value

                if (onPlatformNumber != platformsNumber) //If enabled platforms != platformsNumbers
                {
                    CheckPlatforms(); //Turn on\off platforms
                }
            }

            if (down == true) //When player click spawn button
            {
                if (spawned == false) //If it is possible to spawn, run the spawn method
                {
                    SpawnPlatform();
                    spawned = true; //This means that the platform is already spanned and it takes time that the same interval between spawns, before spawning the next
                }
                else //If spawned == true, wait for the time
                {
                    time = time + Time.fixedDeltaTime; //Calculate timer time
                }
                if (time > timeMax) // and turn it off
                {
                    time = 0;
                    spawned = false;
                }
            }
        }
    }

    public void SpawnPlatform()
    {
        if (PlayerPrefs.GetInt("SpawnOn") == 1) //If you can spawn
        {
            if (PlayerPrefs.GetInt("PlatformsNumber") > 0) //If there are more than zero platforms
            {
                if (direction == 0) //Direction of spawn
                {
                    if (first == true) //If this is the first platform
                    {

                        Vector3 newPos = transform.position + forwardOffsetFirst;//Set the coordinates of the platform offset relative to the player so that they spawn in front of him

                        Platform newPlatform = Instantiate(platform_0); //Spawn the platform from the prefab
                        newPlatform.transform.position = newPos; //Set its coordinates
                        spawnedPlatforms.Add(newPlatform); //Add it to the list of spawned platforms, to work with it later

                        platformsNumber = platformsNumber - 1; //Reducing the number of platforms available to the player
                        PlayerPrefs.SetInt("PlatformsNumber", platformsNumber);
                        CheckPlatforms();

                        first = false;
                    }
                    else //If this is not the first platform, everything is the same, only an offset relative to the previous platform
                    {
                        Vector3 newPos = spawnedPlatforms[spawnedPlatforms.Count - 1].transform.position + forwardOffset;//Set the coordinates of the platform offset relative to the player so that they spawn in front of him

                        Platform newPlatform = Instantiate(platform_0); //Instantiate new forward platform
                        newPlatform.transform.position = newPos; //Set position
                        spawnedPlatforms.Add(newPlatform); //Add to list

                        platformsNumber = platformsNumber - 1; //Minus one platform
                        PlayerPrefs.SetInt("PlatformsNumber", platformsNumber); //Set new value
                        CheckPlatforms(); //Check platforms on number
                    }
                }
                else //Everything is the same as above, only with the direction to the left, not straight
                {
                    if (first == true)
                    {
                        Vector3 newPos = transform.position + leftOffsetFirst;//Set the coordinates of the platform offset relative to the player so that they spawn in front of him

                        Platform newPlatform = Instantiate(platform_1); //Instantiate new left platform
                        newPlatform.transform.position = newPos; //Set position
                        spawnedPlatforms.Add(newPlatform); //Add to list

                        platformsNumber = platformsNumber - 1; //Minus one platform
                        PlayerPrefs.SetInt("PlatformsNumber", platformsNumber);  //Set new value
                        CheckPlatforms(); //Check platforms on number

                        first = false; //Next platform isn't first
                    }
                    else
                    {
                        Vector3 newPos = spawnedPlatforms[spawnedPlatforms.Count - 1].transform.position + leftOffset;//Set the coordinates of the platform offset relative to the player so that they spawn in front of him

                        Platform newPlatform = Instantiate(platform_1); //Instantiate new left platform
                        newPlatform.transform.position = newPos; //Set position
                        spawnedPlatforms.Add(newPlatform); //Add to list


                        platformsNumber = platformsNumber - 1; //Minus one platform
                        PlayerPrefs.SetInt("PlatformsNumber", platformsNumber); // Set new value
                        CheckPlatforms(); //Check platforms on number
                    }
                }

            }
        }
    }

    public void ButtonDown() // If button spawn down
    {
        down = true; 
    }

    public void ButtonUp() // If button spawn up
    {
        down = false; //Button upped now
        spawned = false; //Now no one platforms spawned
        first = true; //New platforms will be first
        time = 0; //Set to default
    }

    void InstantiateBlocks()
    {
        for (int num = 0; num != maximumBlocks; num++) //If num != maximumBlocks value
        {
            GameObject newBlock = Instantiate(prefab); //Spawn new platform to backpack
            newBlock.transform.SetParent(parent); //Set parent

            if (onPlatformNumber != 0) //If it isn't first platform
            {
                newBlock.transform.localPosition = spawnedBlocks[spawnedBlocks.Count - 1].transform.localPosition + offsetBetweenBlocks; //Set position + offset
            }
            else
            {
                newBlock.transform.localPosition = startBlockPosition; //If it is first, set start position
            }
            newBlock.transform.localScale = backpackBlockSize; //Set platform size
            spawnedBlocks.Add(newBlock); //Add to list
            onPlatformNumber++;  //Increase value
        }
    }

    public void CheckPlatforms() // Check platform in characters backpack
    {
        platformsNumber = PlayerPrefs.GetInt("PlatformsNumber"); //Get the number of platforms that are now

        for (int i = 0; i < platformsNumber; i++) //If the number of platforms included is less than the player has, turn them on
        {
            spawnedBlocks[i].gameObject.SetActive(true); //Turn on platform
            onPlatformNumber = i - 1; //Platform numbers = i - 1, becouse list started form 0, but not form 1.

        }
        
        for (int i = spawnedBlocks.Count; i > platformsNumber; i--) //If the number of enabled platforms is more than the player has, turn them off
        {
            spawnedBlocks[i - 1].gameObject.SetActive(false); //Turn off platform
            onPlatformNumber = i - 1; //Platform numbers = i - 1, becouse list started form 0, but not form 1.
        }

    }
}
