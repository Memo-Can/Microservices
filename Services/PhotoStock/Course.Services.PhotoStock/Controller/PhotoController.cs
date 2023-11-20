using Course.Services.PhotoStock.Dto;
using Course.Shared.Controllers;
using Course.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.Services.PhotoStock.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : CustomBaseController
    {

        //CancelationToken kullanılmasının sebebi dosya yüklenirken browser kapandığında arka planda yüklemeyi durdurmak için kullnıyoruz.
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile formFile, CancellationToken cancellationToken)
        {
            if (formFile == null || formFile.Length == 0)
                return CreateActionResultInstance(Response<PhotoDto>.Fail("Photo Empty", 400));

            using var stream = new FileStream(GetPath(formFile.FileName), FileMode.Create);
            await formFile.CopyToAsync(stream, cancellationToken);

            return CreateActionResultInstance(Response<PhotoDto>.Success( new PhotoDto 
            { 
                Url= "photos/" + formFile.FileName
            }, 
            200));
        }

        public IActionResult PhotoDelete(string photoUrl)
        {


            if (!System.IO.File.Exists(GetPath(photoUrl)))
                return CreateActionResultInstance(Response<NoContent>.Fail("Photo Not Found", 404));

            System.IO.File.Delete(GetPath(photoUrl));

            return CreateActionResultInstance(Response<NoContent>.Success(204));

        }

        private string GetPath(string url)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", url);
        }
    }
}
