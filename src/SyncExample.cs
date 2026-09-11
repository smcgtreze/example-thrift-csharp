using Bus;
using Microsoft.Extensions.Logging.Abstractions;
using Thrift;
using Thrift.Protocol;
using Thrift.Server;
using Thrift.Transport.Client;
using Thrift.Transport.Server;

internal static class SyncExample
{
	private const int Port = 9090;
    private const int DelayMilliseconds = 100;
    private const int ClientTimeoutMilliseconds = 0;
    private const string Host = "127.0.0.1";

	public static void Run()
	{
		using var cancellationTokenSource = new CancellationTokenSource();
		var server = new TSimpleAsyncServer(
			new BusService.AsyncProcessor(new BusServiceHandler()),
			new TServerSocketTransport(Port, new TConfiguration(), 100),
			new TBinaryProtocol.Factory(),
			new TBinaryProtocol.Factory(),
			NullLoggerFactory.Instance);

		var serverTask = server.ServeAsync(cancellationTokenSource.Token);
		Task.Delay(DelayMilliseconds).GetAwaiter().GetResult();

		using var clientTransport = new TSocketTransport(Host, Port, new TConfiguration(), ClientTimeoutMilliseconds);
		var client = new BusService.Client(new TBinaryProtocol(clientTransport));
		var status = client.getBusStatus(42).GetAwaiter().GetResult();
		Console.WriteLine($"Client received: {status}");

		cancellationTokenSource.Cancel();
		try
		{
		    serverTask.GetAwaiter().GetResult();
		}
		catch (OperationCanceledException)
		{
            Console.WriteLine("Server task was canceled.");
		}
	}
}
