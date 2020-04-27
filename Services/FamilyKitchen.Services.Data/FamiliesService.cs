using FamilyKitchen.Common;
using FamilyKitchen.Data.Common.Repositories;
using FamilyKitchen.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyKitchen.Services.Data
{
    public class FamiliesService : IFamiliesService
    {
        private readonly IDeletableEntityRepository<Family> familiesRepository;
        private readonly IDeletableEntityRepository<FamilyKitchenUser> usersRepository;
        private readonly IShoppingCartsService shoppingCartsService;
        private readonly UserManager<FamilyKitchenUser> userManager;

        public FamiliesService(IDeletableEntityRepository<Family> familiesRepository,
                                IDeletableEntityRepository<FamilyKitchenUser> usersRepository,
                                IShoppingCartsService shoppingCartsService,
                                UserManager<FamilyKitchenUser> userManager)
        {
            this.familiesRepository = familiesRepository;
            this.usersRepository = usersRepository;
            this.shoppingCartsService = shoppingCartsService;
            this.userManager = userManager;
        }

        public async Task<bool> CreateFamily(string username, string familyNickname, List<string> familyMembers)
        {
            var userHead = this.usersRepository.All().Where(x => x.UserName == username).FirstOrDefault();
            var isHeadExist = this.familiesRepository.All().SelectMany(x => x.FamilyMembers).Any(y => y.UserName == username);
            var isExist = this.familiesRepository.All().Any(x => x.Name == familyNickname);

            if (userHead == null || familyNickname == null || isExist || isHeadExist)
            {
                return false;
            }

            var family = new Family
            {
                Name = familyNickname,
                Moderator = username,
            };

            await this.familiesRepository.AddAsync(family);
            await this.familiesRepository.SaveChangesAsync();

            foreach (var member in familyMembers)
            {
                var userMember = this.usersRepository.All().Where(x => x.UserName == member).FirstOrDefault();
                var isMemberExist = this.familiesRepository.All().SelectMany(x => x.FamilyMembers).Any(y => y.UserName == userMember.UserName);

                if (userMember == null || isMemberExist)
                {
                    return false;
                }

                userMember.Family = family;
                this.usersRepository.Update(userMember);
            }

            userHead.Family = family;
            await this.usersRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddMember(string username, string familyNickname, string member)
        {
            var fam = this.familiesRepository.All().Where(x => x.Name == familyNickname).FirstOrDefault();
            var newMember = this.usersRepository.All().Where(x => x.UserName == member).FirstOrDefault();

            var isExist = this.familiesRepository.All().Any(x => x.Name == familyNickname);
            var isMemberExist = this.familiesRepository.All().SelectMany(x => x.FamilyMembers).Any(y => y.UserName == member);

            if (fam == null || newMember == null || !isExist || isMemberExist)
            {
                return false;
            }

            newMember.Family = fam;

            this.usersRepository.Update(newMember);
            await this.usersRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveMember(string username, string familyNickname, string member)
        {
            var memberUser = this.usersRepository.All().Where(x => x.UserName == member).FirstOrDefault();
            var fam = this.familiesRepository.All().Where(x => x.Name == familyNickname).FirstOrDefault();
            var isAMember = memberUser.Family.Name == familyNickname;
            var isMemberUserModerator = memberUser.Family.Moderator == memberUser.UserName;

            if (fam == null || memberUser == null || !isAMember || isMemberUserModerator)
            {
                return false;
            }

            memberUser.Family = null;

            this.usersRepository.Update(memberUser);
            await this.usersRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteFamily(string username, string familyNickname)
        {
            var userHead = this.usersRepository.All().Where(x => x.UserName == username).FirstOrDefault();
            var fam = this.familiesRepository.All().Where(x => x.Name == familyNickname);
            var members = fam.Select(x => x.FamilyMembers.Select(y => y).ToList()).FirstOrDefault();

            if (userHead == null || fam == null)
            {
                return false;
            }

            foreach (var member in members)
            {
                member.Family = null;
                member.FamilyId = null;
                this.usersRepository.Update(member);
            }

            userHead.Family = null;

            await this.usersRepository.SaveChangesAsync();

            this.familiesRepository.Delete(fam.FirstOrDefault());
            await this.familiesRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> LeaveFamily(string username, string familyNickname)
        {
            var memberUser = this.usersRepository.All().Where(x => x.UserName == username).FirstOrDefault();
            var fam = this.familiesRepository.All().Where(x => x.Name == familyNickname).FirstOrDefault();
            var isAMember = memberUser.Family.Name == familyNickname;
            var isMemberUserModerator = memberUser.Family.Moderator == memberUser.UserName;

            if (fam == null || memberUser == null || !isAMember || isMemberUserModerator)
            {
                return false;
            }

            memberUser.Family = null;
            memberUser.FamilyId = null;
            this.usersRepository.Update(memberUser);
            await this.usersRepository.SaveChangesAsync();

            return true;
        }


        public async Task<bool> GetFamilyCart(string member)
        {
            var memberUser = this.usersRepository.All().Where(x => x.UserName == member);
            var moderatorName = memberUser.Select(x => x.Family.Moderator).FirstOrDefault();
            var moderatorUser = this.usersRepository.All().Where(x => x.UserName == moderatorName).FirstOrDefault();

            var action = await this.shoppingCartsService.GetFamilyCart(moderatorUser, memberUser.FirstOrDefault());

            return action;
        }

        public async Task<bool> ReturnFamilyCart(string member)
        {
            var memberUser = this.usersRepository.All().Where(x => x.UserName == member);
            var moderatorName = memberUser.Select(x => x.Family.Moderator).FirstOrDefault();
            var moderatorUser = this.usersRepository.All().Where(x => x.UserName == moderatorName).FirstOrDefault();

            var action = await this.shoppingCartsService.ReturnFamilyCart(moderatorUser, memberUser.FirstOrDefault());

            return action;
        }

        public string GetFamilyName(string username)
        {
            var user = this.usersRepository.All().Where(x => x.UserName == username);
            var familyName = user.Select(x => x.Family.Name).FirstOrDefault();
            var fam = this.familiesRepository.All().Where(x => x.Name == familyName).FirstOrDefault();
            var isUserModerator = user.Select(x => x.Family.Moderator).FirstOrDefault() == user.FirstOrDefault().UserName;

            if (user == null || !isUserModerator || fam == null)
            {
                return null;
            }

            return fam.Name;
        }
    }
}
