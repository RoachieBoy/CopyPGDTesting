using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Scripts.GameObjects.Portal
{
    public class PortalManager : MonoBehaviour
    {
        public static PortalManager Current;

        [SerializeField] private List<PortalCombination> portals;


        private void Awake() => Current = this;

        public void UsePortal(Portal portal, GameObject obj)
        {
            foreach (var comb in portals.Where(comb => comb.HasPortal(portal)))
            {
                comb.UsePortal(portal, obj);
                return;
            }
        }
    }
}
