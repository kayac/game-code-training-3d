public static class Mathf
{
	public static float PI = (float)System.Math.PI;
	public static float Deg2Rad = PI / 180f;
	public static float Rad2Deg = 180f / PI;

	public static float Min(float a, float b)
	{
		return (a < b) ? a : b;
	}

	public static float Max(float a, float b)
	{
		return (a > b) ? a : b;
	}

	public static float Clamp(float value, float min, float max)
	{
		return (value < min) ? min : ((value > max) ? max : value);
	}

	public static float Clamp01(float value)
	{
		return (value < 0f) ? 0f : ((value > 1f) ? 1f : value);
	}

	public static float Lerp(float a, float b, float t)
	{
		return a + ((b - a) * t);
	}

	public static float Sqrt(float f)
	{
		return (float)System.Math.Sqrt(f);
	}

	public static float Acos(float f)
	{
		return (float)System.Math.Acos(f);
	}

	public static float Asin(float f)
	{
		return (float)System.Math.Asin(f);
	}

	public static float Atan2(float y, float x)
	{
		return (float)System.Math.Atan2(y, x);
	}

	public static float Abs(float f)
	{
		return f < 0f ? -f : f;
	}

	public static float Sign(float f)
	{
		return f < 0f ? -1f : (f > 0f ? 1f : 0f);
	}

	public static float Pow(float f, float p)
	{
		return (float)System.Math.Pow(f, p);
	}

	public static float Exp(float power)
	{
		return (float)System.Math.Exp(power);
	}

	public static float Log(float f)
	{
		return (float)System.Math.Log(f);
	}

	public static float Sin(float f)
	{
		return (float)System.Math.Sin(f);
	}

	public static float Cos(float f)
	{
		return (float)System.Math.Cos(f);
	}

	public static float Tan(float f)
	{
		return (float)System.Math.Tan(f);
	}
}
