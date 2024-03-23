using HuaXiaLibrary.Models;

namespace HuaXiaLibrary.Data
{
	public interface IDatabaseData
	{
		void CreateEquipment(string name, string description, string image, int playerRoleId, int equipmentPartId, int playerLevelId, int equipmentGradeId);
		void DeleteEquipment(int id);
		List<PlayerRoleModel> GetAllPlayerRoles();
        EquipmentFullModel GetEquipmentById(int id);
		List<EquipmentGradeModel> GetEquipmentGrades();
		List<EquipmentPartModel> GetEquipmentParts();
		List<EquipmentFullModel> GetEquipments(int? roleId = null, int? equipmentPartId = null, int? equipmentGradeId = null, int ? pageIndex = 1, int? pageSize = 10);
		List<PlayerLevelModel> GetPlayerLevels();
		PlayerRoleModel GetPlayerRoleById(string id);
		List<EquipmentFullModel> SearchEquipmentsByRoleAndEquipmentPart(int? roleId, int? equipmentPartId);
		List<SkillFullModel> SearchSkillsByRole(int roleId);
		void UpdateEquipment(EquipmentModel equipment);
	}
}