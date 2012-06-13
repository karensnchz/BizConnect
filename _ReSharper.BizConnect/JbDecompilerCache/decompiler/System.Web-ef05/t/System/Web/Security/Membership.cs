// Type: System.Web.Security.Membership
// Assembly: System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Web.dll

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Security
{
  /// <summary>
  /// Validates user credentials and manages user settings. This class cannot be inherited.
  /// </summary>
  public static class Membership
  {
    private static char[] punctuations = "!@#$%^&*()_-+=[{]};:>|./?".ToCharArray();
    private static int s_UserIsOnlineTimeWindow = 15;
    private static object s_lock = new object();
    private static bool s_Initialized = false;
    private static Exception s_InitializeException = (Exception) null;
    private static MembershipProviderCollection s_Providers;
    private static MembershipProvider s_Provider;
    private static bool s_InitializedDefaultProvider;
    private static string s_HashAlgorithmType;
    private static bool s_HashAlgorithmFromConfig;

    /// <summary>
    /// Gets a value indicating whether the current membership provider is configured to allow users to retrieve their passwords.
    /// </summary>
    /// 
    /// <returns>
    /// true if the membership provider supports password retrieval; otherwise, false.
    /// </returns>
    public static bool EnablePasswordRetrieval
    {
      get
      {
        Membership.Initialize();
        return Membership.Provider.EnablePasswordRetrieval;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the current membership provider is configured to allow users to reset their passwords.
    /// </summary>
    /// 
    /// <returns>
    /// true if the membership provider supports password reset; otherwise, false.
    /// </returns>
    public static bool EnablePasswordReset
    {
      get
      {
        Membership.Initialize();
        return Membership.Provider.EnablePasswordReset;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the default membership provider requires the user to answer a password question for password reset and retrieval.
    /// </summary>
    /// 
    /// <returns>
    /// true if a password answer is required for password reset and retrieval; otherwise, false.
    /// </returns>
    public static bool RequiresQuestionAndAnswer
    {
      get
      {
        Membership.Initialize();
        return Membership.Provider.RequiresQuestionAndAnswer;
      }
    }

    /// <summary>
    /// Specifies the number of minutes after the last-activity date/time stamp for a user during which the user is considered online.
    /// </summary>
    /// 
    /// <returns>
    /// The number of minutes after the last-activity date/time stamp for a user during which the user is considered online.
    /// </returns>
    public static int UserIsOnlineTimeWindow
    {
      get
      {
        Membership.Initialize();
        return Membership.s_UserIsOnlineTimeWindow;
      }
    }

    /// <summary>
    /// Gets a collection of the membership providers for the ASP.NET application.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipProviderCollection"/> of the membership providers configured for the ASP.NET application.
    /// </returns>
    public static MembershipProviderCollection Providers
    {
      get
      {
        Membership.Initialize();
        return Membership.s_Providers;
      }
    }

    /// <summary>
    /// Gets a reference to the default membership provider for the application.
    /// </summary>
    /// 
    /// <returns>
    /// The default membership provider for the application exposed using the <see cref="T:System.Web.Security.MembershipProvider"/> abstract base class.
    /// </returns>
    public static MembershipProvider Provider
    {
      get
      {
        Membership.Initialize();
        if (Membership.s_Provider == null)
          throw new InvalidOperationException(System.Web.SR.GetString("Def_membership_provider_not_found"));
        else
          return Membership.s_Provider;
      }
    }

    /// <summary>
    /// The identifier of the algorithm used to hash passwords.
    /// </summary>
    /// 
    /// <returns>
    /// The identifier of the algorithm used to hash passwords, or blank to use the default hash algorithm.
    /// </returns>
    public static string HashAlgorithmType
    {
      get
      {
        Membership.Initialize();
        return Membership.s_HashAlgorithmType;
      }
    }

    internal static bool IsHashAlgorithmFromMembershipConfig
    {
      get
      {
        Membership.Initialize();
        return Membership.s_HashAlgorithmFromConfig;
      }
    }

    /// <summary>
    /// Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.
    /// </summary>
    /// 
    /// <returns>
    /// The number of invalid password or password-answer attempts allowed before the membership user is locked out.
    /// </returns>
    public static int MaxInvalidPasswordAttempts
    {
      get
      {
        Membership.Initialize();
        return Membership.Provider.MaxInvalidPasswordAttempts;
      }
    }

    /// <summary>
    /// Gets the time window between which consecutive failed attempts to provide a valid password or password answer are tracked.
    /// </summary>
    /// 
    /// <returns>
    /// The time window, in minutes, during which consecutive failed attempts to provide a valid password or password answer are tracked. The default is 10 minutes. If the interval between the current failed attempt and the last failed attempt is greater than the <see cref="P:System.Web.Security.Membership.PasswordAttemptWindow"/> property setting, each failed attempt is treated as if it were the first failed attempt.
    /// </returns>
    public static int PasswordAttemptWindow
    {
      get
      {
        Membership.Initialize();
        return Membership.Provider.PasswordAttemptWindow;
      }
    }

    /// <summary>
    /// Gets the minimum length required for a password.
    /// </summary>
    /// 
    /// <returns>
    /// The minimum length required for a password.
    /// </returns>
    public static int MinRequiredPasswordLength
    {
      get
      {
        Membership.Initialize();
        return Membership.Provider.MinRequiredPasswordLength;
      }
    }

    /// <summary>
    /// Gets the minimum number of special characters that must be present in a valid password.
    /// </summary>
    /// 
    /// <returns>
    /// The minimum number of special characters that must be present in a valid password.
    /// </returns>
    public static int MinRequiredNonAlphanumericCharacters
    {
      get
      {
        Membership.Initialize();
        return Membership.Provider.MinRequiredNonAlphanumericCharacters;
      }
    }

    /// <summary>
    /// Gets the regular expression used to evaluate a password.
    /// </summary>
    /// 
    /// <returns>
    /// A regular expression used to evaluate a password.
    /// </returns>
    public static string PasswordStrengthRegularExpression
    {
      get
      {
        Membership.Initialize();
        return Membership.Provider.PasswordStrengthRegularExpression;
      }
    }

    /// <summary>
    /// Gets or sets the name of the application.
    /// </summary>
    /// 
    /// <returns>
    /// The name of the application.
    /// </returns>
    public static string ApplicationName
    {
      get
      {
        return Membership.Provider.ApplicationName;
      }
      set
      {
        Membership.Provider.ApplicationName = value;
      }
    }

    /// <summary>
    /// Occurs when a user is created, a password is changed, or a password is reset.
    /// </summary>
    public static event MembershipValidatePasswordEventHandler ValidatingPassword
    {
      add
      {
        Membership.Provider.ValidatingPassword += value;
      }
      remove
      {
        Membership.Provider.ValidatingPassword -= value;
      }
    }

    static Membership()
    {
    }

    /// <summary>
    /// Adds a new user to the data store.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUser"/> object for the newly created user.
    /// </returns>
    /// <param name="username">The user name for the new user. </param><param name="password">The password for the new user. </param><exception cref="T:System.Web.Security.MembershipCreateUserException">The user was not created. Check the <see cref="P:System.Web.Security.MembershipCreateUserException.StatusCode"/> property for a <see cref="T:System.Web.Security.MembershipCreateStatus"/> value. </exception>
    public static MembershipUser CreateUser(string username, string password)
    {
      return Membership.CreateUser(username, password, (string) null);
    }

    /// <summary>
    /// Adds a new user with a specified e-mail address to the data store.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUser"/> object for the newly created user.
    /// </returns>
    /// <param name="username">The user name for the new user. </param><param name="password">The password for the new user. </param><param name="email">The e-mail address for the new user. </param><exception cref="T:System.Web.Security.MembershipCreateUserException">The user was not created. Check the <see cref="P:System.Web.Security.MembershipCreateUserException.StatusCode"/> property for a <see cref="T:System.Web.Security.MembershipCreateStatus"/> value. </exception>
    public static MembershipUser CreateUser(string username, string password, string email)
    {
      MembershipCreateStatus status;
      MembershipUser user = Membership.CreateUser(username, password, email, (string) null, (string) null, true, out status);
      if (user == null)
        throw new MembershipCreateUserException(status);
      else
        return user;
    }

    /// <summary>
    /// Adds a new user with specified property values to the data store and returns a status parameter indicating that the user was successfully created or the reason the user creation failed.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUser"/> object for the newly created user. If no user was created, this method returns null.
    /// </returns>
    /// <param name="username">The user name for the new user. </param><param name="password">The password for the new user. </param><param name="email">The e-mail address for the new user. </param><param name="passwordQuestion">The password-question value for the membership user.</param><param name="passwordAnswer">The password-answer value for the membership user.</param><param name="isApproved">A Boolean that indicates whether the new user is approved to log on.</param><param name="status">A <see cref="T:System.Web.Security.MembershipCreateStatus"/> indicating that the user was created successfully or the reason that creation failed. </param>
    public static MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, out MembershipCreateStatus status)
    {
      return Membership.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, (object) null, out status);
    }

    /// <summary>
    /// Adds a new user with specified property values and a unique identifier to the data store and returns a status parameter indicating that the user was successfully created or the reason the user creation failed.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUser"/> object for the newly created user. If no user was created, this method returns null.
    /// </returns>
    /// <param name="username">The user name for the new user.</param><param name="password">The password for the new user.</param><param name="email">The e-mail address for the new user.</param><param name="passwordQuestion">The password-question value for the membership user.</param><param name="passwordAnswer">The password-answer value for the membership user.</param><param name="isApproved">A Boolean that indicates whether the new user is approved to log on.</param><param name="providerUserKey">The user identifier for the user that should be stored in the membership data store.</param><param name="status">A <see cref="T:System.Web.Security.MembershipCreateStatus"/> indicating that the user was created successfully or the reason creation failed.</param>
    public static MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
    {
      if (!SecUtility.ValidateParameter(ref username, true, true, true, 0))
      {
        status = MembershipCreateStatus.InvalidUserName;
        return (MembershipUser) null;
      }
      else if (!SecUtility.ValidatePasswordParameter(ref password, 0))
      {
        status = MembershipCreateStatus.InvalidPassword;
        return (MembershipUser) null;
      }
      else if (!SecUtility.ValidateParameter(ref email, false, false, false, 0))
      {
        status = MembershipCreateStatus.InvalidEmail;
        return (MembershipUser) null;
      }
      else if (!SecUtility.ValidateParameter(ref passwordQuestion, false, true, false, 0))
      {
        status = MembershipCreateStatus.InvalidQuestion;
        return (MembershipUser) null;
      }
      else
      {
        if (SecUtility.ValidateParameter(ref passwordAnswer, false, true, false, 0))
          return Membership.Provider.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status);
        status = MembershipCreateStatus.InvalidAnswer;
        return (MembershipUser) null;
      }
    }

    /// <summary>
    /// Verifies that the supplied user name and password are valid.
    /// </summary>
    /// 
    /// <returns>
    /// true if the supplied user name and password are valid; otherwise, false.
    /// </returns>
    /// <param name="username">The name of the user to be validated. </param><param name="password">The password for the specified user. </param>
    public static bool ValidateUser(string username, string password)
    {
      return Membership.Provider.ValidateUser(username, password);
    }

    /// <summary>
    /// Gets the information from the data source and updates the last-activity date/time stamp for the current logged-on membership user.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUser"/> object representing the current logged-on user.
    /// </returns>
    public static MembershipUser GetUser()
    {
      return Membership.GetUser(Membership.GetCurrentUserName(), true);
    }

    /// <summary>
    /// Gets the information from the data source for the current logged-on membership user. Updates the last-activity date/time stamp for the current logged-on membership user, if specified.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUser"/> object representing the current logged-on user.
    /// </returns>
    /// <param name="userIsOnline">If true, updates the last-activity date/time stamp for the specified user. </param>
    public static MembershipUser GetUser(bool userIsOnline)
    {
      return Membership.GetUser(Membership.GetCurrentUserName(), userIsOnline);
    }

    /// <summary>
    /// Gets the information from the data source for the specified membership user.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUser"/> object representing the specified user.
    /// </returns>
    /// <param name="username">The name of the user to retrieve.</param><exception cref="T:System.ArgumentException"><paramref name="username"/> contains a comma (,). </exception><exception cref="T:System.ArgumentNullException"><paramref name="username"/> is null.</exception>
    public static MembershipUser GetUser(string username)
    {
      return Membership.GetUser(username, false);
    }

    /// <summary>
    /// Gets the information from the data source for the specified membership user. Updates the last-activity date/time stamp for the user, if specified.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUser"/> object representing the specified user.
    /// </returns>
    /// <param name="username">The name of the user to retrieve. </param><param name="userIsOnline">If true, updates the last-activity date/time stamp for the specified user. </param><exception cref="T:System.ArgumentException"><paramref name="username"/> contains a comma (,). </exception><exception cref="T:System.ArgumentNullException"><paramref name="username"/> is null.</exception>
    public static MembershipUser GetUser(string username, bool userIsOnline)
    {
      SecUtility.CheckParameter(ref username, true, false, true, 0, "username");
      return Membership.Provider.GetUser(username, userIsOnline);
    }

    /// <summary>
    /// Gets the information from the data source for the membership user associated with the specified unique identifier.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUser"/> object representing the user associated with the specified unique identifier.
    /// </returns>
    /// <param name="providerUserKey">The unique user identifier from the membership data source for the user.</param><exception cref="T:System.ArgumentNullException"><paramref name="providerUserKey"/> is null. </exception>
    public static MembershipUser GetUser(object providerUserKey)
    {
      return Membership.GetUser(providerUserKey, false);
    }

    /// <summary>
    /// Gets the information from the data source for the membership user associated with the specified unique identifier. Updates the last-activity date/time stamp for the user, if specified.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUser"/> object representing the user associated with the specified unique identifier.
    /// </returns>
    /// <param name="providerUserKey">The unique user identifier from the membership data source for the user.</param><param name="userIsOnline">If true, updates the last-activity date/time stamp for the specified user.</param><exception cref="T:System.ArgumentNullException"><paramref name="providerUserKey"/> is null. </exception>
    public static MembershipUser GetUser(object providerUserKey, bool userIsOnline)
    {
      if (providerUserKey == null)
        throw new ArgumentNullException("providerUserKey");
      else
        return Membership.Provider.GetUser(providerUserKey, userIsOnline);
    }

    /// <summary>
    /// Gets a user name where the e-mail address for the user matches the specified e-mail address.
    /// </summary>
    /// 
    /// <returns>
    /// The user name where the e-mail address for the user matches the specified e-mail address. If no match is found, null is returned.
    /// </returns>
    /// <param name="emailToMatch">The e-mail address to search for. </param>
    public static string GetUserNameByEmail(string emailToMatch)
    {
      SecUtility.CheckParameter(ref emailToMatch, false, false, false, 0, "emailToMatch");
      return Membership.Provider.GetUserNameByEmail(emailToMatch);
    }

    /// <summary>
    /// Deletes a user and any related user data from the database.
    /// </summary>
    /// 
    /// <returns>
    /// true if the user was deleted; otherwise, false.
    /// </returns>
    /// <param name="username">The name of the user to delete. </param><exception cref="T:System.ArgumentException"><paramref name="username"/> is an empty string or contains a comma (,). </exception><exception cref="T:System.ArgumentNullException"><paramref name="username"/> is null.</exception>
    public static bool DeleteUser(string username)
    {
      SecUtility.CheckParameter(ref username, true, true, true, 0, "username");
      return Membership.Provider.DeleteUser(username, true);
    }

    /// <summary>
    /// Deletes a user from the database.
    /// </summary>
    /// 
    /// <returns>
    /// true if the user was deleted; otherwise, false.
    /// </returns>
    /// <param name="username">The name of the user to delete.</param><param name="deleteAllRelatedData">true to delete data related to the user from the database; false to leave data related to the user in the database.</param><exception cref="T:System.ArgumentException"><paramref name="username"/> is an empty string or contains a comma (,). </exception><exception cref="T:System.ArgumentNullException"><paramref name="username"/> is null.</exception>
    public static bool DeleteUser(string username, bool deleteAllRelatedData)
    {
      SecUtility.CheckParameter(ref username, true, true, true, 0, "username");
      return Membership.Provider.DeleteUser(username, deleteAllRelatedData);
    }

    /// <summary>
    /// Updates the database with the information for the specified user.
    /// </summary>
    /// <param name="user">A <see cref="T:System.Web.Security.MembershipUser"/> object that represents the user to be updated and the updated information for the user. </param><exception cref="T:System.ArgumentNullException"><paramref name="user"/> is null.</exception>
    public static void UpdateUser(MembershipUser user)
    {
      if (user == null)
        throw new ArgumentNullException("user");
      user.Update();
    }

    /// <summary>
    /// Gets a collection of all the users in the database.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUserCollection"/> of <see cref="T:System.Web.Security.MembershipUser"/> objects representing all of the users in the database.
    /// </returns>
    public static MembershipUserCollection GetAllUsers()
    {
      int totalRecords = 0;
      return Membership.GetAllUsers(0, int.MaxValue, out totalRecords);
    }

    /// <summary>
    /// Gets a collection of all the users in the database in pages of data.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUserCollection"/> of <see cref="T:System.Web.Security.MembershipUser"/> objects representing all the users in the database for the configured applicationName.
    /// </returns>
    /// <param name="pageIndex">The index of the page of results to return. Use 0 to indicate the first page.</param><param name="pageSize">The size of the page of results to return. <paramref name="pageIndex"/> is zero-based.</param><param name="totalRecords">The total number of users.</param><exception cref="T:System.ArgumentException"><paramref name="pageIndex"/> is less than zero.-or-<paramref name="pageSize"/> is less than 1.</exception>
    public static MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
    {
      if (pageIndex < 0)
        throw new ArgumentException(System.Web.SR.GetString("PageIndex_bad"), "pageIndex");
      if (pageSize < 1)
        throw new ArgumentException(System.Web.SR.GetString("PageSize_bad"), "pageSize");
      else
        return Membership.Provider.GetAllUsers(pageIndex, pageSize, out totalRecords);
    }

    /// <summary>
    /// Gets the number of users currently accessing an application.
    /// </summary>
    /// 
    /// <returns>
    /// The number of users currently accessing an application.
    /// </returns>
    public static int GetNumberOfUsersOnline()
    {
      return Membership.Provider.GetNumberOfUsersOnline();
    }

    /// <summary>
    /// Generates a random password of the specified length.
    /// </summary>
    /// 
    /// <returns>
    /// A random password of the specified length.
    /// </returns>
    /// <param name="length">The number of characters in the generated password. The length must be between 1 and 128 characters. </param><param name="numberOfNonAlphanumericCharacters">The minimum number of punctuation characters in the generated password.</param><exception cref="T:System.ArgumentException"><paramref name="length"/> is less than 1 or greater than 128 -or-<paramref name="numberOfNonAlphanumericCharacters"/> is less than 0 or greater than <paramref name="length"/>. </exception>
    public static string GeneratePassword(int length, int numberOfNonAlphanumericCharacters)
    {
      if (length < 1 || length > 128)
        throw new ArgumentException(System.Web.SR.GetString("Membership_password_length_incorrect"));
      if (numberOfNonAlphanumericCharacters > length || numberOfNonAlphanumericCharacters < 0)
      {
        throw new ArgumentException(System.Web.SR.GetString("Membership_min_required_non_alphanumeric_characters_incorrect", new object[1]
        {
          (object) "numberOfNonAlphanumericCharacters"
        }));
      }
      else
      {
        string s;
        int matchIndex;
        do
        {
          byte[] data = new byte[length];
          char[] chArray = new char[length];
          int num1 = 0;
          new RNGCryptoServiceProvider().GetBytes(data);
          for (int index = 0; index < length; ++index)
          {
            int num2 = (int) data[index] % 87;
            if (num2 < 10)
              chArray[index] = (char) (48 + num2);
            else if (num2 < 36)
              chArray[index] = (char) (65 + num2 - 10);
            else if (num2 < 62)
            {
              chArray[index] = (char) (97 + num2 - 36);
            }
            else
            {
              chArray[index] = Membership.punctuations[num2 - 62];
              ++num1;
            }
          }
          if (num1 < numberOfNonAlphanumericCharacters)
          {
            Random random = new Random();
            for (int index1 = 0; index1 < numberOfNonAlphanumericCharacters - num1; ++index1)
            {
              int index2;
              do
              {
                index2 = random.Next(0, length);
              }
              while (!char.IsLetterOrDigit(chArray[index2]));
              chArray[index2] = Membership.punctuations[random.Next(0, Membership.punctuations.Length)];
            }
          }
          s = new string(chArray);
        }
        while (CrossSiteScriptingValidation.IsDangerousString(s, out matchIndex));
        return s;
      }
    }

    private static void Initialize()
    {
      if (Membership.s_Initialized && Membership.s_InitializedDefaultProvider)
        return;
      if (Membership.s_InitializeException != null)
        throw Membership.s_InitializeException;
      if (HostingEnvironment.IsHosted)
        HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Low, "Feature_not_supported_at_this_level");
      lock (Membership.s_lock)
      {
        if (Membership.s_Initialized && Membership.s_InitializedDefaultProvider)
          return;
        if (Membership.s_InitializeException != null)
          throw Membership.s_InitializeException;
        bool local_0 = !Membership.s_Initialized;
        bool local_1 = !Membership.s_InitializedDefaultProvider && (!HostingEnvironment.IsHosted || BuildManager.PreStartInitStage == PreStartInitStage.AfterPreStartInit);
        if (!local_1 && !local_0)
          return;
        bool local_2;
        bool local_3_1;
        try
        {
          RuntimeConfig local_4 = RuntimeConfig.GetAppConfig();
          MembershipSection local_5 = local_4.Membership;
          local_2 = Membership.InitializeSettings(local_0, local_4, local_5);
          local_3_1 = Membership.InitializeDefaultProvider(local_1, local_5);
        }
        catch (Exception exception_0)
        {
          Membership.s_InitializeException = exception_0;
          throw;
        }
        if (local_2)
          Membership.s_Initialized = true;
        if (!local_3_1)
          return;
        Membership.s_InitializedDefaultProvider = true;
      }
    }

    private static bool InitializeSettings(bool initializeGeneralSettings, RuntimeConfig appConfig, MembershipSection settings)
    {
      if (!initializeGeneralSettings)
        return false;
      Membership.s_HashAlgorithmType = settings.HashAlgorithmType;
      Membership.s_HashAlgorithmFromConfig = !string.IsNullOrEmpty(Membership.s_HashAlgorithmType);
      if (!Membership.s_HashAlgorithmFromConfig)
      {
        switch (appConfig.MachineKey.Validation)
        {
          case MachineKeyValidation.AES:
          case MachineKeyValidation.TripleDES:
            Membership.s_HashAlgorithmType = "SHA1";
            break;
          default:
            Membership.s_HashAlgorithmType = appConfig.MachineKey.ValidationAlgorithm;
            break;
        }
      }
      Membership.s_Providers = new MembershipProviderCollection();
      if (HostingEnvironment.IsHosted)
      {
        ProvidersHelper.InstantiateProviders(settings.Providers, (ProviderCollection) Membership.s_Providers, typeof (MembershipProvider));
      }
      else
      {
        foreach (ProviderSettings providerSettings in (ConfigurationElementCollection) settings.Providers)
        {
          Type type = Type.GetType(providerSettings.Type, true, true);
          if (!typeof (MembershipProvider).IsAssignableFrom(type))
          {
            throw new ArgumentException(System.Web.SR.GetString("Provider_must_implement_type", new object[1]
            {
              (object) typeof (MembershipProvider).ToString()
            }));
          }
          else
          {
            MembershipProvider membershipProvider = (MembershipProvider) Activator.CreateInstance(type);
            NameValueCollection parameters = providerSettings.Parameters;
            NameValueCollection config = new NameValueCollection(parameters.Count, (IEqualityComparer) StringComparer.Ordinal);
            foreach (string index in (NameObjectCollectionBase) parameters)
              config[index] = parameters[index];
            membershipProvider.Initialize(providerSettings.Name, config);
            Membership.s_Providers.Add((ProviderBase) membershipProvider);
          }
        }
      }
      Membership.s_UserIsOnlineTimeWindow = (int) settings.UserIsOnlineTimeWindow.TotalMinutes;
      return true;
    }

    private static bool InitializeDefaultProvider(bool initializeDefaultProvider, MembershipSection settings)
    {
      if (!initializeDefaultProvider)
        return false;
      Membership.s_Providers.SetReadOnly();
      if (settings.DefaultProvider == null || Membership.s_Providers.Count < 1)
        throw new ProviderException(System.Web.SR.GetString("Def_membership_provider_not_specified"));
      Membership.s_Provider = Membership.s_Providers[settings.DefaultProvider];
      if (Membership.s_Provider == null)
        throw new ConfigurationErrorsException(System.Web.SR.GetString("Def_membership_provider_not_found"), settings.ElementInformation.Properties["defaultProvider"].Source, settings.ElementInformation.Properties["defaultProvider"].LineNumber);
      else
        return true;
    }

    /// <summary>
    /// Gets a collection of membership users, in a page of data, where the user name contains the specified user name to match.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUserCollection"/> that contains a page of <paramref name="pageSize"/><see cref="T:System.Web.Security.MembershipUser"/> objects beginning at the page specified by <paramref name="pageIndex"/>.Leading and trailing spaces are trimmed from the <paramref name="usernameToMatch"/> parameter value.
    /// </returns>
    /// <param name="usernameToMatch">The user name to search for.</param><param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex"/> is zero-based.</param><param name="pageSize">The size of the page of results to return.</param><param name="totalRecords">The total number of matched users.</param><exception cref="T:System.ArgumentException"><paramref name="usernameToMatch"/> is an empty string.-or-<paramref name="pageIndex"/> is less than zero.-or-<paramref name="pageSize"/> is less than 1.</exception><exception cref="T:System.ArgumentNullException"><paramref name="usernameToMatch"/> is null.</exception>
    public static MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
      SecUtility.CheckParameter(ref usernameToMatch, true, true, false, 0, "usernameToMatch");
      if (pageIndex < 0)
        throw new ArgumentException(System.Web.SR.GetString("PageIndex_bad"), "pageIndex");
      if (pageSize < 1)
        throw new ArgumentException(System.Web.SR.GetString("PageSize_bad"), "pageSize");
      else
        return Membership.Provider.FindUsersByName(usernameToMatch, pageIndex, pageSize, out totalRecords);
    }

    /// <summary>
    /// Gets a collection of membership users where the user name contains the specified user name to match.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUserCollection"/> that contains all users that match the <paramref name="usernameToMatch"/> parameter.Leading and trailing spaces are trimmed from the <paramref name="usernameToMatch"/> parameter value.
    /// </returns>
    /// <param name="usernameToMatch">The user name to search for.</param><exception cref="T:System.ArgumentException"><paramref name="usernameToMatch"/> is an empty string.</exception><exception cref="T:System.ArgumentNullException"><paramref name="usernameToMatch"/> is null.</exception>
    public static MembershipUserCollection FindUsersByName(string usernameToMatch)
    {
      SecUtility.CheckParameter(ref usernameToMatch, true, true, false, 0, "usernameToMatch");
      int totalRecords = 0;
      return Membership.Provider.FindUsersByName(usernameToMatch, 0, int.MaxValue, out totalRecords);
    }

    /// <summary>
    /// Gets a collection of membership users, in a page of data, where the e-mail address contains the specified e-mail address to match.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUserCollection"/> that contains a page of <paramref name="pageSize"/><see cref="T:System.Web.Security.MembershipUser"/> objects beginning at the page specified by <paramref name="pageIndex"/>.
    /// </returns>
    /// <param name="emailToMatch">The e-mail address to search for.</param><param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex"/> is zero-based.</param><param name="pageSize">The size of the page of results to return.</param><param name="totalRecords">The total number of matched users.</param><exception cref="T:System.ArgumentException"><paramref name="pageIndex"/> is less than zero.-or-<paramref name="pageSize"/> is less than 1.</exception>
    public static MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
      SecUtility.CheckParameter(ref emailToMatch, false, false, false, 0, "emailToMatch");
      if (pageIndex < 0)
        throw new ArgumentException(System.Web.SR.GetString("PageIndex_bad"), "pageIndex");
      if (pageSize < 1)
        throw new ArgumentException(System.Web.SR.GetString("PageSize_bad"), "pageSize");
      else
        return Membership.Provider.FindUsersByEmail(emailToMatch, pageIndex, pageSize, out totalRecords);
    }

    /// <summary>
    /// Gets a collection of membership users where the e-mail address contains the specified e-mail address to match.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUserCollection"/> that contains all users that match the <paramref name="emailToMatch"/> parameter.Leading and trailing spaces are trimmed from the <paramref name="emailToMatch"/> parameter value.
    /// </returns>
    /// <param name="emailToMatch">The e-mail address to search for.</param>
    public static MembershipUserCollection FindUsersByEmail(string emailToMatch)
    {
      SecUtility.CheckParameter(ref emailToMatch, false, false, false, 0, "emailToMatch");
      int totalRecords = 0;
      return Membership.FindUsersByEmail(emailToMatch, 0, int.MaxValue, out totalRecords);
    }

    private static string GetCurrentUserName()
    {
      if (HostingEnvironment.IsHosted)
      {
        HttpContext current = HttpContext.Current;
        if (current != null)
          return current.User.Identity.Name;
      }
      IPrincipal currentPrincipal = Thread.CurrentPrincipal;
      if (currentPrincipal == null || currentPrincipal.Identity == null)
        return string.Empty;
      else
        return currentPrincipal.Identity.Name;
    }
  }
}
