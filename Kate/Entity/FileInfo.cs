﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Himeliya.Kate.Entity
{
    class FileInfo
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Size { get; set; }
        public string MD5 { get; set; }
        public string Url { get; set; }
        public string CreateDate { get; set; }
    }
}
