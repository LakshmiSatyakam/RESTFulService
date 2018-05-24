using Newtonsoft.Json;
using RESTFulService.Entity;
using RESTFulService.Models.Members.Config;
using System;
using System.ComponentModel.DataAnnotations;

namespace RESTFulService.Models.Members.DTO
{
    /// <summary>
    /// Members entity for Members table
    /// </summary>
    public class Member : IEntityBase
    {
        #region Public properties
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "FirstName should not be empty")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "LastName should not be empty")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "DateOfBirth should not be empty")]
        //[JsonConverter(typeof(DateConverter), DateConverter.DateFormat)]
        public DateTime DateOfBirth { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email should not be empty")]
        public string Email { get; set; } 
        #endregion
    }
}
