namespace Security.Interfaces.V2.DataLayer
{
    public interface IDml
    {
        void Add(ExecuteAction addFunc);
        void Update(ExecuteAction updateFunc);
        void Delete(ExecuteAction deleteFunc);
    }
}