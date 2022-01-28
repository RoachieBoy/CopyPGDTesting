using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.KeyBindings;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.AbilitiesSystem.AbilityHandler
{
    public class ControlContainer : MonoBehaviour
    {
        [SerializeField] private float _DisabledOpacity, _EnabledOpacity, _UsingOpacity;
        [SerializeField] private Transform _screen;

        private readonly Dictionary<KeyBindingActions, KeyHolder>
            _keys = new Dictionary<KeyBindingActions, KeyHolder>();

        // Start is called before the first frame update
        public void Start()
        {
            List<KeyBindingActions> keyCodes = new List<KeyBindingActions>()
            {
                KeyBindingActions.CrouchKey, KeyBindingActions.WalkLeftKey, KeyBindingActions.WalkRightKey, KeyBindingActions.JumpKey, KeyBindingActions.DashKey
            };

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name.Equals("Screen"))
                {
                    _screen = transform.GetChild(i);
                }
            }

            for (int i = 0; i < keyCodes.Count; i++)
            {
                GameObject key = _screen.GetChild(i).gameObject;
                KeyHolder keyHolder = new KeyHolder();

                keyHolder.Image = key.GetComponentsInChildren<Image>().ToList();

                _keys.Add(keyCodes[i], keyHolder);
                DeActivateKey(keyCodes[i]);
            }
        }

        public void Update()
        {
            foreach (KeyValuePair<KeyBindingActions, KeyHolder> key in _keys)
            {
                if (key.Value.Enabled)
                {
                    if (InputManager.Instance.GetKeyDown(key.Key))
                    {
                        ChangeColorOfKey(key.Key, _UsingOpacity);
                    }

                    else if (InputManager.Instance.GetKeyUp(key.Key) &&
                             Math.Abs(key.Value.Image[0].color.a - _UsingOpacity) < Mathf.Epsilon)
                    {
                        ChangeColorOfKey(key.Key, _EnabledOpacity);
                    }
                }
            }
        }

        private void ChangeColorOfKey(KeyBindingActions key, float newColor)
        {
            foreach (var image in _keys[key].Image)
            {
                Color color = image.color;

                color.a = newColor;

                image.color = color;
            }
        }

        public void ActivateKey(KeyBindingActions key)
        {
            KeyHolder keyHolder = _keys[key];
            keyHolder.Enabled = true;

            _keys[key] = keyHolder;
            
            ChangeColorOfKey(key, _EnabledOpacity);
        }

        public void DeActivateKey(KeyBindingActions key)
        {
            KeyHolder keyHolder = _keys[key];
            keyHolder.Enabled = false;

            _keys[key] = keyHolder;
            ChangeColorOfKey(key, _DisabledOpacity);
        }


        public struct KeyHolder
        {
            public bool Enabled;
            public List<Image> Image;
        }
    }
}