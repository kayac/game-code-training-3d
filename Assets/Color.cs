public struct Color
{
	public Color(float r, float g, float b, float a)
	{
		this.r = r;
		this.g = g;
		this.b = b;
		this.a = a;
	}

	public Color(Color c)
	{
		this.r = c.r;
		this.g = c.g;
		this.b = c.b;
		this.a = c.a;
	}

	public static Color operator*(Color a, float b)
	{
		return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
	}

	public static Color operator+(Color a, Color b)
	{
		return new Color(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
	}

	public override string ToString()
	{
		return string.Format("({0:F2}, {1:F2}, {2:F2}, {3:F2})", r, g, b, a);
	}

	public float r, g, b, a;
}
