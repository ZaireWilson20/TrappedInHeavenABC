using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prime31 {
    public class ProjectileGun : MonoBehaviour
    {
        public float projectionSpeed;
        private Vector3 gunPosVelocity;
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

            Debug.Log("Angle between ground and gun: " + Vector2.Angle(player.gameObject.transform.position.normalized, transform.position.normalized));
            gunPosVelocity = new Vector3(player.transform.position.x - transform.position.x + ((player.transform.position.x - transform.position.x < 0) ? -projectionSpeed : projectionSpeed), player.transform.position.y - transform.position.y + projectionSpeed);
            //gunPosVelocity = (player.transform.position - gunPosVelocity);
            //gunPosVelocity = new Vector3(projectionSpeed * gunPosVelocity.x, projectionSpeed * gunPosVelocity.y);



            if (Input.GetMouseButtonDown(1) && !projected)
            {
                Debug.Log("gun pos " + transform.position + " player veloc " + player.jumpVelocity + " difference veloc " + gunPosVelocity);
                projected = true;
                player.controller.Project(ref player.velocity, projectionSpeed * (gunPosVelocity), true, ref projected);
            }
            else if (projected)
            {
                Debug.Log("hi");
                player.controller.Project(ref player.velocity, gunPosVelocity, false, ref projected);
                if (player.controller.isGrounded)
                {
                    projected = false;
                }
            }
            
            //Debug.Log("Gun is " + (player.transform.position - gunDirection));
    }

        void ProjectPlayer()
        {

        }
    }
}
