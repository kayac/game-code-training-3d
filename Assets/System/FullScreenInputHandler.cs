using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Kayac
{
	// 常にReceiverにイベントを送信する
	public class FullScreenInputHandler : BaseRaycaster
	{
		public class Pointer
		{
			public UnityEngine.Vector2 ScreenPosition { get; private set; }
			public UnityEngine.Vector2 NormalizedScreenPosition { get => FullScreenInputHandler.NormalizePointer(ScreenPosition); }
			public int Id { get; private set; }
			public int FrameCount { get; private set; }

			public Pointer(int id)
			{
				Id = id;
			}

			public void Update(UnityEngine.Vector2 screenPosition, int frameCount)
			{
				ScreenPosition = screenPosition;
				this.FrameCount = frameCount;
			}
		}

		[SerializeField] Camera attachedCamera;
		[SerializeField] GameObject receiver;
		[SerializeField] float distanceFromCamera;

		public UnityEngine.Vector2 NormalizedScreenPosition { get => NormalizePointer(ScreenPosition); }
		public UnityEngine.Vector2 ScreenPosition { get; private set; }
		public int FrameCount { get; private set; }
	
		public override Camera eventCamera{ get => attachedCamera; }

		public IEnumerable<Pointer> GetPointers()
		{
			if (pointers == null)
			{
				pointers = new Dictionary<int, Pointer>();
			}

			foreach (var pair in pointers)
			{
				yield return pair.Value;
			}
		}

		public Pointer GetPointer(int id)
		{
			if (pointers == null)
			{
				pointers = new Dictionary<int, Pointer>();
			}

			pointers.TryGetValue(id, out var pointer);
			return pointer;
		}

		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			var p = eventData.position;
			if ((p.x >= 0f) && (p.x < Screen.width) && (p.y >= 0f) && (p.y < Screen.height))
			{
				var cameraTransform = attachedCamera.transform;
				var result = new RaycastResult
				{
					gameObject = receiver,
					module = this,
					distance = distanceFromCamera,
					worldPosition = cameraTransform.position + (cameraTransform.forward * distanceFromCamera),
					worldNormal = -cameraTransform.forward,
					screenPosition = eventData.position,
					index = resultAppendList.Count,
					sortingLayer = 0,
					sortingOrder = 0
				};
				resultAppendList.Add(result);

				ScreenPosition = eventData.position;

				if (pointers == null)
				{
					pointers = new Dictionary<int, Pointer>();
				}

//Debug.Log("FSIH: " + eventData.pointerId + " " + eventData.position);
				Pointer pointer;
				if (pointers.TryGetValue(eventData.pointerId, out pointer))
				{
//Debug.Log("\tUpdate");
					pointer.Update(eventData.position, Time.frameCount);
				}
				else
				{
//Debug.Log("\tCreate");
					pointer = new Pointer(eventData.pointerId);
					pointer.Update(eventData.position, Time.frameCount);
					pointers.Add(eventData.pointerId, pointer);
				}
			}
		}

		// BaseRaycaster.RaycstがEventSystems.Updateで来るのでUpdate内の順序が定まらない。やむをえずLateUpdateに回す
		public void ManualLateUpdate()
		{
			var currentFrame = Time.frameCount;
			var removed = new List<int>();
			foreach (var pointer in pointers)
			{
				if (pointer.Value.FrameCount < currentFrame)
				{
					removed.Add(pointer.Key);
				}
			}

			foreach (var id in removed)
			{
				pointers.Remove(id);
//				Debug.Log("PointerRemove: " + id);
			}
		}

		public static UnityEngine.Vector2 NormalizePointer(UnityEngine.Vector2 screenPosition)
		{
			var vmin = Mathf.Min(Screen.height, Screen.width);
			return screenPosition / (float)vmin;
		}

		// non public ----
		Dictionary<int, Pointer> pointers;

#if UNITY_EDITOR
		[UnityEditor.CustomEditor(typeof(FullScreenInputHandler))]
		class CustomEditor : Editor
		{
			public override void OnInspectorGUI()
			{
				var self = target as FullScreenInputHandler;
				base.OnInspectorGUI();

				EditorGUILayout.LabelField("NP", self.NormalizedScreenPosition.ToString());
				EditorGUILayout.LabelField("P", self.ScreenPosition.ToString());
			}
		}
#endif
	}
}
