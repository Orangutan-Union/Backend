using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TECHUB.Repository.Context;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

namespace TECHUB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly IPictureService service;

        public PicturesController(IPictureService service)
        {
            this.service = service;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPictureById(int id)
        {
            var pic = await service.GetPictureById(id);

            if (pic is null)
            {
                return NotFound($"No picture found with ID = {id}");
            }

            return Ok(pic);
        }


        [HttpPost]
        public async Task<IActionResult> AddPicture()
        {
            var file = Request.Form.Files[0];

            if (file is null)
            {
                return BadRequest("Something done gone wrong, I tell you hwat");
            }
            var pic = new Picture();
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);

                if (ms.Length < 2097152)
                {
                    //pic.ImageData = ms.ToArray();
                    pic.ImageName = file.FileName;
                }
                else
                {
                    return BadRequest("This picture is too dang big, make sure it's under 2MB in size");
                }
            }
            await service.AddPicture(pic);
            return Ok(pic);
        }
    }
}
