using UnityEngine;

public class Initializer
{
    [RuntimeInitializeOnLoadMethod]
    static void SetResolution()
    {
        Screen.SetResolution(432, 768, FullScreenMode.Windowed, 60);
        Application.targetFrameRate = 60;
    }
}