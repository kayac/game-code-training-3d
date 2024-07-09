public struct Quaternion
{
	public static Quaternion identity = new Quaternion(0f, 0f, 0f, 1f);

	public static Quaternion LookRotation(Vector3 forward, Vector3 upHint)
	{
		// 手抜き
		var f = new UnityEngine.Vector3(forward.x, forward.y, forward.z);
		var u = new UnityEngine.Vector3(upHint.x, upHint.y, upHint.z);
		var q = UnityEngine.Quaternion.LookRotation(f, u);
		return new Quaternion(q.x, q.y, q.z, q.w);
	}

	public static Quaternion AngleAxis(float angle, Vector3 axis)
	{
		// 手抜き
		var a = new UnityEngine.Vector3(axis.x, axis.y, axis.z);
		var q = UnityEngine.Quaternion.AngleAxis(angle, a);
		return new Quaternion(q.x, q.y, q.z, q.w);
	}

	public static Quaternion Inverse(Quaternion q)
	{
		return new Quaternion(-q.x, -q.y, -q.z, q.w);
	}

	public static Quaternion Slerp(Quaternion a, Quaternion b, float t)
	{
		// 手抜き
		var ua = new UnityEngine.Quaternion(a.x, a.y, a.z, a.w);
		var ub = new UnityEngine.Quaternion(b.x, b.y, b.z, b.w);
		var q = UnityEngine.Quaternion.Slerp(ua, ub, t);
		return new Quaternion(q.x, q.y, q.z, q.w);
	}

	public static Vector3 operator*(Quaternion q, Vector3 v)
	{
		// 手抜き
		var uq = new UnityEngine.Quaternion(q.x, q.y, q.z, q.w);
		var uv = new UnityEngine.Vector3(v.x, v.y, v.z);
		var ur = uq * uv;
		return new Vector3(ur.x, ur.y, ur.z);
	}

	public static Quaternion operator*(Quaternion a, Quaternion b)
	{
		// 手抜き
		var ua = new UnityEngine.Quaternion(a.x, a.y, a.z, a.w);
		var ub = new UnityEngine.Quaternion(b.x, b.y, b.z, b.w);
		var uq = ua * ub;
		return new Quaternion(uq.x, uq.y, uq.z, uq.w);
	}

	public Quaternion(float x, float y, float z, float w)
	{
		this.x = x;
		this.y = y;
		this.z = z;
		this.w = w;
	}

	public float x, y, z, w;
}
