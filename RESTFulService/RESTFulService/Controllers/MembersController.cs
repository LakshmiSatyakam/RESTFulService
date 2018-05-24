using Microsoft.AspNetCore.Mvc;
using RESTFulService.Models.Members.DTO;
using RESTFulService.Models.Members.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RESTFulService.Controllers
{
    /// <summary>
    /// Members controller for providing operations like Get all members
    /// and Add a new Member
    /// </summary>
    [Route("api/[controller]")]
    public class MembersController : Controller
    {
        #region Private fields

        /// <summary>
        /// MemberOperations instance
        /// </summary>
        readonly IMemberOperations _membersOperation;
        #endregion

        #region Construction
        public MembersController(IMemberOperations membersOperation)
        {
            _membersOperation = membersOperation;
        }

        #endregion

        #region RESTFul services
        // GET api/values
        [HttpGet]
        public IActionResult GetMembers()
        {
            try
            {
                IEnumerable<Member> membersList = _membersOperation.GetMembers();

                if (membersList.Count() == 0)
                {
                    return Json("No members found");
                }

                return Json(membersList.OrderBy(x => x.DateOfBirth));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult AddMember([FromBody]Member info)
        {
            try
            {
                if (info == null)
                {
                    return BadRequest("Member details are null!");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (_membersOperation.AddMember(info))
                {
                    return Json("Member created successfully");
                }

                return BadRequest("Member creation failed!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}