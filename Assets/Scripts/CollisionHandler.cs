using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delyNextLevel = 2f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip victory;
   
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
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
        audioSource.PlayOneShot(victory);
        // to-do: add SFX upon crash
        // to-do: add particle effects
        GetComponent<Movement>().enabled = false;
        Invoke ("NextLevel", delyNextLevel);
    }

    void StartCrashSequence()
    {
        audioSource.PlayOneShot(crash);
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
