using UnityEngine;

namespace Infrastructure.Logic
{
    public class RectangleFigureBuilder : FigureBuilderBase
    {
        [SerializeField] private int _figureWidth;
        [SerializeField] private int _figureDepth;


        protected override void GenerateFigure()
        {
            for (int x = 0; x < _figureWidth; x++)
            {
                for (int y = 0; y < _figureHeight; y++)
                {
                    for (int z = 0; z < _figureDepth; z++)
                    {
                        Vector3 position = new Vector3(x,y,z);

                        CreateBlock(position + transform.position);
                    }
                }
            }
        }

        public override int GetHeight() =>
            _figureHeight;

        public override int GetWidth() =>
            _figureDepth > _figureWidth ? _figureDepth : _figureWidth;
    }
}