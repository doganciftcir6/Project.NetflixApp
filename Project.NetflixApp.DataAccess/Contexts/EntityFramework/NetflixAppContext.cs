using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.DataAccess.Contexts.EntityFramework
{
    public class NetflixAppContext : DbContext
    {
        //contexti dependecylemek için
        public NetflixAppContext(DbContextOptions<NetflixAppContext> options) : base(options) 
        { 

        }


    }
}
