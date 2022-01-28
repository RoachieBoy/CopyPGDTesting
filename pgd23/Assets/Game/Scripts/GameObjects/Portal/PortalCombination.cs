using UnityEngine;

namespace Game.Scripts.GameObjects.Portal
{
    [System.Serializable]
    public struct PortalCombination
    {
        [SerializeField] private Portal portal1;
        [SerializeField] private Portal portal2;

        public bool HasPortal(Portal portal) => portal1 == portal || portal2 == portal;

        public void UsePortal(Portal portal, GameObject obj)
        {
            if (portal1 == portal)
            {
                portal2.UsePortal(obj);
                portal1.CollideWithPortal();
            }

            if (portal2 == portal)
            {
                portal1.UsePortal(obj);
                portal2.CollideWithPortal();

            }
        }

    }
}
