using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{ 

    private GameMaster gm;


    // Start is called before the first frame update
    void Start(){

    gm = GameObject.FindGameObjectWithTag("gm").GetComponent<GameMaster>();
    transform.position = gm.lastCheckPointPos;

    }

    // Update is called once per frame
    void Update(){
    if (Input.GetKeyDown(KeyCode.K)){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spike")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}