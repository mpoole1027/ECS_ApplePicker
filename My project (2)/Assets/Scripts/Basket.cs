using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public bool isHardMode = false;
    public ScoreCounter scoreCounter;
    // Start is called before the first frame update
    void Start()
    {
        GameObject scoreGO = GameObject.Find("ScoreCounter");

        scoreCounter = scoreGO.GetComponent<ScoreCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 pos = this.transform.position;
        if( isHardMode)
        {
            pos.x = -mousePos3D.x;
        }

        else
        {
            pos.x = mousePos3D.x;
        }
        this.transform.position = pos;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidingWith = collision.gameObject;

        if (collidingWith.CompareTag("Apple"))
        {
            Destroy(collidingWith);

            scoreCounter.score += 100;
            HighScore.TRY_SET_HIGH_SCORE(scoreCounter.score);
        }
    }
}
