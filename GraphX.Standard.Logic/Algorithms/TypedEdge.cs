using QuikGraph;

namespace GraphX.Logic.Algorithms
{
	public enum EdgeTypes
	{
		General,
		Hierarchical
	}

	public interface ITypedEdge
	{
		EdgeTypes Type { get; }
	}

	public class TypedEdge<TVertex> : Edge<TVertex>, ITypedEdge
	{
        public EdgeTypes Type { get; }

        public TypedEdge(TVertex source, TVertex target, EdgeTypes type)
			: base(source, target)
		{
			Type = type;
		}

		public override string ToString()
		{
			return string.Format("{0}: {1}-->{2}", Type, Source, Target);
		}
	}
}