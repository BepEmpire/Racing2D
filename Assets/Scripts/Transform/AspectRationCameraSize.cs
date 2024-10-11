using UnityEngine;

namespace Game.Scripts.Game.Camera
{
	[RequireComponent(typeof(UnityEngine.Camera))]
	public class AspectRationCameraSize : MonoBehaviour
	{
		[Header("Settings")] 
		[SerializeField] private float cameraSize16To9 = 4.3f;
		
		[Tooltip("This is threshold value for Iphone 8, Iphone SE 2 etc.")]
		[SerializeField] private float aspectRatioThreshold16To9 = 1.8f;
		[SerializeField] private bool isVerticalScreen = true;

		private UnityEngine.Camera _camera;

		private void Awake()
		{
			_camera = GetComponent<UnityEngine.Camera>();
		}

		private void Start()
		{
			float aspectRatio = 0;

			if (isVerticalScreen)
			{
				aspectRatio = (float)Screen.height / Screen.width;
			}
			else
			{
				aspectRatio = (float)Screen.width / Screen.height;
			}
			
			Debug.Log($"Screen Width: {Screen.width}, Height: {Screen.height}. " +
			          $"Current aspect ratio is: {aspectRatio}");
			
			if (aspectRatio > aspectRatioThreshold16To9) return;

			_camera.orthographicSize = cameraSize16To9;
		}
	}
}