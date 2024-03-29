using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace API.Interfaces
{
    public interface IPhotoService
    {
      public Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
      public Task<DeletionResult> DeletePhotoAsync(string publicId);

    }
}