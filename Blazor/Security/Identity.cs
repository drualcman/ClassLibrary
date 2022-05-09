using ClassLibrary.Helpers;
using ClassLibrary.Javascript;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClassLibrary.Security
{
    /// <summary>
    /// Manage the actions to Sign In and Sign Out users using Personalized Model Claims
    /// Default authentication type is CookieAuthenticationDefaults.AuthenticationScheme
    /// </summary>
    public static class Identity
    {
        #region login actions
        /// <summary>
        /// Login request from a model user send
        /// Authentication only local storage without token
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="jsRuntime"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static async Task<bool> SignInAsync<TUser>(IJSRuntime jsRuntime, TUser user)
        {
            string token = Token();
            return await SignInAsync(jsRuntime, user, token);
        }

        /// <summary>
        /// Login request from a model user send
        /// Authentication used jwt authentication with token
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="jsRuntime"></param>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<bool> SignInAsync<TUser>(IJSRuntime jsRuntime, TUser user, string token)
        {
            bool result;
            try
            {
                string jsonString = JsonSerializer.Serialize(user);
                string key = token.Split('.')[2];
                Cipher.Secret e = new Cipher.Secret(key);
                string jsonData = await e.EncriptAsync(jsonString);
                await jsRuntime.SetAllDataAsync(jsonData, token);
                result = true;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Login request from a model user send
        /// Authentication used jwt authentication with token
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <returns></returns>
        public static async Task<bool> SessionExpiredAsync(IJSRuntime jsRuntime)
        {
            bool result;
            try
            {
                string token = await jsRuntime.GetUserTokenAsync();
                string payload = Cipher.Hash.Base64.Base64UrlDecode(token.Split('.')[1]);
                JsonElement json = JsonSerializer.Deserialize<JsonElement>(payload);
                DateTimeOffset expired = DateTimeOffset.FromUnixTimeSeconds(json.GetProperty("exp").GetInt64());
                result = expired <= DateTime.Now;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Login request from a model user send
        /// Authentication used CookieAuthenticationDefaults.AuthenticationScheme
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="context"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static async Task<bool> SignInAsync<TUser>(HttpContext context, TUser user)
        {
            return await SignInAsync(context, user, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// Login request from a model user send
        /// Authentication used CookieAuthenticationDefaults.AuthenticationScheme
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="displayName">Who is the property to show default like user name in the Identity</param>
        /// <param name="context"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static async Task<bool> SignInAsync<TUser>(string displayName, HttpContext context, TUser user)
        {
            return await SignInAsync(context, user, CookieAuthenticationDefaults.AuthenticationScheme, displayName);
        }

        /// <summary>
        /// Login request from a model user send
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="context"></param>
        /// <param name="user"></param>
        /// <param name="authenticationType">Set what authentication will used</param>
        /// <returns></returns>
        public static async Task<bool> SignInAsync<TUser>(HttpContext context, TUser user, string authenticationType)
        {
            return await SignInAsync(context, user, CookieAuthenticationDefaults.AuthenticationScheme, "name");
        }

        /// <summary>
        /// Login request from a model user send
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="context"></param>
        /// <param name="user"></param>
        /// <param name="authenticationType">Set what authentication will used</param>
        /// <param name="displayName">Who is the property to show default like user name in the Identity</param>
        /// <returns></returns>
        public static async Task<bool> SignInAsync<TUser>(HttpContext context, TUser user, string authenticationType, string displayName)
        {
            bool result;
            try
            {
                await context.SignInAsync(authenticationType,
                    new ClaimsPrincipal(SetUserData(user, authenticationType, displayName)),
                    new AuthenticationProperties
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
                    });
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Sign out the user
        /// Authentication used jwt authentication with token
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <returns></returns>
        public static async Task SignOutAsync(IJSRuntime jsRuntime)
        {
            await jsRuntime.DelAllDataAsync();
        }

        /// <summary>
        /// Sign out the user
        /// Authentication used CookieAuthenticationDefaults.AuthenticationScheme
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task SignOutAsync(HttpContext context)
        {
            await SignOutAsync(context, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// Sing out the user
        /// </summary>
        /// <param name="context"></param>
        /// <param name="authenticationType">Set what authentication will used</param>
        /// <returns></returns>
        public static async Task SignOutAsync(HttpContext context, string authenticationType)
        {
            await context.SignOutAsync(authenticationType);
        }
        #endregion

        #region user calls
        /// <summary>
        /// Get the claims about the token and return the user model
        /// Authentication used jwt authentication
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <returns></returns>
        public static TUser GetUserAsync<TUser>(string token) where TUser : new()
        {
            if(string.IsNullOrEmpty(token)) return new TUser();
            else
            {
                try
                {
                    string[] data = token.Split('.');
                    string jsonString = Cipher.Hash.Base64.Base64UrlDecode(data[1]);
                    JsonSerializerOptions options = new JsonSerializerOptions { 
                        WriteIndented = true, 
                        NumberHandling = JsonNumberHandling.AllowReadingFromString,
                        AllowTrailingCommas = true,                         
                        Converters = { 
                            new CustomJsonStringEnumConverter(),        //deserialize enumerals from description
                            new CustomStringBooleanConverter()          //deserialize boolean from string
                        }, 
                    };
                    TUser user = JsonSerializer.Deserialize<TUser>(jsonString, options);
                    return user;
                }
                catch  (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new TUser();
                }
            }
        }

        /// <summary>
        /// Get the claims about the active user and return the user model
        /// Authentication used jwt authentication
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="jsRuntime"></param>
        /// <returns></returns>
        public static async Task<TUser> GetUserAsync<TUser>(IJSRuntime jsRuntime) where TUser : new()
        {
            string data = await jsRuntime.GetUserDataAsync();
            if(string.IsNullOrEmpty(data)) return new TUser();
            else
            {
                try
                {
                    string token = await jsRuntime.GetUserTokenAsync();
                    string key = token.Split('.')[2];
                    Cipher.Secret e = new Cipher.Secret(key);
                    string jsonString = await e.DecriptAsync(data);
                    JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true, Converters = { new CustomJsonStringEnumConverter() } };
                    return JsonSerializer.Deserialize<TUser>(jsonString, options);
                }
                catch
                {
                    return new TUser();
                }
            }
        }

        /// <summary>
        /// Get the claims about the active user and return the user model
        /// Authentication used CookieAuthenticationDefaults.AuthenticationScheme
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<TUser> GetUserAsync<TUser>(HttpContext context) where TUser : new()
        {
            return await GetUserAsync<TUser>(context, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// Get the claims about the active user and return the user model
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="context"></param>
        /// <param name="authenticationType">Set what authentication will used</param>
        /// <returns></returns>
        public static async Task<TUser> GetUserAsync<TUser>(HttpContext context, string authenticationType) where TUser : new()
        {
            try
            {
                TUser User = new TUser();
                AuthenticateResult x = await context.AuthenticateAsync(authenticationType);
                if (x.Succeeded)
                {
                    //use reflexion to fill the object
                    Type myType = typeof(TUser);
                    string model = $"{myType.Namespace}.{myType.Name}";
                    //Type t = Assembly.GetExecutingAssembly().GetType(model, true, true);

                    //Get assemblies loaded in the current AppDomain
                    Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

                    int c = 0;
                    do
                    {
                        if (assemblies[c].GetName().Name == myType.Namespace)
                        {
                            Type t = assemblies[c].GetType(model, true, true);
                            foreach (Claim item in x.Principal.Claims)
                            {
                                if (item.Type != ClaimTypes.Name)
                                {
                                    PropertyInfo property = t.GetProperty(item.Type);
                                    //convert to the correct property type
                                    Type tipo = property.PropertyType;
                                    if (!tipo.IsEnum) property.SetValue(User, Convert.ChangeType(item.Value, tipo));
                                    else property.SetValue(User, item.Value);
                                }
                            }
                            c = assemblies.Length;
                        }
                        c++;
                    } while (c < assemblies.Length);


                }
                return User;
            }
            catch (Exception ex)
            {
                throw new Exception("Get user claims exception", ex);
            }
        }

        /// <summary>
        /// Set the claims for the user
        /// Authentication used CookieAuthenticationDefaults.AuthenticationScheme
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="user"></param>
        /// <returns></returns>
        public static ClaimsIdentity SetUserData<TUser>(TUser user)
        {
            return SetUserData(user, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// Set the claims for the user
        /// Authentication used CookieAuthenticationDefaults.AuthenticationScheme
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="displayName">Who is the property to show default like user name in the Identity</param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static ClaimsIdentity SetUserData<TUser>(string displayName, TUser user)
        {
            return SetUserData(user, CookieAuthenticationDefaults.AuthenticationScheme, displayName);
        }

        /// <summary>
        /// Set the claims for the user
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="user"></param>
        /// <param name="authenticationType">Set what authentication will used</param>
        /// <returns></returns>
        public static ClaimsIdentity SetUserData<TUser>(TUser user, string authenticationType)
        {
            return SetUserData(user, authenticationType, "name");
        }

        /// <summary>
        /// Set the claims for the user
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="user"></param>
        /// <param name="displayName">Who is the property to show default like user name in the Identity</param>
        /// <returns></returns>
        public static List<Claim> SetClaims<TUser>(string displayName, TUser user)
        {
            List<Claim> claims = new List<Claim>();
            //use reflexion to get dynamic the properties about the user object
            PropertyInfo[] properties = typeof(TUser).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            bool foundName = false;

            int c = properties.Length;
            int i = 0;
            for (i = 0; i < c; i++)
            {
                //get property name
                string myType = properties[i].Name;
                //get value of the property
                string myValue;
                var myGetValue = properties[i].GetValue(user);
                if(myGetValue != null) myValue = myGetValue.ToString();
                else myValue = string.Empty;

                //if it's Authentication Ignore don't insert in the claims
                Attributes.Identity identityAttributes = properties[i].GetCustomAttribute<Attributes.Identity>();
                if (identityAttributes is null)
                {
                    claims.Add(new Claim(myType, myValue));
                }
                else
                {
                    if (!identityAttributes.ClaimIgnore)
                    {
                        foundName = identityAttributes.DisplayName;
                        if (foundName)
                        {
                            claims.Add(new Claim(myType, myValue));
                            claims.Add(new Claim(ClaimTypes.Name, myValue));
                        }
                        else claims.Add(new Claim(myType, myValue));
                    }
                }
            }
            if (!foundName)
            {
                i = 0;
                do
                {
                    //get property name
                    string myType = properties[i].Name;
                    //get value of the property
                    string myValue = properties[i].GetValue(user).ToString();

                    if (myType.ToLower() == displayName.ToLower())
                    {
                        claims.Add(new Claim(ClaimTypes.Name, myValue));
                        foundName = true;
                    }
                    i++;
                } while (!foundName && i < c);

                if (!foundName)
                {
                    //not found DisplayName property, search a email
                    Claim n = claims.Find(n => n.Value.Contains("@"));
                    if (n != null) claims.Add(new Claim(ClaimTypes.Name, n.Value));
                    else        //if nothing found set the first property like display name
                        claims.Add(new Claim(ClaimTypes.Name, claims[0].Value));
                }
            }
            return claims;
        }

        /// <summary>
        /// Set the claims for the user
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="user"></param>
        /// <param name="authenticationType">Set what authentication will used</param>
        /// <param name="displayName">Who is the property to show default like user name in the Identity</param>
        /// <returns></returns>
        public static ClaimsIdentity SetUserData<TUser>(TUser user, string authenticationType, string displayName)
        {
            List<Claim> claims = SetClaims(displayName, user);
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, authenticationType);
            return claimsIdentity;
        }
        #endregion

        #region helpers
        private static string Token()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray()) i *= ((int)b + 1);
            Cipher.Hash.MD5 md5 = new Cipher.Hash.MD5();
            return md5.ComputeHash(string.Format("{0:x}", i - DateTime.Now.Ticks));
        }
        #endregion
    }
}
