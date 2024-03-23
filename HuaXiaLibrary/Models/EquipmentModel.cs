using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaXiaLibrary.Models
{
	public class EquipmentModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int EquipmentPartId { get; set; }
		public int PlayerLevelId { get; set; }
		public int PlayerRoleId { get; set; }
		public int EquipmentGradeId {  get; set; }
		public string Image { get; set; }
	}
}
