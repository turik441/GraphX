namespace GraphX.Logic.Algorithms
{
	public class WrappedVertex<TVertex>
	{
        public TVertex Original { get; }

        public WrappedVertex(TVertex original)
		{
			Original = original;
		}
	}
}