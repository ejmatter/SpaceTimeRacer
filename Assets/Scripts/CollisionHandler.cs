using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
   // PARAMETERS - for tuning, typically set in editor
   // CACHE - e.g. references for readability or speed
   // STATE - private instance (member) variables
    [SerializeField] float delyNextLevel = 2f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip victorySound;
   
    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return;}

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Collided with Friendly Object!");
                break;
            case "Finish":
                StartVictorySequence();
                break;
            //case "Fuel":
              //Debug.Log("Grabed some Fuel!");
                //break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartVictorySequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(victorySound);
        // to-do: add particle effects
        GetComponent<Movement>().enabled = false;
        Invoke ("NextLevel", delyNextLevel);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        // to-do: add particle effects
        GetComponent<Movement>().enabled = false;
        Invoke ("ReloadLevel", delyNextLevel);
    }

    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    } 
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }


}
