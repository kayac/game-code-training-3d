public interface IMachine
{
	public void SetCamera(
		Vector3 position,
		Quaternion rotation,
		float fieldOfViewYInDegree);

	public void DrawTriangle(
		Vector3 p0,
		Vector3 p1,
		Vector3 p2, 
		Color color); // 三角形を描画する

	// 画面アスペクト比(幅/高さ)
	public float Aspect { get; }

	// ポインタ検出
	public bool PointerDown { get; }
	public Vector2 PointerPosition { get; }

	// キーボード検出
	public bool Up { get; }
	public bool Down { get; }
	public bool Left { get; }
	public bool Right { get; }
	public bool Space { get; }

	// 音
	public int SoundChannelCount { get; }
	public void PlaySound(int channel, float frequency, float dumping, float amplitude);

	// テキストファイルロード(Resources以下のにあるファイル名を指定する)
	public string LoadTextFile(string path);

	// デバグ用 UnityEngine.Debug.LogFormat相当品
	public void Log(string message, params object[] args);
}
