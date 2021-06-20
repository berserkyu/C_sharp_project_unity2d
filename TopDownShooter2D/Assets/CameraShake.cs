using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public void shakeCamera(float duration, float magnitude)
    {
        StartCoroutine(shake(duration, magnitude));
    }
    IEnumerator shake(float duration, float magnitude)
    {
        float frameCnt = duration;
        while (frameCnt >= 0)
        {
            float newX = Random.Range(-1f, 1f) * magnitude + transform.position.x;
            float newY = Random.Range(-1f, 1f) * magnitude + transform.position.y;
            transform.position = new Vector3(newX, newY, transform.position.z);
            frameCnt -= Time.deltaTime;
            yield return null;
        }
    }
}
