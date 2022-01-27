using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Tilemaps;

public class ColorManager : MonoBehaviour
{
    [SerializeField] private List<ColorGroup> _colorGroups;
    private Dictionary<string, int> _nameDictionary = new Dictionary<string, int>();

    public bool ColorBlind = false;

    public static ColorManager Current;
    private void Awake() => Current = this;

    /// <summary>
    ///     Calls the setup of the colorgroups and adds the names of the groups to a dictionary for easy lookup
    /// </summary>
    /// <returns></returns>
    private void Start()
    {
        if (PlayerPrefs.HasKey("setting-colorblind") && PlayerPrefs.GetInt("setting-colorblind") == 1)
        {
            ColorBlind = true;
        }

        int index = 0;
        foreach (var group in _colorGroups)
        {
            group.Setup();

            _nameDictionary.Add(group.Name, index);

            index++;
        }
    }

    /// <summary>
    ///     Calls the color check that changes the color
    /// </summary>
    /// <returns></returns>
    private void Update()
    {
        foreach (var group in _colorGroups)
        {
            group.ColorCheck(ColorBlind);
        }
    }

    #region ColorChanges

    /// <summary>
    ///     Changes the color of a color group by name
    /// </summary>
    /// <param name="name"> the name of the color group </param>
    /// <param name="color"> color to change to </param>
    /// <returns></returns>
    public void ChangeGroupColor(string name, Color color)
    {
        ChangeGroupColor(_nameDictionary[name], color);
    }

    /// <summary>
    ///     Changes the color of a color group by index
    /// </summary>
    /// <param name="index"> the index of the color group </param>
    /// <param name="color"> color to change to </param>
    /// <returns></returns>
    public void ChangeGroupColor(int index, Color color)
    {
        _colorGroups[index].Color = color;
    }

    /// <summary>
    ///     Changes the color of a color group by name
    /// </summary>
    /// <param name="name"> the name of the color group </param>
    /// <param name="color"> color to change to </param>
    /// <returns></returns>
    public void ChangeGroupColorBlindColor(string name, Color color)
    {
        ChangeGroupColorBlindColor(_nameDictionary[name], color);
    }

    /// <summary>
    ///     Changes the color of a color group by index
    /// </summary>
    /// <param name="index"> the index of the color group </param>
    /// <param name="color"> color to change to </param>
    /// <returns></returns>
    public void ChangeGroupColorBlindColor(int index, Color color)
    {
        _colorGroups[index].ColorBlindColor = color;
    }

    /// <summary>
    ///     Changes the color of a color group by name
    /// </summary>
    /// <param name="name"> the name of the color group </param>
    /// <param name="color"> color to change to </param>
    /// <returns></returns>
    public void ChangeGroupCurrentColor(string name, Color color)
    {
        ChangeGroupCurrentColor(_nameDictionary[name], color);
    }

    /// <summary>
    ///     Changes the color of a color group by index
    /// </summary>
    /// <param name="index"> the index of the color group </param>
    /// <param name="color"> color to change to </param>
    /// <returns></returns>
    public void ChangeGroupCurrentColor(int index, Color color)
    {
        _colorGroups[index].CurrentColor = color;
    }

    #endregion
}

[Serializable]
public class ColorGroup
{
    public string Name;
    public List<GameObject> Objects;
    public Color Color;
    public Color ColorBlindColor;

    public Color CurrentColor
    {
        get { return (_colorBlind ? ColorBlindColor : Color); }
        set
        {
            if (_colorBlind)
            {
                ColorBlindColor = value;
            }
            else
            {
                ColorBlindColor = Color;
            }
        }
    }

    private bool _colorBlind = false;

    private Color _prevColor;
    private List<SpriteRenderer> _spriteRenderers = new List<SpriteRenderer>();
    private List<Light2D> _lights = new List<Light2D>();
    private List<Tilemap> _tilemaps = new List<Tilemap>();
    private List<ParticleSystem> _particleSystems = new List<ParticleSystem>();

    /// <summary>
    ///     This function sorts the objects in other variables.
    ///     This way the get component doesn't have to be used every time
    /// </summary>
    /// <returns></returns>
    public void Setup()
    {
        foreach (GameObject objects in Objects)
        {
            if (objects != null)
            {
                if (objects.transform.childCount > 0)
                {
                    if (objects.transform.GetComponentsInChildren<SpriteRenderer>().Length > 0)
                    {
                        _spriteRenderers = _spriteRenderers
                            .Concat(objects.GetComponentsInChildren<SpriteRenderer>())
                            .ToList();
                    }
                    else if (objects.transform.GetComponentsInChildren<Light2D>().Length > 0)
                    {
                        _lights = _lights.Concat(objects.GetComponentsInChildren<Light2D>()).ToList();
                    }
                    else if (objects.transform.GetComponentsInChildren<Tilemap>().Length > 0)
                    {
                        _tilemaps = _tilemaps.Concat(objects.GetComponentsInChildren<Tilemap>()).ToList();
                    }
                    else if (objects.transform.GetComponentsInChildren<ParticleSystem>().Length > 0)
                    {
                        _particleSystems = _particleSystems.Concat(objects.GetComponentsInChildren<ParticleSystem>())
                            .ToList();
                    }
                }
                else
                {
                    if (objects.GetComponent<SpriteRenderer>())
                    {
                        _spriteRenderers.Add(objects.GetComponent<SpriteRenderer>());
                    }
                    else if (objects.GetComponent<Light2D>())
                    {
                        _lights.Add(objects.GetComponent<Light2D>());
                    }
                    else if (objects.GetComponent<Tilemap>())
                    {
                        _tilemaps.Add(objects.GetComponent<Tilemap>());
                    }
                    else if (objects.GetComponent<ParticleSystem>())
                    {
                        _particleSystems.Add(objects.GetComponent<ParticleSystem>());
                    }
                }
            }
        }
    }

    /// <summary>
    ///     checks if one of the sorted objects have an not matching color. If so it changes it.
    /// </summary>
    /// <returns></returns>    
    public void ColorCheck(bool colorBlind)
    {
        if (_prevColor != (colorBlind ? ColorBlindColor : Color) || _colorBlind != colorBlind)
        {
            _prevColor = Color;
            _colorBlind = colorBlind;
            UpdateColor();
        }
    }

    /// <summary>
    ///     Changes the colors of each type
    /// </summary>
    /// <returns></returns>
    private void UpdateColor()
    {
        foreach (SpriteRenderer spriteRenderer in _spriteRenderers.ToList())
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.color = CurrentColor;
            }
        }

        foreach (Light2D light in _lights.ToList())
        {
            if (light != null)
            {
                light.color = CurrentColor;
            }
        }

        foreach (Tilemap tilemap in _tilemaps.ToList())
        {
            if (tilemap != null)
            {
                tilemap.color = CurrentColor;
            }
        }

        foreach (ParticleSystem particleSystem in _particleSystems.ToList())
        {
            if (particleSystem != null)
            {
                var current = particleSystem.main;
                current.startColor = CurrentColor;
            }
        }
    }
}