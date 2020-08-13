using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GYMONE.Models.ViewModels
{
    public class MessageVM
    {
        public MessageVM()
        {

        }

        public MessageVM(MessageDTO row)
        {
            Id = row.Id;
            senderid = row.senderid;
            receiverid = row.receiverid;
            date = row.date;
            message = row.message;
            Read = row.Read;

        }

        public int Id { get; set; }
        public long senderid { get; set; }
        public long receiverid { get; set; }
        public DateTime? date { get; set; }
        public string message { get; set; }
        public bool? Read { get; set; }

        public string sendername { get; set; }
        public string receivername { get; set; }
    }
}