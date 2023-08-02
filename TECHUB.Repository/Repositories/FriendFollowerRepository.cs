using Microsoft.EntityFrameworkCore;
using TECHUB.Repository.Context;
using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;

namespace TECHUB.Repository.Repositories
{
    public class FriendFollowerRepository : IFriendFollowerRepository
    {
        private readonly DatabaseContext context;

        public FriendFollowerRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<FriendFollower>> GetUserFriends(int id)
        {          
            var result = await context.FriendFollowers
                .Where(x => x.UserId == id && x.Type == 1 || x.OtherUserId == id && x.Type == 1)
                .Select(f => new
                {
                    UserId = f.UserId,
                    OtherUserId = f.OtherUserId,
                    Type = f.Type,
                    Date = f.Date,
                    User = new
                    {
                        UserId = f.User!.UserId,
                        DisplayName = f.User.DisplayName,
                        Picture = new
                        {
                            PictureId = f.User.Picture!.PictureId,
                            ImageUrl = f.User.Picture.ImageUrl,
                        }
                    },
                    OtherUser = new
                    {
                        UserId = f.OtherUser!.UserId,
                        DisplayName = f.OtherUser.DisplayName,
                        Picture = new
                        {
                            PictureId = f.OtherUser.Picture!.PictureId,
                            ImageUrl = f.OtherUser.Picture.ImageUrl,
                        }
                    },
                })
                .ToListAsync();

            var friends = new List<FriendFollower>();
            foreach (var item in result)
            {
                friends.Add(new FriendFollower()
                {
                    UserId = item.UserId,
                    OtherUserId = item.OtherUserId,
                    Type = item.Type,
                    Date = item.Date,
                    User = new User()
                    {
                        UserId = item.User.UserId,
                        DisplayName= item.User.DisplayName,
                        Picture = new Picture()
                        {
                            PictureId = item.User.Picture!.PictureId,
                            ImageUrl= item.User.Picture.ImageUrl,
                        }
                    },
                    OtherUser = new User()
                    {
                        UserId = item.OtherUser.UserId,
                        DisplayName = item.OtherUser.DisplayName,
                        Picture = new Picture()
                        {
                            PictureId = item.OtherUser.Picture!.PictureId,
                            ImageUrl = item.OtherUser.Picture.ImageUrl,
                        }
                    }
                });
            }
            return friends;
        }

        public async Task<List<FriendFollower>> GetUserFollowers(int id)
        {
            var result = await context.FriendFollowers
                .Where(x => x.UserId != id && x.Type == 2 && x.OtherUserId == id)
                .Select(f => new
                {
                    UserId = f.UserId,
                    OtherUserId = f.OtherUserId,
                    Type = f.Type,
                    Date = f.Date,
                    User = new
                    {
                        UserId = f.User!.UserId,
                        DisplayName = f.User.DisplayName,
                        Picture = new
                        {
                            PictureId = f.User.Picture!.PictureId,
                            ImageUrl = f.User.Picture.ImageUrl,
                        }
                    },
                    OtherUser = new
                    {
                        UserId = f.OtherUser!.UserId,
                        DisplayName = f.OtherUser.DisplayName,
                        Picture = new
                        {
                            PictureId = f.OtherUser.Picture!.PictureId,
                            ImageUrl = f.OtherUser.Picture.ImageUrl,
                        }
                    },
                })
                .ToListAsync();

            var friends = new List<FriendFollower>();
            foreach (var item in result)
            {
                friends.Add(new FriendFollower()
                {
                    UserId = item.UserId,
                    OtherUserId = item.OtherUserId,
                    Type = item.Type,
                    Date = item.Date,
                    User = new User()
                    {
                        UserId = item.User.UserId,
                        DisplayName = item.User.DisplayName,
                        Picture = new Picture()
                        {
                            PictureId = item.User.Picture!.PictureId,
                            ImageUrl = item.User.Picture.ImageUrl,
                        }
                    },
                    OtherUser = new User()
                    {
                        UserId = item.OtherUser.UserId,
                        DisplayName = item.OtherUser.DisplayName,
                        Picture = new Picture()
                        {
                            PictureId = item.OtherUser.Picture!.PictureId,
                            ImageUrl = item.OtherUser.Picture.ImageUrl,
                        }
                    }
                });
            }
            return friends;
        }

        public async Task<List<FriendFollower>> GetUserFollowing(int id)
        {
            var result = await context.FriendFollowers
                .Where(x => x.UserId == id && x.Type == 2 && x.OtherUserId != id)
                .Select(f => new
                {
                    UserId = f.UserId,
                    OtherUserId = f.OtherUserId,
                    Type = f.Type,
                    Date = f.Date,
                    User = new
                    {
                        UserId = f.User!.UserId,
                        DisplayName = f.User.DisplayName,
                        Picture = new
                        {
                            PictureId = f.User.Picture!.PictureId,
                            ImageUrl = f.User.Picture.ImageUrl,
                        }
                    },
                    OtherUser = new
                    {
                        UserId = f.OtherUser!.UserId,
                        DisplayName = f.OtherUser.DisplayName,
                        Picture = new
                        {
                            PictureId = f.OtherUser.Picture!.PictureId,
                            ImageUrl = f.OtherUser.Picture.ImageUrl,
                        }
                    },
                })
                .ToListAsync();

            var friends = new List<FriendFollower>();
            foreach (var item in result)
            {
                friends.Add(new FriendFollower()
                {
                    UserId = item.UserId,
                    OtherUserId = item.OtherUserId,
                    Type = item.Type,
                    Date = item.Date,
                    User = new User()
                    {
                        UserId = item.User.UserId,
                        DisplayName = item.User.DisplayName,
                        Picture = new Picture()
                        {
                            PictureId = item.User.Picture!.PictureId,
                            ImageUrl = item.User.Picture.ImageUrl,
                        }
                    },
                    OtherUser = new User()
                    {
                        UserId = item.OtherUser.UserId,
                        DisplayName = item.OtherUser.DisplayName,
                        Picture = new Picture()
                        {
                            PictureId = item.OtherUser.Picture!.PictureId,
                            ImageUrl = item.OtherUser.Picture.ImageUrl,
                        }
                    }
                });
            }
            return friends;
        }


