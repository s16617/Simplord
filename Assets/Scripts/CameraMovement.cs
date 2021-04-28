using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector2 velocity;
    public float smoothTimeY = 0.05f;
    public float smoothTimeX = 0.05f;

    public float shiftY = 2f;

    public GameObject player;

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    [SerializeField]
    public GameObject zoomOutPos;

    private Vector3 currentPos;

    private bool mapActive = false;

    public Camera mainCamera;
    [SerializeField]
    public Camera zoomOutCamera;

    public Canvas UI;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Hero");
        mainCamera = gameObject.GetComponent<Camera>();
        zoomOutCamera.enabled = false;
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (mapActive)
            {
                mapActive = false;
                ZoomIn();
            }
            else
            {
                mapActive = true;
                ZoomOut();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

        currentPos = new Vector3(posX, posY + shiftY, transform.position.z);


        if (!mapActive)
        {
            transform.position = currentPos;

            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
        }
    }

    public void ZoomIn()
    {
        mainCamera.enabled = true;
        zoomOutCamera.enabled = false;
        UI.worldCamera = mainCamera;
    }

    public void ZoomOut()
    {
        mainCamera.enabled = false;
        zoomOutCamera.enabled = true;
        UI.worldCamera = zoomOutCamera;
    }
}
