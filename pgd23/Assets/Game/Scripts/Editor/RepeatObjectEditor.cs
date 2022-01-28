using Game.Scripts.Tools;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Editor
{
    [CustomEditor(typeof(RepeatObject))]
    public class RepeatObjectEditor : UnityEditor.Editor
    {
        private RepeatObject _repeatObject;
        private SerializedProperty _amountOfObjects;
        private SerializedProperty _offset;

        private void Awake()
        {
            _repeatObject = (RepeatObject)target;
            _amountOfObjects = serializedObject.FindProperty("amountOfObjects");
            _offset = serializedObject.FindProperty("offset");
        }

        public override void OnInspectorGUI()
        {
            var previousAmountOfObjects = _amountOfObjects.intValue;
            var previousOffset = _offset.vector2Value;

            base.OnInspectorGUI();

            serializedObject.Update();

            if (previousAmountOfObjects != _amountOfObjects.intValue) _repeatObject.CorrectChildCount();
            if (previousOffset != _offset.vector2Value) _repeatObject.RepositionChildren();
        }
    }
}
