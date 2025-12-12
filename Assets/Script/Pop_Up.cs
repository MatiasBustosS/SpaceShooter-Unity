using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pop_Up : MonoBehaviour 
{ 
    [SerializeField] private RectTransform popup;
    [SerializeField] private float openDis = -25;
    [SerializeField] private float closeDis = -345;
    
    [SerializeField] private float moveSpeed  = 10;
    
    [SerializeField] private InputActionReference popUp;
    private bool isOpen = false;
    private Coroutine currentMove;

    private enum SideMove
    {
        Top,
        Bottom,
        Left,
        Right
    }
    
    [SerializeField] private SideMove sideMove;

    private void OnEnable()
    {
        popUp.action.Enable();
        popUp.action.started += OnPopUpPerformed;
    }

    private void OnDisable()
    {
        popUp.action.started -= OnPopUpPerformed;
        popUp.action.Disable();
    }

    private void OnPopUpPerformed(InputAction.CallbackContext context)
    {
        if (!isOpen) OpenPopUp();
        
        else ClosePopUp();
        
    }

    private void OpenPopUp()
    {
        isOpen = true;
        StartSmoothMove(GetTargetPosition(openDis));
    }

    private void ClosePopUp()
    {
        isOpen = false;
        StartSmoothMove(GetTargetPosition(closeDis));
    }
    
    private Vector2 GetCurrentPosition()
    {
        return sideMove == SideMove.Top || sideMove == SideMove.Bottom ? popup.offsetMax : popup.offsetMin;
    }
    
    private Vector2 GetTargetPosition(float distance)
    {
        Vector2 pos = popup.localPosition;

        switch (sideMove)
        {
            case SideMove.Top:
                pos.y = distance;
                break;
            case SideMove.Bottom:
                pos.y = distance;
                break;
            case SideMove.Left:
                pos.x = distance;
                break;
            case SideMove.Right:
                pos.x = distance;
                break;
        }

        return pos;
    }

    private void StartSmoothMove(Vector2 targetPosition)
    {
        if (currentMove != null)
            StopCoroutine(currentMove);

        currentMove = StartCoroutine(SmoothMoveCoroutine(popup.anchoredPosition, targetPosition));
    }

    private IEnumerator SmoothMoveCoroutine(Vector2 from, Vector2 to)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            popup.anchoredPosition = Vector2.Lerp(from, to, t);
            yield return null;
        }
        
        popup.anchoredPosition = to; 
        currentMove = null;
    }
}