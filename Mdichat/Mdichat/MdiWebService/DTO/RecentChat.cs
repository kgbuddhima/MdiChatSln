﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Mdichat.MdiWebService.DTO
{
    public class RecentChat
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string LastMessage { get; set; }
        public DateTime MessageDate { get; set; }
        public bool IsInComming { get; set; }
        public byte[] UserImage { get; set; }
        public int SenderId { get; set; }
        public byte[] FileData { get; set; }
        public string FileName { get; set; }
        public bool IsFile { get; set; }
        public string ImageFilePath { get; set; }


    }
}
