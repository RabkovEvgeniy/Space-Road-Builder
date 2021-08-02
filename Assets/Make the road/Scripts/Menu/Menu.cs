using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Header("Menu panels")]
    public GameObject StartMenu;
    public GameObject InGame;
    public GameObject LevelComplete;
    public GameObject GameOver; // Menu panels

    int levelId, xScore, Score;

    [Header("Texts on level complete panel")]
    public Text xscore;
    public Text score;

    int gamesPlayed;

    [Header("Levels score")]
    public Text LevelScore_InGame;
    public Text LevelScore_LevelComplete;

    [Header("A image of the next level and the past")]
    public Image levelNow_InGame;
    public Image levelNow_LevelComplete;
    public Image levelNext_InGame;
    public Image levelNext_LevelComplete;

    [Header("Levels icons Path: Sprites/Levels")]
    public Sprite[] levelIcons;

    int nextLevel;

    [Header("StartUp script in Canvas")]
    public StartUp StartUpScript;

    [Header("Settings Panel")]
    public GameObject SettingsPanel;

    public Image music;
    public Image vibration;

    [Header("Button Sprites path:Sprites/Settings")]

    public Sprite musicOn;
    public Sprite musicOff;

    public Sprite vibrationOn;
    public Sprite vibrationOff;

    bool settingsOpened;

    int platfomNumber;
    [Header("Change it if you have sounds for this")]
    public bool haveAudioForWin, haveAudioForLose;
    [Header("AudioSources when player win or lose")]
    public AudioSource audioWin;
    public AudioSource audioLose;

    void Start() //Load data
    {
        levelId = PlayerPrefs.GetInt("levelId"); //Get level id
        nextLevel = PlayerPrefs.GetInt("levelId") + 1; //Set next level id

        if (PlayerPrefs.GetInt("Music") == 1) //If the music is turned off, turn it off at the start of the game.
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }

    void FixedUpdate()
    {
        if (InGame.active) //If the game is started, set the score and level pictures.
        {
            SetInGame();
        }
        if (PlayerPrefs.GetInt("LevelComplete") == 1 && InGame.active) //If the level is passed, turn off the game panel, turn on the finish panel and run the method.
        {
            SetInLevelCompleted();
        }
        if (PlayerPrefs.GetInt("Dead") == 1 && InGame.active) //If the game fails, add the value of the games played and turn on the loss panel.
        {
            SetInDead();
        }    
    }

    void SetInGame() //If InGame panel active
    {
        levelNow_InGame.sprite = levelIcons[levelId]; //Set level now sprite
        levelNext_InGame.sprite = levelIcons[nextLevel]; // Set level next sprite

        LevelScore_InGame.text = "" + PlayerPrefs.GetInt("Score"); //Set score now
    }
    void SetInLevelCompleted() //If level completed
    {
        InGame.SetActive(false); //Disable game panel
        LevelComplete.SetActive(true); //Enable finish panel
         
        levelId = PlayerPrefs.GetInt("levelId"); //Get levelId
        if (!StartUpScript.TestingMode) //If it isn't testing mode
        { 
            levelId = levelId + 1; //Calculate new level id
            if (levelId == 12) levelId = 1;
        }
        PlayerPrefs.SetInt("levelId", levelId); //Set level id
        nextLevel = levelId + 1; //Calculate next level id

        xScore = PlayerPrefs.GetInt("xScore"); //Get xScore
        switch (xScore) //Depending of xScore add general score
        {
            case 1:
                Score = PlayerPrefs.GetInt("Score");
                Score = Score + 50;
                 PlayerPrefs.SetInt("Score", Score);
                break;
            case 2:
                Score = PlayerPrefs.GetInt("Score");
                Score = Score + 100;
                PlayerPrefs.SetInt("Score", Score);
                break;

            case 3:
                Score = PlayerPrefs.GetInt("Score");
                Score = Score + 150;
                PlayerPrefs.SetInt("Score", Score);
                break;
            case 4:
                Score = PlayerPrefs.GetInt("Score");
                Score = Score + 200;
                PlayerPrefs.SetInt("Score", Score);
                break;

            case 5:
                Score = PlayerPrefs.GetInt("Score");
                Score = Score + 250;
                PlayerPrefs.SetInt("Score", Score);
                break;
            case 6:
                Score = PlayerPrefs.GetInt("Score");
                Score = Score + 300;
                PlayerPrefs.SetInt("Score", Score);
                break;

            case 7:
                Score = PlayerPrefs.GetInt("Score");
                Score = Score + 350;
                PlayerPrefs.SetInt("Score", Score);
                break;
            case 8:
                Score = PlayerPrefs.GetInt("Score");
                Score = Score + 400;
                PlayerPrefs.SetInt("Score", Score);
                break;

            case 9:
                Score = PlayerPrefs.GetInt("Score");
                Score = Score + 450;
                PlayerPrefs.SetInt("Score", Score);
                break;
            case 10:
                Score = PlayerPrefs.GetInt("Score");
                Score = Score + 500;
                PlayerPrefs.SetInt("Score", Score);
                break;
        }

        xscore.text = "x" + xScore; //Set xScore text
        score.text = "Total score: " + Score; //Set total score text

        //Get and calculate how much games played
        gamesPlayed = PlayerPrefs.GetInt("gamesPlayed"); 
        gamesPlayed = gamesPlayed + 1;
        PlayerPrefs.SetInt("gamesPlayed", gamesPlayed);

        //Set new level sprites
        levelNow_LevelComplete.sprite = levelIcons[levelId];
        levelNext_LevelComplete.sprite = levelIcons[nextLevel];

        LevelScore_LevelComplete.text = "" + PlayerPrefs.GetInt("Score"); //Set score now again

        PlayerPrefs.SetInt("LevelComplete", 0); //Disable level complete

        GameObject.Find("Particle").transform.GetChild(0).GetComponent<ParticleSystem>().Play(); //Play win particles

        if (haveAudioForWin)
        {
            audioWin.Play();
        }

    }
    void SetInDead() //If player deaded
    {
        InGame.SetActive(false); //Disable game panel
        GameOver.SetActive(true); //Enable game over panel

        //Calculate and set how much games played
        gamesPlayed = PlayerPrefs.GetInt("gamesPlayed");
        gamesPlayed = gamesPlayed + 1;
        PlayerPrefs.SetInt("gamesPlayed", gamesPlayed);

        //Set to default
        PlayerPrefs.SetInt("Dead", 0);
        PlayerPrefs.SetInt("Deads", 0);

        if (haveAudioForLose)
        {
            audioLose.Play();
        }
    }

    public void OnClick_Play()
    {
        //When you press the Play button, turn off the menu and turn on the game panel.
        StartMenu.SetActive(false);
        InGame.SetActive(true);

        //Game status = started.
        PlayerPrefs.SetInt("IsStart", 1);

        //Set default speed.
        PlayerPrefs.SetFloat("Speed", 4.5f);
        PlayerPrefs.SetFloat("AddPlatNum", 1.5f);

        //Finish score = 0, level complete = false.
        PlayerPrefs.SetInt("xScore", 0);
        PlayerPrefs.SetInt("LevelComplete", 0);
        //Player can spawn blocks
        PlayerPrefs.SetInt("SpawnOn", 1);

        settingsOpened = false; //If settings panel opened, disable it
        SettingsPanel.SetActive(false);
    }
    public void OnClick_Next() //When you press the Next button on finish panel.
    {
        //Open start menu panel
        LevelComplete.SetActive(false);
        StartMenu.SetActive(true);
        //Load new level
        SceneManager.LoadScene(levelId);
    }
    public void OnClick_NextOfOver() //When you press the Next button on a loss.
    {
        //Open start menu panel
        GameOver.SetActive(false);
        StartMenu.SetActive(true);
        //Load level again
        SceneManager.LoadScene(levelId);
        //Set to default
        PlayerPrefs.SetInt("Dead", 0);
    }
    public void OnClick_Setting() 
    {
        if (settingsOpened) //If settings panel opened, disable it
        {
            SettingsPanel.SetActive(false);
            settingsOpened = false;
        }
        else //If settings panel closed, enable
        {
            SettingsPanel.SetActive(true);
            settingsOpened = true;

            if (PlayerPrefs.GetInt("Music") == 1) //If music on, set music on sprite or if music off, set music off sprite
            {
                music.sprite = musicOn; //Set sprite
            }
            else
            {
                music.sprite = musicOff; //Set sprite
            }

            if (PlayerPrefs.GetInt("Vibration") == 1) //In the same way
            { 
                vibration.sprite = vibrationOn; //Set sprite
            }
            else
            {
                vibration.sprite = vibrationOff; //Set sprite
            }
        }
    }
    public void OnClick_Music() //If music on - disable music, if music off - enable music
    {
        if (PlayerPrefs.GetInt("Music") == 1)
        {
            AudioListener.volume = 0; //General game volume == 0
            PlayerPrefs.SetInt("Music", 0); //Music off

            music.sprite = musicOff; //Set music sprite == musicOff
        }
        else
        {
            AudioListener.volume = 1; //General game volume == 1
            PlayerPrefs.SetInt("Music", 1); //Music on

            music.sprite = musicOn; //Set music sprite == musicOn
        }
    }
    public void OnClick_Vibration() //Same way too
    {
        if (PlayerPrefs.GetInt("Vibration") == 1)
        {
            PlayerPrefs.SetInt("Vibration", 0); //Vibration off
            vibration.sprite = vibrationOff; //Set sprite
        }
        else
        {
            PlayerPrefs.SetInt("Vibration", 1); //Vibration on
            vibration.sprite = vibrationOn; //Set sprite vibration on
        }
    }
}
