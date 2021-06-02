using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Configuration;
using StaffLibrary.Data;
using StaffLibrary.Models.Base;
using StaffLibrary.Models;
using WebApp.Models;


namespace WebApp.Controllers
{
	[RoutePrefix("api/staff")]
    public class StaffController : ApiController
    {
		[HttpGet]
		[Route("")]
		public IHttpActionResult GetByType(string type)
		{
			StaffInDB obj = new StaffInDB();
			StaffType enumType = (StaffType)Enum.Parse(typeof(StaffType), type);

			List<Staff> staffList = obj.GetAllStaff(enumType);

			return Ok(staffList);
		}

		[HttpGet]
		[Route("{id:int}")]
		public IHttpActionResult GetStaffById(int id, string type) 
		{
			StaffInDB obj = new StaffInDB();
			StaffType enumType = (StaffType)Enum.Parse(typeof(StaffType), type);

			Staff findObj = obj.GetStaffById(id, enumType);

			if (findObj == null)
			{
				return NotFound();
			}
			else
			{
				return Ok(findObj);
			}
		}

		[HttpPost]
		[Route("add")]
		public IHttpActionResult AddStaff(string type, StaffObject reqObj)
		{
			StaffInDB obj = new StaffInDB();
			Staff addObj = null;
			StaffType enumType = (StaffType)Enum.Parse(typeof(StaffType), type);

			if (enumType == StaffType.Teacher)
			{
				addObj = new Teacher();
				((Teacher)addObj).Subject = reqObj.Subject;
			}
			else if (enumType == StaffType.Admin)
			{
				addObj = new Admin();
				((Admin)addObj).Department = reqObj.Department;
			}
			else if (enumType == StaffType.Support)
			{
				addObj = new Support();
				((Support)addObj).Age = reqObj.Age;
			}

			addObj.Name = reqObj.Name;
			addObj.Phone = reqObj.Phone;

			bool operationResult = obj.AddStaffDetails(addObj);

			if (operationResult)
			{
				return StatusCode(HttpStatusCode.Created);
			}
			else
			{
				return Ok("Not added");
			}			
		}

		[HttpPut]
		[Route("update/{id:int}")]
		public IHttpActionResult UpdateStaff(int id, string type, StaffObject reqObj)
		{
			StaffInDB obj = new StaffInDB();
			StaffType enumType = (StaffType)Enum.Parse(typeof(StaffType), type);

			Staff updateObj = obj.GetStaffById(id, enumType);

			if (updateObj == null)
			{
				return NotFound();
			}
			else
			{
				if (!string.IsNullOrWhiteSpace(reqObj.Name))
				{
					updateObj.Name = reqObj.Name;
                }
				if (!string.IsNullOrWhiteSpace(reqObj.Phone))
				{
					updateObj.Phone = reqObj.Phone;
                }
				if (updateObj.Type == StaffType.Teacher)
				{
					if (!string.IsNullOrWhiteSpace(reqObj.Subject))
					{
						((Teacher)updateObj).Subject = reqObj.Subject;
                    }
				}
				else if (updateObj.Type == StaffType.Admin)
				{
					if (!string.IsNullOrWhiteSpace(reqObj.Department))
                    {
						((Admin)updateObj).Department = reqObj.Department;
                    }
				}
				else if(updateObj.Type == StaffType.Support)
                {
					if (reqObj.Age != 0)
					{
						((Support)updateObj).Age = reqObj.Age;
					}
                }
				
				bool operationResult = obj.UpdateStaffDetails(updateObj);

				if(operationResult)
				{
					return Ok("Successfully updated");
				}
				else
				{
					return Ok("Not updated");
				}			
			}			
		}

		[HttpDelete]
		[Route("delete/{id:int}")]
		public IHttpActionResult DeleteStaff(int id, string type)
		{
			StaffInDB obj = new StaffInDB();
			StaffType enumType = (StaffType)Enum.Parse(typeof(StaffType), type);

			Staff deleteObj = obj.GetStaffById(id, enumType);

			if(deleteObj == null)
			{
				return NotFound();
			}
			else
			{
				obj.DeleteStaffDetails(deleteObj);
				return Ok();
			}
		}			
	}
}
