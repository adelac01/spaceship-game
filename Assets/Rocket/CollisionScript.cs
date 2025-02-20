using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionScript : MonoBehaviour
{

    int sceneIndex = 0;

    bool isRespawning = false;
    bool isTransitioning = false;
    bool avaliableFlashlight = false;

    float xSpawn;
    float ySpawn;
    float zSpawn = 0f;

    Vector3 startPlatform;
    Vector3 checkpointPlatform;

    GameObject spotlight;

    [SerializeField] float levelLoadDelay = 3f;
    [SerializeField] float respawnDelay = 2f;

    // Called at the beginning of each scene thats been loaded
    private void Start() {

        GameObject checkpoint;

        startPlatform = GameObject.FindGameObjectWithTag("Start").transform.position;
        xSpawn = startPlatform.x;
        ySpawn = startPlatform.y + 0.826f;

        checkpoint = GameObject.FindGameObjectWithTag("Checkpoint");
        if(checkpoint != null) {
            checkpointPlatform = checkpoint.transform.position;
        }

        spotlight = GameObject.Find("Spot Light");
        if(spotlight == null) {
            avaliableFlashlight = false;
        } else {
            avaliableFlashlight = true;
        }
        
    }

    private void OnCollisionEnter(Collision collision) {

        // Log into the console the tag of the object the rocketship comes into contact with (Debugging purposes)
        Debug.Log($"Collision:{collision.gameObject.tag}");

        // A check if respawning is occuring such that the event happens only once
        if(isRespawning || isTransitioning) return;

        // What to do based on different collision cases
        switch(collision.gameObject.tag){
            case "Start":
                //Do nothing
                break;
            case "Checkpoint":
                //Set spawn at checkpoint
                setSpawn();
                break;
            case "End Platform":
                //Load next scene or level
                StartCoroutine(sceneLoad());
                break;
            default:
                //Start the respawn sequence
                StartCoroutine(respawn());
                Debug.Log(ySpawn);
                break;
        }
    }

    // Load the next level after beating it
    private IEnumerator sceneLoad() {

        sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        isTransitioning = true;
        Debug.Log(sceneIndex);

        GetComponent<Movement>().enabled = false;
        yield return new WaitForSeconds(levelLoadDelay);

        // If all levels are beaten, just loop back to the beginning
        if(sceneIndex >= 4) {
            sceneIndex = 0;
        }

        isTransitioning = false;
        GetComponent<Movement>().enabled = true;

        SceneManager.LoadScene(sceneIndex);
    }  

    // Deactivate visual elements of the rocket, then activate again once respawned
    private IEnumerator respawn() {
        foreach(Renderer r in GetComponentsInChildren<Renderer>()) {
            r.enabled = false;
        }

        if(avaliableFlashlight == true){ 
            spotlight.SetActive(false); 
        }

        GetComponent<Movement>().enabled = false;
        isRespawning = true;
        yield return new WaitForSeconds(respawnDelay);
        transform.position = new Vector3(xSpawn,ySpawn,zSpawn);
        transform.rotation = Quaternion.identity;

        foreach(Renderer r in GetComponentsInChildren<Renderer>()) {
            r.enabled = true;
        }

        if(avaliableFlashlight == true) {
            spotlight.SetActive(true);
        }

        GetComponent<Movement>().enabled = true;
        isRespawning = false;
    }

    private void setSpawn() {
        xSpawn = checkpointPlatform.x;
        ySpawn = checkpointPlatform.y + 0.826f;
    }

}
