using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Homework.Models
{
    public class CheckEmailDuplicateAttribute : DataTypeAttribute
    {
        客戶資料Repository repo;
        客戶聯絡人Repository repoContact;
        public CheckEmailDuplicateAttribute() : base(DataType.Text)
        {
            repo = RepositoryHelper.Get客戶資料Repository();
            repoContact = RepositoryHelper.Get客戶聯絡人Repository(repo.UnitOfWork);
        }        
        //Override IsValid method to get client id inside validation Context
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                // Perform the duplicate email check
                if (repoContact.IsEmailDuplicated(value.ToString(), Convert.ToInt32(validationContext.ObjectType.GetProperty("客戶Id").GetValue(validationContext.ObjectInstance)) ))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}