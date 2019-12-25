using GhostChat.Data;
using GhostChat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GhostChat.BusinessLogic
{
    public class MessageManager
    {
        private ApplicationContext db;
        public MessageManager(ApplicationContext db)
        {
            this.db = db;
        }

        public List<MessagesItem> GetMessages(User user, User usersFriend)
        {
            List<MessagesItem> outgoingMessages = (from m in db.Messages
                                                   join um in db.UserMessages on m.Id equals um.MessageId
                                                   where m.Sender.Id == user.Id && um.UserID == usersFriend.Id
                                                   select new MessagesItem
                                                   {
                                                       Text = m.Text,
                                                       CreationTime = m.CreationTime,
                                                       Type = "Outgoing"
                                                   }).ToList();

            List<MessagesItem> incomingMessages = (from m in db.Messages
                                                   join um in db.UserMessages on m.Id equals um.MessageId
                                                   where m.Sender.Id == usersFriend.Id && um.UserID == user.Id
                                                   select new MessagesItem
                                                   {
                                                       Text = m.Text,
                                                       CreationTime = m.CreationTime,
                                                       Type = "Incoming"
                                                   }).ToList();

            List<MessagesItem> allMessages = outgoingMessages.Concat(incomingMessages)
                .OrderByDescending(x => x.CreationTime.Date)
                .ThenBy(x => x.CreationTime.TimeOfDay)
                .ToList();

            return allMessages;
        }
    }
}
