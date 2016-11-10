using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class Unitializer
{
    private static readonly string HasInitScene = "unitializer_has_first_scene";
    private static readonly string InitScenePath = "unitializer_first_scene_path";
    private static readonly string EditingSceneName = "unitializer_editing_scene_name";
    private static readonly string EditingScenePath = "unitializer_editing_scene_path";

    private static bool wasPlaying = false;

    static Unitializer()
    {
        EditorApplication.playmodeStateChanged -= Unitializer.OnPlaymodeStateChanged;
        EditorApplication.playmodeStateChanged += Unitializer.OnPlaymodeStateChanged;
    }

    private static void OnPlaymodeStateChanged()
    {
        var isPlaying = EditorApplication.isPlaying;

        if (wasPlaying == false)
        {
            if (isPlaying == false)
            {
                OnBeforeLaunch();
            } else
            {
                OnAfterLaunch();
            }
        } else
        {
            if (isPlaying)
            {
                OnBeforeQuit();
            } else
            {
                OnAfterQuit();
            }
        }

        wasPlaying = isPlaying;
    }

    private static void OnBeforeLaunch()
    {
        var scenes = EditorBuildSettings.scenes;
        var hasInitScene = scenes.Length > 0;

        EditorPrefs.SetBool(HasInitScene, hasInitScene);

        if (hasInitScene == false)
        {
            Debug.Log("[Unitializer] Can't find scenes in Build Settings.");
            return;
        }

        var initScene = scenes [0];
        var editingScene = EditorSceneManager.GetActiveScene();

        EditorPrefs.SetString(InitScenePath, initScene.path);
        EditorPrefs.SetString(EditingSceneName, editingScene.name);
        EditorPrefs.SetString(EditingScenePath, editingScene.path);

        EditorSceneManager.OpenScene(initScene.path);
    }

    private static void OnAfterLaunch()
    {
        var hasInitScene = EditorPrefs.GetBool(HasInitScene);
        if (hasInitScene == false)
        {
            return;
        }

        var editingSceneName = EditorPrefs.GetString(EditingSceneName);

        EditorSceneManager.UnloadScene(editingSceneName);
    }

    private static void OnBeforeQuit()
    {
        
    }

    private static void OnAfterQuit()
    {
        var hasInitScene = EditorPrefs.GetBool(HasInitScene);
        if (hasInitScene == false)
        {
            Debug.Log("[Unitializer] Could not find scenes in Build Settings.");
            return;
        }

        var editingScenePath = EditorPrefs.GetString(EditingScenePath);

        EditorSceneManager.OpenScene(editingScenePath);

        EditorPrefs.DeleteKey(HasInitScene);
        EditorPrefs.DeleteKey(InitScenePath);
        EditorPrefs.DeleteKey(EditingSceneName);
        EditorPrefs.DeleteKey(EditingScenePath);
    }
}
