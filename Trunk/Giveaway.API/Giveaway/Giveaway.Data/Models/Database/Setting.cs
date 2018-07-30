using Giveaway.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Giveaway.Data.Models.Database
{
	/// <summary>
	/// Model used for declaration of Setting
	/// </summary>
	[Table("Setting")]
	public class Setting : BaseEntity
	{
		#region Required Properties

		[Required]
		public GroupSettingType GroupType { get; set; }

		[Required]
		public string SettingName { get; set; }

		[Required]
		public string SettingValue { get; set; }

		#endregion
	}
}
