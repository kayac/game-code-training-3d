using UnityEngine;
using UnityEngine.EventSystems;

public class Main : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField] Object applicationScript;
	[SerializeField] SoundSynthesizer soundSynthesizer;
	[SerializeField] Kayac.FullScreenInputHandler inputHandler;
	[SerializeField] Camera mainCamera;
	[SerializeField] MeshFilter meshFilter;

	void Start()
	{
		Application.targetFrameRate = 60;
		soundSynthesizer.ManualStart(8);
		
		machine = new Machine(soundSynthesizer);
		mesh = new Mesh();
		BootApplication();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || (currentScript != applicationScript))
		{
			currentScript = applicationScript;
			BootApplication();
		}

		var p = inputHandler.ScreenPosition;
		p.y /= Screen.height * 0.5f;
		p.y -= 1f;
		p.x -= Screen.width * 0.5f;
		p.x /= Screen.height * 0.5f;
		machine.Update(pointerDown, new Vector2(p.x, p.y));
		if (application != null)
		{
			application.Update(machine);
		}

		// カメラ設定
		var cp = machine.CameraPosisition;
		var cr = machine.CameraRotation;
		var fovY = machine.CameraFieldOfViewYInDegree;
		mainCamera.transform.position = new UnityEngine.Vector3(cp.x, cp.y, cp.z);
		mainCamera.transform.rotation = new UnityEngine.Quaternion(cr.x, cr.y, cr.z, cr.w);
		mainCamera.fieldOfView = fovY;

		// メッシュ生成
		mesh.Clear();
		var srcVertices = machine.Vertices;

		var vertices = new UnityEngine.Vector3[srcVertices.Count];
		var colors = new UnityEngine.Color[srcVertices.Count];
		var indices = new int[srcVertices.Count];

		for (int i = 0; i < srcVertices.Count; i++)
		{
			var v = srcVertices[i];
			vertices[i] = new UnityEngine.Vector3(v.Position.x, v.Position.y, v.Position.z);
			colors[i] = new UnityEngine.Color(v.Color.r, v.Color.g, v.Color.b, v.Color.a);
			indices[i] = i;
		}

		mesh.vertices = vertices;
		mesh.colors = colors;
		mesh.SetIndices(indices, MeshTopology.Triangles, 0);

		meshFilter.sharedMesh = mesh;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		this.pointerDown = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		this.pointerDown = false;
	}

	// non public ----
	Machine machine;
	UserApplication application;
	bool pointerDown;
	Mesh mesh;
	Object currentScript;

	void BootApplication()
	{
		Debug.Log("Boot Application " + applicationScript.name);
		soundSynthesizer.StopAll();
		machine.Reset();
		mesh.Clear();

		application = UserApplication.Instantiate(applicationScript);
		if (application == null)
		{
			Debug.LogError(applicationScript.name + "という名前のクラスはないようだ。");
		}
	}
}
