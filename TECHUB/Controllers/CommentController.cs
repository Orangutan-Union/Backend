﻿using Microsoft.AspNetCore.Mvc;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

namespace TECHUB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService service;
        public CommentController(ICommentService service) { this.service = service; }

        [HttpPost]
        public async Task<IActionResult> CreateComment(Comment comment) 
        {
            return Ok(await service.CreateComment(comment));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            return Ok(await service.DeleteComment(id));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            return Ok(await service.GetCommentById(id));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment(Comment comment)
        {
            return Ok(await service.UpdateComment(comment));
        }
    }
}
