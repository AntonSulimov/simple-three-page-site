using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels.Profiles;

namespace Web.Controllers
{
    [Authorize]
    public class ProfilesController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<User> _userManager;

        public ProfilesController(
            AppDbContext appDbContext,
            UserManager<User> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var profile = await _appDbContext.Profiles
                .FirstOrDefaultAsync(i => i.UserId == userId);

            if (profile == null)
            {
                return View(new ProfileViewModel());
            }

            var vm = new ProfileViewModel { 
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                BirthDate = profile.BirthDate,
                AboutMe = profile.AboutMe,
                Address = profile.Address,
                Phone = profile.Phone,
                Site = profile.Site,
                Avatar = profile.ProfilePicture
            };

            if (vm.FullName.Length == 0)
            {
                vm.FirstName = User.FindFirst(ClaimTypes.Email).Value;
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProfileViewModel vm)
        {
            if(ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var profile = await _appDbContext.Profiles
                    .FirstOrDefaultAsync(i => i.UserId == userId);

                var isNewProfile = false;
                if (profile == null)
                {
                    profile = new Profile {
                        UserId = userId
                    };
                    isNewProfile = true;
                }

                profile.FirstName = vm.FirstName;
                profile.LastName = vm.LastName;
                profile.BirthDate = vm.BirthDate;
                profile.AboutMe = vm.AboutMe;
                profile.Phone = vm.Phone;
                profile.Address = vm.Address;
                profile.Site = vm.Site;

                if (vm.ProfilePicture != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        vm.ProfilePicture.CopyTo(memoryStream);
                        profile.ProfilePicture = memoryStream.ToArray();
                    };
                }

                if (isNewProfile)
                {
                    _appDbContext.Profiles.Add(profile);
                }
                else
                {
                    _appDbContext.Profiles.Update(profile);
                }
                await _appDbContext.SaveChangesAsync();

                return RedirectToAction("Details");
            }

            return View("Details", vm);
        }
    }
}