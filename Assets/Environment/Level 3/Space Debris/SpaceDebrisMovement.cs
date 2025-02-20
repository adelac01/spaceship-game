using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceDebrisMovement : MonoBehaviour
{


    bool isAlive = true;
    
    //Delay for the debris to respawn
    [SerializeField] float debrisRespawnTime = 1f;

    //Data values (Speed)
    [SerializeField] [Range(-50f,50f)] float speedFactor = 5f;
    [SerializeField] [Range(-50f,50f)] float transformX = 5f;
    [SerializeField] [Range(-50f,50f)] float transformY = 5f;
    [SerializeField] [Range(0f,10f)] float rotationFactor = 10f;
    [SerializeField] [Range(0f,10f)] float rotationX = 10f;
    [SerializeField] [Range(0f,10f)] float rotationY = 10f;
    [SerializeField] [Range(0f,10f)] float rotationZ = 10f;

    //Data values (spawn)
    [SerializeField] float xSpawn;
    [SerializeField] float ySpawn;

    //Manually set the boundary limit for the scene
    [SerializeField] float boundaryLimitUp = -40f;
    [SerializeField] float boundaryLimitRight = -40f;
    [SerializeField] float boundaryLimitDown = -40f;
    [SerializeField] float boundaryLimitLeft = -40f;


    void Start(){
        xSpawn = gameObject.transform.position.x;
        ySpawn = gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update(){

        //A check so debris only spawns once
        if(!isAlive){return;}

        //Start the debris respawning coroutine if the game object exits outside of the defined border
        if(gameObject.transform.position.y >= boundaryLimitUp || gameObject.transform.position.x >= boundaryLimitRight || gameObject.transform.position.y <= boundaryLimitDown || gameObject.transform.position.x <= boundaryLimitLeft){
            StartCoroutine(debrisRespawn(debrisRespawnTime));
        }

        //Causes movement from one end to the other end on the screen
        movement();
    }

    void movement(){

        float X = Time.deltaTime*rotationFactor*rotationX;
        float Y = Time.deltaTime*rotationFactor*rotationY;
        float Z = Time.deltaTime*rotationFactor*rotationZ;

        transform.Translate(Time.deltaTime*speedFactor*transformX, Time.deltaTime*speedFactor*transformY, 0f);
        transform.Rotate(X,Y,Z);
    }


    private IEnumerator debrisRespawn(float delay){
        isAlive = false;
        Instantiate(gameObject,new Vector3(xSpawn,ySpawn,0f),Quaternion.identity);
        Destroy(gameObject);
        yield return new WaitForSeconds(delay);
        isAlive = true;
    }
}
