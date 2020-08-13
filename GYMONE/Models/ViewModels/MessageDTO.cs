using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GYMONE.Models.ViewModels
{
    [Table("tblmessage")]
    public class MessageDTO
    {
        [Key]
        public int Id { get; set; }
        public long senderid { get; set; }
        public long receiverid { get; set; }
        public DateTime? date { get; set; }
        public string message { get; set; }
        public bool? Read { get; set; }
    }
}