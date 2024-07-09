// 1. UnityEngineをUsingしてはならない
public class SampleApplication : UserApplication
{	
	// 毎フレーム(=1/60秒間隔で)呼ばれる
	public override void Update(IMachine machine)
	{
		var cameraPosition = new Vector3(0f, 0f, -4f);
		if (machine.PointerDown)
		{
			cameraPosition.x = machine.PointerPosition.x;
			cameraPosition.y = machine.PointerPosition.y;
		}

		machine.SetCamera(
			cameraPosition,
			Quaternion.LookRotation(new Vector3(0f, 0f, 1f), new Vector3(0f, 1f, 0f)),
			60f);

		var color = new Color(0f, 1f, 0f, 1f);
		if (machine.Space)
		{
			color = new Color(1f, 0f, 0f, 1f);
		}

		machine.DrawTriangle(
			new Vector3(0f, 0f, 0f),
			new Vector3(0.5f, 0f, 0f),
			new Vector3(0f, 0.5f, 0f),
			color);

		machine.Log("cameraPosition : {0}", cameraPosition);
	}
}
