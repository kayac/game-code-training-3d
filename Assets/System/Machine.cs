using System.Collections.Generic;
using UnityEngine;

public class Machine : IMachine
{
	public class Vertex
	{
		public Vertex(Vector3 position, Color color)
		{
			Position = position;
			Color = color;
		}
		public Vector3 Position;
		public Color Color;
	}
	public float Aspect { get => (float)Screen.width / (float)Screen.height; }
	public bool PointerDown { get => pointerDown; }
	public Vector2 PointerPosition { get => pointerPosition; }
	public int SoundChannelCount { get => soundSynthesizer.ChannelCount; }
	public bool Up { get => upOn; }
	public bool Down { get => downOn; }
	public bool Left { get => leftOn; }
	public bool Right { get => rightOn; }
	public bool Space { get => spaceOn; }


	// internal
	public Vector3 CameraPosisition { get; private set; }
	public Quaternion CameraRotation { get; private set; }
	public float CameraFieldOfViewYInDegree { get; private set; }
	public IReadOnlyList<Vertex> Vertices { get => vertices; }

	public Machine(SoundSynthesizer soundSynthesizer)
	{
		this.soundSynthesizer = soundSynthesizer;
		this.vertices = new List<Vertex>();
		Reset();
	}

	public void Reset()
	{
		vertices.Clear();
		CameraPosisition = new Vector3(0f, 0f, 0f);
		CameraRotation = Quaternion.identity;
		CameraFieldOfViewYInDegree = 60f;
	}

	public void SetCamera(
		Vector3 position,
		Quaternion rotation,
		float fieldOfViewYInDegree)
	{
		CameraPosisition = position;
		CameraRotation = rotation;
		CameraFieldOfViewYInDegree = fieldOfViewYInDegree;
	}

	public void DrawTriangle(
		Vector3 p0,
		Vector3 p1,
		Vector3 p2,
		Color color)
	{
		vertices.Add(new Vertex(p0, color));
		vertices.Add(new Vertex(p1, color));
		vertices.Add(new Vertex(p2, color));
	}
		
	public void PlaySound(int channel, float frequency, float dumping, float amplitude)
	{
		soundSynthesizer.Play(
			channel, 
			frequency, 
			Mathf.Clamp01(dumping) * 0.001f, 
			Mathf.Clamp01(amplitude) * 1f);
	}

	public void Update(bool pointerDown, Vector2 pointerPosition)
	{
		this.pointerDown = pointerDown;
		this.pointerPosition = pointerPosition;
		this.vertices.Clear();

		upOn = Input.GetKey(KeyCode.UpArrow);
		downOn = Input.GetKey(KeyCode.DownArrow);
		leftOn = Input.GetKey(KeyCode.LeftArrow);
		rightOn = Input.GetKey(KeyCode.RightArrow);
		spaceOn = Input.GetKey(KeyCode.Space);
	}

	public string LoadTextFile(string path)
	{
		string ret = null;
		var asset = Resources.Load<TextAsset>(path);
		if (asset != null)
		{
			ret = asset.text;
		}
		return ret;
	}

	public void Log(string message, params object[] args)
	{
		Debug.LogFormat(message, args);
	}

	// non public ----
	bool pointerDown;
	Vector2 pointerPosition;
	SoundSynthesizer soundSynthesizer;
	List<Vertex> vertices;
	bool upOn;
	bool downOn;
	bool leftOn;
	bool rightOn;
	bool spaceOn;
}
