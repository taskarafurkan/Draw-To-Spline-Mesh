using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DrawMechanic
{
    public class DrawManager : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        #region Fields

        // Allowed distance between last line location
        public const float UNIT_DISTANCE = 0.05f;

        [SerializeField] private Line _line;
        [SerializeField] private Camera _camera;

        [SerializeField] private RectTransform _topBoundRectTransform;
        [SerializeField] private RectTransform _bottomBoundRectTransform;
        [SerializeField] private RectTransform _leftBoundRectTransform;
        [SerializeField] private RectTransform _rightBoundRectTransform;

        private bool _pointerDown = false;
        private Vector3 _mousePosition;

        private Vector3[] _linePointsArray;

        public event Action<Vector3[]> OnDrawFinish;

        #endregion

        #region Methods
        public void OnPointerDown(PointerEventData eventData)
        {
            _line.ResetLineRenderer();
            _pointerDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _pointerDown = false;

            _linePointsArray = new Vector3[_line.GetPointsCount()];
            _line.GetPoints(_linePointsArray);

            OnDrawFinish?.Invoke(_linePointsArray);
        }

        private void Update()
        {
            if (_pointerDown)
            {
                // Convert input to world position
                _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

                // If input equal or greater than the bounds, restrict movement
                if (Input.GetMouseButton(0))
                {
                    if (_mousePosition.x <= _leftBoundRectTransform.position.x)
                    {
                        _mousePosition.x = _leftBoundRectTransform.position.x;
                    }

                    if (_mousePosition.x >= _rightBoundRectTransform.position.x)
                    {
                        _mousePosition.x = _rightBoundRectTransform.position.x;
                    }

                    if (_mousePosition.y <= _bottomBoundRectTransform.position.y)
                    {
                        _mousePosition.y = _bottomBoundRectTransform.position.y;
                    }

                    if (_mousePosition.y >= _topBoundRectTransform.position.y)
                    {
                        _mousePosition.y = _topBoundRectTransform.position.y;
                    }

                    _line.SetPosition(_mousePosition);
                }
            }
        }
        #endregion
    }
}