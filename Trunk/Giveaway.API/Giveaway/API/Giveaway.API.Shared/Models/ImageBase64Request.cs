﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Giveaway.API.Shared.Models
{
    public class ImageBase64Request
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string File { get; set; }
    }
}