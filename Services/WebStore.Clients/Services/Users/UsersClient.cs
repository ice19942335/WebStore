using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Entities.Dto.User;
using WebStore.Entities.Entities.Identity;
using WebStore.Interfaces.services;

namespace WebStore.Clients.Services.Users
{
    public class UsersClient : BaseClient, IUsersClient
    {
        public UsersClient(IConfiguration configuration) : base(configuration) => ServiceAddress = "api/users";

        protected override string ServiceAddress { get; set; }

        #region Implementation of IUserStore<User>

        public async Task<string> GetUserIdAsync(User user, CancellationToken cancel) =>
            await (await PostAsync($"{ServiceAddress}/UserId", user))
                .Content
                .ReadAsAsync<string>(cancel);

        public async Task<string> GetUserNameAsync(User user, CancellationToken cancel) =>
            await (await PostAsync($"{ServiceAddress}/UserName", user))
                .Content
                .ReadAsAsync<string>(cancel);

        public async Task SetUserNameAsync(User user, string name, CancellationToken cancel)
        {
            user.UserName = name;
            await PostAsync($"{ServiceAddress}/UserName/{name}", user);
        }

        public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancel) =>
            await (await PostAsync($"{ServiceAddress}/NormalUserName/", user))
                .Content
                .ReadAsAsync<string>(cancel);

