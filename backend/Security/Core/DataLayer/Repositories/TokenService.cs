using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Security.CommonContracts;
using Security.Contracts.Repository;
using Security.Model;

namespace Security.Core.DataLayer.Repositories
{
    public class TokenService: ITokenService
    {
        private readonly ICommonDb _commonDb;

        public TokenService(ICommonDb commonDb)
        {
            _commonDb = commonDb;
        }

        /// <summary>
        /// Создание токена
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public string Create(int idUser)
        {
            var tokenExpireSetting = ConfigurationManager.AppSettings["TokenExpire"];
            if (!int.TryParse(tokenExpireSetting, out var tokenExpireMinutes))
                tokenExpireMinutes = 20;

            var token = new Token()
            {
                TokenId = Generate(),
                Created = DateTime.UtcNow,
                Expire = DateTime.UtcNow.AddMinutes(tokenExpireMinutes),
                IdUser = idUser
            };

            _commonDb.ExecuteNonQuery("insert into sec.Tokens(tokenId, created, expire, idUser) values(@tokenId, @created, @expire, @idUser)", token);

            return token.TokenId;
        }

        /// <summary>
        /// Создание токена
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <returns></returns>
        public string Create(string loginOrEmail)
        {
            var tokenExpireSetting = ConfigurationManager.AppSettings["TokenExpire"];
            if (!int.TryParse(tokenExpireSetting, out var tokenExpireMinutes))
                tokenExpireMinutes = 20;

            var token = Generate();
            _commonDb.ExecuteNonQuery("insert into sec.Tokens(tokenId, created, expire, idUser) select @tokenId, @created, @expire, idMember from sec.UsersView where login = @loginOrEmail or email = @loginOrEmail", new
            {
                tokenId = token,
                created = DateTime.UtcNow,
                expire = DateTime.UtcNow.AddMinutes(tokenExpireMinutes),
                loginOrEmail
            });

            return token;
        }

        /// <summary>
        /// Прекращение действия отдельного токена
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="reason">Причина</param>
        public void StopExpire(string tokenId, string reason = null)
        {
            _commonDb.ExecuteNonQuery("update sec.Tokens set expire = @expire where tokenId = @tokenId", new {tokenId, expire = DateTime.UtcNow});
        }

        /// <summary>
        /// Прекращение действия всех токенов связанных с пользователем
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="reason">Причина</param>
        public void StopExpireForUser(string tokenId, string reason = null)
        {
            _commonDb.ExecuteNonQuery("update sec.Tokens set expire = @expire where tokenId in (select tokenId from sec.Tokens where idUser = (select idUser from sec.Tokens where tokenId = @tokenId))", new {expire = DateTime.UtcNow, tokenId});
        }

        /// <summary>
        /// Проверка срока действия токена
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool CheckExpire(string token)
        {
            return _commonDb.ExecuteScalar<bool>("select cast(1 as bit) from sec.Tokens where tokenId = @token and expire > GETUTCDATE() union select CAST(0 as bit)", new {token});
        }

        /// <summary>
        /// Создание токена
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public async Task<string> CreateAsync(int idUser)
        {
            var tokenExpireSetting = ConfigurationManager.AppSettings["TokenExpire"];
            if (!int.TryParse(tokenExpireSetting, out var tokenExpireMinutes))
                tokenExpireMinutes = 20;

            var token = new Token()
            {
                TokenId = Generate(),
                Created = DateTime.UtcNow,
                Expire = DateTime.UtcNow.AddMinutes(tokenExpireMinutes),
                IdUser = idUser
            };

            await _commonDb.ExecuteNonQueryAsync("insert into sec.Tokens(tokenId, created, expire, idUser) values(@tokenId, @created, @expire, @idUser)", token);

            return token.TokenId;
        }

        public async Task<string> CreateAsync(string loginOrEmail)
        {
            var tokenExpireSetting = ConfigurationManager.AppSettings["TokenExpire"];
            if (!int.TryParse(tokenExpireSetting, out var tokenExpireMinutes))
                tokenExpireMinutes = 20;

            var token = Generate();
            await _commonDb.ExecuteNonQueryAsync("insert into sec.Tokens(tokenId, created, expire, idUser) select @tokenId, @created, @expire, idMember from sec.UsersView where login = @loginOrEmail or email = @loginOrEmail", new
            {
                tokenId = token,
                created = DateTime.UtcNow,
                expire = DateTime.UtcNow.AddMinutes(tokenExpireMinutes),
                loginOrEmail
            });

            return token;
        }

        /// <summary>
        /// Прекращение действия отдельного токена
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="reason">Причина</param>
        public Task StopExpireAsync(string tokenId, string reason = null)
        {
            return _commonDb.ExecuteNonQueryAsync("update sec.Tokens set expire = @expire where tokenId = @tokenId", new { tokenId, expire = DateTime.UtcNow });
        }

        /// <summary>
        /// Прекращение действия всех токенов связанных с пользователем
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="reason">Причина</param>
        public Task StopExpireForUserAsync(string tokenId, string reason = null)
        {
            return _commonDb.ExecuteNonQueryAsync("update sec.Tokens set expire = @expire where tokenId in (select tokenId from sec.Tokens where idUser = (select idUser from sec.Tokens where tokenId = @tokenId))", new { expire = DateTime.UtcNow, tokenId });
        }

        /// <summary>
        /// Проверка срока действия токена
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<bool> CheckExpireAsync(string token)
        {
            return _commonDb.ExecuteScalarAsync<bool>("select cast(1 as bit) from sec.Tokens where tokenId = @token and expire > GETUTCDATE() union select CAST(0 as bit)", new { token });
        }

        /// <summary>
        /// Генерация токена
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private static string Generate()
        {
            var size = 100;
            // Characters except I, l, O, 1, and 0 to decrease confusion when hand typing tokens
            var charSet = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            var chars = charSet.ToCharArray();
            var data = new byte[1];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(size);
            foreach (var b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
