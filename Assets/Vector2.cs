public struct Vector2
{
	public float sqrMagnitude { get => (x * x) + (y * y); } 
	public float magnitude { get => Mathf.Sqrt(sqrMagnitude); }
	public Vector2 normalized 
	{ 
		get
		{
			var rcpM = 1f / magnitude;
			return new Vector2(x * rcpM, y * rcpM);
		} 
	}

	public Vector2(float x, float y)
	{
		this.x = x;
		this.y = y;
	}

	public Vector2(Vector2 v)
	{
		this.x = v.x;
		this.y = v.y;
	}

	public Vector2(float xy)
	{
		this.x = xy;
		this.y = xy;
	}

	public static Vector2 operator+(Vector2 a, Vector2 b)
	{
		return new Vector2(a.x + b.x, a.y + b.y);
	}

	public static Vector2 operator-(Vector2 a, Vector2 b)
	{
		return new Vector2(a.x - b.x, a.y - b.y);
	}

	public static Vector2 operator*(Vector2 a, float b)
	{
		return new Vector2(a.x * b, a.y * b);
	}

	public static Vector2 operator/(Vector2 a, float b)
	{
		return new Vector2(a.x / b, a.y / b);
	}

	public static Vector2 operator-(Vector2 a)
	{
		return new Vector2(-a.x, -a.y);
	}

	public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
	{
		return a + ((b - a) * t);
	}

	public static float Dot(Vector2 a, Vector2 b)
	{
		return (a.x * b.x) + (a.y * b.y);
	}

	public void Normalize()
	{
		var rcpM = 1f / this.magnitude;
		x *= rcpM;
		y *= rcpM;
	}

	public override string ToString()
	{
		return string.Format("({0:F2}, {1:F2})", x, y);
	}

	public float x, y;
}
