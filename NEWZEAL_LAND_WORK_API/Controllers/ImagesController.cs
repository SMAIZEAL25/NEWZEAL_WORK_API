using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NEWZEAL_LAND_WORK_API.DTO;
using System.ComponentModel.DataAnnotations;
using NEWZEAL_LAND_WORK_API.CustomActionModelState;
using NEWZEAL_LAND_WORK_API.Domain_Models;
using NEWZEAL_LAND_WORK_API.Repositories;


namespace NEWZEAL_LAND_WORK_API.Controllers
{
    [Route("https/Nzwalksresourse/")]
    [ApiController]
    [Authorize(Roles = "Writer, Reader")]


    public class ImageController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost("api/upload")]
       
        public async Task<IActionResult> ImageUpload([FromForm] ImageDomianDTO imageDomianDTO)
        {
            ValidateFileUpload(imageDomianDTO);

            if (ModelState.IsValid)
            {
                //convert DTO to Domian Model there by storing them in a variable 
                var imageDomainModel = new Image
                {
                    file = imageDomianDTO.File,
                    FileName = imageDomianDTO.FileName,
                    FileExtension = Path.GetExtension(imageDomianDTO.File.FileName),
                    FileDescription = imageDomianDTO.Description,
                    FileSizeInBytes = imageDomianDTO.File.Length
                };

                //user respository to upload image 

                await imageRepository.UploadImage(imageDomainModel);

                return Ok(imageDomainModel);


            }

            return BadRequest(ModelState);

        }

        // private class within the block 

        private void ValidateFileUpload(ImageDomianDTO imageDomianDTO)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(imageDomianDTO.File.FileName)))
            {
                ModelState.AddModelError("file", "unsupported file extension");
            }
            if (imageDomianDTO.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10mb, pleas upload a smaller size file");
            }
        }

    }
}
