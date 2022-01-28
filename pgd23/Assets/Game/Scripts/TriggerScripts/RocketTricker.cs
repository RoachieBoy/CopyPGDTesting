using Game.Scripts.GameObjects.Rockets;
using UnityEngine;

namespace Game.Scripts.TriggerScripts
{
    public class RocketTricker : MonoBehaviour
    {
        [SerializeField] PlainRocket[] rockets;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.transform.tag == "Player")
            {
                for (int i = 0; i < rockets.Length; i++)
                {
                    rockets[i].activate = true;
                }
            }
        }
    }
}
