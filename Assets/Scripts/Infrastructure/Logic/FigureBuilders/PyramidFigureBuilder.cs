using UnityEngine;

namespace Infrastructure.Logic
{
    public class PyramidFigureBuilder : FigureBuilderBase
    {
        protected override void GenerateFigure()
        {
            for (int y = 0; y <= _figureHeight; y++)
            {
                for (int x = -y; x < y; x++)
                {
                    for (int z = -y; z < y; z++)
                    {
                        Vector3 newPos = new Vector3(x, y, z);
                        CreateBlock(transform.position - newPos);
                    }
                }
            }
        }

        public override int GetHeight() =>
            _figureHeight * 2;

        public override int GetWidth() =>
            _figureHeight * 2 - 1;
    }
}