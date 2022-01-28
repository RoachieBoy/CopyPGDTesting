using Game.Scripts.Core_LevelManagement.EventManagement;
using UnityEngine;

namespace Game.Scripts.GameObjects.Rockets
{
    public class HeatSeekingRocket : PlainRocket
    {
        [SerializeField] private float rotateSpeed;

        private Vector2 _direction;

        private void Start()
        {
            Rb = GetComponent<Rigidbody2D>();
            EventManager.Instance.onTriggerRocket += Activate;
        }

        private void OnDisable()
        {
            EventManager.Instance.onTriggerRocket -= Activate;
        }

        public void Activate()
        {
            activate = true;
            StartCoroutine(LifeTimeRoutine(lifeTime));
            ProjectileTrail = Instantiate(rocketTrail, enginePosition.transform.position, transform.rotation);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void FixedUpdate()
        {
            if (!activate)
                return;

            _direction = target.position - transform.position;
            _direction.Normalize();

            var rotateAmount = Vector3.Cross(_direction, transform.up).z;

            Rb.angularVelocity = -rotateAmount * rotateSpeed;
            Rb.velocity = transform.up * speed;
        }

        public new void Update()
        {
            EngineUpdate();
            RocketEngineParticleEffect();
        }
    }
}
