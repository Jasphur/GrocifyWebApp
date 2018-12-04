using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using CompareAttribute = System.Web.Mvc.CompareAttribute;


namespace GrocifyAppMVC.Models
{
	public class IndexViewModel
	{
		[Display(Name = "Wijzig wachtwoord")]
		public bool HasPassword { get; set; }

		public IList<UserLoginInfo> Logins { get; set; }
		public string PhoneNumber { get; set; }
		public bool TwoFactor { get; set; }
		public bool BrowserRemembered { get; set; }

		[Display(Name = "Wijzig emailadres")]
		public bool HasEmail { get; set; }

		[Display(Name = "Gebruikersnaam:")]
		public string UserName { get;  set; }

		[Display(Name = "Emailadres:")]
		public string ShowEmail { get; set; }
	}



	public class ManageLoginsViewModel
	{
		public IList<UserLoginInfo> CurrentLogins { get; set; }
		public IList<AuthenticationDescription> OtherLogins { get; set; }
	}

	public class FactorViewModel
	{
		public string Purpose { get; set; }
	}

	public class SetPasswordViewModel
	{
		[Required(ErrorMessage = ErrorMessages.Required)]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Nieuw wachtwoord")]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Bevestig nieuw wachtwoord")]
		[Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}

	public class ChangePasswordViewModel
	{
		[Required(ErrorMessage = ErrorMessages.Required)]
		[DataType(DataType.Password)]
		[Display(Name = "Huidig wachtwoord")]
		public string OldPassword { get; set; }

		[Required(ErrorMessage = ErrorMessages.Required)]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Nieuw wachtwoord")]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Bevestig nieuw wachtwoord")]
		[Compare("NewPassword", ErrorMessage = "Het ingevoerde controle wachtwoord komt niet overeen met het ingevoerde wachtwoord.")]
		public string ConfirmPassword { get; set; }
	}

	public class ChangeEmailViewModel
	{
		//[Required]
		//[DataType(DataType.EmailAddress)]
		//[Display(Name = "Huidig emailadres")]
		//public string OldEmail { get; set; }

		[Required(ErrorMessage = ErrorMessages.Required)]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Nieuw emailadres")]
		public string NewEmail { get; set; }

		[DataType(DataType.EmailAddress)]
		[Display(Name = "Bevestig nieuw emailadres")]
		[Compare("NewEmail", ErrorMessage = "Het ingevoerde controle emailadres komt niet overeen met het ingevoerde emailadres.")]
		public string ConfirmEmail { get; set; }
	}

	//public class ChangeUserNameViewModel
	//{

	//	[Required]
	//	[DataType(DataType.Custom)]
	//	[Display(Name = "Nieuw gebruiker")]
	//	public string NewUserName { get; set; }

	//	[DataType(DataType.Custom)]
	//	[Display(Name = "Bevestig nieuw gebruiker")]
	//	[Compare("NewUserName", ErrorMessage = "Het ingevoerde controle gebruiker komt niet overeen met het ingevoerde gebruiker.")]
	//	public string ConfirmUserName { get; set; }
	//}

	public class AddPhoneNumberViewModel
	{
		[Required(ErrorMessage = ErrorMessages.Required)]
		[Phone]
		[Display(Name = "Phone Number")]
		public string Number { get; set; }
	}

	public class VerifyPhoneNumberViewModel
	{
		[Required(ErrorMessage = ErrorMessages.Required)]
		[Display(Name = "Code")]
		public string Code { get; set; }

		[Required(ErrorMessage = ErrorMessages.Required)]
		[Phone]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }
	}

	public class ConfigureTwoFactorViewModel
	{
		public string SelectedProvider { get; set; }
		public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
	}
}