using Game.Scripts.Core_LevelManagement.CameraManagement;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent trigger;

    [SerializeField] private bool nameraChange;
    [SerializeField] private float newView;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") triggerTrigger(collision.gameObject);
    }

    private void triggerTrigger(GameObject obj)
    {
        trigger?.Invoke();
        CameraMovement.Current.ChangeToFollowing(obj.transform, false, false, newView);
    }
}
