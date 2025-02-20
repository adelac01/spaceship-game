using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{

    [SerializeField] float thrustingSpeed = 10f;
    [SerializeField] float turningSpeed = 10f;

    Rigidbody rb;

    void Start() {

        // Access's unity's internal collision system
        rb = GetComponent<Rigidbody>();

    }

    void Update() {
        thrustingMovement();
        rotationMovement();
        reloadLevel();
        chooseLevel();

        if (Input.GetKey("escape")){
            Application.Quit();
        }
    }

    void thrustingMovement() {
        if(Input.GetKey(KeyCode.Space)) {
            Debug.Log("Movement:Thrust");
            rb.AddRelativeForce(Vector3.up * thrustingSpeed * Time.deltaTime);
        }
    }

    // A to turn left, D to turn right
    void rotationMovement() {
        if(Input.GetKey(KeyCode.A)) {
            rotate(turningSpeed);
        } else if(Input.GetKey(KeyCode.D)) {
            rotate(-turningSpeed);           
        }
    }

    void rotate(float turningConstant) {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward*turningConstant*Time.deltaTime);
        rb.freezeRotation = false;
    }

    // Bind r to reload the level
    void reloadLevel() {
        if(Input.GetKey(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // Switch quickly to other levels
    void chooseLevel() {
        if(Input.GetKey(KeyCode.Alpha1)) {
            SceneManager.LoadScene(0);
        } else if(Input.GetKey(KeyCode.Alpha2)) {
            SceneManager.LoadScene(1);
        } else if(Input.GetKey(KeyCode.Alpha3)) {
            SceneManager.LoadScene(2);
        } else if(Input.GetKey(KeyCode.Alpha4)) {
            SceneManager.LoadScene(3);
        } else if(Input.GetKey(KeyCode.Alpha0)) {
            SceneManager.LoadScene(4);
        }
    }

}

    
