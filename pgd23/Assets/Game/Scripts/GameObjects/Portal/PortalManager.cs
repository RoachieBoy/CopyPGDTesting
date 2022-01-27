using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public static PortalManager current;

    [SerializeField] private List<PortalCombination> portals;


    private void Awake() => current = this;

    public void UsePortal(Portal portal, GameObject obj)
    {
        foreach (PortalCombination comb in portals)
        {
            if (comb.HasPortal(portal))
            {
                comb.UsePortal(portal, obj);
                return;
            }
            
        }
    }
}
