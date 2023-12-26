using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wardrobe.Application.Features.Queries.AppUser.GettAllUsers
{
    public class GetAllUsersQueryRequest : IRequest<GetAllUsersQueryResponse>
    {
        public int? Page { get; set; } = 1; 
        public int? Size { get; set; } = 5;
    }
}
