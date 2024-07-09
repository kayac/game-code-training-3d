public struct Vector3
{
	public float sqrMagnitude { get => (x * x) + (y * y) + (z * z); } 
	public float magnitude { get => Mathf.Sqrt(sqrMagnitude); }
	public Vector3 normalized 
	{ 
		get
		{
			var rcpM = 1f / magnitude;
			return new Vector3(x * rcpM, y * rcpM, z * rcpM);
		} 
	}
	
	public Vector3(float x, float y, float z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public Vector3(Vector3 v)
	{
		this.x = v.x;
		this.y = v.y;
		this.z = v.z;
	}

	public Vector3(Vector2 v2, float z)
	{
		this.x = v2.x;
		this.y = v2.y;
		this.z = z;
	}

	public Vector3(float xyz)
	{
		this.x = xyz;
		this.y = xyz;
		this.z = xyz;
	}

	public static Vector3 operator+(Vector3 a, Vector3 b)
	{
		return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
	}

	public static Vector3 operator-(Vector3 a, Vector3 b)
	{
		return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
	}

	public static Vector3 operator*(Vector3 a, float b)
	{
		return new Vector3(a.x * b, a.y * b, a.z * b);
	}

	public static Vector3 operator/(Vector3 a, float b)
	{
		return new Vector3(a.x / b, a.y / b, a.z / b);
	}

	public static Vector3 operator-(Vector3 a)
	{
		return new Vector3(-a.x, -a.y, -a.z);
	}

	public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
	{
		return a + ((b - a) * t);
	}

	public static float Dot(Vector3 a, Vector3 b)
	{
		return (a.x * b.x) + (a.y * b.y) + (a.z * b.z);
	}

	public static Vector3 Cross(Vector3 a, Vector3 b)
	{
		return new Vector3(
			(a.y * b.z) - (a.z * b.y),
			(a.z * b.x) - (a.x * b.z),
			(a.x * b.y) - (a.y * b.x)
		);
	}

	public void Normalize()
	{
		var rcpM = 1f / this.magnitude;
		x *= rcpM;
		y *= rcpM;
		z *= rcpM;
	}

	public override string ToString()
	{
		return string.Format("({0:F2}, {1:F2}, {2:F2})", x, y, z);
	}

	public float x, y, z;
}
