using TMPro;
using UnityEngine;

namespace Game.Scripts.VisualEffects.TextEffects
{
    public class CharacterWobble : MonoBehaviour
    {
        private TMP_Text _textMesh;
        private Mesh _mesh;
        private Vector3[] _vertices;

        private const float SinMovementSpeed = 4f;
        private const float SinWaveSpeed = 5f;

        private const int IndexOne = 1,
            IndexTwo = 2,
            IndexThree = 3;

        private void Start()
        {
            _textMesh = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            _textMesh.ForceMeshUpdate();
            _mesh = _textMesh.mesh;
            _vertices = _mesh.vertices;

            for (var i = 0; i < _textMesh.textInfo.characterCount; i++)
            {
                var c = _textMesh.textInfo.characterInfo[i];

                var index = c.vertexIndex;

                Vector3 offset = Wobble(Time.time + i);
                _vertices[index] += offset;
                _vertices[index + IndexOne] += offset;
                _vertices[index + IndexTwo] += offset;
                _vertices[index + IndexThree] += offset;
            }

            _mesh.vertices = _vertices;
            _textMesh.canvasRenderer.SetMesh(_mesh);
        }

        /// <summary>
        ///     Sinus wave to wobble the characters in 
        /// </summary>
        /// <param name="time"> duration of the sinus movement </param>
        /// <returns> vector 2 </returns>
        private static Vector2 Wobble(float time)
        {
            return new Vector2(Mathf.Sin(time * SinMovementSpeed), Mathf.Cos(time * SinWaveSpeed));
        }
    }
}