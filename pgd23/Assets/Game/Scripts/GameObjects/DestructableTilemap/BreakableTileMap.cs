using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakableTileMap : MonoBehaviour
{
    private Tilemap tileMap;

    private void Start()
    {
        // TileMap
        tileMap = GetComponent<Tilemap>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Destructor"))
        {
            Debug.Log("Whew");
            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                tileMap.SetTile(tileMap.WorldToCell(hitPosition), null);
            }
        }
    }
}
