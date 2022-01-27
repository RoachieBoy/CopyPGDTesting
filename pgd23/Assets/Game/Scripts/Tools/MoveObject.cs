using System.Collections;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    private Coroutine _coroutine;

    public void Move(Vector2 Pos, float time, float waitTime = 0)
    {
        if(_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(MoveObjec(Pos, time, waitTime));
    }

    private IEnumerator MoveObjec(Vector3 pos, float time, float waitTime = 0)
    {
        var direction = pos;
        var speed = direction.magnitude / time;
        direction.Normalize();

        yield return new WaitForSeconds(waitTime);

        while(time > 0)
        {
            var increment = direction * speed * Time.deltaTime;
            transform.position += increment;

            time -= Time.deltaTime;
            yield return null;
        }
    }    
}
