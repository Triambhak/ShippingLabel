using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using ShippingLabelWebApi.Models;
using System.Data;
using System.Security.Claims;

namespace ShippingLabelWebApi.AppDbcontext
{

    public partial class Appdbcontext : DbContext
    {
        public int UserId { get; set; }
        public Appdbcontext()
        {
        }

        public Appdbcontext(DbContextOptions<Appdbcontext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }

}
