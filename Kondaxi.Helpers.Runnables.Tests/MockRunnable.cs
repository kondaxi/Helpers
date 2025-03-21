namespace Kondaxi.Helpers.Runnables.Tests
{
    class MockRunnable : IRunnable
    {
        public static int RunCount { get; private set; }

        public void Run()
        {
            RunCount++;
        }
    }
}
