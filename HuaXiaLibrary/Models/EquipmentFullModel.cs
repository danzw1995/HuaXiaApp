using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaXiaLibrary.Models
{
	public class EquipmentFullModel
	{
        public int Id { get; set; }
        [DisplayName("名称")]
		[Required]
		public string Name { get; set; }
		[DisplayName("描述")]
		[Required]
		public string Description { get; set; }
		[DisplayName("部位")]
		[Required]
		public int EquipmentPartId { get; set; }
        public string EquipmentPartName { get; set; }
		[DisplayName("等级")]
		[Required]
		public int PlayerLevelId { get; set; }
        public int Level { get; set; }
		[DisplayName("职业")]
		[Required]
		public int PlayerRoleId { get; set; }
        public string PlayerRoleName { get; set; }
		[DisplayName("图片")]
		public string Image { get; set; }

    }
}
