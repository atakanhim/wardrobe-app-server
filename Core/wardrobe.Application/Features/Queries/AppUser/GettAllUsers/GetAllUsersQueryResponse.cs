using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wardrobe.Application.DTOs.User;

namespace wardrobe.Application.Features.Queries.AppUser.GettAllUsers
{
    public class GetAllUsersQueryResponse
    {
        public IEnumerable<ListUser> Users { get; set; } // Kullanıcı listesi
        public int Page { get; set; } // İstenen sayfa numarası
        public int Size { get; set; } // Sayfa başına kullanıcı sayısı
        public int TotalCount { get; set; } // Toplam kullanıcı sayısı
    }
}
