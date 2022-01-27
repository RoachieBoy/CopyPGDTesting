using UnityEngine;

public class RepeatObject : MonoBehaviour
{
    [SerializeField] private GameObject original;
    [Range(0, 50)] [SerializeField] private int amountOfObjects = 1;
    [SerializeField] private Vector2 offset;

    public void Start()
    {
        CorrectChildCount();
    }

    public void CorrectChildCount()
    {
        if (transform.childCount < amountOfObjects) SpawnChildren();
        if (transform.childCount > amountOfObjects) DespawnChildren();
    }

    private void SpawnChildren()
    {
        while (transform.childCount < amountOfObjects)
        {
            var numberInRow = transform.childCount;
            var position = transform.position + new Vector3(numberInRow * offset.x, numberInRow * offset.y, 0);
            Instantiate(original, position, Quaternion.identity, transform);
        }
    }

    private void DespawnChildren()
    {
        while(transform.childCount > amountOfObjects)
        {
            DestroyImmediate(transform.GetChild(transform.childCount - 1).gameObject);
        }
    }

    public void RepositionChildren()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            var position = transform.position + new Vector3(i * offset.x, i * offset.y, 0);
            transform.GetChild(i).transform.position = position;
        }
    }
}
