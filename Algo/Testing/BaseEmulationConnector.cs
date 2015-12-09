namespace StockSharp.Algo.Testing
{
	using System;

	using StockSharp.BusinessEntities;
	using StockSharp.Messages;

	/// <summary>
	/// The base connection of emulation.
	/// </summary>
	public abstract class BaseEmulationConnector : Connector
	{
		/// <summary>
		/// Initialize <see cref="BaseEmulationConnector"/>.
		/// </summary>
		protected BaseEmulationConnector()
		{
			EmulationAdapter = new EmulationMessageAdapter(TransactionIdGenerator);
		}

		/// <summary>
		/// The adapter, executing messages in <see cref="IMarketEmulator"/>.
		/// </summary>
		public EmulationMessageAdapter EmulationAdapter
		{
			get; }

		/// <summary>
		/// Gets a value indicating whether the re-registration orders via the method <see cref="IConnector.ReRegisterOrder(StockSharp.BusinessEntities.Order,StockSharp.BusinessEntities.Order)"/> as a single transaction.
		/// </summary>
		public override bool IsSupportAtomicReRegister => EmulationAdapter.Emulator.Settings.IsSupportAtomicReRegister;

		/// <summary>
		/// To start the messages generating timer <see cref="TimeMessage"/> with the <see cref="Connector.MarketTimeChangedInterval"/> interval.
		/// </summary>
		protected override void StartMarketTimer()
		{
		}

		private void SendInGeneratorMessage(MarketDataGenerator generator, bool isSubscribe)
		{
			if (generator == null)
				throw new ArgumentNullException(nameof(generator));

			SendInMessage(new GeneratorMessage
			{
				IsSubscribe = isSubscribe,
				SecurityId = generator.SecurityId,
				Generator = generator,
				DataType = generator.DataType,
			});
		}

		/// <summary>
		/// To register the trades generator.
		/// </summary>
		/// <param name="generator">The trades generator.</param>
		public void RegisterTrades(TradeGenerator generator)
		{
			SendInGeneratorMessage(generator, true);
		}

		/// <summary>
		/// To delete the trades generator, registered earlier through <see cref="RegisterTrades"/>.
		/// </summary>
		/// <param name="generator">The trades generator.</param>
		public void UnRegisterTrades(TradeGenerator generator)
		{
			SendInGeneratorMessage(generator, false);
		}

		/// <summary>
		/// To register the order books generator.
		/// </summary>
		/// <param name="generator">The order books generator.</param>
		public void RegisterMarketDepth(MarketDepthGenerator generator)
		{
			SendInGeneratorMessage(generator, true);
		}

		/// <summary>
		/// To delete the order books generator, earlier registered through <see cref="RegisterMarketDepth"/>.
		/// </summary>
		/// <param name="generator">The order books generator.</param>
		public void UnRegisterMarketDepth(MarketDepthGenerator generator)
		{
			SendInGeneratorMessage(generator, false);
		}

		/// <summary>
		/// To register the orders log generator.
		/// </summary>
		/// <param name="generator">The orders log generator.</param>
		public void RegisterOrderLog(OrderLogGenerator generator)
		{
			SendInGeneratorMessage(generator, true);
		}

		/// <summary>
		/// To delete the orders log generator, earlier registered through <see cref="RegisterOrderLog"/>.
		/// </summary>
		/// <param name="generator">The orders log generator.</param>
		public void UnRegisterOrderLog(OrderLogGenerator generator)
		{
			SendInGeneratorMessage(generator, false);
		}
	}
}