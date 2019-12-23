using GhostChat.Data;
using GhostChat.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace GhostChat.BusinessLogic
{
    public class FriendshipManager
    {
        private IRepository<Friendship> friendships;
        public FriendshipManager(IRepository<Friendship> friendships)
        {
            this.friendships = friendships;
        }

        public List<User> GetFriendsList(User user)
        {
            List<User> friends = friendships.GetAll()
                    .Where(x => x.RequestingUser.Id == user.Id && x.AreFriends)
                    .Select(x => x.AcceptingUser)
                    .ToList();

            friends.AddRange(friendships.GetAll()
                .Where(x => x.AcceptingUser.Id == user.Id && x.AreFriends)
                .Select(x => x.RequestingUser)
                .ToList());

            return friends;
        }

        public void RemoveFriendship(User firstUser, User secondUser)
        {
            List<Friendship> friendshipSearch = friendships.GetAll()
                    .Where(x => x.RequestingUser.Id == firstUser.Id && x.AcceptingUser.Id == secondUser.Id)
                    .ToList();

            friendshipSearch.AddRange(friendships.GetAll()
                .Where(x => x.RequestingUser.Id == secondUser.Id && x.AcceptingUser.Id == firstUser.Id)
                .ToList());

            Friendship friendshipToRemove = friendshipSearch.First();
            friendships.Remove(friendshipToRemove);
        }
    }
}
