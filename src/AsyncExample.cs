using Bus;
using Microsoft.Extensions.Logging.Abstractions;
using Thrift;
using Thrift.Protocol;
using Thrift.Server;
using Thrift.Transport.Client;
using Thrift.Transport.Server;

internal static class AsyncExample
{
	private const int Port = 9090;
    private const int DelayMilliseconds = 100;
    private const string Host = "127.0.0.1";

	public static async Task Run()
	{
		using CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

		var server = new TSimpleAsyncServer(
			new BusService.AsyncProcessor(new BusServiceHandler()),
			new TServerSocketTransport(Port, new TConfiguration(), DelayMilliseconds),
			new TBinaryProtocol.Factory(),
			new TBinaryProtocol.Factory(),
			NullLoggerFactory.Instance);

		var serverTask = server.ServeAsync(cancellationTokenSource.Token);
		await Task.Delay(DelayMilliseconds);

		using var clientTransport = new TSocketTransport(Host, Port, new TConfiguration(), 0);
		var client = new BusService.Client(new TBinaryProtocol(clientTransport));
		const int request = 42;
		var response = await client.getBusStatus(request);
		Console.WriteLine($"Client received: {response}");

		cancellationTokenSource.Cancel();
		try
		{
			await serverTask;
		}
		catch (OperationCanceledException)
        {
            Console.WriteLine("Server task was canceled.");
        }
	}
}

// It is a class that:
//    internal: Can only be accessed from within the assembly it is defined (or friend assemblies).
//    sealed: Cannot be inherited.
internal sealed class BusServiceHandler : BusService.IAsync
{
	public Task<string> getBusStatus(int busId, CancellationToken cancellationToken = default)
	{
		return Task.FromResult($"Bus {busId} is operational");
	}
}
