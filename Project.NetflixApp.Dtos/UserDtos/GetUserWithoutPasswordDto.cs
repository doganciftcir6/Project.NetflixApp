using Project.NetflixApp.Dtos.GenderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Dtos.UserDtos
{
    public class GetUserWithoutPasswordDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }

        public int GenderId { get; set; }
        public GetGenderDto Gender { get; set; }
    }
}
