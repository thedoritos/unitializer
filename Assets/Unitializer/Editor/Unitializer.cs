using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class Unitializer
{
    private static Scene current;

    private static bool wasPlaying = false;

    static Unitializer()
    {
        EditorApplication.playmodeStateChanged += () =>
        {
            var isPlaying = EditorApplication.isPlaying;

            if (wasPlaying == false)
            {
                if (isPlaying == false)
                    OnBeforeLaunch();
                else
                    OnAfterLaunch();
            } else
            {
                if (isPlaying)
                    OnBeforeQuit();
                else
                    OnAfterQuit();
            }

            wasPlaying = isPlaying;
        };
//        EditorSceneManager.LoadScene(0);
//        EditorSceneManager.OpenScene("Assets/Scenes/AScene.unity");
//        EditorSceneManager.UnloadScene(1);
//        EditorSceneManager.UnloadScene("BScene.unity");
//        EditorSceneManager.CloseScene(current, true);
    }

    private static void OnBeforeLaunch()
    {
        current = EditorSceneManager.GetActiveScene();
        EditorSceneManager.OpenScene("Assets/Scenes/AScene.unity");
    }

    private static void OnAfterLaunch()
    {
        EditorSceneManager.UnloadScene(current.name);
    }

    private static void OnBeforeQuit()
    {
    }

    private static void OnAfterQuit()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/BScene.unity");
    }
}
