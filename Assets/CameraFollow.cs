using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Player playerRef; 
    // Start is called before the first frame update
    void Start()
    {
        playerRef = gameObject.GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        float newX = Mathf.Lerp(transform.position.x, playerRef.gameObject.transform.position.x, .4f);
        float newY = Mathf.Lerp(transform.position.y, playerRef.gameObject.transform.position.y, .4f);
        gameObject.transform.position = new Vector3(newX, newY, -367);
    }
}
