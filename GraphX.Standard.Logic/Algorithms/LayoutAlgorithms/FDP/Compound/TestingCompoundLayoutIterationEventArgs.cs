using System.Collections.Generic;
using GraphX.Measure;
using QuikGraph;

namespace GraphX.Logic.Algorithms.LayoutAlgorithms
{
    public class TestingCompoundLayoutIterationEventArgs<TVertex, TEdge, TVertexInfo, TEdgeInfo>
        : CompoundLayoutIterationEventArgs<TVertex, TEdge>, ILayoutInfoIterationEventArgs<TVertex, TEdge, TVertexInfo, TEdgeInfo>
        where TVertex : class 
        where TEdge : IEdge<TVertex>
    {
        public Point GravitationCenter { get; private set; }

        public TestingCompoundLayoutIterationEventArgs(
            int iteration, 
            double statusInPercent, 
            string message, 
            IDictionary<TVertex, Point> vertexPositions, 
            IDictionary<TVertex, Size> innerCanvasSizes,
            IDictionary<TVertex, TVertexInfo> vertexInfos,
            Point gravitationCenter) 
            : base(iteration, statusInPercent, message, vertexPositions, innerCanvasSizes)
        {
            VertexInfos = vertexInfos;
            GravitationCenter = gravitationCenter;
        }

        public override object GetVertexInfo(TVertex vertex)
        {
            if (VertexInfos.TryGetValue(vertex, out var info))
                return info;

            return null;
        }

        public IDictionary<TVertex, TVertexInfo> VertexInfos { get; }

        public IDictionary<TEdge, TEdgeInfo> EdgeInfos => null;
    }
}
