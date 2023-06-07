using System.Collections;
using Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneLoader : Singleton<SceneLoader>
{
    private AsyncOperation _nextSceneLoadOperation;
    

    private void Start()
    {
        UnityEngine.SceneManagement.SceneManager.activeSceneChanged += ActiveScenesChanged;
        LoadFirstScene();
    }

    private void ActiveScenesChanged(Scene current, Scene next) => Debug.Log("Active scene has been changed: " + current.name + "-->" + next.name);

    private void OnDestroy()
    {

    }


    private void LoadFirstScene()
    {
        LoadScene(Scenes.LogoScene);
    }

    public void LoadScene(Scenes sceneToLoad)
    {
        StartCoroutine(LoadSceneRoutine(sceneToLoad));
    }

    private IEnumerator LoadSceneRoutine(Scenes sceneName)
    {
        //if there are more scene than loading scene, that means there is a scene need to unload.
        if (SceneManager.sceneCount > 1)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects); //unload active scene
        }
        _nextSceneLoadOperation = SceneManager.LoadSceneAsync((int)sceneName, LoadSceneMode.Additive);
        while (!_nextSceneLoadOperation.isDone)
        {
            /*UpdateProgressUI(_nextSceneLoadOperation.progress);*/
            yield return null;
        }
        //Set active newly loaded scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)sceneName));
        Resources.UnloadUnusedAssets();
        yield break;
    }
    

    public enum Scenes
    {
        LoadingScene = 0,
        LogoScene = 1,
        MainMenuScene = 2,
        GameScene = 3
    }
}
