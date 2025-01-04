using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoTapController : MonoBehaviour
{
    private VideoPlayer videoPlayer;
     public Camera arCamera;

    private void Start()
    {
        var boxCollider = GetComponent<BoxCollider>();
        arCamera = Vuforia.VuforiaBehaviour.Instance.transform.GetComponentInChildren<Camera>();

        if (boxCollider != null)
        {
            Debug.Log($"Collider bounds: {boxCollider.bounds}");
        }
        else
        {
            Debug.LogError("BoxCollider not found on this GameObject.");
        }

        Debug.Log($"{this.name}: VideoTapController script is running.");

        videoPlayer = GetComponentInChildren<VideoPlayer>(true);

        if (videoPlayer == null)
        {
            Debug.LogError("No VideoPlayer component found!");
        }
        else
        {
            Debug.Log("VideoPlayer component successfully found.");
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse input detected.");
            HandleTap(Input.mousePosition);
        }
#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("Touch input detected.");
            HandleTap(Input.GetTouch(0).position);
        }
#endif
    }

    private void HandleTap(Vector3 inputPosition)
    {
         if (arCamera == null)
        {
            Debug.LogError("AR Camera is not assigned!");
            return;
        }else{
             Debug.LogError("AR Camera is assigned!");
        }

       // Ray ray = Camera.main.ScreenPointToRay(inputPosition);
        Ray ray = arCamera.ScreenPointToRay(inputPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log($"Raycast hit: {hit.transform.name}");
            if (hit.transform == transform)
            {
                ToggleVideoPlayback();
            }
        }
    }



    private void ToggleVideoPlayback()
    {
        if (videoPlayer == null) return;

        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            Debug.Log("Video Paused");
        }
        else
        {
            videoPlayer.Play();
            Debug.Log("Video Playing");
        }
    }
}

//     private void HandleTap(Vector3 inputPosition)
// {
//     Ray ray = Camera.main.ScreenPointToRay(inputPosition);

//     // Visualize the ray
//     Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);

//     if (Physics.Raycast(ray, out RaycastHit hit))
//     {
//         Debug.Log($"Raycast hit: {hit.transform.name}");
//         if (hit.transform == transform)
//         {
//             ToggleVideoPlayback();
//         }
//         else
//         {
//             Debug.Log("Raycast hit a different object.");
//         }
//     }
//     else
//     {
//         Debug.Log("Raycast did not hit any object.");
//     }
// }

// public class VideoTapController : MonoBehaviour
// {
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }
