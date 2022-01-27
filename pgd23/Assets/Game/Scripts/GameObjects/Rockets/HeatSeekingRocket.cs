using Game.Scripts.Core_LevelManagement.EventManagement;
using UnityEngine;

public class HeatSeekingRocket : PlainRocket
{
    [SerializeField] private float rotateSpeed;

    private Vector2 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        projectileTrail = (GameObject)Instantiate(rocketTrail, enginePosition.transform.position, transform.rotation);
    }

    /// <summary>
    /// 
    /// </summary>
    public override void FixedUpdate()
    {
        if (!activate)
            return;

        direction = target.position - transform.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross((Vector3)direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * this.speed;
    }

    public new void Update()
    {
        EngineUpdate();
        RocketEngineParticleEffect();
    }
}
