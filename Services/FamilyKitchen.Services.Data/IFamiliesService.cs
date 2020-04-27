namespace FamilyKitchen.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFamiliesService
    {
        Task<bool> CreateFamily(string username, string famulyNickname, List<string> familyMeber);

        Task<bool> AddMember(string username, string familyNickname, string member);

        Task<bool> RemoveMember(string username, string familyNickname, string member);

        Task<bool> DeleteFamily(string username, string familyNickname);

        Task<bool> LeaveFamily(string username, string familyNickname);

        Task<bool> GetFamilyCart(string member);

        Task<bool> ReturnFamilyCart(string member);

        string GetFamilyName(string username);
    }
}
