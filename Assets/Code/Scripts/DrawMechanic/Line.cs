using UnityEngine;

namespace DrawMechanic
{
    public class Line : MonoBehaviour
    {
        #region Fields
        [SerializeField] private LineRenderer _lineRenderer;
        #endregion

        #region Methods
        public void SetPosition(Vector2 position)
        {
            if (!CanAppend(position)) return;

            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, position);
        }

        public void ResetLineRenderer()
        {
            _lineRenderer.positionCount = 0;
        }

        public void GetPoints(Vector3[] lineRendererPositions)
        {
            _lineRenderer.GetPositions(lineRendererPositions);
        }

        public int GetPointsCount()
        {
            return _lineRenderer.positionCount;
        }

        private bool CanAppend(Vector2 position)
        {
            if (_lineRenderer.positionCount == 0) return true;

            return Vector2.Distance(_lineRenderer.GetPosition(_lineRenderer.positionCount - 1), position) > DrawManager.UNIT_DISTANCE;
        }

        #endregion
    }
}