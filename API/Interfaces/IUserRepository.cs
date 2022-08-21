using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        public void Update(AppUser user);

       public  Task<bool> SaveAllAsync();

        public Task<IEnumerable<AppUser>> GetUsersAsync();

        public Task<AppUser> GetUserByIdAsync(int id);

       public Task<AppUser> GetUserByUsernameAsync(string username); 

       Task<IEnumerable<MemberDto>>  GetMembersAsync();

        Task<MemberDto>  GetMemberAsync(string username);
    }
}