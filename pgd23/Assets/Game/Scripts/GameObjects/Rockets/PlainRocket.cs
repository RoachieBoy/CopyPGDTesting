using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Game.Scripts.GameObjects.Rockets.RocketDirections.DirectionsRocket;

namespace Game.Scripts.GameObjects.Rockets
{
    public class PlainRocket : MonoBehaviour
    {
        [SerializeField] private RocketDirections.DirectionsRocket cs;
        [SerializeField] protected Transform target, enginePosition;
        [SerializeField] protected float speed;
        [SerializeField] private ParticleSystem rocketExplosion;
        [SerializeField] protected GameObject rocketTrail;
        [SerializeField] protected float lifeTime;
        [SerializeField] private float distanceToPlayer;
        
        private bool _isUp, _isUpRight, _isRight, _isRightDown, _isDown, _isDownLeft, _isLeft, _isLeftUp, _isNoDirection;
        
        public bool activate;
        
        protected GameObject ProjectileTrail;
        protected Rigidbody2D Rb;

        private void Start()
        {
            Rb = GetComponent<Rigidbody2D>();
        }


        /// <summary>
        /// 
        /// </summary>
        public virtual void Update()
        {
            switch (cs)
            {
                case Up:
                    _isUp = true;
                    break;
                case UpRight:
                    _isUpRight = true;
                    break;
                case Right:
                    _isRight = true;
                    break;
                case RightDown:
                    _isRightDown = true;
                    break;
                case Down:
                    _isDown = true;
                    break;
                case DownLeft:
                    _isDownLeft = true;
                    break;
                case Left:
                    _isLeft = true;
                    break;
                case LeftUp:
                    _isLeftUp = true;
                    break;
                case NoDirection:
                    _isNoDirection = true;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            EngineUpdate();
            RocketEngineParticleEffect();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void FixedUpdate()
        {
            if (_isUp)
            {
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.z = 0;  //this number is the degree of rotation around Z Axis
                transform.rotation = Quaternion.Euler(rotationVector);
            }
            if (_isUpRight)
            {
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.z = 315;
                transform.rotation = Quaternion.Euler(rotationVector);
            }
            if (_isRight)
            {
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.z = 270;
                transform.rotation = Quaternion.Euler(rotationVector);
            }
            if (_isRightDown)
            {
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.z = 225;
                transform.rotation = Quaternion.Euler(rotationVector);
            }
            if (_isDown)
            {
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.z = 180;
                transform.rotation = Quaternion.Euler(rotationVector);
            }
            if (_isDownLeft)
            {
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.z = 135;
                transform.rotation = Quaternion.Euler(rotationVector);
            }
            if (_isLeft)
            {
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.z = 90;
                transform.rotation = Quaternion.Euler(rotationVector);
            }
            if (_isLeftUp)
            {
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.z = 45;
                transform.rotation = Quaternion.Euler(rotationVector);
            }

            if (!activate)
                return;

            if (_isUp)
            {
                Rb.velocity = Rb.transform.up * speed;
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.z = 0;  //this number is the degree of rotation around Z Axis
                transform.rotation = Quaternion.Euler(rotationVector);
            }
            if (_isUpRight)
            {
                Rb.velocity = new Vector2(1, 1) * speed;
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.z = 315;
                transform.rotation = Quaternion.Euler(rotationVector);
            }
            if (_isRight)
            {
                Rb.velocity = new Vector2(1, 0) * speed;
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.z = 270;
                transform.rotation = Quaternion.Euler(rotationVector);
            }
            if (_isRightDown)
            {
                Rb.velocity = new Vector2(1, -1) * speed;
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.z = 225;
                transform.rotation = Quaternion.Euler(rotationVector);
            }
            if (_isDown)
            {
                Rb.velocity = new Vector2(0, -1) * speed;
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.z = 180;
                transform.rotation = Quaternion.Euler(rotationVector);
            }
            if (_isDownLeft)
            {
                Rb.velocity = new Vector2(-1, -1) * speed;
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.z = 135;
                transform.rotation = Quaternion.Euler(rotationVector);
            }
            if (_isLeft)
            {
                Rb.velocity = new Vector2(-1, 0) * speed;
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.z = 90;
                transform.rotation = Quaternion.Euler(rotationVector);
            }
            if (_isLeftUp)
            {
                Rb.velocity = new Vector2(-1, 1) * speed;
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.z = 45;
                transform.rotation = Quaternion.Euler(rotationVector);
            }
            if (_isNoDirection) Rb.velocity = Vector2.zero;
        }


        public virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Obstacle"))
            {
                Explosion();
            }
        }

        protected IEnumerator LifeTimeRoutine(float lifeTime)
        {
            yield return new WaitForSeconds(lifeTime);
            Explosion();
        }

        public virtual void Explosion()
        {
            Instantiate(rocketExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(ProjectileTrail);
        }

        protected void EngineUpdate()
        {
            if (activate)
                return;

            if (Vector3.Distance(transform.position, target.position) < distanceToPlayer)
            {

                activate = true;
                StartCoroutine(LifeTimeRoutine(lifeTime));
                ProjectileTrail = Instantiate(rocketTrail, enginePosition.transform.position, transform.rotation);
            }
        }
        public void RocketEngineParticleEffect()
        {
            if (ProjectileTrail == null)
            {
                return;
            }
            else
            {
                ProjectileTrail.transform.position = enginePosition.transform.position;
            }
        }
    }
}
