using System.Collections;
using System.Collections.Generic;
using DigitalRubyShared;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerControl : MonoBehaviour
{
    private int activeSceneIndex;
    private SwipeGestureRecognizer swipeRightGesture;
    private SwipeGestureRecognizer swipeLeftGesture;

    // Use this for initialization
    void Start()
    {
        activeSceneIndex = 0;
        CreateSwipeLeftGesture();
        CreateSwipeRightGesture();
        DontDestroyOnLoad(this.gameObject);
    }

	void Update()
	{
		if(Input.GetKeyUp(KeyCode.LeftArrow))
			PreviousScene();
		if(Input.GetKeyUp(KeyCode.RightArrow))
			NextScene();
	}

    private void CreateSwipeLeftGesture()
    {
        swipeLeftGesture = new SwipeGestureRecognizer();
        swipeLeftGesture.Direction = SwipeGestureRecognizerDirection.Left;
        swipeLeftGesture.StateUpdated += SwipeLeftGestureCallback;
        swipeLeftGesture.DirectionThreshold = 1.0f; // allow a swipe, regardless of slope
        FingersScript.Instance.AddGesture(swipeLeftGesture);
    }

    private void CreateSwipeRightGesture()
    {
        swipeRightGesture = new SwipeGestureRecognizer();
        swipeRightGesture.Direction = SwipeGestureRecognizerDirection.Right;
        swipeRightGesture.StateUpdated += SwipeRightGestureCallback;
        swipeRightGesture.DirectionThreshold = 1.0f; // allow a swipe, regardless of slope
        FingersScript.Instance.AddGesture(swipeRightGesture);
    }

    private void SwipeLeftGestureCallback(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended)
        {
            HandleSwipe(gesture.FocusX, gesture.FocusY, true);
        }
    }

    private void SwipeRightGestureCallback(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended)
        {
            HandleSwipe(gesture.FocusX, gesture.FocusY, false);
        }
    }

    private void HandleSwipe(float endX, float endY, bool isLeft)
    {
        if (isLeft)
        {
			NextScene();
        }
        else
        {
			PreviousScene();
        }
    }

    private void NextScene()
    {
        if (activeSceneIndex < SceneManager.sceneCountInBuildSettings -1)
            activeSceneIndex++;
        else
            activeSceneIndex = 1;
        SceneManager.LoadScene(activeSceneIndex);
    }

    private void PreviousScene()
    {
        if (activeSceneIndex > 1)
            activeSceneIndex--;
        else
            activeSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
        SceneManager.LoadScene(activeSceneIndex);
    }

}
