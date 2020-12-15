using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public IEnumerator Shake (float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            Debug.Log(duration);
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;

            Debug.Log(elapsed);

        }

        transform.localPosition = originalPosition;

    }
}
