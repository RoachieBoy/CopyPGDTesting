using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RepeatObject))]
public class RepeatObjectEditor : Editor
{
    RepeatObject repeatObject;

    private SerializedProperty amountOfObjects;
    private SerializedProperty offset;

    private void Awake()
    {
        repeatObject = (RepeatObject)target;
        amountOfObjects = serializedObject.FindProperty("amountOfObjects");
        offset = serializedObject.FindProperty("offset");
    }

    public override void OnInspectorGUI()
    {
        int previousAmountOfObjects = amountOfObjects.intValue;
        Vector2 previousOffset = offset.vector2Value;

        base.OnInspectorGUI();

        serializedObject.Update();

        if (previousAmountOfObjects != amountOfObjects.intValue) repeatObject.CorrectChildCount();
        if (previousOffset != offset.vector2Value) repeatObject.RepositionChildren();
    }
}
