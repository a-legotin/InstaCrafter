﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InstaBackup.Mapper;
using InstaBackup.Models;
using InstaSharper.API;

namespace InstaBackup.Scrapper
{
    public class FollowersScrapper : IScrapper<List<InstaUser>>
    {
        private readonly IInstaApi _instaApi;

        public FollowersScrapper(IInstaApi instaApi)
        {
            _instaApi = instaApi;
        }
        public async Task<List<InstaUser>> Scrap(string username)
        {
            var users = await _instaApi.GetUserFollowersAsync(username);
            var scrapped = new List<InstaUser>();
            foreach (var following in users.Value)
            {
                var user = MapperInternal.Instance.Map<InstaSharper.Classes.Models.InstaUserShort, InstaUser>(following);
                scrapped.Add(user);
            }
            return scrapped;
        }
    }
}