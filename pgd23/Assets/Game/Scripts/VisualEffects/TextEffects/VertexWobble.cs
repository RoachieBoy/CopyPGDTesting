using TMPro;
using UnityEngine;

namespace Game.Scripts.VisualEffects.TextEffects
{
    public class VertexWobble : MonoBehaviour
    {
        private TMP_Text _textMesh;
        private Mesh _mesh;
        private Vector3[] _vertices;
        
        private const float SinMovementSpeed = 3f;
        private const float SinWaveSpeed = 2.5f;

        private void Start()
        {
            _textMesh = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            VertexWobbling();
        }

        /// <summary>
        ///     Sinus movement for the vertex wobble
        /// </summary>
        /// <param name="time"> duration of the sinus movement </param>
        /// <returns> vector 2 </returns>
        private static Vector2 Wobble(float time)
        {
            return new Vector2(Mathf.Sin(time * SinMovementSpeed), Mathf.Cos(time * SinWaveSpeed));
        }

        /// <summary>
        ///     Method that allows for the vertex of a text object to wobble 
        /// </summary>
        private void VertexWobbling()
        {
            _textMesh.ForceMeshUpdate();
            _mesh = _textMesh.mesh;
            _vertices = _mesh.vertices;

            for (var i = 0; i < _vertices.Length; i++)
            {
                Vector3 offset = Wobble(Time.time + i);

                _vertices[i] += offset;
            }

            _mesh.vertices = _vertices;
            _textMesh.canvasRenderer.SetMesh(_mesh);
        }
    }
}