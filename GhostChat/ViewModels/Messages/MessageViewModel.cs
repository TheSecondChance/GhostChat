using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GhostChat.ViewModels
{
    public class MessageViewModel
    {
        [Required]
        public Guid RecipientId { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
