using System.Collections.Generic;
using Game.Scripts.AbilitiesSystem.AbilityHandler;
using UnityEngine;

namespace Game.Scripts.GameObjects.Lasers
{
    public class Laser : MonoBehaviour
    {
        [Header("Laser Objects")] [SerializeField]
        private GameObject laserEmitter;

        [SerializeField] private GameObject _particleEmitter;
        [SerializeField] private Material laserMaterial;

        [Header("Laser Settings")] [SerializeField, Range(1, 4)]
        private int amountOfLasers = 1;

        [SerializeField] private float laserLength = 3;
        [SerializeField] private float rotationSpeed = 1;

        private List<LineRenderer> lasers = new List<LineRenderer>();
        private List<LaserParticles> _particles = new List<LaserParticles>();


        private void Start()
        {
            // Initialize all the needed lasers
            for (int i = 0; i < amountOfLasers; i++)
                lasers.Add(InitiateLaser(i));

            // Convert the list to an array
            lasers.ToArray();
        }

        private void Update()
        {
            RotateEmitter();
            UpdateLasers();
        }

        private void RotateEmitter()
        {
            laserEmitter.transform.Rotate(transform.forward * rotationSpeed * Time.deltaTime);
        }

        private void UpdateLasers()
        {
            for (int i = 0; i < amountOfLasers; i++)
            {
                // Determine startpoint
                // Edge of sprite
                var magnitude = laserEmitter.transform.localScale.z;
                // Rotation
                var objectRotation = laserEmitter.transform.localRotation.eulerAngles.z * Mathf.Deg2Rad;
                var angle = -objectRotation + i * (360 / amountOfLasers) * Mathf.Deg2Rad;
                var rot = new Vector2((float) Mathf.Cos(angle), -(float) Mathf.Sin(angle));

                var pos1 = (Vector2) laserEmitter.transform.position + rot * magnitude;
                lasers[i].SetPosition(0, pos1);
                // rotation => Direction 
                var pos2 = pos1 + rot * laserLength;

                // Check with raycast whether something is hit
                RaycastHit2D hit = Physics2D.Raycast(pos1, rot, laserLength);
                if (hit == true)
                {
                    if (hit.collider.tag.Equals("Player"))
                    {
                        PlayerController.Die();
                    }

                    // End laser there
                    pos2 = hit.point;
                    
                    //place, rotate and play particle
                    _particles[i].Holder.position = hit.point;
                    Vector3 rotation = _particles[i].Holder.eulerAngles;
                    rotation.z =  Vector3.Angle(pos2, pos1);
                    _particles[i].Holder.eulerAngles = rotation;

                    _particles[i].ToggleParticles(true);
                }
                else
                {
                    _particles[i].ToggleParticles(false);
                }

                lasers[i].SetPosition(1, pos2);
            }
        }

        private LineRenderer InitiateLaser(int i)
        {
            // Creating new GameObject and adding a LineRenderer
            GameObject g = new GameObject("Laser " + i);
            g.transform.parent = transform;
            LineRenderer l = g.AddComponent<LineRenderer>();
            // Setting the width
            l.SetWidth(.2f, .2f);

            GameObject particleSystem = Instantiate(_particleEmitter, Vector3.zero, Quaternion.identity, g.transform);
            ParticleSystem[] children = particleSystem.GetComponentsInChildren<ParticleSystem>();
            
            _particles.Add(new LaserParticles(children[0], children[1]));

            g.GetComponent<Renderer>().material = laserMaterial;
            return l;
        }
    }
}

public class LaserParticles
{
    private ParticleSystem _flash;
    private ParticleSystem _emitter;

    public LaserParticles(ParticleSystem flash, ParticleSystem emitter)
    {
        _flash = flash;
        _emitter = emitter;
    }

    public void ToggleParticles(bool turnOn)
    {
        if (turnOn && !_flash.isPlaying)
        {
            Holder.gameObject.SetActive(true);
            _flash.Play();
            _emitter.Play();
        }
        else if (!turnOn && _flash.isPlaying)
        {
            Holder.gameObject.SetActive(false);
            _flash.Stop();
            _emitter.Stop();
        }
    }

    public Transform Holder
    {
        get { return _flash.transform.parent; }
    }
}