        public async Task SetNormalizedUserNameAsync(User user, string name, CancellationToken cancel)
        {
            user.NormalizedUserName = name;
            await PostAsync($"{ServiceAddress}/NormalUserName/{name}", user);
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancel) =>
            await (await PostAsync($"{ServiceAddress}/User", user))
                .Content
                .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancel) =>
            await (await PutAsync($"{ServiceAddress}/User", user))
                .Content
                .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancel) =>
            await (await PostAsync($"{ServiceAddress}/User/Delete", user))
                .Content
                .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();

        public async Task<User> FindByIdAsync(string id, CancellationToken cancel) =>
            await GetAsync<User>($"{ServiceAddress}/User/FindById/{id}");

        public async Task<User> FindByNameAsync(string name, CancellationToken cancel) =>
            await GetAsync<User>($"{ServiceAddress}/User/Normal/{name}");

        #endregion

        #region Implementation of IUserRoleStore<User>

        public async Task AddToRoleAsync(User user, string role, CancellationToken cancel) =>
            await PostAsync($"{ServiceAddress}/Role/{role}", user);

        public async Task RemoveFromRoleAsync(User user, string role, CancellationToken cancel) =>
            await PostAsync($"{ServiceAddress}/Role/Delete/{role}", user);

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancel) =>
            await (await PostAsync($"{ServiceAddress}/roles", user))
                .Content
                .ReadAsAsync<IList<string>>(cancel);

        public async Task<bool> IsInRoleAsync(User user, string role, CancellationToken cancel) =>
            await (await PostAsync($"{ServiceAddress}/InRole/{role}", user))
                .Content
                .ReadAsAsync<bool>(cancel);

        public async Task<IList<User>> GetUsersInRoleAsync(string role, CancellationToken cancel) =>
            await GetAsync<List<User>>($"{ServiceAddress}/UsersInRole/{role}");

        #endregion

        #region Implementation of IUserPasswordStore<User>

        public async Task SetPasswordHashAsync(User user, string hash, CancellationToken cancel) =>
            await PostAsync($"{ServiceAddress}/SetPasswordHash", new PasswordHashDto { Hash = hash, User = user });

        public async Task<string> GetPasswordHashAsync(User user, CancellationToken cancel) =>
            await (await PostAsync($"{ServiceAddress}/GetPasswordHash", user))
                .Content
                .ReadAsAsync<string>(cancel);

        public async Task<bool> HasPasswordAsync(User user, CancellationToken cancel) =>
            await (await PostAsync($"{ServiceAddress}/HasPassword", user))
                .Content
                .ReadAsAsync<bool>(cancel);

        #endregion

        #region Implementation of IUserEmailStore<User>

        public async Task SetEmailAsync(User user, string email, CancellationToken cancel)
        {
            user.Email = email;
            await PostAsync($"{ServiceAddress}/SetEmail/{email}", user);
        }

        public async Task<string> GetEmailAsync(User user, CancellationToken cancel) =>
            await (await PostAsync($"{ServiceAddress}/GetEmail", user))
                .Content
                .ReadAsAsync<string>(cancel);

        public async Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancel) =>
            await (await PostAsync($"{ServiceAddress}/GetEmailConfirmed", user))
                .Content
                .ReadAsAsync<bool>(cancel);

        public async Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancel)
        {
            user.EmailConfirmed = confirmed;
            await PostAsync($"{ServiceAddress}/SetEmailConfirmed/{confirmed}", user);
        }

        public async Task<User> FindByEmailAsync(string email, CancellationToken cancel) =>
            await GetAsync<User>($"{ServiceAddress}/User/FindByEmail/{email}");

        public async Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancel) =>
            await (await PostAsync($"{ServiceAddress}/User/GetNormalizedEmail", user))
                .Content
                .ReadAsAsync<string>(cancel);

        public async Task SetNormalizedEmailAsync(User user, string email, CancellationToken cancel) =>
            await PostAsync($"{ServiceAddress}/SetnormalizedEmail/{email}", user);

        #endregion

        #region Implementation of IUserPhoneNumberStore<User>

        public async Task SetPhoneNumberAsync(User user, string phone, CancellationToken cancel)
        {
            user.PhoneNumber = phone;
            await PostAsync($"{ServiceAddress}/SetPhoneNumber/{phone}", user);
        }

        public async Task<string> GetPhoneNumberAsync(User user, CancellationToken cancel) =>
            await (await PostAsync($"{ServiceAddress}/GetPhoneNumber", user))
                .Content
                .ReadAsAsync<string>(cancel);

        public async Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken cancel) =>
            await (await PostAsync($"{ServiceAddress}/GetPhoneNumberConfirmed", user))
                .Content
                .ReadAsAsync<bool>(cancel);

        public async Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancel)
        {
            user.PhoneNumberConfirmed = confirmed;
            await PostAsync($"{ServiceAddress}/SetPhoneNumberConfirmed/{confirmed}", user);
        }

        #endregion

        #region Implementation of IUserLoginStore<User>

        public async Task AddLoginAsync(User user, UserLoginInfo login, CancellationToken cancel) =>
            await PostAsync($"{ServiceAddress}/AddLogin", new AddLoginDto() { User = user, UserLoginInfo = login });

        public async Task RemoveLoginAsync(User user, string loginProvider, string providerKey, CancellationToken cancel) =>
            await PostAsync($"{ServiceAddress}/RemoveLogin/{loginProvider}/{providerKey}", user);

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancel) =>
            await (await PostAsync($"{ServiceAddress}/GetLogins", user))
                .Content
                .ReadAsAsync<List<UserLoginInfo>>(cancel);

        public async Task<User> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancel) =>
            await GetAsync<User>($"{ServiceAddress}/User/FindByLogin/{loginProvider}/{providerKey}");

        #endregion

        #region Implementation of IUserLockoutStore<User>

        public async Task<DateTimeOffset?> GetLockoutEndDateAsync(User user, CancellationToken cancel) =>
            await (await PostAsync($"{ServiceAddress}/GetLockoutEndDate", user))
                .Content
                .ReadAsAsync<DateTimeOffset?>(cancel);

        public async Task SetLockoutEndDateAsync(User user, DateTimeOffset? endDate, CancellationToken cancel)
        {
            user.LockoutEnd = endDate;
            await PostAsync($"{ServiceAddress}/SetLockoutEndDate",
                new SetLockoutDto() { User = user, LockoutEnd = endDate });
        }

        public async Task<int> IncrementAccessFailedCountAsync(User user, CancellationToken cancel) =>
            await (await PostAsync($"{ServiceAddress}/IncrementAccessFailedCount", user))
                .Content
                .ReadAsAsync<int>(cancel);

        public async Task ResetAccessFailedCountAsync(User user, CancellationToken cancel) =>
            await PostAsync($"{ServiceAddress}/ResetAccessFailedCont", user);

        public async Task<int> GetAccessFailedCountAsync(User user, CancellationToken cancel) =>
            await (await PostAsync($"{ServiceAddress}/GetAccessFailedCount", user))
                .Content
                .ReadAsAsync<int>(cancel);

        public async Task<bool> GetLockoutEnabledAsync(User user, CancellationToken cancel) =>
            await (await PostAsync($"{ServiceAddress}/GetLockoutEnabled", user))
                .Content
                .ReadAsAsync<bool>(cancel);

        public async Task SetLockoutEnabledAsync(User user, bool enabled, CancellationToken cancel) =>
            await PostAsync($"{ServiceAddress}/SetLockoutEnabled/{enabled}", user);

        #endregion

        #region Implementation of IUserTwoFactorStore<User>

        public async Task SetTwoFactorEnabledAsync(User user, bool enabled, CancellationToken cancel)
        {
            user.TwoFactorEnabled = enabled;
            await PostAsync($"{ServiceAddress}/SetTwoFactor/{enabled}", user);
        }

        public async Task<bool> GetTwoFactorEnabledAsync(User user, CancellationToken cancel) =>
            await (await PostAsync($"{ServiceAddress}/GetTwoFactorEnabled", user))
                .Content
                .ReadAsAsync<bool>(cancel);

        #endregion

        #region Implementation of IUserClaimStore<User>

        public async Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancel) =>
            await (await PostAsync($"{ServiceAddress}/GetClaims", user))
                .Content
                .ReadAsAsync<List<Claim>>(cancel);

        public async Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancel) =>
            await PostAsync($"{ServiceAddress}/AddClaims", new AddClaimsDto() { User = user, Claims = claims });

        public async Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim, CancellationToken cancel) =>
            await PostAsync($"{ServiceAddress}/ReplaceClaim", new ReplaceClaimsDto() { User = user, Claim = claim, NewClaim = newClaim });

        public async Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancel) =>
            await PostAsync($"{ServiceAddress}/RemoveClaims", new RemoveClaimsDto() { User = user, Claims = claims });

        public async Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancel) =>
            await (await PostAsync($"{ServiceAddress}/GetUsersForClaim", claim))
                .Content
                .ReadAsAsync<List<User>>(cancel);

        #endregion

        #region Implementation of IDisposable

        public void Dispose() => Client.Dispose();

        #endregion
    }
}
