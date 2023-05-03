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


        //[HttpPost]
        //public async Task<IActionResult> Create(Picture picture)
        //{
        //    context.Pictures.Add(picture);
        //    await context.SaveChangesAsync();

        //    return Ok(picture);
        //}
    }
}
