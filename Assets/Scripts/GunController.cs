using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject player;
    private Vector3 v3Pos;
    private float angle;
    private float distance = 30f;
    private float prevX;
    private float prevY;
    private bool firstRot = true; 
    private Player playerRef;


    private void Start()
    {
        playerRef = player.GetComponent<Player>();    
    }

    private void Update()
    {

        RotateGun(); 

    }

    private void RotateGun()
    {
        v3Pos = Input.mousePosition;
        v3Pos.z = (player.transform.position.z - Camera.main.transform.position.z);
        v3Pos = Camera.main.ScreenToWorldPoint(v3Pos);
        v3Pos = v3Pos - player.transform.position;
        angle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;
        if (angle < 0.0f) angle += 360.0f;
        transform.localEulerAngles = new Vector3(0, 0, angle);

        float xPos = Mathf.Cos(Mathf.Deg2Rad * angle) * distance;
        float yPos = Mathf.Sin(Mathf.Deg2Rad * angle) * distance;
        if(firstRot)
        {
            prevX = player.transform.position.x + xPos;
            prevY = player.transform.position.y + yPos;
        }

       
        

        transform.localPosition = new Vector3(Mathf.Lerp(prevX, player.transform.position.x + xPos , .6f), Mathf.Lerp(prevY, player.transform.position.y + yPos, .6f), 0);
        prevX = player.transform.position.x + xPos + playerRef.velocity.x;
        prevY = player.transform.position.y + yPos + playerRef.velocity.y;
    }

    public void ShootProjectile()
    {

    }
}
