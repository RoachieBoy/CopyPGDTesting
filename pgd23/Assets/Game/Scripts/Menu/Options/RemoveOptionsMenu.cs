using UnityEngine;

namespace Game.Scripts.Menu.Options
{
    /// <inheritdoc />
    public class RemoveOptionsMenu: MonoBehaviour
    {
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            gameObject.SetActive(false);
        }
    }
}