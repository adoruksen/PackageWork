using UnityEngine;

public static class InputNormalizer
{
    public static float GetWithNormalizer(float with = 1080)
    {
        return Screen.width / with;
    }

    public static float GetHeightNormalizer(float height = 1920)
    {
        return Screen.height / height;
    }

    public static Vector2 GetNormalizer(float width = 1080, float height = 1920)
    {
        return new Vector2(width / Screen.width, height / Screen.height);
    }
}