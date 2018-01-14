namespace CommonContracts
{
    public interface IGlobalSettings
    {
        string MigrationAssemblyName { get; }
        string DefaultConnectionString { get; }
    }
}