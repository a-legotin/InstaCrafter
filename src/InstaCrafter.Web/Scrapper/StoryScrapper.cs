using System.Threading.Tasks;
using AutoMapper;
using InstaBackup.Mapper;
using InstaBackup.Models;
using InstaSharper.API;

namespace InstaBackup.Scrapper
{
    public class StoryScrapper : IScrapper<InstaStory>
    {
        private readonly IInstaApi _instaApi;

        public StoryScrapper(IInstaApi instaApi)
        {
            _instaApi = instaApi;
        }
        public async Task<InstaStory> Scrap(string username)
        {
            var user = await _instaApi.GetUserAsync(username);
            var story = await _instaApi.GetUserStoryAsync(long.Parse(user.Value.Pk));
            var storyToBackup = MapperInternal.Instance.Map<InstaSharper.Classes.Models.InstaStory, InstaStory>(story.Value);
            return storyToBackup;
        }
    }
}