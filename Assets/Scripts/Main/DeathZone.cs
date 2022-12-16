using UnityEngine;

namespace Main
{
    public class DeathZone : MonoBehaviour
    {
        [field: SerializeField] private MainManager MainManagerScript { get; set; }

        private void OnCollisionEnter(Collision other)
        {
            Destroy(other.gameObject);
            MainManagerScript.GameOver();
        }
    }
}