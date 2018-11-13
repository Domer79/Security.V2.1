namespace Security.CommonContracts
{
    public interface IGlobalSettings
    {
        string MigrationAssemblyName { get; }
        string DefaultConnectionString { get; }
        string SecurityServerAddress { get; }
    }
}