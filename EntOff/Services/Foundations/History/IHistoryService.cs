using EntOff.Models.Entities.History;
namespace EntOff.Services.Foundations.History
{
    public interface IHistoryService
    {
        ValueTask<Histo> CreateHistoryAsync(Histo hist);
        IQueryable<Histo> GetHistoryAsync();
        
    }
}
