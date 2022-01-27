using Game.Scripts.GameObjects.Obstacles;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Game.Scripts.GameObjects.Obstacles.RocketDirections.DirectionsRocket;

public class PlainRocket : MonoBehaviour
{
    [SerializeField] private RocketDirections.DirectionsRocket cs;
    [SerializeField] protected Transform target, enginePosition;
    [SerializeField] protected float speed;
    [SerializeField] ParticleSystem rocketExplosion;
    [SerializeField] protected GameObject rocketTrail;
    [SerializeField] protected float lifeTime;
    private bool isUp, isUpRight, isRight, isRightDown, isDown, isDownLeft, isLeft, isLeftUp, isNoDirection;
    public bool activate = false;
    protected GameObject projectileTrail;

    [SerializeField] private float distanceToPlayer;

    protected Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    /// <summary>
    /// 
    /// </summary>
    public virtual void Update()
    {
        switch (cs)
        {
            case Up:
                isUp = true;
                break;
            case UpRight:
                isUpRight = true;
                break;
            case Right:
                isRight = true;
                break;
            case RightDown:
                isRightDown = true;
                break;
            case Down:
                isDown = true;
                break;
            case DownLeft:
                isDownLeft = true;
                break;
            case Left:
                isLeft = true;
                break;
            case LeftUp:
                isLeftUp = true;
                break;
            case NoDirection:
                isNoDirection = true;
                break;

        }
        EngineUpdate();
        RocketEngineParticleEffect();
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual void FixedUpdate()
    {
        if (isUp)
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.z = 0;  //this number is the degree of rotation around Z Axis
            transform.rotation = Quaternion.Euler(rotationVector);
        }
        if (isUpRight)
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.z = 315;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
        if (isRight)
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.z = 270;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
        if (isRightDown)
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.z = 225;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
        if (isDown)
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.z = 180;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
        if (isDownLeft)
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.z = 135;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
        if (isLeft)
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.z = 90;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
        if (isLeftUp)
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.z = 45;
            transform.rotation = Quaternion.Euler(rotationVector);
        }

        if (!activate)
            return;

        if (isUp)
        {
            rb.velocity = rb.transform.up * speed;
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.z = 0;  //this number is the degree of rotation around Z Axis
            transform.rotation = Quaternion.Euler(rotationVector);
        }
        if (isUpRight)
        {
            rb.velocity = new Vector2(1, 1) * speed;
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.z = 315;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
        if (isRight)
        {
            rb.velocity = new Vector2(1, 0) * speed;
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.z = 270;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
        if (isRightDown)
        {
            rb.velocity = new Vector2(1, -1) * speed;
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.z = 225;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
        if (isDown)
        {
            rb.velocity = new Vector2(0, -1) * speed;
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.z = 180;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
        if (isDownLeft)
        {
            rb.velocity = new Vector2(-1, -1) * speed;
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.z = 135;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
        if (isLeft)
        {
            rb.velocity = new Vector2(-1, 0) * speed;
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.z = 90;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
        if (isLeftUp)
        {
            rb.velocity = new Vector2(-1, 1) * speed;
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.z = 45;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
        if (isNoDirection) rb.velocity = Vector2.zero;
    }


    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Obstacle")
        {
            Explosion();
        }
    }

    public virtual IEnumerator LifeTimeRoutine(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Explosion();
    }

    public virtual void Explosion()
    {
        Instantiate(rocketExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(projectileTrail);
    }

    public virtual void EngineUpdate()
    {
        if (activate)
            return;

        if (Vector3.Distance(transform.position, target.position) < distanceToPlayer)
        {

            activate = true;
            StartCoroutine(LifeTimeRoutine(lifeTime));
            projectileTrail = (GameObject)Instantiate(rocketTrail, enginePosition.transform.position, transform.rotation);
        }
    }
    public virtual void RocketEngineParticleEffect()
    {
        if (projectileTrail == null)
        {
            return;
        }
        else
        {
            projectileTrail.transform.position = enginePosition.transform.position;
        }
    }
}
