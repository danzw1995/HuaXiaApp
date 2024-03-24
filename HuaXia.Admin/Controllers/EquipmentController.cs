using HuaXiaLibrary.Data;
using HuaXiaLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace HuaXia.Admin.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EquipmentController : ControllerBase
	{
		private readonly IDatabaseData _db;

		public EquipmentController(IDatabaseData db)
		{
			_db = db;
		}

		// GET: api/Equipment/GetList
		[HttpGet("GetList")]
		public ActionResult<IEnumerable<EquipmentFullModel>> GetEquipments([FromQuery] int? playerRoleId, [FromQuery] int? equipmentPartId, [FromQuery] int? equipmentGradeId, [FromQuery] int? page = 1, [FromQuery] int? limit = 10)
		{

			List<EquipmentFullModel> equipments = _db.GetEquipments(playerRoleId, equipmentPartId, equipmentGradeId, page, limit);

			return Ok(equipments);
		}

		// DELETE: api/Equipment/id
		[HttpDelete("{id}")]
		public ActionResult DeleteEquipment(int id)
		{

			_db.DeleteEquipment(id);

			return Ok("success");
		}

		// POST: api/Equipment/Import
		[HttpPost("Import")]
		public ActionResult ImportEquipment([FromForm] IFormFile file)
		{

			try
			{

				List<EquipmentPartModel> equipmentParts = _db.GetEquipmentParts();

				List<PlayerRoleModel> playerRoles = _db.GetAllPlayerRoles();

				List<PlayerLevelModel> playerLevels = _db.GetPlayerLevels();
				List<EquipmentGradeModel> equipmentGrades = _db.GetEquipmentGrades();

				Dictionary<string, int> equipmentPartDict = new Dictionary<string, int>();

				foreach(var part in  equipmentParts)
				{
					equipmentPartDict[part.Name] = part.Id;
				}

				Dictionary<string, int> playerRoleDict = new Dictionary<string, int>();

				foreach (var role in playerRoles)
				{
					playerRoleDict[role.Name] = role.Id;
				}

				Dictionary<string, int> playerLevelDict = new Dictionary<string, int>();

				foreach (var level in playerLevels)
				{
					playerLevelDict[level.Level.ToString()] = level.Id;
				}

				Dictionary<string, int> equipmentGradeDict = new Dictionary<string, int>();

				foreach (var grade in equipmentGrades)
				{
					equipmentGradeDict[grade.Grade.ToString()] = grade.Id;
				}


				IWorkbook workbook = new XSSFWorkbook(file.OpenReadStream());

				ISheet sheet = workbook.GetSheetAt(0);

				List<EquipmentModel> equipments = new List<EquipmentModel>();

				IRow headerRow = sheet.GetRow(0);

	

				for (int i = 1; i <= sheet.LastRowNum; i++)
				{
					IRow row = sheet.GetRow(i);

					equipments.Add(new EquipmentModel
					{
						Name = row.GetCell(0).ToString(),
						Description = row.GetCell(1).ToString(),
						Image = row.GetCell(2).ToString(),
						EquipmentPartId = equipmentPartDict[row.GetCell(3).ToString()],
						PlayerLevelId = playerLevelDict[row.GetCell(4).ToString()],
						PlayerRoleId = playerRoleDict[row.GetCell(5).ToString()],
						EquipmentGradeId = equipmentGradeDict[row.GetCell(6).ToString()],
					});

				}

				foreach (EquipmentModel equipment in equipments)
				{
					_db.CreateEquipment(equipment.Name, equipment.Description, equipment.Image, equipment.PlayerRoleId, equipment.EquipmentPartId, equipment.PlayerLevelId, equipment.EquipmentGradeId);
				}


				return Ok("success");

			}
			catch (Exception)
			{
				throw;
			}

		}
		// GET: api/Equipment/Export
		[HttpGet("Export")]
		public ActionResult exportEquipment()
		{

			List<EquipmentFullModel> equipments = _db.SearchEquipmentsByRoleAndEquipmentPart(null, null);

			IWorkbook workbook = new XSSFWorkbook();
			ISheet sheet = workbook.CreateSheet("Equipment");

			IRow headerRow = sheet.CreateRow(0);
			headerRow.CreateCell(0).SetCellValue("名称");
			headerRow.CreateCell(1).SetCellValue("描述");
			headerRow.CreateCell(2).SetCellValue("图片");
			headerRow.CreateCell(3).SetCellValue("部位");
			headerRow.CreateCell(4).SetCellValue("等级");
			headerRow.CreateCell(5).SetCellValue("职业");
			headerRow.CreateCell(6).SetCellValue("档次");

			for (int i = 0; i < equipments.Count; i++)
			{
				IRow row = sheet.CreateRow(i + 1);
				row.CreateCell(0).SetCellValue(equipments[i].Name);
				row.CreateCell(1).SetCellValue(equipments[i].Description);
				row.CreateCell(2).SetCellValue(equipments[i].Image);
				row.CreateCell(3).SetCellValue(equipments[i].EquipmentPartName);
				row.CreateCell(4).SetCellValue(equipments[i].Level);
				row.CreateCell(5).SetCellValue(equipments[i].PlayerRoleName);
				row.CreateCell(6).SetCellValue(equipments[i].Grade);

			}
			using (var memoryStream = new MemoryStream())
			{
				workbook.Write(memoryStream);
				return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "equipment.xlsx");
			}
		}


	}
}
