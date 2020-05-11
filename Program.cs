using Encog.ML.Bayesian;
using Encog.ML.Bayesian.Query.Enumeration;
using System;

namespace BayesianTaxiProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            BayesianNetwork network = new BayesianNetwork();
            BayesianEvent UberDriver = network.CreateEvent("uber_driver");
            BayesianEvent WitnessSawUberDriver = network.CreateEvent("saw_uber_driver");
            network.CreateDependency(UberDriver, WitnessSawUberDriver);
            network.FinalizeStructure();
            UberDriver?.Table?.AddLine(0.85, true);
            WitnessSawUberDriver?.Table?.AddLine(0.80, true, true);
            WitnessSawUberDriver?.Table?.AddLine(0.20, true, false);
            network.Validate();
            Console.WriteLine(network.ToString());
            Console.WriteLine($"Parameter count: {network.CalculateParameterCount()}");
            EnumerationQuery query = new EnumerationQuery(network);
            query.DefineEventType(WitnessSawUberDriver, EventType.Evidence);
            query.DefineEventType(UberDriver, EventType.Outcome);
            query.SetEventValue(WitnessSawUberDriver, false);
            query.SetEventValue(UberDriver, false);
            query.Execute();
            Console.WriteLine(query.ToString());
        }
    }
}
