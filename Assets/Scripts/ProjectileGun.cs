using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prime31 {
    public class ProjectileGun : MonoBehaviour
    {
        public float projectionScale;
        public float projectionHeight;

        public float timeToLand = .3f; 
        private Vector3 targetPos;
        private Vector3 directionToPlayer; 
        private Vector3 gunPosVelocity;
        public Transform targetPicture;
        float xVelocity;
        float yVelocity; 
        Player player;
        bool init = false;
        bool projected = false;

        // Start is called before the first frame update
        void Start()
        {

            player = GetComponent<GunController>().player.GetComponent<Player>();
        }

        // Update is called once per frame
        void Update()
        {

            



            if (Input.GetMouseButtonDown(1) && !projected)                
            {
                directionToPlayer = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
                Vector3 difVec = directionToPlayer;
                targetPos = new Vector3(player.transform.position.x + projectionScale * directionToPlayer.x, player.transform.position.y + projectionScale * directionToPlayer.y);
                Vector3 tempTarget = targetPos;
                //targetPos = new Vector3(projectionScale * targetPos.x, projectionScale * targetPos.y);

                float xdistance = targetPos.x - transform.position.x;
                float ydistance = targetPos.y - transform.position.y;
                float throwAngle = Mathf.Atan((ydistance + 4.90f * Mathf.Pow(timeToLand, 2)) / xdistance);
                float totalVelo = xdistance / (Mathf.Cos(throwAngle) * timeToLand);

                float xVelo, yVelo;
                xVelo = totalVelo * Mathf.Cos(throwAngle);

                xVelocity = xVelo; 
                yVelo = totalVelo * Mathf.Sin(throwAngle);

                //Debug.Log("Angle between ground and gun: " + Vector2.Angle(player.gameObject.transform.position.normalized, transform.position.normalized));
                //gunPosVelocity = new Vector3(player.transform.position.x - transform.position.x + ((player.transform.position.x - transform.position.x < 0) ? -projectionSpeed : projectionSpeed), player.transform.position.y - transform.position.y + projectionSpeed);
                //gunPosVelocity = (player.transform.position - gunPosVelocity);
                //gunPosVelocity = new Vector3(projectionSpeed * gunPosVelocity.x, projectionSpeed * gunPosVelocity.y);

                projected = true;
                player.proj = true; 
                targetPicture.position = targetPos;

                player.velocity = new Vector3(xVelo, yVelo);


                /*
                Debug.Log("gun pos " + transform.position + " player veloc " + player.jumpVelocity + " difference veloc " + gunPosVelocity);
                projected = true;
                player.controller.Project(ref player.velocity, projectionSpeed * (gunPosVelocity), true, ref projected);
                */
            }
            else if(projected && !player.controller.isGrounded)
            {
                player.velocity.x = xVelocity;
                if(player.transform.position.x >= targetPos.x || player.transform.position.y >= targetPos.y)
                {
                    //projected = false;
                    //player.proj = false; 
                }
                /*TODO: if player reaches target position, make projected false*/
            }
            else if (player.controller.isGrounded)
            {
                projected = false;
                player.proj = false; 
                /*
                Debug.Log("hi");
                player.controller.Project(ref player.velocity, gunPosVelocity, false, ref projected);
                if (player.controller.isGrounded)
                {
                    projected = false;
                }
                */
            }
            
            //Debug.Log("Gun is " + (player.transform.position - gunDirection));
    }

        void ProjectPlayer()
        {

        }
    }
}
