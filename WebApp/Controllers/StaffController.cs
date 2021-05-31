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
		static string implementationType = ConfigurationManager.AppSettings["ImplementationType"];

		[HttpGet]
		[Route("")]
		public IHttpActionResult GetByType(string type)
		{
			var staffObj = (IStaff)Activator.CreateInstance(Type.GetType("StaffLibrary.Data." + implementationType + ", StaffLibrary"));
			List<Staff> staffList = null;
			Type staffType = Type.GetType("StaffLibrary.Models." + type + ", StaffLibrary");
			staffList = staffObj.GetAllStaff(staffType);

			return Ok(staffList);
		}

		[HttpGet]
		[Route("{id:int}")]
		public IHttpActionResult GetStaffById(int id, string type) 
		{
			var staffObj = (IStaff)Activator.CreateInstance(Type.GetType("StaffLibrary.Data." + implementationType + ", StaffLibrary"));
			Type staffType = Type.GetType("StaffLibrary.Models." + type + ", StaffLibrary");
			Staff findObj = staffObj.GetStaffById(id, staffType);

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
			var staffObj = (IStaff)Activator.CreateInstance(Type.GetType("StaffLibrary.Data." + implementationType + ", StaffLibrary"));
			Staff addObj = null;
			Type staffType = Type.GetType("StaffLibrary.Models." + type + ", StaffLibrary");

			int maxId = 0;

			if (staffObj is StaffInMemory)
				maxId = ((StaffInMemory)staffObj).GetMaxId();
			else if (staffObj is StaffInXml)
				maxId = ((StaffInXml)staffObj).GetMaxId();
			else if (staffObj is StaffInJson)
				maxId = ((StaffInJson)staffObj).GetMaxId();
			else if (staffObj is StaffInDB)
				maxId = 0;

			if (staffType == typeof(Teacher))
			{
				addObj = new Teacher();
				((Teacher)addObj).Subject = reqObj.Subject;
			}
			else if (staffType == typeof(Admin))
			{
				addObj = new Admin();
				((Admin)addObj).Department = reqObj.Department;
			}
			else if (staffType == typeof(Support))
			{
				addObj = new Support();
				((Support)addObj).Age = reqObj.Age;
			}

			addObj.Name = reqObj.Name;
			addObj.Phone = reqObj.Phone;
			addObj.Id = maxId + 1;

			bool operationResult = staffObj.AddStaffDetails(addObj);

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
			var staffObj = (IStaff)Activator.CreateInstance(Type.GetType("StaffLibrary.Data." + implementationType + ", StaffLibrary"));
			Type staffType = Type.GetType("StaffLibrary.Models." + type + ", StaffLibrary");
			Staff updateObj = staffObj.GetStaffById(id, staffType);

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
				if (updateObj is Teacher)
				{
					if (!string.IsNullOrWhiteSpace(reqObj.Subject))
					{
						((Teacher)updateObj).Subject = reqObj.Subject;
                    }
				}
				else if (updateObj is Admin)
				{
					if (!string.IsNullOrWhiteSpace(reqObj.Department))
                    {
						((Admin)updateObj).Department = reqObj.Department;
                    }
				}
				else if(updateObj is Support)
                {
					if (reqObj.Age != 0)
					{
						((Support)updateObj).Age = reqObj.Age;
					}
                }
				
				bool operationResult = staffObj.UpdateStaffDetails(updateObj);

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
			var staffObj = (IStaff)Activator.CreateInstance(Type.GetType("StaffLibrary.Data." + implementationType + ", StaffLibrary"));
			Type staffType = Type.GetType("StaffLibrary.Models." + type + ", StaffLibrary");
			Staff deleteObj = staffObj.GetStaffById(id, staffType);

			if(deleteObj == null)
			{
				return NotFound();
			}
			else
			{
				staffObj.DeleteStaffDetails(deleteObj);
				return Ok();
			}
		}			
	}
}
