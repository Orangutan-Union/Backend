﻿using Microsoft.EntityFrameworkCore;
using TECHUB.Repository.Context;
using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;

namespace TECHUB.Repository.Repositories
{
    public class PictureRepository : IPictureRepository
    {
        private readonly DatabaseContext context;

        public PictureRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<Picture> GetPictureById(int id)
        {
            return await context.Pictures.FirstOrDefaultAsync(x => x.PictureId == id);
        }

        public async Task<Picture> Add(Picture picture)
        {
            context.Pictures.Add(picture);
            await context.SaveChangesAsync();
            return picture;
        }

        public async Task<Picture> DeletePicture(int id)
        {
            var pic = await context.Pictures.FindAsync(id);

            if (pic is not null)
            {
                context.Pictures.Remove(pic);
                await context.SaveChangesAsync();
            }

            return pic;
        }
    }
}
