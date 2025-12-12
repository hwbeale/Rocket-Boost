using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int nextlevel;
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
            Debug.Log("Friendly");
            break;

            case "Finish":
            nextlevel ++;
            NextLevel();
            break;

            default:
            ReloadLevel();
            break;
        }

        void ReloadLevel()
        {
            int CurrentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(CurrentScene);
        }
        void NextLevel()
        {
            int CurrentScene = SceneManager.GetActiveScene().buildIndex;
            int nextScene = CurrentScene + 1;

            if (nextScene == SceneManager.sceneCountInBuildSettings)
            {
                nextScene = 0;
            }

            SceneManager.LoadScene(nextScene);
        }
    }
}