        public async Task<List<FriendFollower>> GetBlockedUsers(int id)
        {
            var result = await context.FriendFollowers
                .Where(x => x.UserId == id && x.Type == 3 && x.OtherUserId != id)
                .Select(f => new
                {
                    UserId = f.UserId,
                    OtherUserId = f.OtherUserId,
                    Type = f.Type,
                    Date = f.Date,
                    User = new
                    {
                        UserId = f.User!.UserId,
                        DisplayName = f.User.DisplayName,
                        Picture = new
                        {
                            PictureId = f.User.Picture!.PictureId,
                            ImageUrl = f.User.Picture.ImageUrl,
                        }
                    },
                    OtherUser = new
                    {
                        UserId = f.OtherUser!.UserId,
                        DisplayName = f.OtherUser.DisplayName,
                        Picture = new
                        {
                            PictureId = f.OtherUser.Picture!.PictureId,
                            ImageUrl = f.OtherUser.Picture.ImageUrl,
                        }
                    },
                })
                .ToListAsync();

            var blockedUsers = new List<FriendFollower>();
            foreach (var item in result)
            {
                blockedUsers.Add(new FriendFollower()
                {
                    UserId = item.UserId,
                    OtherUserId = item.OtherUserId,
                    Type = item.Type,
                    Date = item.Date,
                    User = new User()
                    {
                        UserId = item.User.UserId,
                        DisplayName = item.User.DisplayName,
                        Picture = new Picture()
                        {
                            PictureId = item.User.Picture!.PictureId,
                            ImageUrl = item.User.Picture.ImageUrl,
                        }
                    },
                    OtherUser = new User()
                    {
                        UserId = item.OtherUser.UserId,
                        DisplayName = item.OtherUser.DisplayName,
                        Picture = new Picture()
                        {
                            PictureId = item.OtherUser.Picture!.PictureId,
                            ImageUrl = item.OtherUser.Picture.ImageUrl,
                        }
                    }
                });
            }
            return blockedUsers;
        }

        public async Task<List<FriendFollower>> GetBlockingUsers(int id)
        {
            var result = await context.FriendFollowers
                .Where(x => x.UserId != id && x.Type == 3 && x.OtherUserId == id)
                .Select(f => new
                {
                    UserId = f.UserId,
                    OtherUserId = f.OtherUserId,
                    Type = f.Type,
                    Date = f.Date,
                    User = new
                    {
                        UserId = f.User!.UserId,
                        DisplayName = f.User.DisplayName,
                        Picture = new
                        {
                            PictureId = f.User.Picture!.PictureId,
                            ImageUrl = f.User.Picture.ImageUrl,
                        }
                    },
                    OtherUser = new
                    {
                        UserId = f.OtherUser!.UserId,
                        DisplayName = f.OtherUser.DisplayName,
                        Picture = new
                        {
                            PictureId = f.OtherUser.Picture!.PictureId,
                            ImageUrl = f.OtherUser.Picture.ImageUrl,
                        }
                    },
                })
                .ToListAsync();

            var blockedUsers = new List<FriendFollower>();
            foreach (var item in result)
            {
                blockedUsers.Add(new FriendFollower()
                {
                    UserId = item.UserId,
                    OtherUserId = item.OtherUserId,
                    Type = item.Type,
                    Date = item.Date,
                    User = new User()
                    {
                        UserId = item.User.UserId,
                        DisplayName = item.User.DisplayName,
                        Picture = new Picture()
                        {
                            PictureId = item.User.Picture!.PictureId,
                            ImageUrl = item.User.Picture.ImageUrl,
                        }
                    },
                    OtherUser = new User()
                    {
                        UserId = item.OtherUser.UserId,
                        DisplayName = item.OtherUser.DisplayName,
                        Picture = new Picture()
                        {
                            PictureId = item.OtherUser.Picture!.PictureId,
                            ImageUrl = item.OtherUser.Picture.ImageUrl,
                        }
                    }
                });
            }
            return blockedUsers;
        }

        public async Task<List<FriendFollower>> GetUserFriendsFollowers(int id)
        {
            return await context.FriendFollowers.Where(f => f.UserId == id || f.OtherUserId == id).ToListAsync();
        }

        public async Task<FriendFollower> AddFriendFollower(FriendFollower ff)
        {
            context.FriendFollowers.Add(ff);
            await context.SaveChangesAsync();
            return ff;
        }

        public async Task<FriendFollower> UpdateFriendFollower(FriendFollower ff)
        {
            context.Entry(ff).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return ff;
        }

        public async Task<bool> DeleteFriendFollower(FriendFollower ff)
        {
            context.FriendFollowers.Remove(ff);
            await context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> FindUsers(int userid, int targetid)
        {
            bool userCheck = await context.Users.AnyAsync(x => x.UserId == userid);
            bool targetuserCheck = await context.Users.AnyAsync(x => x.UserId == targetid);
            if (userCheck && targetuserCheck)
            {
                return true;
            }
            return false;
        }
    }
}
