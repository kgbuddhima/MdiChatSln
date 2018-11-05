// Helpers/Settings.cs

using MdiChat.MdiWebService.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;

namespace MdiChat.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string SettingsKey = "settings_key";
	    private const string DeviceTokenKey = "device_token";
	    private const string AuthTokenKey = "auth_token";
	    private const string PasswordResetCodeKey = "passowrd_reset_code_key";
        private const string PermissionCodeKey = "permissionCode_key";
        private const string PermissionCodeOnKey = "permissionCodeOnOff_key";
        private const string IsNewUserkey = "is_new_user_key";
        private static  MdiUser MdiUser;
        //PasswordResetCode
        private static readonly string SettingsDefault = string.Empty;
        private static readonly bool PermissionCodeOnDefault = false;
        private static readonly bool IsFirstTimeDefault = true;
        private static ISettings MdiSettings => CrossSettings.Current;

        #endregion

        public static string GeneralSettings
		{
			get
			{
				return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(SettingsKey, value);
			}
		}

	    public static string DeviceToken
	    {
	        get
	        {
	            return AppSettings.GetValueOrDefault(DeviceTokenKey, SettingsDefault);
	        }
	        set
	        {
	            AppSettings.AddOrUpdateValue(DeviceTokenKey, value);
	        }
	    }
	    public static string AuthToken
	    {
	        get
	        {
	            return AppSettings.GetValueOrDefault(AuthTokenKey, SettingsDefault);
	        }
	        set
	        {
	            AppSettings.AddOrUpdateValue(AuthTokenKey, value);
	        }
	    }

	    public static string PasswordResetCode
	    {
	        get
	        {
	            return AppSettings.GetValueOrDefault(PasswordResetCodeKey, SettingsDefault);
	        }
	        set
	        {
	            AppSettings.AddOrUpdateValue(PasswordResetCodeKey, value);
	        }
	    }

        /// <summary>
        /// The permission code which use to login to the app
        /// </summary>
        public static string PermissionCode
        {
            get
            {
                return AppSettings.GetValueOrDefault(PermissionCodeKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(PermissionCodeKey, value);
            }
        }

        /// <summary>
        /// The permission code which use to login to the app
        /// </summary>
        public static bool PermissionCodeOn
        {
            get
            {
                return AppSettings.GetValueOrDefault(PermissionCodeOnKey, PermissionCodeOnDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(PermissionCodeOnKey, value);
            }
        }

        public static string FirebaseToken
	    {
	        get => MdiSettings.GetValueOrDefault(nameof(FirebaseToken), default(string));

	        set => MdiSettings.AddOrUpdateValue(nameof(FirebaseToken), value);
	    }
	    public static string NotificationRegistrationId
        {
	        get => MdiSettings.GetValueOrDefault(nameof(NotificationRegistrationId), default(string));

	        set => MdiSettings.AddOrUpdateValue(nameof(NotificationRegistrationId), value);
	    }

        /// <summary>
        /// remember is the user logged in to the app for the first time in this device
        /// </summary>
        public static bool IsFirstTimeUser
        {
            get
            {
                return AppSettings.GetValueOrDefault(IsNewUserkey, IsFirstTimeDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(IsNewUserkey, value);
            }
        }

        public static MdiUser User
	    {
	        get => MdiSettings.GetValueOrDefault(nameof(User), default(MdiUser));

	        set => MdiSettings.AddOrUpdateValue(nameof(User), value);
	    }

	    public static List<MdiContact> UserContacts
	    {
	        get => MdiSettings.GetValueOrDefault(nameof(UserContacts), default(List<MdiContact>));
	        set => MdiSettings.AddOrUpdateValue(nameof(UserContacts), value);
        }

	    public static T GetValueOrDefault<T>(this ISettings settings, string key, T @default) where T : class
	    {
	        string serialized = settings.GetValueOrDefault(key, string.Empty);
	        T result = @default;

	        try
	        {
	            JsonSerializerSettings serializeSettings = GetSerializerSettings();
	            result = JsonConvert.DeserializeObject<T>(serialized);
	        }
	        catch (Exception ex)
	        {
	            System.Diagnostics.Debug.WriteLine($"Error deserializing settings value: {ex}");
	        }

	        return result;
	    }

	    public static bool AddOrUpdateValue<T>(this ISettings settings, string key, T obj) where T : class
	    {
	        try
	        {
	            JsonSerializerSettings serializeSettings = GetSerializerSettings();
	            string serialized = JsonConvert.SerializeObject(obj, serializeSettings);

	            return settings.AddOrUpdateValue(key, serialized);
	        }
	        catch (Exception ex)
	        {
	            System.Diagnostics.Debug.WriteLine($"Error serializing settings value: {ex}");
	        }

	        return false;
	    }

        private static JsonSerializerSettings GetSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

    }
}