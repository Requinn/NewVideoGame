using UnityEngine;
using System.Collections;

public class PlatformHandlerMain : MonoBehaviour {
    public GameObject[] platforms;
    public int platformCount = 0;
    public float decayTime;
    private GameObject _currentPlatform, _newPlatform;
    private GameObject _newEndPoint, _newStartPoint;
    private Vector3 _attachPoint, _platformModelSize;
    private Vector3 _platformCenterDist;
    private bool _platCleared;
	// Use this for initialization
	void Start () {
        //_attachPoint is the endpoint of the platforms;
        _currentPlatform = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        //X recieve trigger signal (shoot a thing, interact with a thing, etc)
        //X locate the EndPoint of a platform
        //X take half the length of a platform ahead of the current one's X or Y w/e
        //X place it
        //begin decay of the old platform
        //repeat
        if (_platCleared)
        {
            //get new platform from the pool
            _newPlatform = GenerateRandomPlatform(); 

            //new platform's endpoint and start points
            _newEndPoint = _newPlatform.transform.GetChild(1).gameObject; 
            _newStartPoint = _newPlatform.transform.GetChild(0).gameObject;

            //calculate the distance we should place our new platform from the edge of the current one using those points
            _platformCenterDist = new Vector3((_newEndPoint.transform.localPosition.x - _newStartPoint.transform.localPosition.x)/2, 0 , 0);
            
            //get the attach point of the current platform
            _attachPoint = _currentPlatform.transform.GetChild(1).transform.position;

            GameObject platform = (GameObject)Instantiate(_newPlatform, _attachPoint + _platformCenterDist, GetComponentInParent<Transform>().rotation);
            
            //make our new platform the current one
            _currentPlatform = platform;

            platformCount++;
            //reset value so we can clear the new platform's objective again
            _platCleared = false;
        }
    }

    private void activate(float signal)
    {
        _platCleared = true;
    }

    private GameObject GenerateRandomPlatform()
    {
        //cast a randomized float to int within the platform array's size
        int i = (int)Random.Range(0, platforms.Length);
        return platforms[i];
    }
}
