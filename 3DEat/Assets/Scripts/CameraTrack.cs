using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    public Transform player;

    [Header("追蹤速度"),Range(0.1f , 50.5f)]
    public float speed = 1.5f;


    private void Track()
    {
        Vector3 posTrack=player.position;
        posTrack.y += 3f;
        posTrack.z += -3f;

        Vector3 posCam = transform.position;

        posCam = Vector3.Lerp(posCam, posTrack, 0.5f * Time.deltaTime * speed);
        transform.position = posCam;
    }

    //需要追蹤事件寫在這
    private void LateUpdate()
    {
        Track();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
