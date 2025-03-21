using System.Reflection;

namespace Kondaxi.Helpers.Runnables.Tests;

public class RunnerTests
{

    [Test]
    public void Test1()
    {
        // Act
        Runner.Run(Assembly.GetExecutingAssembly());


    }
}