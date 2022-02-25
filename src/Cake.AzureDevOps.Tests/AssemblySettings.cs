using Xunit;

// Disable parallel test execution inside this assembly, since there are tests
// relying on environment variables
[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly)]