using Backend.DTOs;

namespace Backend.services
{
    public interface IPostsService
    {
        public Task<IEnumerable<PostDto>> Get();
    }
}
