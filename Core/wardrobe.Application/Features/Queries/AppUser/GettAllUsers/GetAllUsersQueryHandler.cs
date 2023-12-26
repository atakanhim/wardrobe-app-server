using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wardrobe.Application.Abstractions.Services;

namespace wardrobe.Application.Features.Queries.AppUser.GettAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, GetAllUsersQueryResponse>
    {
        readonly IUserService _userService;

        public GetAllUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetAllUsersQueryResponse> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            // Eğer page veya size null ise, varsayılan değerleri kullan
            int page = request.Page ?? 1;
            int size = request.Size ?? 5;

            // UserService kullanarak kullanıcı listesini al
            var users = await _userService.GetAllUsersAsync(page, size);

            // Toplam kullanıcı sayısını almak için bir yöntem kullanmanız gerekebilir.
            // Örnek olarak, burada basitçe users.Count kullanıyorum, 
            // ama gerçekte bu, veritabanındaki toplam kullanıcı sayısını temsil etmelidir.
            int totalCount = users.Count; // Bu, daha karmaşık bir sorgu gerektirir.

            // GetAllUsersQueryResponse modelini doldurun
            var response = new GetAllUsersQueryResponse
            {
                Users = users,
                Page = page,
                Size = size,
                TotalCount = totalCount
            };

            return response;
        }
    }
}
