using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class LevelEditor : EditorWindow
{
    public GameObject _main, _light, _finish, _level, _particle, adder, barrels, changeDir, cone, deadBlock, deadCycle, deadPlatform, platform;
    public Material mat_1, mat_2, mat_3, mat_4, mat_5, mat_6, mat_7, mat_8;

    Scene scene;
    int windowId = 0;
    bool finishSpawned, editorClosed;

    InstantiatePlayer instantiatePlayer;
    BlocksArray blocksArray;
    OthersArray othersArray;

    Camera camera;
    Color colorBackground = new Color(0.5686275f, 0.9803922f, 0.7843137f, 1);

    Texture2D image_1, image_2, image_3;

    MeshRenderer mesh;

    void OnGUI()
    {
        switch (windowId) //Turn on the window we need
        {
            case 0:
                GUILayout.Label("First stage | Creating a scene with basic objects", EditorStyles.boldLabel); //Title Text
                editorClosed = false;  //Editor opened, save changes

                image_1 = Resources.Load("p1", typeof(Texture2D)) as Texture2D; //Add image
                GUILayout.Label(image_1, GUILayout.Width(300), GUILayout.Height(300));

                if (GUI.Button(new Rect(60, 310 + EditorGUIUtility.singleLineHeight, 180, 60), "Create new level"))
                {
                    NewScene(); //Creating new scene
                    windowId = 1; //Setting next window id
                }
                if (GUI.Button(new Rect(10, 430 + EditorGUIUtility.singleLineHeight, 100, 40), "Next"))
                {
                    windowId = 1; //Setting next window id
                    LoadObjects();
                }

                break;

            case 1:
                GUILayout.Label("Second stage | Setup background and platforms color", EditorStyles.boldLabel); //Title Text

                colorBackground = EditorGUILayout.ColorField("Background Color", colorBackground); //You can choose any color
                if (GUI.Button(new Rect(10, 30 + EditorGUIUtility.singleLineHeight, 120, 40),"Set color")) //Set camera color and fog
                {
                    camera.backgroundColor = colorBackground;
                    RenderSettings.fogColor = colorBackground;
                }
                if (GUI.Button(new Rect(135, 30 + EditorGUIUtility.singleLineHeight, 120, 40), "Reset to standart")) //Restoring the default colors
                {
                    colorBackground = new Color(0.5686275f, 0.9803922f, 0.7843137f, 1);

                    camera.backgroundColor = colorBackground;
                    RenderSettings.fogColor = colorBackground;
                }
                GUI.Label(new Rect(175, 75 + EditorGUIUtility.singleLineHeight, 160, 40), "Set platforms color", EditorStyles.boldLabel);
                //You can choose the color for the platforms
                if (GUI.Button(new Rect(10, 110 + EditorGUIUtility.singleLineHeight, 100, 100), Resources.Load("1") as Texture)) 
                {
                    ChangePlatformsColor(mat_1); //Setting one color for all platforms
                }
                if (GUI.Button(new Rect(115, 110 + EditorGUIUtility.singleLineHeight, 100, 100), Resources.Load("2") as Texture))
                {
                    ChangePlatformsColor(mat_2);//Setting one color for all platforms
                }
                if (GUI.Button(new Rect(220, 110 + EditorGUIUtility.singleLineHeight, 100, 100), Resources.Load("3") as Texture))
                {
                    ChangePlatformsColor(mat_3);//Setting one color for all platforms
                }
                if (GUI.Button(new Rect(325, 110 + EditorGUIUtility.singleLineHeight, 100, 100), Resources.Load("4") as Texture))
                {
                    ChangePlatformsColor(mat_4);//Setting one color for all platforms
                }
                if (GUI.Button(new Rect(10, 215 + EditorGUIUtility.singleLineHeight, 100, 100), Resources.Load("5") as Texture))
                {
                    ChangePlatformsColor(mat_5);//Setting one color for all platforms
                }
                if (GUI.Button(new Rect(115, 215 + EditorGUIUtility.singleLineHeight, 100, 100), Resources.Load("6") as Texture))
                {
                    ChangePlatformsColor(mat_6);//Setting one color for all platforms
                }
                if (GUI.Button(new Rect(220, 215 + EditorGUIUtility.singleLineHeight, 100, 100), Resources.Load("7") as Texture))
                {
                    ChangePlatformsColor(mat_7);//Setting one color for all platforms
                }
                if (GUI.Button(new Rect(325, 215 + EditorGUIUtility.singleLineHeight, 100, 100), Resources.Load("8") as Texture))
                {
                    ChangePlatformsColor(mat_8);//Setting one color for all platforms
                }
                if (GUI.Button(new Rect(115, 430 + EditorGUIUtility.singleLineHeight, 100, 40), "Next"))
                {
                    windowId = 2; //Setting next window id
                }
                if (GUI.Button(new Rect(10, 430 + EditorGUIUtility.singleLineHeight, 100, 40), "Back"))
                {
                    windowId = 0; //Setting back window id
                }
                break;

            case 2:
                GUILayout.Label("Stage three | Adding objects to the scene", EditorStyles.boldLabel); //Title Text
                GUILayout.Label("Select the object you need and click to add it to the scene", EditorStyles.label); //Title Text

                GUI.Label(new Rect(40, 10 + EditorGUIUtility.singleLineHeight, 160, 40), "Adder", EditorStyles.boldLabel);
                if (GUI.Button(new Rect(10, 40 + EditorGUIUtility.singleLineHeight, 100, 100), Resources.Load("i1") as Texture))
                {
                    SpawnNewObject(adder, Vector3.zero, 2); //Spawn a new object when the button is pressed
                }

                GUI.Label(new Rect(140, 10 + EditorGUIUtility.singleLineHeight, 160, 40), "Barrels", EditorStyles.boldLabel);
                if (GUI.Button(new Rect(115, 40 + EditorGUIUtility.singleLineHeight, 100, 100), Resources.Load("i2") as Texture))
                {
                    SpawnNewObject(barrels, Vector3.zero, 2);//Spawn a new object when the button is pressed
                }

                GUI.Label(new Rect(220, 10 + EditorGUIUtility.singleLineHeight, 160, 40), "Change Direction", EditorStyles.boldLabel);
                if (GUI.Button(new Rect(220, 40 + EditorGUIUtility.singleLineHeight, 100, 100), Resources.Load("i3") as Texture))
                {
                    SpawnNewObject(changeDir, Vector3.zero, 2);//Spawn a new object when the button is pressed
                }

                GUI.Label(new Rect(350, 10 + EditorGUIUtility.singleLineHeight, 160, 40), "Cones", EditorStyles.boldLabel);
                if (GUI.Button(new Rect(325, 40 + EditorGUIUtility.singleLineHeight, 100, 100), Resources.Load("i4") as Texture))
                {
                    SpawnNewObject(cone, Vector3.zero, 2);//Spawn a new object when the button is pressed
                }

                GUI.Label(new Rect(25, 140 + EditorGUIUtility.singleLineHeight, 160, 40), "Dead Block", EditorStyles.boldLabel);
                if (GUI.Button(new Rect(10, 170 + EditorGUIUtility.singleLineHeight, 100, 100), Resources.Load("i5") as Texture))
                {
                    SpawnNewObject(deadBlock, Vector3.zero, 2);//Spawn a new object when the button is pressed
                }

                GUI.Label(new Rect(130, 140 + EditorGUIUtility.singleLineHeight, 160, 40), "Dead Cycle", EditorStyles.boldLabel);
                if (GUI.Button(new Rect(115, 170 + EditorGUIUtility.singleLineHeight, 100, 100), Resources.Load("i6") as Texture))
                {
                    SpawnNewObject(deadCycle, Vector3.zero, 2);//Spawn a new object when the button is pressed
                }

                GUI.Label(new Rect(225, 140 + EditorGUIUtility.singleLineHeight, 160, 40), "Dead Platform", EditorStyles.boldLabel);
                if (GUI.Button(new Rect(220, 170 + EditorGUIUtility.singleLineHeight, 100, 100), Resources.Load("i7") as Texture))
                {
                    SpawnNewObject(deadPlatform, Vector3.zero, 2);//Spawn a new object when the button is pressed
                }

                GUI.Label(new Rect(350, 140 + EditorGUIUtility.singleLineHeight, 160, 40), "Platform", EditorStyles.boldLabel);
                if (GUI.Button(new Rect(325, 170 + EditorGUIUtility.singleLineHeight, 100, 100), Resources.Load("i8") as Texture))
                {
                    SpawnNewObject(platform, Vector3.zero, 1); //Spawn a new object when the button is pressed
                    ChangePlatformsColor(blocksArray.blocks[0].GetComponent<MeshRenderer>().sharedMaterial); //Set the new platform to the same color as the previous one
                }

                if (GUI.Button(new Rect(10, 280 + EditorGUIUtility.singleLineHeight, 160, 40), "Rotate 90 degrees")) //Rotate the selected object 90 degrees.
                {
                    if (Selection.activeTransform.localEulerAngles.y == 0) //Either -90 or 0 degrees
                    {
                        Selection.activeTransform.localEulerAngles = new Vector3(0, -90, 0);
                        Selection.activeTransform.localPosition = new Vector3(Selection.activeTransform.localPosition.x + Selection.activeTransform.localScale.x / 2 - Selection.activeTransform.localScale.z / 2, Selection.activeTransform.localPosition.y, Selection.activeTransform.localPosition.z - Selection.activeTransform.localScale.z / 2 + Selection.activeTransform.localScale.x /2);
                    }
                    else
                    {
                        Selection.activeTransform.localEulerAngles = new Vector3(0, 0, 0);
                        Selection.activeTransform.localPosition = new Vector3(Selection.activeTransform.localPosition.x - Selection.activeTransform.localScale.x / 2 + Selection.activeTransform.localScale.z / 2, Selection.activeTransform.localPosition.y, Selection.activeTransform.localPosition.z + Selection.activeTransform.localScale.z / 2 - Selection.activeTransform.localScale.x / 2);
                    }
                }
                if (GUI.Button(new Rect(175, 280 + EditorGUIUtility.singleLineHeight, 160, 40), "Delete selected object")) //Delete the selected object
                {
                    if (UnityEditor.Selection.activeGameObject.name == "Finish") //If this is the finish line, let us spawn the finish line again
                    {
                        finishSpawned = false;
                    }
                    DestroyImmediate(UnityEditor.Selection.activeObject,false);
                    RemoveDeletedObjects(); //Check that there are no empty objects in the arrays
                }

                if (GUI.Button(new Rect(115, 430 + EditorGUIUtility.singleLineHeight, 100, 40), "Next"))
                {
                    windowId = 3; //Setting next window id     
                    RemoveDeletedObjects();
                    if (!finishSpawned) //If the finish is not spawned, spawn
                    {
                        SpawnNewObject(_finish, Vector3.zero, 3);
                        finishSpawned = true;
                    }
                }
                if (GUI.Button(new Rect(10, 430 + EditorGUIUtility.singleLineHeight, 100, 40), "Back"))
                {
                    windowId = 1; //Setting next window id
                }
                break;

            case 3:
                GUILayout.Label("Stage four | Setup finish", EditorStyles.boldLabel); //Title Text
                GUILayout.Label("The finish has been automatically added, check if it is located correctly", EditorStyles.label); //Title Text

                image_2 = Resources.Load("p2", typeof(Texture2D)) as Texture2D; //Add image
                GUILayout.Label(image_2, GUILayout.Width(400), GUILayout.Height(400));

                if (GUI.Button(new Rect(115, 430 + EditorGUIUtility.singleLineHeight, 100, 40), "Next"))
                {
                    windowId = 4; //Setting next window id
                    
                }
                if (GUI.Button(new Rect(10, 430 + EditorGUIUtility.singleLineHeight, 100, 40), "Back"))
                {
                    windowId = 2; //Setting next window id
                }
                break;
            case 4:
                GUILayout.Label("Stage Six | Adding a scene to the game", EditorStyles.boldLabel);
                GUILayout.Label("Open the settings and add the scene to Scene in Build, then close the settings", EditorStyles.label);

                image_3 = Resources.Load("p3", typeof(Texture2D)) as Texture2D; //Add image
                GUILayout.Label(image_3, GUILayout.Width(450), GUILayout.Height(300));

                if (GUILayout.Button("Open Settings", GUILayout.Width(100), GUILayout.Height(50))) //Open the scenes added to the build window
                {
                    EditorWindow.GetWindow(System.Type.GetType("UnityEditor.BuildPlayerWindow,UnityEditor"));
                }
                if (GUI.Button(new Rect(10, 430 + EditorGUIUtility.singleLineHeight, 100, 40), "Back"))
                {
                    windowId = 3; //Setting previous window id//Setting previous window id
                }
                if (GUI.Button(new Rect(115, 430 + EditorGUIUtility.singleLineHeight, 100, 40), "Next"))
                {
                    windowId = 5; //Setting new window id
                }
                break;

            case 5:
                GUILayout.Label("End!", EditorStyles.boldLabel); //Title Text 
                GUILayout.Label("Level creation is over! If you have any questions, you can go to the documentation", EditorStyles.label);
                GUILayout.Label("or write to us by mail zaampo.g @gmail.com", EditorStyles.label);

                GUI.Label(new Rect(175, 180 + EditorGUIUtility.singleLineHeight, 160, 40), "!Save the scene first", EditorStyles.boldLabel);
                if (GUI.Button(new Rect(10, 430 + EditorGUIUtility.singleLineHeight, 100, 40), "Back"))
                {
                    windowId = 4; //Setting previous window id
                }
                if (GUI.Button(new Rect(190, 210 + EditorGUIUtility.singleLineHeight, 100, 40), "Let's test it!"))
                {
                    editorClosed = true; //Setting editor window closed
                    this.Close(); //Closing editor window
                    EditorSceneManager.OpenScene("Assets/Make the road/Scenes/" + "Menu.unity"); //Opening main scene
                    GameObject.Find("Canvas").GetComponent<StartUp>().TestingMode = true; //Turn on test mode
                    GameObject.Find("Canvas").GetComponent<StartUp>().LevelIdForTest = EditorBuildSettings.scenes.Length - 1; //Setting test id = new level id
                    UnityEditor.EditorApplication.isPlaying = true; //Starting game
                }
                break;
        }

        if (GUI.changed)  //If there have been changes
        {
            if (!editorClosed) //And if the editor is open
            {
                if (instantiatePlayer == null) //If there is no script, get it
                {
                    instantiatePlayer = GameObject.Find("Main").GetComponent<InstantiatePlayer>();
                }
                EditorUtility.SetDirty(instantiatePlayer); //Save changes
                EditorUtility.SetDirty(blocksArray); //Save changes
                EditorUtility.SetDirty(othersArray); //Save changes
                EditorSceneManager.MarkSceneDirty(instantiatePlayer.gameObject.scene); //changes can be saved
            }
        }
    }

    [MenuItem("Level Editor/Open Editor")]
    public static void ObjectsPanel() //Open the level editor by clicking on the button in the menu
    {
        EditorWindow window = (LevelEditor)EditorWindow.GetWindowWithRect(typeof(LevelEditor), new Rect(0, 0, 500, 500));
    }

    [MenuItem("Level Editor/Documentation")]
    private static void Documentation()//Opening the documentation in a browser
    {
        Application.OpenURL("https://drive.google.com/file/d/1-d2snZaBWmvPLETB-AoJD1h8F5Fe-8Qm/view");
    }

    void NewScene() //Create a new scene and add standard objects
    {
        int levelId = 0;
        levelId = EditorBuildSettings.scenes.Length;

        scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);
        EditorSceneManager.SaveScene(scene, "Assets/Make the road/Scenes/" + "Level_" + levelId + ".unity", false);
        EditorSceneManager.OpenScene("Assets/Make the road/Scenes/" + "Level_" + levelId + ".unity");

        SpawnNewObject(_main, Vector3.zero, 0);
        SpawnNewObject(_light, Vector3.zero, 0);
        SpawnNewObject(_particle, new Vector3(0, 0, -9), 0);
        SpawnNewObject(_level, Vector3.zero, 0);

        //Set fog setting
        RenderSettings.fog = true;
        RenderSettings.fogColor = new Color(0.5686275f, 0.9803922f, 0.7843137f, 1);
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogStartDistance = 8;
        RenderSettings.fogEndDistance = 20;

        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientLight = new Color(0.9056604f, 0.8687036f, 0.5938057f, 0); //Adjusting the light so the scene isn't dark

        windowId = 1;

        LoadObjects();
        blocksArray.blocks.Add(blocksArray.transform.GetChild(0).gameObject);
    }
    void SpawnNewObject(GameObject newObject, Vector3 newPosition, int array) //Spawn new object, set position and add to objects array  
    { //0 - dont add to array, 1 - blocks array, 2 - others objects array, 3 - only for finish
        GameObject newGameObject = PrefabUtility.InstantiatePrefab(newObject) as GameObject;
        newGameObject.name = newObject.name;

        switch (array) //Choosing which array to add the object to
        {
            case 1: //Calculate and set the position of the object
                newGameObject.transform.SetParent(blocksArray.transform);
                newGameObject.transform.localEulerAngles = new Vector3(0, blocksArray.blocks[blocksArray.blocks.Count - 1].transform.localEulerAngles.y, 0);

                if (blocksArray.blocks[blocksArray.blocks.Count - 1].transform.localEulerAngles.y == 0)
                {
                    newPosition = new Vector3(blocksArray.blocks[blocksArray.blocks.Count - 1].transform.localPosition.x, blocksArray.blocks[blocksArray.blocks.Count - 1].transform.localPosition.y, blocksArray.blocks[blocksArray.blocks.Count - 1].transform.localScale.z / 2 + newGameObject.transform.localScale.z / 2 + blocksArray.blocks[blocksArray.blocks.Count - 1].transform.localPosition.z + 4);
                }
                else
                {
                    newPosition = new Vector3(blocksArray.blocks[blocksArray.blocks.Count - 1].transform.localPosition.x - (4 + newGameObject.transform.localScale.z / 2 + blocksArray.blocks[blocksArray.blocks.Count - 1].transform.localScale.z / 2), blocksArray.blocks[blocksArray.blocks.Count - 1].transform.localPosition.y, blocksArray.blocks[blocksArray.blocks.Count - 1].transform.localPosition.z);
                }
                blocksArray.blocks.Add(newGameObject);
                break;
            case 2: //Calculate and set the position of the object
                newGameObject.transform.SetParent(othersArray.transform);

                if (othersArray.others.Count == 0)
                {
                    newPosition = new Vector3(0, 5.05f, 4);
                }
                else
                {
                    if (othersArray.others[othersArray.others.Count - 1].transform.localEulerAngles.y == 0)
                    {
                        newPosition = new Vector3(othersArray.others[othersArray.others.Count - 1].transform.localPosition.x, othersArray.others[othersArray.others.Count - 1].transform.localPosition.y, othersArray.others[othersArray.others.Count - 1].transform.position.z + othersArray.others[othersArray.others.Count - 1].transform.localScale.z / 2 + newGameObject.transform.localScale.z / 2 + 1);
                    }
                    else
                    {
                        newGameObject.transform.localEulerAngles = new Vector3(0, othersArray.others[othersArray.others.Count - 1].transform.localEulerAngles.y, 0);
                        newPosition = new Vector3(othersArray.others[othersArray.others.Count - 1].transform.position.x - (othersArray.others[othersArray.others.Count - 1].transform.localScale.x / 2 + newGameObject.transform.localScale.x / 2 + 1), othersArray.others[othersArray.others.Count - 1].transform.localPosition.y, othersArray.others[othersArray.others.Count - 1].transform.localPosition.z);
                    }
                }
                othersArray.others.Add(newGameObject);
                break;
            case 3: //Calculate and set the position of the object
                if (blocksArray.blocks[blocksArray.blocks.Count - 1].transform.localEulerAngles.y == 0)
                {
                    newPosition = new Vector3(blocksArray.blocks[blocksArray.blocks.Count - 1].transform.position.x, blocksArray.blocks[blocksArray.blocks.Count - 1].transform.position.y, blocksArray.blocks[blocksArray.blocks.Count - 1].transform.position.z + 6);
                }
                else
                {
                    newGameObject.transform.localEulerAngles = new Vector3(0,blocksArray.blocks[blocksArray.blocks.Count - 1].transform.localEulerAngles.y, 0);
                    newPosition = new Vector3(blocksArray.blocks[blocksArray.blocks.Count - 1].transform.position.x - 6, blocksArray.blocks[blocksArray.blocks.Count - 1].transform.position.y, blocksArray.blocks[blocksArray.blocks.Count - 1].transform.position.z);
                }
                break;
        
        }

        newGameObject.transform.position = newPosition;
        UnityEditor.Selection.activeObject = newGameObject; //Selecting new object
    } 
    void RemoveDeletedObjects() //Remove deleted objects from the lists so that later there are no errors
    {

        if (blocksArray.blocks.Count != 0)
        {
            for (int i = blocksArray.blocks.Count - 1; i != 0; i--)
            {
                if (blocksArray.blocks[i] == null)
                {
                    blocksArray.blocks.Remove(blocksArray.blocks[i]);
                }
            }
        }
        if (othersArray.others.Count != 0)
        {
            for (int i = othersArray.others.Count - 1; i != 0; i--)
            {
                if (othersArray.others[i] == null)
                {
                    othersArray.others.Remove(othersArray.others[i]);
                }
            }
        }
    }
    void ChangePlatformsColor(Material mat) //Change the color of all platforms in the array
    {
        for (int i = 0; i != blocksArray.blocks.Count; i++)
        {
            mesh = blocksArray.blocks[i].GetComponent<MeshRenderer>();
            mesh.material = mat;
        }
    }
    void LoadObjects() //Loading objects
    {
        blocksArray = GameObject.Find("Level").GetComponentInChildren<BlocksArray>(); //Array of all spawned houses/b
        othersArray = GameObject.Find("Level").transform.GetChild(1).GetComponent<OthersArray>(); //Array of all spawned houses/bridge
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
}
