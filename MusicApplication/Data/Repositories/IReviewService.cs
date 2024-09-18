using MusicApplication.Models;

namespace MusicApplication.Data.Repositories
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllById(int id);
        Task<Review> GetByIdAsync(int id);
        void Add(Review review);
        Task<Review> UpdateAsync(Review review);
        Task DeleteAsync(int id);
        string GetCurrentUserId();
        string GetCurrentUserUsername(string id);
    }
}
