﻿using Project.NetflixApp.DataAccess.Contexts.EntityFramework;
using Project.NetflixApp.DataAccess.Repositories.Abstract;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.DataAccess.Repositories.Concrete
{
    public class TypeEntityRepository : GenericRepository<TypeEntity>, ITypeEntityRepository
    {
        public TypeEntityRepository(NetflixAppContext netflixAppContext) : base(netflixAppContext)
        {
        }
    }
}
