using System.Diagnostics;
using Transports;

internal static class SyncExample
{
	private const int Port = 8443;
	private const string CapiHost = "busmaps.com";
	private const string ApiKey = "825858535684745631cd5fef5c1626ee";

	public static void Run()
	{
		RunNextDeparturesExample();
	}

	private static void RunNextDeparturesExample()
	{
		var handler = new NextDeparturesServiceHandler(new HttpClient());
		var request = new NextDeparturesRequest
		{
			Location = "41.20731210697544, -8.561138923508015",
			CountryIso = "PRT",
			Key = new Key
			{
				CapiHost = CapiHost,
				ApiKey = ApiKey,
				Port = Port,
			}
		};

		try
        {
            var response = MeasureElapsedTime(() => handler.GetNextDepartures(request).GetAwaiter().GetResult(), out long elapsedMs);
            Console.WriteLine($"Next departures handler returned {response.GetType().Name} with {response.StopDepartures.Count} stops in {elapsedMs} ms.");
            printResponse(response);
        }
        catch (Exception ex)
		{
			Console.WriteLine($"Next departures sample call failed: {ex.Message}");
		}
    }

    private static T MeasureElapsedTime<T>(Func<T> predicate, out long elapsedMilliseconds)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        T response = predicate();
        stopwatch.Stop();
        elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
        
		return response;
    }
	
	static void printResponse(NextDeparturesResponse response)
	{
		foreach (var stop in response.StopDepartures)
		{
			Console.WriteLine($"Stop: {stop.StopName} ({stop.StopId})");
			foreach (var departure in stop.DepartureList)
			{
				Console.WriteLine($"  - {departure.DepartureTime} | {departure.RouteShortName} | {departure.TripHeadsign}");
			}
		}
	}
}
