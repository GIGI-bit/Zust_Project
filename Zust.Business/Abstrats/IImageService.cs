﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zust.Business.Abstrats
{
    public interface IImageService
    {
        public Task<string> SaveFile(IFormFile file, string folderName);
    }
}
