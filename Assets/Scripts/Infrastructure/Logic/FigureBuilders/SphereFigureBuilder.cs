using UnityEngine;

namespace Infrastructure.Logic
{
    public class SphereFigureBuilder : FigureBuilderBase
    {
        private const int BLOCK_SIZE = 1;
        
        protected override void GenerateFigure()
        {
            for (int x = 0; x < _figureHeight; x++)
            {
                for (int y = 0; y < _figureHeight; y++)
                {
                    for (int z = 0; z < _figureHeight; z++)
                    {
                        float radius = _figureHeight / 2f;
                        Vector3 position = new Vector3(
                            x * BLOCK_SIZE - radius,
                            y * BLOCK_SIZE - radius,
                            z * BLOCK_SIZE - radius
                        );

                        if (Vector3.Distance(position, Vector3.zero) <= radius)
                            CreateBlock(position + transform.position);
                    }
                }
            }
        }

        public override int GetHeight() =>
            _figureHeight;

        public override int GetWidth() =>
            _figureHeight;
    }
}