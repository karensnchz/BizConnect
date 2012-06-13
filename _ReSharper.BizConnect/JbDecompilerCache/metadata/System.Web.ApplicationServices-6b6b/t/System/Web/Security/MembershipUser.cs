// Type: System.Web.Security.MembershipUser
// Assembly: System.Web.ApplicationServices, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Web.ApplicationServices.dll

using System;
using System.Runtime;
using System.Runtime.CompilerServices;

namespace System.Web.Security
{
    /// <summary>
    /// Exposes and updates membership user information in the membership data store.
    /// </summary>
    [TypeForwardedFrom("System.Web, Version=2.0.0.0, Culture=Neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [Serializable]
    public class MembershipUser
    {
        /// <summary>
        /// Creates a new membership user object with the specified property values.
        /// </summary>
        /// <param name="providerName">The <see cref="P:System.Web.Security.MembershipUser.ProviderName"/> string for the membership user.</param><param name="name">The <see cref="P:System.Web.Security.MembershipUser.UserName"/> string for the membership user.</param><param name="providerUserKey">The <see cref="P:System.Web.Security.MembershipUser.ProviderUserKey"/> identifier for the membership user.</param><param name="email">The <see cref="P:System.Web.Security.MembershipUser.Email"/> string for the membership user.</param><param name="passwordQuestion">The <see cref="P:System.Web.Security.MembershipUser.PasswordQuestion"/> string for the membership user.</param><param name="comment">The <see cref="P:System.Web.Security.MembershipUser.Comment"/> string for the membership user.</param><param name="isApproved">The <see cref="P:System.Web.Security.MembershipUser.IsApproved"/> value for the membership user.</param><param name="isLockedOut">true to lock out the membership user; otherwise, false.</param><param name="creationDate">The <see cref="P:System.Web.Security.MembershipUser.CreationDate"/><see cref="T:System.DateTime"/> object for the membership user.</param><param name="lastLoginDate">The <see cref="P:System.Web.Security.MembershipUser.LastLoginDate"/><see cref="T:System.DateTime"/> object for the membership user.</param><param name="lastActivityDate">The <see cref="P:System.Web.Security.MembershipUser.LastActivityDate"/><see cref="T:System.DateTime"/> object for the membership user.</param><param name="lastPasswordChangedDate">The <see cref="P:System.Web.Security.MembershipUser.LastPasswordChangedDate"/><see cref="T:System.DateTime"/> object for the membership user.</param><param name="lastLockoutDate">The <see cref="P:System.Web.Security.MembershipUser.LastLockoutDate"/><see cref="T:System.DateTime"/> object for the membership user.</param><exception cref="T:System.ArgumentException"><paramref name="providerName"/> is null.-or-<paramref name="providerName"/> is not found in the <see cref="P:System.Web.Security.Membership.Providers"/> collection.</exception>
        public MembershipUser(string providerName, string name, object providerUserKey, string email, string passwordQuestion, string comment, bool isApproved, bool isLockedOut, DateTime creationDate, DateTime lastLoginDate, DateTime lastActivityDate, DateTime lastPasswordChangedDate, DateTime lastLockoutDate);

        /// <summary>
        /// Creates a new instance of a <see cref="T:System.Web.Security.MembershipUser"/> object for a class that inherits the <see cref="T:System.Web.Security.MembershipUser"/> class.
        /// </summary>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        protected MembershipUser();

        /// <summary>
        /// Returns the user name for the membership user.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="P:System.Web.Security.MembershipUser.UserName"/> for the membership user.
        /// </returns>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public override string ToString();

        /// <summary>
        /// Gets the password for the membership user from the membership data store.
        /// </summary>
        /// 
        /// <returns>
        /// The password for the membership user.
        /// </returns>
        public virtual string GetPassword();

        /// <summary>
        /// Gets the password for the membership user from the membership data store.
        /// </summary>
        /// 
        /// <returns>
        /// The password for the membership user.
        /// </returns>
        /// <param name="passwordAnswer">The password answer for the membership user.</param>
        public virtual string GetPassword(string passwordAnswer);

        /// <summary>
        /// Updates the password for the membership user in the membership data store.
        /// </summary>
        /// 
        /// <returns>
        /// true if the update was successful; otherwise, false.
        /// </returns>
        /// <param name="oldPassword">The current password for the membership user.</param><param name="newPassword">The new password for the membership user.</param><exception cref="T:System.ArgumentException"><paramref name="oldPassword"/> is an empty string.-or-<paramref name="newPassword"/> is an empty string.</exception><exception cref="T:System.ArgumentNullException"><paramref name="oldPassword"/> is null.-or-<paramref name="newPassword"/> is null.</exception>
        public virtual bool ChangePassword(string oldPassword, string newPassword);

        /// <summary>
        /// Updates the password question and answer for the membership user in the membership data store.
        /// </summary>
        /// 
        /// <returns>
        /// true if the update was successful; otherwise, false.
        /// </returns>
        /// <param name="password">The current password for the membership user.</param><param name="newPasswordQuestion">The new password question value for the membership user.</param><param name="newPasswordAnswer">The new password answer value for the membership user.</param><exception cref="T:System.ArgumentException"><paramref name="password"/> is an empty string.-or-<paramref name="newPasswordQuestion"/> is an empty string.-or-<paramref name="newPasswordAnswer"/> is an empty string.</exception><exception cref="T:System.ArgumentNullException"><paramref name="password"/> is null.</exception>
        public virtual bool ChangePasswordQuestionAndAnswer(string password, string newPasswordQuestion, string newPasswordAnswer);

        /// <summary>
        /// Resets a user's password to a new, automatically generated password.
        /// </summary>
        /// 
        /// <returns>
        /// The new password for the membership user.
        /// </returns>
        /// <param name="passwordAnswer">The password answer for the membership user.</param>
        public virtual string ResetPassword(string passwordAnswer);

        /// <summary>
        /// Resets a user's password to a new, automatically generated password.
        /// </summary>
        /// 
        /// <returns>
        /// The new password for the membership user.
        /// </returns>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public virtual string ResetPassword();

        /// <summary>
        /// Clears the locked-out state of the user so that the membership user can be validated.
        /// </summary>
        /// 
        /// <returns>
        /// true if the membership user was successfully unlocked; otherwise, false.
        /// </returns>
        public virtual bool UnlockUser();

        /// <summary>
        /// Gets the logon name of the membership user.
        /// </summary>
        /// 
        /// <returns>
        /// The logon name of the membership user.
        /// </returns>
        public virtual string UserName { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        /// <summary>
        /// Gets the user identifier from the membership data source for the user.
        /// </summary>
        /// 
        /// <returns>
        /// The user identifier from the membership data source for the user.
        /// </returns>
        public virtual object ProviderUserKey { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        /// <summary>
        /// Gets or sets the e-mail address for the membership user.
        /// </summary>
        /// 
        /// <returns>
        /// The e-mail address for the membership user.
        /// </returns>
        public virtual string Email { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        /// <summary>
        /// Gets the password question for the membership user.
        /// </summary>
        /// 
        /// <returns>
        /// The password question for the membership user.
        /// </returns>
        public virtual string PasswordQuestion { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        /// <summary>
        /// Gets or sets application-specific information for the membership user.
        /// </summary>
        /// 
        /// <returns>
        /// Application-specific information for the membership user.
        /// </returns>
        public virtual string Comment { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        /// <summary>
        /// Gets or sets whether the membership user can be authenticated.
        /// </summary>
        /// 
        /// <returns>
        /// true if the user can be authenticated; otherwise, false.
        /// </returns>
        public virtual bool IsApproved { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        /// <summary>
        /// Gets a value indicating whether the membership user is locked out and unable to be validated.
        /// </summary>
        /// 
        /// <returns>
        /// true if the membership user is locked out and unable to be validated; otherwise, false.
        /// </returns>
        public virtual bool IsLockedOut { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        /// <summary>
        /// Gets the most recent date and time that the membership user was locked out.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.DateTime"/> object that represents the most recent date and time that the membership user was locked out.
        /// </returns>
        public virtual DateTime LastLockoutDate { get; }

        /// <summary>
        /// Gets the date and time when the user was added to the membership data store.
        /// </summary>
        /// 
        /// <returns>
        /// The date and time when the user was added to the membership data store.
        /// </returns>
        public virtual DateTime CreationDate { get; }

        /// <summary>
        /// Gets or sets the date and time when the user was last authenticated.
        /// </summary>
        /// 
        /// <returns>
        /// The date and time when the user was last authenticated.
        /// </returns>
        public virtual DateTime LastLoginDate { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the membership user was last authenticated or accessed the application.
        /// </summary>
        /// 
        /// <returns>
        /// The date and time when the membership user was last authenticated or accessed the application.
        /// </returns>
        public virtual DateTime LastActivityDate { get; set; }

        /// <summary>
        /// Gets the date and time when the membership user's password was last updated.
        /// </summary>
        /// 
        /// <returns>
        /// The date and time when the membership user's password was last updated.
        /// </returns>
        public virtual DateTime LastPasswordChangedDate { get; }

        /// <summary>
        /// Gets whether the user is currently online.
        /// </summary>
        /// 
        /// <returns>
        /// true if the user is online; otherwise, false.
        /// </returns>
        public virtual bool IsOnline { get; }

        /// <summary>
        /// Gets the name of the membership provider that stores and retrieves user information for the membership user.
        /// </summary>
        /// 
        /// <returns>
        /// The name of the membership provider that stores and retrieves user information for the membership user.
        /// </returns>
        public virtual string ProviderName { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }
    }
}
