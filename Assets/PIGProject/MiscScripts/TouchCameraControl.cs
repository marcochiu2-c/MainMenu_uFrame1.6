using UnityEngine;
using System.Collections;

public class TouchCameraControl : MonoBehaviour 
{
	public float moveSensitivityX = 1.0f;
	public float moveSensitivityY = 1.0f;
	public bool updateZoomSensitivity = true;
	public float orthoZoomSpeed = 10.0f;
	public float minZoom = 20.0f;
	public float maxZoom = 537.0f;
	public bool invertMoveX = false;
	public bool invertMoveY = false;
	public float mapWidth = 40.0f;
	public float mapHeight = 40.0f;

	private Transform _transform;
	private Camera _camera;
	private float horizontalExtent, verticalExtent;
	private float minX, maxX, minY, maxY;
	
	void Start()
	{
		_camera = Camera.main;

		CalculateMapBounds ();
	}

	void Update()
	{
		if(updateZoomSensitivity)
		{
			moveSensitivityX = _camera.orthographicSize / 3.0f;
			moveSensitivityY = _camera.orthographicSize / 3.0f;
		}

		Touch[] touches = Input.touches;

		if (touches.Length > 0)
		{
			//Single touch
			if (touches.Length == 1)
			{
				if(touches[0].phase == TouchPhase.Moved)
				{
					Vector2 delta = touches[0].deltaPosition;

					float positionX = delta.x * moveSensitivityX * Time.deltaTime;
					positionX = invertMoveX ? positionX : positionX * -1;

					float positionY = delta.y * moveSensitivityY * Time.deltaTime;
					positionY = invertMoveY ? positionY : positionY * -1;
					 
					_camera.transform.position += new Vector3(positionX, positionY, 0);
					
				}
			}

			//Double touch(zoom)
			if (touches.Length == 2)
			{
				Touch touchOne = touches[0];
				Touch touchTwo = touches[1];

				Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
				Vector2 touchTwoPrevPos = touchTwo.position - touchTwo.deltaPosition;

				float prevTouchDeltaMag = (touchOnePrevPos - touchTwoPrevPos).magnitude;
				float touchDeltaMag = (touchOne.position - touchTwo.position).magnitude;

				float deltaMagDiff = prevTouchDeltaMag - touchDeltaMag;

				_camera.orthographicSize += deltaMagDiff * orthoZoomSpeed;
				_camera.orthographicSize = Mathf.Clamp (_camera.orthographicSize, minZoom, maxZoom);
			
				CalculateMapBounds ();
			}
		}
	}

	void CalculateMapBounds() 
	{
		verticalExtent = _camera.orthographicSize;
		horizontalExtent = verticalExtent * _camera.aspect;
		minX = horizontalExtent - mapWidth/2.0f;
		maxX = mapWidth/2.0f - horizontalExtent;
		minY = verticalExtent - mapHeight/2.0f;
		maxY = mapHeight/2.0f - verticalExtent;
	}

	void LateUpdate()
	{
		Vector3 limitedCameraPosition = _camera.transform.position;
		limitedCameraPosition.x = Mathf.Clamp (limitedCameraPosition.x, minX, maxX);
		limitedCameraPosition.y = Mathf.Clamp (limitedCameraPosition.y, minY, maxY);
		_camera.transform.position = limitedCameraPosition;
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube (Vector3.zero, new Vector3 (mapWidth, mapHeight, 0));
	}
}