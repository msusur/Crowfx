using Crow.Library.DatabaseLayer.DataAttributes;

namespace ConsoleApplication1.Repositories
{
    public interface IDataRepository
    {
        Data LoadDataById(int id);

        [Insert]
        void InsertData(Data data);

        [Update]
        void UpdateData(Data data);

        [Delete]
        void DeleteData(Data data);
    }
}