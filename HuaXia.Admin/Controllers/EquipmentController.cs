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

				IWorkbook workbook = new XSSFWorkbook(file.OpenReadStream());

				ISheet sheet = workbook.GetSheetAt(0);

				List<EquipmentModel> equipments = new List<EquipmentModel>();

				IRow headerRow = sheet.GetRow(0);

				Dictionary<string, int> nameDict = new Dictionary<string, int>();
				
				for(int i =0; i < headerRow.Cells.Count; i ++)
				{
					string name = headerRow.GetCell(i).StringCellValue;

					nameDict.Add(name, i);
					
				}


				for (int i = 1; i <= sheet.LastRowNum; i ++)
				{
					IRow row = sheet.GetRow(i);

					equipments.Add(new EquipmentModel
					{
						Name = row.GetCell(nameDict[nameof(EquipmentModel.Name)]).StringCellValue,
						Description = row.GetCell(nameDict[nameof(EquipmentModel.Description)]).StringCellValue,
						Image = row.GetCell(nameDict[nameof(EquipmentModel.Image)]).StringCellValue,
						EquipmentPartId = (int)row.GetCell(nameDict[nameof(EquipmentModel.EquipmentPartId)]).NumericCellValue,
						PlayerLevelId = (int)row.GetCell(nameDict[nameof(EquipmentModel.PlayerLevelId)]).NumericCellValue,
						PlayerRoleId = (int)row.GetCell(nameDict[nameof(EquipmentModel.PlayerRoleId)]).NumericCellValue,
						EquipmentGradeId = (int)row.GetCell(nameDict[nameof(EquipmentModel.EquipmentGradeId)]).NumericCellValue,
					});

				}
				
				foreach(EquipmentModel equipment in equipments)
				{
					_db.CreateEquipment(equipment.Name, equipment.Description, equipment.Image, equipment.PlayerRoleId, equipment.EquipmentPartId, equipment.PlayerLevelId, equipment.EquipmentGradeId);
				}


				return Ok("success");

			} catch (Exception)
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
			headerRow.CreateCell(0).SetCellValue(nameof(EquipmentFullModel.Name));
			headerRow.CreateCell(1).SetCellValue(nameof(EquipmentFullModel.Description));
			headerRow.CreateCell(2).SetCellValue(nameof(EquipmentFullModel.Image));
			headerRow.CreateCell(3).SetCellValue(nameof(EquipmentFullModel.EquipmentPartId));
			headerRow.CreateCell(4).SetCellValue(nameof(EquipmentFullModel.PlayerLevelId));
			headerRow.CreateCell(5).SetCellValue(nameof(EquipmentFullModel.PlayerRoleId));
			headerRow.CreateCell(5).SetCellValue(nameof(EquipmentFullModel.EquipmentGradeId));

			for(int i = 0;i < equipments.Count; i ++)
			{
				IRow row = sheet.CreateRow(i + 1);
				row.CreateCell(0).SetCellValue(equipments[i].Name);
				row.CreateCell(1).SetCellValue(equipments[i].Description);
				row.CreateCell(2).SetCellValue(equipments[i].Image);
				row.CreateCell(3).SetCellValue(equipments[i].EquipmentPartId);
				row.CreateCell(4).SetCellValue(equipments[i].PlayerLevelId);
				row.CreateCell(5).SetCellValue(equipments[i].PlayerRoleId);
				row.CreateCell(5).SetCellValue(equipments[i].EquipmentGradeId);

			}
			using (var memoryStream = new  MemoryStream())
			{
				workbook.Write(memoryStream);
				return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "equipment.xlsx");
			}
		}


	}
}
