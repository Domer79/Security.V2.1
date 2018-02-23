namespace Security.V2.CommonContracts
{
    public interface IGlobalSettings
    {
        string MigrationAssemblyName { get; }
        string DefaultConnectionString { get; }
        string SecurityServerAddress { get; }
    }
}