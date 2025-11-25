using System;
using UnityEngine;

public class BeamVFXController : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    public void  EnableBeamForDuration(float duration)
    {
        lineRenderer.enabled = true;
        // Gecikmeli kapatma için Unity'nin Invoke metodu kullanılır
        CancelInvoke(nameof(DisableBeam)); // Önceki çağrıyı iptal et
        Invoke(nameof(DisableBeam), duration);
    }

    private void DisableBeam()
    {
        lineRenderer.enabled = false;
    }
}
