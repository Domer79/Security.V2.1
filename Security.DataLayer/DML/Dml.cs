using Security.Interfaces.V2.DataLayer;

namespace Security.DataLayer.DML
{
    public class Dml: IDml
    {
        private readonly IDmlCommandCollection _commandCollection;

        public Dml(IDmlCommandCollection commandCollection)
        {
            _commandCollection = commandCollection;
        }

        public void Add(ExecuteAction addFunc)
        {
            _commandCollection.Add(addFunc);
        }

        public void Update(ExecuteAction updateFunc)
        {
            _commandCollection.Add(updateFunc);
        }

        public void Delete(ExecuteAction deleteFunc)
        {
            _commandCollection.Add(deleteFunc);
        }
    }
}
