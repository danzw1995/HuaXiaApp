using HuaXiaLibrary.Databases;
using HuaXiaLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaXiaLibrary.Data
{
	public class SqlData : IDatabaseData
	{
		private readonly ISqlDataAccess _db;
		private const string connectionStringName = "SqlDB";

		public SqlData(ISqlDataAccess db)
		{
			_db = db;
		}

		public List<PlayerRoleModel> GetAllPlayerRoles()
		{
			return _db.LoadData<PlayerRoleModel, dynamic>("dbo.spPlayerRoles_GetAll", new { }, connectionStringName, true);
		}

		public PlayerRoleModel GetPlayerRoleById(string id)
		{
			return _db.LoadData<PlayerRoleModel, dynamic>("dbo.spPlayerRoles_GetById", new { id }, connectionStringName, true).First();

		}

		public List<EquipmentPartModel> GetEquipmentParts()
		{
			return _db.LoadData<EquipmentPartModel, dynamic>("dbo.spEquipmentParts_GetAll", new { }, connectionStringName, true);

		}

		public List<EquipmentFullModel> GetEquipments(int? roleId = null, int? equipmentPartId = null, int? pageIndex = 1, int? pageSize = 10)
		{
			return _db.LoadData<EquipmentFullModel, dynamic>("dbo.spEquipments_Pagination", new { roleId, equipmentPartId, pageIndex, pageSize }, connectionStringName, true);
		}


		public List<EquipmentFullModel> SearchEquipmentsByRoleAndEquipmentPart(int? roleId, int? equipmentPartId)
		{
			return _db.LoadData<EquipmentFullModel, dynamic>("dbo.spEquipments_SearchByRoleAndEquipmentPart", new { roleId, equipmentPartId }, connectionStringName, true);
		}

		public EquipmentFullModel GetEquipmentById(int id)
		{
			return _db.LoadData<EquipmentFullModel, dynamic>("dbo.spEquipments_GetEquipmentById", new { id }, connectionStringName, true).FirstOrDefault();
		}

		public void CreateEquipment(string name, string description, string image, int playerRoleId, int equipmentPartId, int playerLevelId )
		{
			_db.SaveData("dbo.spEquipments_Create",
				new { name, description, image, playerRoleId, equipmentPartId, playerLevelId },
				connectionStringName,
				true);
		}
		public void UpdateEquipment(EquipmentModel equipment)
		{
			_db.SaveData("dbo.spEquipments_Update",
				new { id = equipment.Id, name = equipment.Name, description = equipment.Description, image = equipment.Image, playerRoleId = equipment.PlayerRoleId, playerLevelId = equipment.PlayerLevelId, equipmentPartId = equipment.EquipmentPartId },
				connectionStringName,
				true);
		}

		public void DeleteEquipment(int id)
		{
			_db.SaveData("dbo.spEquipments_Delete",
				new { id },
				connectionStringName,
				true);
		}

		public List<SkillFullModel> SearchSkillsByRole(int roleId)
		{
			return _db.LoadData<SkillFullModel, dynamic>("dbo.spSkills_SearchByRoleId", new { roleId }, connectionStringName, true);

		}

		public List<PlayerLevelModel> GetPlayerLevels()
		{
			return _db.LoadData<PlayerLevelModel, dynamic>("dbo.spPlayerLevels_GetAll", new { }, connectionStringName, true);
		}


	}
}
