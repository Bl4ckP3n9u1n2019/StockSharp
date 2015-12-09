namespace SampleDiagram
{
	using System;

	using StockSharp.Xaml.Diagram;

	public enum CompositionType
	{
		Composition,
		Strategy
	}

	public class CompositionItem
	{
		public CompositionType Type { get; }

		public CompositionDiagramElement Element { get; }

		public CompositionItem(CompositionType type, CompositionDiagramElement element)
		{
			if (element == null)
				throw new ArgumentNullException(nameof(element));

			Type = type;
			Element = element;
		}
	}
}