using UnityEngine;

namespace Main
{
    public class Paddle : MonoBehaviour
    {
        [field: SerializeField] private float Speed { get; set; } = 2.0f;
        [field: SerializeField] private float MaxMovement { get; set; } = 2.0f;

        private void Update()
        {
            var pos = transform.position;
            pos.x += Input.GetAxis("Horizontal") * Speed * Time.deltaTime;

            if (pos.x > MaxMovement)
            {
                pos.x = MaxMovement;
            }

            if (pos.x < -MaxMovement)
            {
                pos.x = -MaxMovement;
            }

            transform.position = pos;
        }
    }
}