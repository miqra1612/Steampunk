using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{

    public SceneController sceneController;
    public RectTransform intro2;
    public float tweenDelay = 1f;
    public Button plugIn;

    // Start is called before the first frame update

    private void Start()
    {
        plugIn.onClick.AddListener(() => sceneController.OpenScene("Map"));
    }

    private void OpenIntro2()
    {
        intro2.DOAnchorPos(Vector2.zero, tweenDelay);
    }
}
