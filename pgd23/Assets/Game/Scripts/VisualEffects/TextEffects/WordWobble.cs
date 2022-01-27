using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.Scripts.VisualEffects.TextEffects
{
    public class WordWobble : MonoBehaviour
    {
        private TMP_Text _textMesh;
        private Mesh _mesh;
        private Vector3[] _vertices;
        private List<int> _wordIndexes;
        private List<int> _wordLengths;
        
        private const float SinMovementSpeed = 10f;
        private const float SinWaveSpeed = 4f;

        private const int IndexOne = 1,
            IndexTwo = 2,
            IndexThree = 3;

        private void Start()
        {
            _textMesh = GetComponent<TMP_Text>();

            _wordIndexes = new List<int> {0};
            _wordLengths = new List<int>();

            var s = _textMesh.text;
            for (var index = s.IndexOf(' '); index > -1; index = s.IndexOf(' ', index + 1))
            {
                _wordLengths.Add(index - _wordIndexes[_wordIndexes.Count - 1]);
                _wordIndexes.Add(index + 1);
            }

            _wordLengths.Add(s.Length - _wordIndexes[_wordIndexes.Count - 1]);
        }

        private void Update()
        {
            _textMesh.ForceMeshUpdate();
            _mesh = _textMesh.mesh;
            _vertices = _mesh.vertices;

            var colors = _mesh.colors;

            for (var w = 0; w < _wordIndexes.Count; w++)
            {
                var wordIndex = _wordIndexes[w];
                Vector3 offset = Wobble(Time.time + w);

                for (var i = 0; i < _wordLengths[w]; i++)
                {
                    var c = _textMesh.textInfo.characterInfo[wordIndex + i];

                    var index = c.vertexIndex;

                    _vertices[index] += offset;
                    _vertices[index + IndexOne] += offset;
                    _vertices[index + IndexTwo] += offset;
                    _vertices[index + IndexThree] += offset;
                }
            }

            _mesh.vertices = _vertices;
            _mesh.colors = colors;
            _textMesh.canvasRenderer.SetMesh(_mesh);
        }

        /// <summary>
        ///     Wobbles a word in a sinus wave 
        /// </summary>
        /// <param name="time"> duration of the sinus movement </param>
        /// <returns> vector 2 </returns>
        private static Vector2 Wobble(float time)
        {
            return new Vector2(Mathf.Sin(time * SinMovementSpeed), Mathf.Cos(time * SinWaveSpeed));
        }
    }
}