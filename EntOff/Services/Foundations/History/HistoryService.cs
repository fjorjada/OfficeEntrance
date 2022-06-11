using EntOff.Api.Entrance.Storages;
using EntOff.Models.Entities.History;
using EntOff.Services.Foundations.History;

namespace EntOff.Api.Services.Foundations.Tags
{
    public partial class HistoryService : IHistoryService
    {
        private readonly IStorageEntrance storageEntrance;

        public HistoryService(IStorageEntrance storageEntrance)
        {
            this.storageEntrance = storageEntrance;
        }
        public async ValueTask<Histo> CreateHistoryAsync(Histo his)
        {
            
            await this.storageEntrance.InsertHistoryAsync(his);
            return his;
        }
        public IQueryable<Histo> GetHistoryAsync()
        {
            var history = this.storageEntrance.SelectAllHistory();

            return history;
        }
    }
}