using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaXiaLibrary.Models
{
	public class SkillFullModel
	{
        public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
        public int Level { get; set; }
        public int PlayerLevelId { get; set; }
        public int PlayerRoleId { get; set; }
        public string RoleName  { get; set; }
        public string Image { get; set; }
    }
}
