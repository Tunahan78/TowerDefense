// CameraEvents.cs
using System;

public static class CameraEvents
{
    // Kule İnşa Sistemi veya Patlama Sistemi tarafından tetiklenir
    public static event Action<float, float> OnCameraShakeRequest;

    public static void TriggerCameraShake(float amplitude, float duration)
    {
        OnCameraShakeRequest?.Invoke(amplitude, duration);
    }
}
