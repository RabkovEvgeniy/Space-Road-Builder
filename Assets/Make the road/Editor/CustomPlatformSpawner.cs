using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(PlatformSpawner))]
public class CustomPlatformSpawner : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PlatformSpawner platformSpawner = (PlatformSpawner)target;

        if (GUILayout.Button("Reset to standard")) //If the button was pressed, restore the default values
        {
            platformSpawner.platformsNumber = 25;

            platformSpawner.maximumBlocks = 50;
            platformSpawner.startBlockPosition = new Vector3(2.057612e-16f, -0.223f, -1.39f);
            platformSpawner.offsetBetweenBlocks = new Vector3(0, 0.2999997f, 0);
            platformSpawner.backpackBlockSize = new Vector3(2.5f, 0.25f, 1.5f);

            platformSpawner.forwardOffsetFirst = new Vector3(0, 0.1f, 0.6f);
            platformSpawner.forwardOffset = new Vector3(0, 0.332001f, 0.3948901f);
            platformSpawner.leftOffsetFirst = new Vector3(-0.6f, 0.1f, 0);
            platformSpawner.leftOffset = new Vector3(-0.3948901f, 0.332001f, 0);
        }
        if (GUI.changed) //Saving changes
        {
            EditorUtility.SetDirty(platformSpawner);
            EditorSceneManager.MarkSceneDirty(platformSpawner.gameObject.scene);
        }
    }
}
