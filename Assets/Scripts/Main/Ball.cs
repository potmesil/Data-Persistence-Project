using UnityEngine;

namespace Main
{
    public class Ball : MonoBehaviour
    {
        private Rigidbody Rb { get; set; }

        private void Start()
        {
            Rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionExit(Collision other)
        {
            var velocity = Rb.velocity;

            //after a collision we accelerate a bit
            velocity += velocity.normalized * 0.01f;

            //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
            if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f)
            {
                velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
            }

            //max velocity
            if (velocity.magnitude > 3.0f)
            {
                velocity = velocity.normalized * 3.0f;
            }

            Rb.velocity = velocity;
        }
    }
}